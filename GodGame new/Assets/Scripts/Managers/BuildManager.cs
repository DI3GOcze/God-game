using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    
    public UnityEngine.XR.Interaction.Toolkit.XRRayInteractor rayInteractor;
    public Transform desiredBuildingParent;
    public string noCollidingLayerName;

    private BuildingScriptableObject _selectedBuilding;
    private GameObject _buildingGhost;
    public GameObject popupSpawnPoint;

    // Singleton
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update() {
        
        if (_buildingGhost == null)
            return;

        // If raycaster is hitting something
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // If didnt hit terrain
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Terrain"))
            {
                _buildingGhost.SetActive(false);
                return;
            }
                
            // If hit terrain show ghost of building
            _buildingGhost.SetActive(true);
                 
            // Set position of ghost to the rayHit
            _buildingGhost.transform.position = hit.point;

            // Rotate ghost
            _buildingGhost.transform.rotation = Quaternion.Euler(new Vector3(_buildingGhost.transform.eulerAngles.x, rayInteractor.transform.eulerAngles.y, _buildingGhost.transform.eulerAngles.z));
        }
        else
        {
            _buildingGhost.SetActive(false);
        }
    }

    private void OnDisable() {
        if (_buildingGhost != null){
            Destroy(_buildingGhost.gameObject);
            _buildingGhost = null;
            _selectedBuilding = null;
        }
    }

    /// <summary>
    /// Instatiates selected building if player has enough resources
    /// </summary>
    public void ConstructBuilding()
    {
        // If on building selected
        if (_buildingGhost == null)
            return;

        if (!_buildingGhost.activeInHierarchy)
            return;

        var inventory = Warehouse.warehouseInvetory;

        var woodAmount = inventory.ItemAmount(ResourceTypes.WOOD);
        var stoneAmount = inventory.ItemAmount(ResourceTypes.STONE);

        // If not enough resources
        if(woodAmount < _selectedBuilding.woodCost || stoneAmount < _selectedBuilding.stoneCost) 
            return;
        
        // Take resources from inventory
        inventory.SeizeItem(ResourceTypes.WOOD, _selectedBuilding.woodCost);
        inventory.SeizeItem(ResourceTypes.STONE, _selectedBuilding.stoneCost);

        Instantiate(_selectedBuilding.buildingPrefab, _buildingGhost.transform.position, _buildingGhost.transform.rotation, desiredBuildingParent);
        
        Destroy(_buildingGhost.gameObject);
        _selectedBuilding = null;
    }

    public void SelectBuilding(BuildingScriptableObject building)
    {
        // If clicked on same building, toggle ghost
        if (building == _selectedBuilding)
        {
            Destroy(_buildingGhost);
            _selectedBuilding = null;
            PopUpManager.instance.CreatePopUp(popupSpawnPoint.transform.position, "Deselected");
            return;
        }

        _selectedBuilding = building;
        
        // Destroy currently selected building ghost
        Destroy(_buildingGhost);

        // Create ghost of new selected prefab
        _buildingGhost = Instantiate(_selectedBuilding.buildingGhost, Vector3.zero, _selectedBuilding.buildingGhost.transform.rotation);
        PopUpManager.instance.CreatePopUp(popupSpawnPoint.transform.position, "Selected");
    }
}

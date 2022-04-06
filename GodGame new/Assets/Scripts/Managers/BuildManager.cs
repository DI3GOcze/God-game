using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BuildManager : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.XRRayInteractor rayInteractor;
    public Transform desiredBuildingParent;
    public string noCollidingLayerName;

    private BuildingScriptableObject selectedBuilding;
    private GameObject buildingGhost;
    
    private void Update() {
        if (buildingGhost == null)
            return;

        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Terrain"))
            {
                buildingGhost.SetActive(false);
                return;
            }
                
            
            buildingGhost.SetActive(true);
                 
            // Set position of ghost to the rayHit
            buildingGhost.transform.position = hit.point;

            var direction = (hit.point - rayInteractor.transform.position).normalized;

            // Rotate ghost
            var eulerRotation = buildingGhost.transform.eulerAngles;
            buildingGhost.transform.rotation = Quaternion.Euler(new Vector3(buildingGhost.transform.eulerAngles.x, rayInteractor.transform.eulerAngles.y, buildingGhost.transform.eulerAngles.z));
        }
        else
        {
            buildingGhost.SetActive(false);
        }
    }

    private void OnDisable() {
        Destroy(buildingGhost);
    }

    public void ConstructBuilding()
    {
        if (buildingGhost == null)
            return;

        if (!buildingGhost.activeInHierarchy)
            return;

        Instantiate(selectedBuilding.buildingPrefab, buildingGhost.transform.position, buildingGhost.transform.rotation, desiredBuildingParent);
        return;
    }

    public void SelectBuilding(BuildingScriptableObject building)
    {
        selectedBuilding = building;
        
        // Destroy currently selected building ghost
        Destroy(buildingGhost);

        // Create ghost of new selected prefab
        buildingGhost = Instantiate(selectedBuilding.buildingPrefab, Vector3.zero, selectedBuilding.buildingPrefab.transform.rotation, desiredBuildingParent);
        
        // We need to assign noncolliding layer, because ray of interactor woud collide with the ghost
        buildingGhost.layer = LayerMask.NameToLayer(noCollidingLayerName);
    }
}

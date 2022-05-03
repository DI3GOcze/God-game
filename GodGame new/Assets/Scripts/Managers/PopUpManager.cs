using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;
    public GameObject popupPrefab;
    private void Awake() {
        instance = this;
    }

    private Transform _playerView;

    private void Start() {
         _playerView = Camera.main.transform;
    }

    /// <summary>
    /// Creates popup message in game world on postion with text
    /// </summary>
    /// <param name="position">Desired position</param>
    /// <param name="text">Desired text message</param>
    /// <returns>GameObject of created popup message</returns>
    public GameObject CreatePopUp(Vector3 position, string text)
    {
        Debug.Log(position);
        var popup = Instantiate(popupPrefab, position, Quaternion.LookRotation(position - _playerView.position));
        popup.GetComponentInChildren<PopUpHandler>().text = text;
        return popup;
    }

}

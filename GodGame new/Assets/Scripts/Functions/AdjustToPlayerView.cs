using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustToPlayerView : MonoBehaviour
{
    private Transform _playerView;

    void Start()
    {
        _playerView = Camera.main.transform;
    }

    void Update()
    {
        // Align to player height
        transform.position = new Vector3(transform.position.x, _playerView.position.y, transform.position.z);

        // 
        transform.rotation = Quaternion.LookRotation(transform.position - _playerView.position);
    }
}

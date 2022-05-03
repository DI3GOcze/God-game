using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    private void Awake() {
        World.Instance.campFires.Add(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpHandler : MonoBehaviour
{
    public float delay = 0.1f;
    public string text = "";
    void Start()
    {
        GetComponent<TextMeshPro>().text = text;
        Destroy (gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
    }
}

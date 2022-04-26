using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableNextFrame : MonoBehaviour
{
    public void DiableNextFrame(GameObject disabledObject)
    {
        StartCoroutine(Disable(disabledObject));
    } 

    IEnumerator Disable(GameObject disabledObject)
    {
        yield return null;
        disabledObject.SetActive(false);
    }
}

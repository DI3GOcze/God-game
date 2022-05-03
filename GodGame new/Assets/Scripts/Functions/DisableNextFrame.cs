using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableNextFrame : MonoBehaviour
{   
    /// <summary>
    /// Disables given object next frame
    /// </summary>
    /// <param name="disabledObject">Desired object</param>
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

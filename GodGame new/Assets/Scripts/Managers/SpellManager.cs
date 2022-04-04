using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public GameObject spawningPoint;
    public void SpawnSpell(MagicSpell magicSPell)
    {
        if (PlayerStatManager.instance.stamina >= magicSPell.staminaCost) {
            Instantiate(magicSPell.spellPrefab, spawningPoint.transform.position, magicSPell.spellPrefab.transform.rotation);
            PlayerStatManager.instance.DecreseStamina(magicSPell.staminaCost);
            return ;
        }
        else {
            return ;
        }
    }
}

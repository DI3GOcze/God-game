using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellManager : MonoBehaviour
{
    public GameObject spawningPoint;
    
    /// <summary>
    /// Spawns given magic speel for players mana 
    /// </summary>
    /// <param name="magicSPell">Desired magic spell</param>
    public void SpawnSpell(MagicSpell magicSPell)
    {
        if (PlayerStatManager.instance.mana >= magicSPell.manaCost) {
            Instantiate(magicSPell.spellPrefab, spawningPoint.transform.position, magicSPell.spellPrefab.transform.rotation);
            PlayerStatManager.instance.DecreseMana(magicSPell.manaCost);
            return ;
        }
        else {
            return ;
        }
    }
}

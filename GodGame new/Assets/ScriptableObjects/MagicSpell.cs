using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MagicSpell", order = 1)]
public class MagicSpell : ScriptableObject
{
    public GameObject spellPrefab;
    public float staminaCost;
}

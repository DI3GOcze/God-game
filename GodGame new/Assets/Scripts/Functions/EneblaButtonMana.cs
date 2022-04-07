using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EneblaButtonMana : MonoBehaviour
{
    public MagicSpell spell;
    private PlayerStatManager _playerStats;
    private Button _button;
    private void Start() {
        _playerStats = PlayerStatManager.instance;
        _button = GetComponent<Button>();
    }
    private void Update() {
        if (_playerStats.mana >= spell.manaCost)
            _button.interactable = true;
        else  
            _button.interactable = false;
    }
}

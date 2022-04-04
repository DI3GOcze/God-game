using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateManager
{
    void OnActivated();
    void OnTick() ;  
    void OnDeactivated();
    void SwitchState(StateBase nextState);
    void ResetManager();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSafety_StayInCover : StateBase
{
    GoToSafetyManager Manager;

    public GoToSafety_StayInCover(GoToSafetyManager Manager)
    {
        this.Manager = Manager;
    }

}

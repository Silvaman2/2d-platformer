using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponState
{
    public abstract void StartState(Weapon stateManager);

    public abstract void UpdateState(Weapon stateManager);

    public abstract void FixedUpdateState(Weapon stateManager);
}

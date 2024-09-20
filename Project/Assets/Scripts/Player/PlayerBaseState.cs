using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void StartState(PlayerController player);
    public abstract void UpdateState(PlayerController player);
    public abstract void FixedUpdateState(PlayerController player);
    public abstract void EndState(PlayerController player);
    public abstract void Actions(PlayerController player);

    public abstract void Animations(PlayerController player);
}

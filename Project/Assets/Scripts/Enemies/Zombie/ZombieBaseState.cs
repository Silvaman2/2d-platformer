using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieBaseState
{
    public abstract void StartState(Zombie enemy);
    public abstract void UpdateState(Zombie enemy);
    public abstract void FixedUpdateState(Zombie enemy);
    public abstract void EndState(Zombie enemy);
    public abstract void Animations(Zombie enemy);
}

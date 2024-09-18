using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletDeath : DisposableGameObject
{
    public void Destroy()
    {
        this.Hide();
    }

    protected override void SummonStart()
    {
    }
}

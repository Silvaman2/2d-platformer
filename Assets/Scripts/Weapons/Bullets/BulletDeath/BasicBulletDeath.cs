using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletDeath : MonoBehaviour
{
    public void Destroy()
    {
        Object.Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWorldPosition : MonoBehaviour
{
    private Vector2 position;
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
    }
}

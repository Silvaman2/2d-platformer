using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    [SerializeField] public int framesPerSecond;

    private void Awake()
    {
        Application.targetFrameRate = framesPerSecond;
    }
}

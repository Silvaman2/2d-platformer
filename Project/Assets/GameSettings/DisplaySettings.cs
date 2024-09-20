using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    [SerializeField] int targetFramesPerSecond;

    private void Awake()
    {
        Application.targetFrameRate = targetFramesPerSecond;
    }
}

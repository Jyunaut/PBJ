using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoiseEvents : MonoBehaviour
{
    public static NoiseEvents Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public event Action onTriggerDragEnter;
    public void DragTriggerEnter()
    {
        if (onTriggerDragEnter != null)
        {
            onTriggerDragEnter();
        }
    }

    public event Action onRunTriggerEnter;
    public void RunTriggerEnter()
    {
        if (onRunTriggerEnter != null)
        {
            onRunTriggerEnter();
        }
    }

    public event Action onNoiseExit;
    public void NoiseExit()
    {
        if (onNoiseExit != null)
        {
            onNoiseExit();
        }
    }
}

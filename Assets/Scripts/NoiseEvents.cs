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

    public event Action onDragTriggerEnter;
    public void DragTriggerEnter()
    {
        if (onDragTriggerEnter != null)
        {
            onDragTriggerEnter();
        }
    }

    public event Action onDragTriggerStay;
    public void DragTriggerStay()
    {
        if (onDragTriggerStay != null)
        {
            onDragTriggerStay();
        }
    }

    public event Action onDragTriggerExit;
    public void DragTriggerExit()
    {
        if (onDragTriggerExit != null)
        {
            onDragTriggerExit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For debugging
using UnityEngine.UI;

public class NoiseController : MonoBehaviour
{
    public float MIN = 0f, MAX = 100f; 
    public float noise = 0.0f;
    public float defaultIncrement = 0.5f;
    public float increment = 0.5f;

    private void Start()
    {
        NoiseEvents.Instance.onTriggerDragEnter += OnTriggerDragEnter;
        NoiseEvents.Instance.onRunTriggerEnter += OnTriggerRunEnter;
        NoiseEvents.Instance.onNoiseExit += OnNoiseExit;
        
        noise = 0.0f;
    }

    private void Update()
    {
        //debug
        noise += Time.deltaTime * increment;

        if(noise >= 50f && noise <= 51f)
        {
            NoiseManager.Instance.SetState(NoiseManager.MentalState.stress);
        }
        if(noise >= 70f && noise <= 71f)
        {
            NoiseManager.Instance.SetState(NoiseManager.MentalState.panic);
        }
    }

    private void OnTriggerDragEnter()
    {
        increment = 12f;
    }

    private void OnTriggerRunEnter()
    {
        increment = 8f;
    }

    private void OnNoiseExit()
    {
        increment = defaultIncrement;
    }
}

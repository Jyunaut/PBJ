using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For debugging
using UnityEngine.UI;

[RequireComponent(typeof(NoiseEvents))]
public class NoiseController : MonoBehaviour
{
    public float MIN = 0f, MAX = 100f; 
    [SerializeField]private float _noise = 0.0f;
    [SerializeField]private Slider debugSlider;

    private void Start()
    {
        NoiseEvents.Instance.onDragTriggerEnter += OnTriggerDragEnter;
        NoiseEvents.Instance.onDragTriggerStay += OnTriggerDragStay;
        NoiseEvents.Instance.onDragTriggerExit += OnTriggerDragExit;
        
        //debug
        debugSlider.minValue = MIN; 
        debugSlider.maxValue = MAX;
        _noise = 0.0f;
    }

    private void Update()
    {
        //debug
        _noise += Time.deltaTime * 0.15f;
        // Debug.Log(_noise);
        debugSlider.value = Mathf.Clamp(_noise, MIN, MAX);
    }

    private void OnTriggerDragEnter()
    {
        Debug.Log("BOOOOOM");
    }

    private void OnTriggerDragStay()
    {
        Debug.Log("CREEEEEEEEEEEEK");
    }

    private void OnTriggerDragExit()
    {
        Debug.Log("CRAAAAAAACKLE");
    }
}

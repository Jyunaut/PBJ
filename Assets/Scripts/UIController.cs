using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _resetBtn;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _endUI;

    private void Start()
    {
        _playBtn?.onClick.AddListener(OnClickPlay);
        _resumeBtn?.onClick.AddListener(OnClickResume);
        _resetBtn?.onClick.AddListener(OnClickReset);
        HideUI(_pauseUI);
        HideUI(_endUI);
        HideUI(_gameUI);
    }

    private void Update()
    {
        // debug
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickPause();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            ShowUI(_endUI);
        }
    }

    private void OnClickPlay()
    {
        Debug.Log("Clicked! Play");
        HideUI(_menuUI);
        ShowUI(_gameUI);
    }

    private void OnClickPause()
    {
        Debug.Log("Pressed! Pause");
        HideUI(_gameUI);
        ShowUI(_pauseUI);
    }

    private void OnClickResume()
    {
        Debug.Log("Clicked! Resume");
        HideUI(_pauseUI);
        ShowUI(_gameUI);
    }

    private void OnClickReset()
    {
        Debug.Log("Clicked! Reset");
        HideUI(_endUI);
        ShowUI(_menuUI);
    }

    private void HideUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    private void ShowUI(GameObject ui)
    {
        ui.SetActive(true);
    }
}

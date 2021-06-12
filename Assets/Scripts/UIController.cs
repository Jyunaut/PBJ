using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _resetBtn;
    
    private void Start()
    {
        _playBtn?.onClick.AddListener(OnClickPlay);
        _resumeBtn?.onClick.AddListener(OnClickResume);
        _resetBtn?.onClick.AddListener(OnClickReset);
    }

    private void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressed! Pause");
            UIManager.Instance.SetState(UIManager.GameState.Pause);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Pressed! Force End");
            UIManager.Instance.SetState(UIManager.GameState.End);
        }
    }

    private void OnClickPlay()
    {
        Debug.Log("Clicked! Play");
        UIManager.Instance.SetState(UIManager.GameState.play);
    }

    private void OnClickResume()
    {
        Debug.Log("Clicked! Resume");
        UIManager.Instance.SetState(UIManager.GameState.Resume);
    }

    private void OnClickReset()
    {
        Debug.Log("Clicked! Reset");
        UIManager.Instance.SetState(UIManager.GameState.Reset);
    }
}

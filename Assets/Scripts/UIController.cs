using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _resetBtn;
    [SerializeField] private Button _quitBtn;

    private void Start()
    {
        _playBtn?.onClick.AddListener(OnClickPlay);
        _resumeBtn?.onClick.AddListener(OnClickResume);
        _resetBtn?.onClick.AddListener(OnClickReset);
        _quitBtn?.onClick.AddListener(OnClickReset);
    }

    private void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressed! Pause");
            UIManager.Instance.SetState(UIManager.States.Pause);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Pressed! Force End");
            UIManager.Instance.SetState(UIManager.States.End);
        }
    }

    private void OnClickPlay()
    {
        Debug.Log("Clicked! Play");
        UIManager.Instance.SetState(UIManager.States.play);
    }

    private void OnClickResume()
    {
        Debug.Log("Clicked! Resume");
        UIManager.Instance.SetState(UIManager.States.Resume);
    }

    private void OnClickReset()
    {
        Debug.Log("Clicked! Reset");
        UIManager.Instance.SetState(UIManager.States.Reset);
    }

    private void OnClickQuit()
    {
        Debug.Log("Clicked! Quit");
        UIManager.Instance.SetState(UIManager.States.Quit);
    }
}

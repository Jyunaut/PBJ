using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIController))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public enum States
    {
        play,
        End,
        Caught,
        Menu,
        Pause,
        Resume,
        Reset,
        Quit
    }

    public States state = States.Menu;
    private UIController controller;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _endUI;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        controller = GetComponent<UIController>();
        UpdateScene();
    }

    public void SetState(States newState)
    {
        state = newState;
        UpdateScene();
    }

    private void UpdateScene()
    {
        switch (state)
        {
            case States.play:
                HideUI(_menuUI, _pauseUI, _endUI);
                ShowUI(_gameUI);
                break;
            case States.End:
                HideUI(_gameUI, _menuUI, _pauseUI);
                ShowUI(_endUI);
                break;
            case States.Caught:
                HideUI(_gameUI, _menuUI, _pauseUI);
                ShowUI(_endUI);
                break;
            case States.Menu:
                HideUI(_gameUI, _pauseUI, _endUI);
                ShowUI(_menuUI);
                break;
            case States.Pause:
                HideUI(_gameUI, _menuUI, _endUI);
                ShowUI(_pauseUI);
                Time.timeScale = 0f;
                break;
            case States.Resume:
                HideUI(_menuUI, _pauseUI, _endUI);
                ShowUI(_gameUI);
                Time.timeScale = 1f;
                break;
            case States.Reset:
                LoadScene(0);
                SetState(States.Menu);
                break;
            case States.Quit:
                Application.Quit();
                break;
            default:
                Debug.Log("Invalid State");
                break;
        }
    }

    private IEnumerator LoadScene(int scene)
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(scene);

        while(!asyncload.isDone)
        {
            yield return null;
        }
    }

    private void HideUI(params GameObject[] ui)
    {
        foreach(GameObject n in ui)
            n.SetActive(false);
    }

    private void ShowUI(params GameObject[] ui)
    {
        foreach(GameObject n in ui)
            n.SetActive(true);
    }

}

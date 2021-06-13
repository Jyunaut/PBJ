using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIController))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public enum GameState
    {
        play,
        End,
        Caught,
        Menu,
        Pause,
        Resume,
        Reset
    }

    public GameState state = GameState.Menu;
    private UIController _controller;
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
        _controller = GetComponent<UIController>();
        UpdateScene();
    }

    public void SetState(GameState newState)
    {
        state = newState;
        UpdateScene();
    }

    private void UpdateScene()
    {
        switch (state)
        {
            case GameState.play:
                HideUI(_menuUI, _pauseUI, _endUI);
                ShowUI(_gameUI);
                Time.timeScale = 1f;
                break;
            case GameState.End:
                HideUI(_gameUI, _menuUI, _pauseUI);
                ShowUI(_endUI);
                Time.timeScale = 0f;
                break;
            case GameState.Caught:
                HideUI(_gameUI, _menuUI, _pauseUI);
                ShowUI(_endUI);
                Time.timeScale = 0f;
                break;
            case GameState.Menu:
                HideUI(_gameUI, _pauseUI, _endUI);
                ShowUI(_menuUI);
                Time.timeScale = 0f;
                break;
            case GameState.Pause:
                HideUI(_gameUI, _menuUI, _endUI);
                ShowUI(_pauseUI);
                Time.timeScale = 0f;
                break;
            case GameState.Resume:
                HideUI(_menuUI, _pauseUI, _endUI);
                ShowUI(_gameUI);
                Time.timeScale = 1f;
                break;
            case GameState.Reset:
                LoadScene(0);
                SetState(GameState.Menu);
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

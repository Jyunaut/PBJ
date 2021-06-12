using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    public enum SceneState
    {
        Game,
        Caught,
        End,
        Menu,
        Pause
    }

    public SceneState state = SceneState.Menu;

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
    }

    public void SetState()
    {
        // TODO: Set the state then update scene
    }

    private void UpdateScene()
    {
        // TODO: Handle update scene logic from state
    }

}

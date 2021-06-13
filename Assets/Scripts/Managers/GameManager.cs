using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Transform goalPos;
    public int goalCount = 0;
    public Text endPrompt;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(goalPos.position, -Vector2.up);

        if (hit.collider != null)
        {
            Delivered(hit.collider.gameObject);
        }
    }

    public void Spooked()
    {
        UIManager.Instance.SetState(UIManager.GameState.Caught);
        endPrompt.text = "You are too scared in the night";
        endPrompt.gameObject.SetActive(true);
    }

    private void Delivered(GameObject goal)
    {
        goalCount++;
        goal.transform.position += new Vector3(-5f, 0f, 0f);
        if (goalCount == 5)
        {
            UIManager.Instance.SetState(UIManager.GameState.End);
            endPrompt.text = "You are very brave to make PBJ at midnight!";
            endPrompt.gameObject.SetActive(true);
        }
    }
}

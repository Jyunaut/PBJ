using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NoiseEvents))]
[RequireComponent(typeof(NoiseController))]
public class NoiseManager : MonoBehaviour
{
    public static NoiseManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public enum MentalState
    {
        brave,
        stress,
        panic
    }

    // Defining this struct to check if player is both busy in a mental and storing state value
    public struct MentalBurst 
    {
        public MentalState state { get; set; }
        public bool isConvulsing { get; set; }
    }


    public NoiseController controller;
    public MentalBurst mental;
    public Text hidePrompt;
    public GameObject player;

    private void Start()
    {
        controller = GetComponent<NoiseController>();
    }

    public void SetState(MentalState newState)
    {
        mental.state = newState;
        UpdateState();
    }

    private void UpdateState()
    {
        switch (mental.state)
        {
            case MentalState.brave:
            if(!mental.isConvulsing)
            {

            }
                break;
            case MentalState.stress:
            if(!mental.isConvulsing)
            {
                StartCoroutine(Stressing());
            }
                break;
            case MentalState.panic:
            if(!mental.isConvulsing)
            {
                StartCoroutine(Panicking());
            }
                break;
            default:
                Debug.Log("Invalid State");
                break;
        }
    }

    IEnumerator Stressing()
    {
        yield return new WaitForSeconds(3);
        mental.isConvulsing = false;
        Debug.Log("Character is stressing");
    }

    IEnumerator Panicking()
    {
        hidePrompt.gameObject?.SetActive(true);
        yield return new WaitForSeconds(5);
        hidePrompt.gameObject?.SetActive(false);
        mental.isConvulsing = false;

        // Put character hide 
        if(!player.activeInHierarchy)
        {
            controller.noise = 0f;
        }
        else
        {
            GameManager.Instance.Spooked();
        }
        Debug.Log("Character is Panicking (supposed to be dead if not hiding)");
    }
}

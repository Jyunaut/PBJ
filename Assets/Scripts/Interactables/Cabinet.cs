using UnityEngine;

public class Cabinet : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Hide in cabinet");
    }
}
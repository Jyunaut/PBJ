using UnityEngine;

public class Stool : MonoBehaviour, IInteractable
{
    public void Interact(GameObject gameObject)
    {
        // Play Animation
        print("Play cupboard animation");
    }
}
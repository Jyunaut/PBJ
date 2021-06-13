using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _eyes;
    [SerializeField] private Transform _eyesLocation;
    
    private GameObject _player;
    private GameObject _obj;

    public void Interact(GameObject gameObject)
    {
        _player = gameObject;
        _player.SetActive(false);
        _obj = Instantiate(_eyes, _eyesLocation);
    }

    void Update()
    {
        if (_player != null)
            if (!_player.activeInHierarchy && Input.GetButtonDown(PlayerInput.Interact))
            {
                _player.SetActive(true);
                Destroy(_obj);
            }
    }
}

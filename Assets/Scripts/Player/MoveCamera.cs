using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _border1;
    [SerializeField] private Transform _border2;
    [SerializeField] private Transform _cameraPosition1;
    [SerializeField] private Transform _cameraPosition2;
    [SerializeField] private Transform _cameraPosition3;

    void Update()
    {
        if (transform.position.x < _border1.position.x)
            _camera.transform.position = _cameraPosition1.position;
        else if (transform.position.x >= _border1.position.x && transform.position.x <= _border2.position.x)
            _camera.transform.position = _cameraPosition2.position;
        else if (transform.position.x > _border2.position.x)
            _camera.transform.position = _cameraPosition3.position;
    }
}

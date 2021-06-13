using UnityEngine;

public class StoolDestination : MonoBehaviour
{
    [SerializeField] private GameObject _spawnedObject;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Tag.Draggable))
        {
            Instantiate(_spawnedObject, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }
    }
}
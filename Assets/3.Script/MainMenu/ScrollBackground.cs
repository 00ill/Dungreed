using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }
}

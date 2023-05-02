using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class BackgroundLoop : MonoBehaviour
{
    private float width;
    private void Awake()
    {
        width = transform.GetComponent<BoxCollider2D>().size.x;
        transform.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void LateUpdate()
    {
        if (transform.position.x <= -width * transform.localScale.y)
            Reposition();
    }
    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 2 * transform.localScale.y, 0);
        transform.position = (Vector2)transform.position + offset;
    }

}

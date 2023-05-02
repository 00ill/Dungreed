using UnityEngine;

public class InGameCursor : MonoBehaviour
{

    [HideInInspector] public Vector3 position;
    private void Awake()
    {
        Cursor.visible = false;
    }
    private void LateUpdate()
    {
        position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x
     , Input.mousePosition.y, -Camera.main.transform.position.z));
        transform.position = position;
    }
}

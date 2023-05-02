using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGround : MonoBehaviour
{
    [SerializeField] private GameObject eat;
    private int count = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(count == 0) { Instantiate(eat, new Vector3(collision.gameObject.transform.position.x, -2.8f, 0), Quaternion.identity);  count++; }
            
        }
    }

    public void nextStage()
    {
        GameObject.FindGameObjectsWithTag("Player")[0].transform.position = new Vector3(96.9f, 97.63f, 0);
        GameManager.Instance.stageNum = 1;
    }

    public void dest()
    {
        Destroy(gameObject);
    }
}

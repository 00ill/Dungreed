using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //���⵵ ���� ����ؼ� ����ؾ��ϰ� ����Ʈ�� ������ ���ѰŰ�
    // p.p-t.p normalize�ؼ� dist��ŭ �Ÿ� ���� ���� �� ��ġ ����� ��ġ�� ���ѰŰ�
    //���⼭ ������ �ҷ��ñ� �������
    [SerializeField] private GameObject player;
    [SerializeField] private ObjectPool swingPool;
    [SerializeField] private InGameCursor _cursor;

    private Quaternion quaternion;
    private Vector3 directionVector;
    private readonly float dist = 1;
    private GameObject temp;

    public void MakeSwing()
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan(Mathf.Abs(_cursor.position.y - player.transform.position.y)
            / Mathf.Abs(_cursor.position.x - player.transform.position.x));

        if (_cursor.position.x >= player.transform.position.x)
        {
            if (_cursor.position.y >= player.transform.position.y)
                quaternion = Quaternion.Euler(new Vector3(0, 0, 270 + angle));
            else
                quaternion = Quaternion.Euler(new Vector3(0, 0, 270 - angle));
        }
        else
        {
            if (_cursor.position.y >= player.transform.position.y)
                quaternion = Quaternion.Euler(new Vector3(0, 0, 90 - angle));
            else
                quaternion = Quaternion.Euler(new Vector3(0, 0, 90 + angle));
        }

        directionVector = (_cursor.position - player.transform.position).normalized;

        temp = swingPool.GetObject();
        temp.transform.position = player.transform.position + directionVector * dist;
        temp.transform.rotation = quaternion;
    }
}
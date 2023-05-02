using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //카메라에 배경을 고정할 거에요
    //맨 뒷 구름은 카메라랑 같이 움직임
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject townBackground;
    [SerializeField] private GameObject dashGage;
    private Vector3 dashGagePosition;
    private StageData stageData;

    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private void Awake()
    {
        dashGagePosition = dashGage.transform.position;
        TryGetComponent(out stageData);
    }

    
    private void LateUpdate()
    {
        transform.position = player.transform.position + Vector3.back * 1000;
        townBackground.transform.position = player.transform.position + Vector3.forward;
        if (player.transform.position.x < stageData.stageLeft[GameManager.Instance.stageNum].x)
        {
            transform.position = new Vector3(stageData.stageLeft[GameManager.Instance.stageNum].x, transform.position.y, transform.position.z);
        }
        if (player.transform.position.y < stageData.stageLeft[GameManager.Instance.stageNum].y)
        {
            transform.position = new Vector3(transform.position.x, stageData.stageLeft[GameManager.Instance.stageNum].y, transform.position.z);
        }
        if (player.transform.position.y > stageData.stageRight[GameManager.Instance.stageNum].y)
        {
            transform.position = new Vector3(transform.position.x, stageData.stageRight[GameManager.Instance.stageNum].y, transform.position.z);
        }
        if (player.transform.position.x > stageData.stageRight[GameManager.Instance.stageNum].x)
        {
            transform.position = new Vector3(stageData.stageRight[GameManager.Instance.stageNum].x, transform.position.y, transform.position.z);
        }
        dashGage.transform.position = dashGagePosition + transform.position + Vector3.forward;
        townBackground.transform.position = transform.position + Vector3.forward;
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-0.5f, 0.5f) * shakePower;
            float yAmount = Random.Range(-0.5f, 0.5f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);
            townBackground.transform.position += new Vector3(xAmount, yAmount, 0f);
            dashGage.transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;
        shakeFadeTime = power / length;
    }
}

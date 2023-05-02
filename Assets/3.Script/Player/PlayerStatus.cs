using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//#pragma warning disable 0168
#pragma warning disable IDE0044 // 읽기 전용 한정자 추가
#pragma warning disable IDE0051
#pragma warning disable IDE0414

public class PlayerStatus : MonoBehaviour
{
    public float HP; //체력
    public float MaxHP = 200; //최대 체력
    public int gold = 0; //소지금
    public int block = 0; //피해무시 확률
    public int criticalChance = 2; //크리티컬 확률
    public int criticalDamage = 100; // 추가데미지(%) 100이면 두 배
    public int dashDamage = 50; //무기 공격력의 몇%가 대쉬 데미지인가
    public int trueDamage = 0; // 몬스터의 방어력을 무시하고 들어가는 데미지
    public int skillCooldown = 0; // 스킬을 더 자주 사용할 수 있게 해줍니다.
    public float movementSpeed = 6.2f; // 기본 6.2
    public float defense = 7.5f; // 7.5면 8%데미지 감소
    public float toughness = 0; // 받는 데미지 고정으로 감소
    public float attackSpeed; // 1초에 몇 번 공격할 수 있는가 // 무기별 값들임
    public float reloadSpeed; //리로드에 걸리는 시간 // 무기별 값들임

    [SerializeField] private Slider HPSlider;
    [SerializeField] private Text HpText;

    private SpriteRenderer spriteRenderer;
    private bool isTakingDamage;
    private void Awake()
    {
        MaxHP = 200;
        HP = MaxHP;
        TryGetComponent(out spriteRenderer);
    }

    private void Update()
    {
        HPSlider.value = HP / MaxHP;
        HpText.text = HP + " / " + MaxHP;

        if (HP <= 0)
        {
            SceneManager.LoadScene("InGame");
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isTakingDamage)
        {
            HP -= damage;
            transform.GetComponent<PlayerSound>().PlayHit();
            StartCoroutine(TakeDamage_co());
        }
    }

    private IEnumerator TakeDamage_co()
    {
        isTakingDamage = true;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 255f);
        isTakingDamage = false;
    }

}
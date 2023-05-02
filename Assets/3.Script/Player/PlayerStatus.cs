using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//#pragma warning disable 0168
#pragma warning disable IDE0044 // �б� ���� ������ �߰�
#pragma warning disable IDE0051
#pragma warning disable IDE0414

public class PlayerStatus : MonoBehaviour
{
    public float HP; //ü��
    public float MaxHP = 200; //�ִ� ü��
    public int gold = 0; //������
    public int block = 0; //���ع��� Ȯ��
    public int criticalChance = 2; //ũ��Ƽ�� Ȯ��
    public int criticalDamage = 100; // �߰�������(%) 100�̸� �� ��
    public int dashDamage = 50; //���� ���ݷ��� ��%�� �뽬 �������ΰ�
    public int trueDamage = 0; // ������ ������ �����ϰ� ���� ������
    public int skillCooldown = 0; // ��ų�� �� ���� ����� �� �ְ� ���ݴϴ�.
    public float movementSpeed = 6.2f; // �⺻ 6.2
    public float defense = 7.5f; // 7.5�� 8%������ ����
    public float toughness = 0; // �޴� ������ �������� ����
    public float attackSpeed; // 1�ʿ� �� �� ������ �� �ִ°� // ���⺰ ������
    public float reloadSpeed; //���ε忡 �ɸ��� �ð� // ���⺰ ������

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
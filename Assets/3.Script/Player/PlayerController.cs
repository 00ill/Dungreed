using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    ///타일 하나하나 안나눠도 된다. 블럭 단위로 해놓고 스크립트 하나 계속 써도 될듯

    //입력받을 키
    //안한 거
    //c 스테이터스, v 장비창, tab 빠른이동 - 인게임에서는 미니 맵, esc 일시정지 창, f 겹친 대상과 상호작용, 마우스 휠 무기변경
    //한 거 
    //이동 asd, w=space,우클릭 대쉬, 좌클릭 공격

    //던전이랑 마을이랑 다른 거는 isTown bool 변수 하나 만들까

    //땅에 닿았을  처리를 어떻게 할거냐
    //땅에 태그를 달아서 충돌하면 점프 횟수 초기화

    //통과해서 위로 올라가야 하는 땅은 어떡하나
    //플레이어랑 위치 검사 계속해서 플레이어가 아래있으면 트리거를 키고 아닐 때 트리거를 꺼
    //대쉬중에도 통과해야하니까 대쉬중이면 켜버려

    //해야할 거
    //대쉬 카운트 업데이트 함수
    //플레이어 스테이터스
    //체력, 허기, 골드

    //공격하는 거 코루틴으로 만들어서 공격 속도 조절해야해

    //공격할 때 카메라 진동있다.

    //맵 랜덤 생성이 가능한가

    //오늘 할 일
    //맵 찍기, 캐릭터 바꾸기, 캐릭터 이동 구현 정하기, 이펙트넣기, 사운드 넣기. 배경 넣기, 배경이동

    //대쉬중인지 아닌지 체크해서 대쉬 중일 떄는 최대 속도 제한을 풀어+
    //점프는 코루틴으로 하면 안될 삘
    //지면에 붙어있을 때는 y속도를 항상 0으로하면
    //enter true exit false

    //플레이어 싱글톤으로 만들어서 관리하자

    //하향점프관련
    //플레이어가 충돌한 게 그라운드면 플레이어가 아래점프를 했을 때 플레이어의 콜라이더가 꺼진다.
    //시간기준으로 끌 수는 없는게 플레이어가 공중에서 행동을 할 수 있어서.
    //부딪힌 콜라이더만 끄는 방법은 안되는게 같은 플랫폼에 있는 몬스터도 떨어질 테니까
    //콜라이더 엑시트할 때 플레이어의 충돌을 활성화시키면 될 듯
    //정리하면
    //플레이어의 콜라이더를 조절해야하는 게 맞고
    //맨 밑바닥이 아닌 한 하향점프를 하면 플레이어의 콜라이더가 꺼지고
    //충돌한 시점의 그라운드 플랫폼을 저장해두고 그 콜라이더와 접촉이 해제될 때 콜라이더를 다시 키는 느낌으로다가//
    //업데이트에는 하향점프 키누르면 없어지게하는데 이건 isground 필요할듯

    //플레이어 다음 씬으로 이동할 때 그대로 넘겨주기

    ///해야할 거 다 적어용
    ///플레이어 먼지 이펙트만 구현
    ///던전구현
    ///던전안 몬스터구현
    ///마을 구현
    ///마을안 npc와의 상호작용
    ///몬스터와 플레이어 전투 계산식 적용
    ///인벤토리랑 스테이터스 창 ui만들기
    ///아이템 추가 변경가능
    ///그러고 보스방만들면 끝?

    private SpriteRenderer Player_sr;
    private Animator Player_anim;
    private PlayerSound Player_sound;

    [SerializeField] InGameCursor inGameCursor;
    [SerializeField] CameraMovement cameraMovement;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private GameObject dashStack1;
    [SerializeField] private GameObject dashStack2;

    private readonly float dashCooldown = 1.5f;
    public int dashCount = 2;
    private bool isCharging = false;

    private bool isBeforeAttack = true;
    public bool IsBeforeAttack => isBeforeAttack;
    private bool isAttackCooldown;
    [SerializeField] EffectManager effectManager;

    [SerializeField] private GameObject inventory;
    private bool isInventoryOpen;

    private SpriteRenderer cursor_sr;
    private void Awake()
    {
        TryGetComponent(out Player_sr);
        TryGetComponent(out Player_anim);
        TryGetComponent(out Player_sound);
        inventory.SetActive(isInventoryOpen);
        cursor_sr = GameObject.FindGameObjectWithTag("Cursor").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (dashCount < 2) { if (!isCharging) { StartCoroutine(ChargeDash()); } }
        if (dashCount == 2) { dashStack1.SetActive(true); dashStack2.SetActive(true); }
        else if (dashCount == 1) { dashStack1.SetActive(true); dashStack2.SetActive(false); }
        else if (dashCount == 0) { dashStack1.SetActive(false); dashStack2.SetActive(false); }
        if (inGameCursor.position.x < transform.position.x) { Player_sr.flipX = true; }
        else { Player_sr.flipX = false; }

        if (Input.GetMouseButtonDown(0) && !isInventoryOpen)
        {
            if (!isAttackCooldown)
            {
                if (GameManager.Instance.equipWeapon == 0)
                {

                    Player_sound.PlaySwing();
                    cameraMovement.StartShake(.1f, .3f);
                    isBeforeAttack = !isBeforeAttack;
                    effectManager.MakeSwing();
                    StartCoroutine(AttackDelay_co());
                }
                else if (GameManager.Instance.equipWeapon == 1)
                {
                    Player_sound.PlaySwing();
                    isBeforeAttack = false;
                    StartCoroutine(AttackDelay_co());
                    StartCoroutine(GameObject.FindGameObjectWithTag("Boomerang").GetComponent<Boomerang>().Attack_co());
                    StartCoroutine(AttackDelay_co2());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            inventory.SetActive(isInventoryOpen);
            Cursor.visible = isInventoryOpen;
            cursor_sr.enabled = !isInventoryOpen;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            int temp = GameManager.Instance.equipWeapon;
            GameManager.Instance.equipWeapon = GameManager.Instance.equipWeapon2;
            GameManager.Instance.equipWeapon2 = temp;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player_anim.SetBool("Jump", false);
        }
    }

    private IEnumerator ChargeDash()
    {
        isCharging = true;
        yield return new WaitForSeconds(dashCooldown);
        dashCount++;
        isCharging = false;
    }

    private IEnumerator AttackDelay_co()
    {
        isAttackCooldown = true;
        yield return new WaitForSeconds(0.2f);
        isAttackCooldown = false;
    }

    private IEnumerator AttackDelay_co2()
    {
        isAttackCooldown = true;
        yield return new WaitForSeconds(2.1f);
        isAttackCooldown = false;
    }
}
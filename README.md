# 리드미 연습


# 2팀 코드컨벤션

## 개요
- 본 프로젝트 진행 간 개발팀의 코드 작성 및 유지보수에 있어 통일성 유지로 가독성 향상, 작업환경 및 소요시간의 효율성을 높이기 위함

## 규칙

### 중요 규칙
- **어? 금지**
- **아...! 금지**
- **세이브 생활화**

### Naming Convention
- 애매한 변수명은 [변수명 짓기 사이트](https://www.curioustore.com/#!/) 참고 가능
- 단어 사이에 띄어쓰기 목적의 언더바 ( _ )는 사용하지 않는다

#### 클래스 및 파일 명
- 파스칼 케이스 적용
- 프리팹, 스크립트 외 같은 포맷의 파일이 여러개일 경우 용도별로 정렬하기 위해 `항목(type)_index_파일명(파스칼케이스)` 적용

```cs
// ex>
public class PlayerMovement
{
}
```

```
ex >

BGM_00_Normal.mp3
BGM_01_EventGagueMax.mp3

SE_00_NormalClick.mp3
SE_01_ButtonClick.mp3

CharacterMovement.cs
```

#### 변수
- `private` 속성은 언더바( _ ) 후 카멜케이스 적용
- `public` 속성은 파스칼케이스 적용
  - 기본적으로 `public` 변수는 사용하지 않는다. 사용시 `프로퍼티`로 선언
  - 유니티 인스펙터에 노출시켜야 할 경우 `private` 과 `[SerializeField]` 사용
- bool 타입은 상태를 나타내기 위해 `is`를 붙인다.
  - 상태를 저장/표기하기 위함이 아닌 경우 붙이지 않아도 무방.
- 변수명이 길어 약칭을 사용할 경우 주석으로 표기
- 타입을 즉각 알 수 없는 `var`타입은 사용 지양

```cs
// ex>
public class Player
{
    private bool _isRun;
    public bool IsRun { get; private set; }
    
    private int eventStep;
    public int EventStep { get; private set; }
    
    [SerializeField] private float _maxHp;
    
    // 플레이어 공격력
    private float _atk;
}
```

#### 함수
- 동사형으로 작성
- 함수명 : 파스칼케이스 적용
- 함수 매개변수 : 카멜케이스 적용
- 함수 명 기준의 기능 단위로 함수/모듈화
- 함수 내에서 한 기능의 로직이 끝나면 개행으로 분리
- 함수의 외부 노출을 위해 `public`사용 시 `<summary>`주석으로 설명 추가
- 함수 명으로 쓸데없는 부가설명 금지

```cs
// ex>
public class Player
{
    private void UseItem(GameObject targetCharacter)
    {
    }

    private void CreateInstanceOnPlane(int index, GameObject prefab)
    {
    }

    /// <summary>
    /// 아이템 획득 메서드
    /// </summary>
    public void GetItem()
    {
    }

    private void PlayerHpDown(float damage)
    {
        // 쓸데없는 부가 설명의 좋은 예
        // Player 클래스 내에서 Player에게 영향이 가해지는 기능의 함수는
        // 'Player'가 붙지 않아도 된다.
    }

    private void HpDown(float damage)
    {
        // 옳은 함수명
    }
}
```

#### 인터페이스
- 인터페이스 이름 앞에 알파벳 대문자 'I' 추가

```cs
public Interface IAction
{
}
```

#### ENUM
- 이름 앞에 알파벳 대문자 'E' 추가
- 항상 대문자만 사용

```cs
public enum ECharacterState
{
    NORMAL,
    HAPPY,
    ANGRY
}
```
### Coroutine
- 이름 마지막에 '_co' 추가

```cs
private IEnumerator Move_co()
{
    yield return null;
}
```


### 추가적인 스크립트 내 주석 표기(권장사항, 강요 없음)
- `// TODO : 내용`
  - 추후 진행해야 할 사항
- `// FIXME : 내용`
  - 수정 및 보완을 다른 사람에게 부탁할 때


## 6. 참여자
<table>
    <tr align="center">
        <td><B>강윤석<B></td>
        <td><B>김현희<B></td>
        <td><B>이성준<B></td>
        <td><B>주성현<B></td>
        <td><B>최영일<B></td>
    </tr>
    <tr align="center">
        <td>
            <img src="https://github.com/Cowhydra.png?size=100" width="100">
            <br>
            <a href="https://github.com/Cowhydra"><I>Cowhydra</I></a>
        </td>
        <td>
            <img src="https://github.com/Kim-hyun-hee.png?size=100" width="100">
            <br>
            <a href="https://github.com/Kim-hyun-hee"><I>Kim-hyun-hee</I></a>
        </td>
        <td>
            <img src="https://github.com/MARZ2AMBITION.png?size=100" width="100">
            <br>
            <a href="https://github.com/MARZ2AMBITION"><I>MARZ2AMBITION</I></a>
        </td>
        <td>
            <img src="https://github.com/PowerJu.png?size=100" width="100">
            <br>
            <a href="https://github.com/PowerJu"><I>PowerJu</I></a>
        </td>
        <td>
            <img src="https://github.com/00ill.png?size=100" width="100">
            <br>
            <a href="https://github.com/00ill"><I>00ill</I></a>
        </td>
    </tr>
</table>


using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private InGameCursor _cursor;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] GameObject shortSword;
    private SpriteRenderer shortSword_sr;

    private Vector3 beforeAttack = new Vector3(0, 0.1f, 0);
    private Vector3 afterAttackRight = new Vector3(0.3f, -0.3f, 0);
    private Vector3 afterAttackLeft = new Vector3(-0.3f, -0.3f, 0);
    private Vector3 afterAttackRightfor4 = new Vector3(0.1f, -0.2f, 0);
    private Vector3 afterAttackLeftfor3 = new Vector3(-0.1f, -0.2f, 0);
    private float angle;

    private int tempQuar;
    private void Awake()
    {
        shortSword_sr = shortSword.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance.equipWeapon == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        if(GameManager.Instance.equipWeapon ==0)
        {
            tempQuar = Quardrant();
            angle = 90 + Mathf.Rad2Deg * Mathf.Atan(Mathf.Abs(_cursor.position.y - _playerController.transform.position.y) /
                Mathf.Abs(_cursor.position.x - _playerController.transform.position.x));
            if (_playerController.IsBeforeAttack) { shortSword_sr.sortingOrder = 9; }
            else { shortSword_sr.sortingOrder = 11; }

            if (_playerController.IsBeforeAttack) { transform.position = _playerController.transform.position + beforeAttack; }
            else
            {
                if (tempQuar == 1) { transform.position = _playerController.transform.position + afterAttackRight; }
                else if (tempQuar == 2) { transform.position = _playerController.transform.position + afterAttackLeft; }
                else if (tempQuar == 3) { transform.position = _playerController.transform.position + afterAttackLeftfor3; }
                else if (tempQuar == 4) { transform.position = _playerController.transform.position + afterAttackRightfor4; }
            }

            if (tempQuar == 1)
            {
                if (_playerController.IsBeforeAttack) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 15 + angle));
                else transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - 15 + angle));
            }
            else if (tempQuar == 2)
            {
                if (!_playerController.IsBeforeAttack) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 360 - 15 - angle));
                else transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 + 15 - angle));
            }
            else if (tempQuar == 3)
            {
                if (_playerController.IsBeforeAttack) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 360 + 15 + angle));
                else transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - 15 + angle));
            }
            else if (tempQuar == 4)
            {
                if (!_playerController.IsBeforeAttack) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 15 - angle));
                else transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - 15 - angle));
            }
        }
        else
        {
            transform.position = _playerController.transform.position;
        }
       

    }

    private int Quardrant()
    {
        if (_playerController.transform.position.x < _cursor.position.x && _playerController.transform.position.y < _cursor.position.y)
            return 1;
        else if (_playerController.transform.position.x >= _cursor.position.x && _playerController.transform.position.y < _cursor.position.y)
            return 2;
        else if (_playerController.transform.position.x >= _cursor.position.x && _playerController.transform.position.y >= _cursor.position.y)
            return 3;
        else if (_playerController.transform.position.x < _cursor.position.x && _playerController.transform.position.y >= _cursor.position.y)
            return 4;
        return 0;
    }
}

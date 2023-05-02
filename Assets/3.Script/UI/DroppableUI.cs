using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;
    private Color initColor;
    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        initColor = image.color;
    }

    /// <summary>
    /// ���콺 ����Ʈ�� ���� ������ ���� ���� ���η� �� �� 1ȸ ȣ��
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ������ ������ ��������� ����
    }

    /// <summary>
    /// ���콺 ����Ʈ�� ���� ������ ���� ������ �������� �� 1ȸ ȣ��
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // ������ ������ ������ �Ͼ������ ����
    }

    /// <summary>
    /// ���� ������ ���� ���� ���ο��� ����� ���� �� 1ȸ ȣ��
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        // pointerDrag�� ���� �巡���ϰ� �ִ� ���(=������)
        if (eventData.pointerDrag != null)
        {
            // �巡���ϰ� �ִ� ����� �θ� ���� ������Ʈ�� �����ϰ�, ��ġ�� ���� ������Ʈ ��ġ�� �����ϰ� ����
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            if (transform.gameObject.CompareTag("Equip_1"))
                GameManager.Instance.equipWeapon = eventData.pointerDrag.GetComponent<IWeapon>().GetWeaponNum();
            else if (transform.gameObject.CompareTag("Equip_2"))
                GameManager.Instance.equipWeapon2 = eventData.pointerDrag.GetComponent<IWeapon>().GetWeaponNum();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EElements;
using EGround;
using EInfluence;

public class GroundSetting : MonoBehaviour
{
    internal Ground gnd;
    internal Influence influence;
    internal int temp;

    [SerializeField] internal GameObject showRscUI;   //���� ���� ������ ǥ���ϱ� ���� UI�׷�

    [SerializeField] internal SpriteRenderer tiles;

    internal Sprite renderingGnd    //������ ǥ���� �� �� ���� ��������Ʈ 
    {   
        set
        {
            //���� ������ ��������Ʈ�� ���ٸ�
            if (tiles.sprite == null) tiles.sprite = value;
        }
    }

    internal void NowEnableGround(bool _e)  //���� ���� Ȱ��ȭ �Ǿ��ִ���
    {
        if (_e)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            tiles.color = Color.white;
            tiles.gameObject.SetActive(true);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.gray;
            tiles.color = Color.gray;
            tiles.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()  //���� Ŭ���Ͽ� �Ӽ�����
    {
        if (gnd.enableGround && Input.GetMouseButtonDown(0) && !showRscUI.activeInHierarchy)
        {
            //���� ������ ǥ���ϰ� ������ ��ġ�� �̵�
            showRscUI.SetActive(true);
            showRscUI.GetComponent<RectTransform>().anchoredPosition = gameObject.transform.position;

            //�׷�ȿ� �ִ� ���ӿ�����Ʈ�� ������Ʈ�� ��� ������ ǥ��
            showRscUI.transform.GetChild(0).GetComponent<ShowResource>().ShowState(gnd);
            showRscUI.transform.GetChild(1).GetComponent<SlotManager>().gnd = this.gnd;
            showRscUI.transform.GetChild(1).GetComponent<SlotManager>().ShowSlot();
            showRscUI.transform.GetChild(2).GetComponent<FillGauge>().ShowBar(gnd);
        }
    }
}

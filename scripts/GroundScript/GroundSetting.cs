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

    [SerializeField] internal GameObject showRscUI;   //땅에 대한 정보를 표시하기 위한 UI그룹

    [SerializeField] internal SpriteRenderer tiles;

    internal Sprite renderingGnd    //지형에 표시할 산 및 평지 스프라이트 
    {   
        set
        {
            //현재 지형에 스프라이트가 없다면
            if (tiles.sprite == null) tiles.sprite = value;
        }
    }

    internal void NowEnableGround(bool _e)  //현재 땅이 활성화 되어있는지
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

    private void OnMouseDown()  //땅을 클릭하여 속성설정
    {
        if (gnd.enableGround && Input.GetMouseButtonDown(0) && !showRscUI.activeInHierarchy)
        {
            //지형 정보를 표시하고 선택한 위치로 이동
            showRscUI.SetActive(true);
            showRscUI.GetComponent<RectTransform>().anchoredPosition = gameObject.transform.position;

            //그룹안에 있는 게임오브젝트의 컴포넌트를 얻어 정보를 표시
            showRscUI.transform.GetChild(0).GetComponent<ShowResource>().ShowState(gnd);
            showRscUI.transform.GetChild(1).GetComponent<SlotManager>().gnd = this.gnd;
            showRscUI.transform.GetChild(1).GetComponent<SlotManager>().ShowSlot();
            showRscUI.transform.GetChild(2).GetComponent<FillGauge>().ShowBar(gnd);
        }
    }
}

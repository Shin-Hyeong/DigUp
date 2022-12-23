using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EElements;
using EGround;
using EInfluence;

public class GroundManager : MonoBehaviour
{
    internal Influence guiInf;
    GameObject[] GroundGet = new GameObject[9];

    GroundSetting groundSettingTemp; //지형 오브젝트에 있는 GroundSetting을 전달한 임시 인스턴스

    private void Start()
    {
        //Debug.Log("게임 메니저 시작");
        GameManager.gameManager.groundManager = this;
    }

    internal void SettingGndManager()   //지형 초기 함수
    {
        for (int i = 0; i < 9; i++)
        {
            GroundGet[i] = transform.GetChild(i).gameObject;    //GroundManager의 자식으로 넣은 Ground프리팹을 각 인덱스로 가져옴
            groundSettingTemp = GroundGet[i].GetComponent<GroundSetting>(); //Ground에 있는 GroundSetting컴포넌트를 받아옴
            groundSettingTemp.gnd = guiInf.grounds[i];  //세력에 있는 ground를 오브젝트의 컴포넌트 Ground에 저장

            //현재 땅의 타일을 지정
            if (groundSettingTemp.gnd.thisGroundType == groundType.산)
                groundSettingTemp.renderingGnd = GameManager.gameManager.MountainTilesList[groundSettingTemp.gnd.thisGroundIndex];
            else if (groundSettingTemp.gnd.thisGroundType == groundType.평지)
                groundSettingTemp.renderingGnd = GameManager.gameManager.FieldTilesList[groundSettingTemp.gnd.thisGroundIndex];

            groundSettingTemp.NowEnableGround(groundSettingTemp.gnd.enableGround);  // 불러온 세력의 땅의 활성도를 대입해서 여부 확인

        }
    }
}

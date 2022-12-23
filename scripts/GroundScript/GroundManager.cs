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

    GroundSetting groundSettingTemp; //���� ������Ʈ�� �ִ� GroundSetting�� ������ �ӽ� �ν��Ͻ�

    private void Start()
    {
        //Debug.Log("���� �޴��� ����");
        GameManager.gameManager.groundManager = this;
    }

    internal void SettingGndManager()   //���� �ʱ� �Լ�
    {
        for (int i = 0; i < 9; i++)
        {
            GroundGet[i] = transform.GetChild(i).gameObject;    //GroundManager�� �ڽ����� ���� Ground�������� �� �ε����� ������
            groundSettingTemp = GroundGet[i].GetComponent<GroundSetting>(); //Ground�� �ִ� GroundSetting������Ʈ�� �޾ƿ�
            groundSettingTemp.gnd = guiInf.grounds[i];  //���¿� �ִ� ground�� ������Ʈ�� ������Ʈ Ground�� ����

            //���� ���� Ÿ���� ����
            if (groundSettingTemp.gnd.thisGroundType == groundType.��)
                groundSettingTemp.renderingGnd = GameManager.gameManager.MountainTilesList[groundSettingTemp.gnd.thisGroundIndex];
            else if (groundSettingTemp.gnd.thisGroundType == groundType.����)
                groundSettingTemp.renderingGnd = GameManager.gameManager.FieldTilesList[groundSettingTemp.gnd.thisGroundIndex];

            groundSettingTemp.NowEnableGround(groundSettingTemp.gnd.enableGround);  // �ҷ��� ������ ���� Ȱ������ �����ؼ� ���� Ȯ��

        }
    }
}

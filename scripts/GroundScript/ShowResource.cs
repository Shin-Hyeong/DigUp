using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using EElements;
using EGround;
using EInfluence;

public class ShowResource : MonoBehaviour
{
    Influence influence;
    Ground gnd;
    List<Sprite> resourceImg = new List<Sprite>(9);
    [SerializeField] GameObject[] inGroundImg = new GameObject[3];  //�ڿ�ǥ�� ������Ʈ

    [SerializeField] Text levelText, explorerText, infName;

    //���� �ִ� �ڿ����� �׸����� ǥ��
    resourceNode[] tmp = new resourceNode[3];

    private int produceTemp;    //ǥ���� ���� ������ �Ӽ�

    private void SettingRscImg(int _index, valueCategory _spices, int _prodTemp)   //�ڿ������� ���� �̹����� �̸��� ����
    {
        resourceImg = GameManager.gameManager.MiningItemList;
        resourceNode tmp = inGroundImg[_index].GetComponent<resourceNode>();
        switch (_spices)
        {
            case valueCategory.����:
                tmp.PrintResource(resourceImg[0], _spices, _prodTemp);
                break;
            case valueCategory.ö����:
                tmp.PrintResource(resourceImg[1], _spices, _prodTemp);
                break;
            case valueCategory.��:
                tmp.PrintResource(resourceImg[2], _spices, _prodTemp);
                break;
            case valueCategory.��:
                tmp.PrintResource(resourceImg[3], _spices, _prodTemp);
                break;
            case valueCategory.��ź:
                tmp.PrintResource(resourceImg[4], _spices, _prodTemp);
                break;
            case valueCategory.��ũ����Ʈ:
                tmp.PrintResource(resourceImg[5], _spices, _prodTemp);
                break;
            case valueCategory.����:
                tmp.PrintResource(resourceImg[6], _spices, _prodTemp);
                break;
            case valueCategory.õ������:
                tmp.PrintResource(resourceImg[7], _spices, _prodTemp);
                break;
            case valueCategory.�ֽ���:
                tmp.PrintResource(resourceImg[8], _spices, _prodTemp);
                break;
        }
        tmp.gameObject.SetActive(true);
    }

    private void SettingRscTxt(int _index, int _p)
    {
        resourceNode tmp = inGroundImg[_index].GetComponent<resourceNode>();
    }

    internal void ShowState(Ground _g)   // �ڿ� ���� ǥ��
    {
        gnd = _g;

        //����, Ž��, �ڿ�����, �����̸�
        //�̰� �ڿ����� ��Ÿ�� �� ǥ���ϴ� �޼����ε�
        // Ȥ�� ���� �ϴ� ������
        //infName.text = influence.Name.ToString();
        levelText.text = "���� : " + _g.nowGroundLevel.ToString();
        explorerText.text = "Ž�� : " + _g.exploerLevel.ToString();

        for(int i = 0; i < _g.resouceInGround.Count; i++)
        {

            //_g�� �޾ƿ� ���� resourceNode�� �ؽ�Ʈ�� ���� ����
            SettingRscImg(i, _g.resouceInGround[i], _g.GetResourceProduce(_g.resouceInGround[i]));
        }
    }

    internal void ConfirmMine(int _index)   //���������� Ķ �ڿ� (��ư ����)
    {
        gnd.effectMine = _index;
        Debug.Log(gnd.SetImportantMine());
        for (int i = 0; i < gnd.resouceInGround.Count; i++)
        {
            if(i == gnd.effectMine)
                SettingRscImg(i, gnd.resouceInGround[i], gnd.SetImportantMine());
            else
                SettingRscImg(i, gnd.resouceInGround[i], gnd.GetResourceProduce(gnd.resouceInGround[i]));
        }
    }
}

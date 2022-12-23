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
    [SerializeField] GameObject[] inGroundImg = new GameObject[3];  //자원표시 오브젝트

    [SerializeField] Text levelText, explorerText, infName;

    //땅에 있는 자원들을 그림으로 표시
    resourceNode[] tmp = new resourceNode[3];

    private int produceTemp;    //표시할 값을 전달할 속성

    private void SettingRscImg(int _index, valueCategory _spices, int _prodTemp)   //자원종류에 따라 이미지와 이름을 설정
    {
        resourceImg = GameManager.gameManager.MiningItemList;
        resourceNode tmp = inGroundImg[_index].GetComponent<resourceNode>();
        switch (_spices)
        {
            case valueCategory.구리:
                tmp.PrintResource(resourceImg[0], _spices, _prodTemp);
                break;
            case valueCategory.철광석:
                tmp.PrintResource(resourceImg[1], _spices, _prodTemp);
                break;
            case valueCategory.금:
                tmp.PrintResource(resourceImg[2], _spices, _prodTemp);
                break;
            case valueCategory.은:
                tmp.PrintResource(resourceImg[3], _spices, _prodTemp);
                break;
            case valueCategory.석탄:
                tmp.PrintResource(resourceImg[4], _spices, _prodTemp);
                break;
            case valueCategory.보크사이트:
                tmp.PrintResource(resourceImg[5], _spices, _prodTemp);
                break;
            case valueCategory.석유:
                tmp.PrintResource(resourceImg[6], _spices, _prodTemp);
                break;
            case valueCategory.천연가스:
                tmp.PrintResource(resourceImg[7], _spices, _prodTemp);
                break;
            case valueCategory.텅스텐:
                tmp.PrintResource(resourceImg[8], _spices, _prodTemp);
                break;
        }
        tmp.gameObject.SetActive(true);
    }

    private void SettingRscTxt(int _index, int _p)
    {
        resourceNode tmp = inGroundImg[_index].GetComponent<resourceNode>();
    }

    internal void ShowState(Ground _g)   // 자원 스탯 표시
    {
        gnd = _g;

        //레벨, 탐색, 자원종류, 세력이름
        //이거 자원정보 나타낼 때 표시하는 메서드인데
        // 혹시 몰라서 일단 구현함
        //infName.text = influence.Name.ToString();
        levelText.text = "레벨 : " + _g.nowGroundLevel.ToString();
        explorerText.text = "탐색 : " + _g.exploerLevel.ToString();

        for(int i = 0; i < _g.resouceInGround.Count; i++)
        {

            //_g에 받아온 값을 resourceNode의 텍스트로 값을 전달
            SettingRscImg(i, _g.resouceInGround[i], _g.GetResourceProduce(_g.resouceInGround[i]));
        }
    }

    internal void ConfirmMine(int _index)   //집중적으로 캘 자원 (버튼 연결)
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

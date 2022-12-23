using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using EStorage;
using EElements;
using EInfluence;
using UnityEditor.SceneManagement;
using System.ComponentModel;

public class SellOption : MonoBehaviour
{
    [SerializeField] DrawGraph drawGraph;
    
    internal EStorage.Storage Storage;
    internal Influence Influence;
    Text sellingValue;
    [SerializeField] GameObject sellingBar;
    [SerializeField] RectTransform fillBar, emptyBar;

    [SerializeField] Text valueName, valueInfo;
    string nowValue;
    int minValue, maxValue;
    [SerializeField] GameObject itemContent;
    [SerializeField] GameObject item , sellPopUp;

    int itemDef = 0;

    private int count = 0;
    private int itemType =0;

    internal IEnumerator Born()
    {
        yield return null;
        drawGraph.storage = Storage; 

        sellingValue = sellingBar.GetComponent<Text>();

        nowValue = sellingValue.text;

        //nowValue.Split("/");
        //minValue = (int)nowValue[0];
        //maxValue = (int)nowValue[1];

        fillBar.sizeDelta = new Vector2(emptyBar.rect.width, emptyBar.rect.height);
        yield break;
    }

    internal void setMinMaxValue()
    {
        //Debug.Log(itemType);
        maxValue = Influence.resorce[itemType];
        minValue = maxValue/2;
    }

    void Update()
    {

        float dividedSize = (emptyBar.rect.width / (float)maxValue);

        fillBar.sizeDelta = new Vector2((float)minValue * dividedSize, emptyBar.rect.height);

        //키보드 조작
        /*if(Input.GetKeyDown(KeyCode.Return))
        {
            SellThisItem(itemType);
            gameObject.SetActive(false);
        }*/
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }

        const float wheelControll = 0f;
        if(wheelControll < Input.GetAxis("Mouse ScrollWheel"))
        {
            IncreaseValue();
        }else if(wheelControll > Input.GetAxis("Mouse ScrollWheel"))
        {
            DecreaseValue();
        }
    }

    public void ShowOption(int _itemType)
    {
        Debug.Log(_itemType);
        itemType = _itemType;
        sellPopUp.SetActive(true);
        setMinMaxValue();
        ReWriteValue();
        //gameObject.SetActive(true);
    }

    internal void ShowInfo(int _itemType)
    {
        valueName.text = (valueCategory)_itemType + "";
        valueInfo.text = ""; //자원에 대한 설명 출력인데 아직 없음
        drawGraph.showthisgraph(_itemType);
    }

    private void ReWriteValue()
    {
        nowValue = minValue.ToString() + "/" + maxValue.ToString();
        sellingValue.text = nowValue;
    }

    public void IncreaseValue()
    {
        int temp = 1;
        if (Input.GetAxisRaw("Shift") > 0)
        {
            temp += 9;
        }
        if (Input.GetAxisRaw("Ctrl") > 0)
        {
            temp *= 10;
        }

        minValue += temp;
        if (minValue > maxValue)
            minValue = maxValue;
        ReWriteValue();
    }

    public void DecreaseValue()
    {
        int temp = -1;
        if (Input.GetAxisRaw("Shift") > 0)
        {
            temp -= 9;
        }
        if (Input.GetAxisRaw("Ctrl") > 0)
        {
            temp *= 10;
        }

        minValue += temp;
        if (minValue < 0)
            minValue = 0;
        ReWriteValue();
    }

    public void SellThisItem()
    {
        Influence.gold += Storage.Selling((valueCategory)itemType, minValue, Influence.ID);
        Print();
    }

    private void OnEnable()
    {
        Print();
    }

    internal void Print()
    {
        int[] resorce = Influence.resorce;
        int rsize = resorce.Length;

        for(int i = 0; i < rsize; i++)
        {
            if(resorce[i] > 0)
            {
                GameObject temp;
                itemNode tempItemNode;
                temp = GameObject.Instantiate(item);
                tempItemNode = temp.GetComponent<itemNode>();
                tempItemNode.influence = this.Influence;
                tempItemNode.storage = this.Storage;
                tempItemNode.sellOption = this;
                tempItemNode._itemType = i;
                tempItemNode.sell.onClick.AddListener(()=>this.ShowOption(tempItemNode._itemType));
                temp.transform.SetParent(itemContent.transform, false);
                temp.transform.localPosition = new Vector3(-5, -5 - (i * 80), 0);
                tempItemNode.Print();
            }
        }
    }
}

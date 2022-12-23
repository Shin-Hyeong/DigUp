using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using EElements;
using EStorage;
using EInfluence;

public class itemNode : MonoBehaviour
{
    [SerializeField] internal Text resource, stock, property;
    [SerializeField]
    public int _itemType;

    //Scrollbar moreItem;
    //Scrollbar infoItem;

    internal Storage storage;
    internal Influence influence;

    [SerializeField] internal Button sell;
    [SerializeField] internal Image resorceImage;

    Color increaseValue = Color.red;
    Color decreaseValue = Color.blue;

    internal SellOption sellOption;
    // Start is called before the first frame update

    // Update is called once per frame
    internal void Print()
    {
        int tempTrun = System.Math.Min(DataBases.turn, 19);
        float propCalcul = tempTrun > 1 ? storage.marketPrice[tempTrun - 1, _itemType]
            - storage.marketPrice[tempTrun, _itemType] : 0;
        SetIndeValue(propCalcul);


        stock.text = "Àç°í : " + influence.resorce[_itemType].ToString();
        resource.text = (valueCategory)_itemType + "";
        resorceImage.sprite = GameManager.gameManager.StorageItemImageList[_itemType];
    }

    public void PrintInfo()
    {
        sellOption.ShowInfo(_itemType);
    }

    private void SetIndeValue(float props)
    {
        property.text = props >= 0 ? "¡è\n" + string.Format("{0:0.00}",Mathf.Abs(props)) + ""
            : "¡é\n" + string.Format("{0:0.00}", Mathf.Abs(props)) + "";
        property.color = props >= 0 ? increaseValue : decreaseValue;
    }
}
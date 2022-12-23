using UnityEngine;
using UnityEngine.UI;

using EElements;
using EGround;

public class resourceNode : MonoBehaviour   //이거는 자원표시안에 있는 아이템
{
    Ground gnd;
    // 자원의 이미지랑 이름, 생산량
    internal Image thisImg;
    [SerializeField] Text rscName;
    [SerializeField] Text rscProd;

    const string prodTxt = "생산량 : ";

    internal void PrintResource(Sprite _img, valueCategory _name, int _prod)
    {
        thisImg = GetComponent<Image>();
        thisImg.sprite = _img;
        rscName.text = _name.ToString();
        rscProd.text = prodTxt + _prod.ToString();
    }
}

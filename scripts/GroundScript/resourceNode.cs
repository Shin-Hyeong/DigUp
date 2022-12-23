using UnityEngine;
using UnityEngine.UI;

using EElements;
using EGround;

public class resourceNode : MonoBehaviour   //�̰Ŵ� �ڿ�ǥ�þȿ� �ִ� ������
{
    Ground gnd;
    // �ڿ��� �̹����� �̸�, ���귮
    internal Image thisImg;
    [SerializeField] Text rscName;
    [SerializeField] Text rscProd;

    const string prodTxt = "���귮 : ";

    internal void PrintResource(Sprite _img, valueCategory _name, int _prod)
    {
        thisImg = GetComponent<Image>();
        thisImg.sprite = _img;
        rscName.text = _name.ToString();
        rscProd.text = prodTxt + _prod.ToString();
    }
}

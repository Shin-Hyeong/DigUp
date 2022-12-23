using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using EGround;

public class FillGauge : MonoBehaviour
{
    Slider bar;
    internal Ground gnd;
    private float fillSpeed;        //������ �ӵ� (���Կ� �޾ƿð�)
    private float nowGauge = 0f;    //���� ������
    private bool progress = false;  //�۾� �ϰ��ִ� ����

    /*(�ش� ������ ��ġ�� ��� ������ �۾��ɷ� / 10%) ��ŭ ���*/

    internal void ShowBar(Ground _g)    //������ ǥ��
    {
        gnd = _g;
        gameObject.SetActive(true);
        bar = GetComponent<Slider>();

        if(this.gameObject.activeInHierarchy)
            InitGauge();
    }

    internal void InitGauge()   //������ �ʱ� ����
    {
        fillSpeed = gnd.slot.GetSpeed();
        //Debug.Log(fillSpeed);
        if (!progress)
        {
            bar.value = 0f;
            progress = true;
            StartCoroutine(Progessing());
        }
    }

    IEnumerator Progessing()  //�۾� ���� ����
    {
        fillSpeed = gnd.slot.GetSpeed();
        bar.value += fillSpeed * 0.01f;

        if (bar.value >= 100)
        {
            progress = false;
            FilledBar();
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Progessing());
            yield break;
        }
    }

    private void FilledBar()    //�������� �� á�ٸ�
    {
        gnd.SetProducingValue();
        for (int i = 0; i < gnd.resouceInGround.Count; i++)
        {
            //�ڿ��� ������ â��� ������ �Ǵµ� 
            int itemIndex = (int)gnd.resouceInGround[i];
            Debug.Log(gnd.master.resorce[itemIndex]);
        }
        InitGauge();
    }
}

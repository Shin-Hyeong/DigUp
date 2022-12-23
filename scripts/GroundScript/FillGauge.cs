using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using EGround;

public class FillGauge : MonoBehaviour
{
    Slider bar;
    internal Ground gnd;
    private float fillSpeed;        //게이지 속도 (슬롯에 받아올거)
    private float nowGauge = 0f;    //현재 게이지
    private bool progress = false;  //작업 하고있는 상태

    /*(해당 지형에 배치된 모든 직원의 작업능력 / 10%) 만큼 상승*/

    internal void ShowBar(Ground _g)    //게이지 표시
    {
        gnd = _g;
        gameObject.SetActive(true);
        bar = GetComponent<Slider>();

        if(this.gameObject.activeInHierarchy)
            InitGauge();
    }

    internal void InitGauge()   //게이지 초기 설정
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

    IEnumerator Progessing()  //작업 진행 상태
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

    private void FilledBar()    //게이지가 다 찼다면
    {
        gnd.SetProducingValue();
        for (int i = 0; i < gnd.resouceInGround.Count; i++)
        {
            //자원을 세력의 창고로 보내야 되는데 
            int itemIndex = (int)gnd.resouceInGround[i];
            Debug.Log(gnd.master.resorce[itemIndex]);
        }
        InitGauge();
    }
}

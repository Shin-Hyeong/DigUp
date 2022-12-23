using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EInfluence;
using EPerson;

public class PersonStat : MonoBehaviour
{
    [SerializeField]
    internal Text personName, money, property, situation;

    internal Person me;
    [SerializeField]
    internal Influence master;
    [SerializeField]
    internal Button adBt,interviewBt;
    [SerializeField]
    internal Slider[] stat = new Slider[3];

    bool EnterClick = false;

    internal void textprint()
    {

        personName.text = me.Name;
        money.text = me.giveMoney+"G";
        property.text = "특성:"+me.getProperty();
        situation.text = "상태: " + me.getReliy() + "(" + me.reliAbility +") ," + me.getCrowd();
        if (me.masterGetInterview() && !me.makeInterview)
        {
            interviewBt.interactable = true;
            interviewBt.gameObject.GetComponent<Image>().color = new Color(1,1,1,1);
            interviewBt.transform.GetChild(0).GetComponent<Text>().text = "면접";
            interviewBt.onClick.AddListener(me.CanHireInterview); 
        }
        else
        {
            interviewBt.interactable = false;
            interviewBt.gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            interviewBt.transform.GetChild(0).GetComponent<Text>().text = "";
        }
        if (me.masterGetAddber()&& !me.makeproperty)
        {
            adBt.interactable = true;
            adBt.gameObject.GetComponent<Image>().color = new Color(1,1,1,1);
            adBt.transform.GetChild(0).GetComponent<Text>().text = "홍보";
            adBt.onClick.AddListener(me.CanHireAd); 
        }
        else
        {
            adBt.interactable = false;
            adBt.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            adBt.transform.GetChild(0).GetComponent<Text>().text = "";
        }
        for (int i = 0; i < 3; i++)
        {
            stat[i].value = me.stat[i];
        }
    }

    public void SetEnter(bool index)
    {
        EnterClick = index;
        //Debug.Log(EnterClick);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SetPopUp();
        }
    }

    void SetPopUp()
    {
        if (!EnterClick)
        {
            this.gameObject.SetActive(false);
            //Debug.Log(EnterClick);
        }
    }
}
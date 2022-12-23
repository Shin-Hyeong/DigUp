using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EPerson;

public class PersonProfile : MonoBehaviour
{
    [SerializeField]
    internal Text personName, money, isHire, property;
    [SerializeField]
    internal Button hireBt;
    internal Person me;
    [SerializeField]
    internal Slider[] stat = new Slider[3];
    [SerializeField] internal Transform nextProfilePos;

    [SerializeField] internal PersonHireMenuControll masterPage;

    [SerializeField] internal bool is_hire;

    [SerializeField] internal Image face;

    internal void textPrint()
    {
        personName.text = me.Name;
        property.text = "Ư��:"+me.getProperty();
        face.sprite = GameManager.gameManager.PersonFaceImageList[me.faceGraphic];
        if (me.isHire) { money.text = "�ӱ�: " + me.giveMoney; isHire.text = "�ذ��ϱ�"; is_hire = true; }
        else { money.text = "����: " + me.hireMoney; isHire.text = "����ϱ�"; is_hire = false; }
        for(int i = 0; i < 3; i++)
        {
            stat[i].value = me.stat[i];
        }
    }

    internal void kill()
    {
        this.me.killSelf();
        masterPage.firePageStart = masterPage.firePageStart;
        masterPage.Print();
    }

    internal void Hire()
    {
        this.me.Hire();
        masterPage.hirePageStart = masterPage.hirePageStart;
        masterPage.Print();
    }

    public void setSelect()
    {
        masterPage.selectProfile = this;
        masterPage.setPopUp();
    }
}

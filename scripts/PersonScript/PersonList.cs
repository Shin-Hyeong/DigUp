using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EInfluence;
using EPerson;

public class PersonList : MonoBehaviour
{
    internal List<Person> iconList;
    [SerializeField]
    internal GameObject icon;
    [SerializeField]
    internal GameObject arbrtIcon;
    internal Influence master;
    List<GameObject> memory = new List<GameObject>(6);

    int ppersonPageStart;
    internal int personPageStart
    {
        get { return ppersonPageStart; }
        set
        {
            if (value >= (iconList.Count - 5)) { ppersonPageStart = iconList.Count - 5; }
            else ppersonPageStart = value;
            if (ppersonPageStart < 0) ppersonPageStart = 0;
            Debug.Log(iconList.Count - 5 + "    " + value);
            this.Print();
        }
    }

    internal void Print()
    {
        int iconListLength;
        iconList = master.people;
        List<Person> iconPage = new List<Person>(5);
        int memoryCount = memory.Count;
        iconListLength = System.Math.Min(5, iconList.Count);
        for (int i = 0; i < memoryCount; i++)
        {
            Destroy(memory[i]);
        }
        memory.Clear();
        
        for (int i = 0; i < iconListLength; i++)
        {
            iconPage.Add(iconList[i + personPageStart]);
            GameObject tempicon = Instantiate(icon);
            tempicon.transform.SetParent(this.transform,false);
            tempicon.transform.localPosition = new Vector3(127 - (50 * i), 0f, 0);
            tempicon.GetComponent<PersonIcon>().me = iconList[i + personPageStart];
            tempicon.GetComponent<PersonIcon>().Print();
            //tempicon.GetComponent<Image>().color = iconList[i].color;
            memory.Add(tempicon);
        }

    }
   public void pagerightside()
    {
        personPageStart++;
    }
    public void pageleftside()
    {
        personPageStart--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EPerson;
using EInfluence;
using EElements;

public class PersonHireMenuControll : MonoBehaviour
{
    internal List<Person> hireList, fireList; //얘네 둘은 창을 키고, 
    internal Influence master;
    [SerializeField]
    internal GameObject profile, popUp;

    [SerializeField]
    internal PersonList personList;
    [SerializeField]
    internal Text totalPay, worning, hireOrKillText, popUpName, popUpHireMoney, popUpPay;

    [SerializeField] internal Image face;

    [SerializeField] internal Button hireOrKill;
    internal PersonProfile selectProfile;

    List<GameObject> memory = new List<GameObject>(7);

    int phirePageStart;
    internal int hirePageStart {
        get { return phirePageStart; }
        set { if (value >= (hireList.Count - 4)) { phirePageStart = hireList.Count - 4;  Debug.Log(value+",  "+ (hireList.Count - 4)); }
            else phirePageStart = value;
            if (phirePageStart < 0) phirePageStart = 0;
            
            this.Print();
        }
    }
    int pfirePageStart;
    internal int firePageStart {
        get { return pfirePageStart;}
         set {
                if (value >= (fireList.Count - 3)) pfirePageStart = fireList.Count - 3;
                else pfirePageStart = value;
                if (pfirePageStart < 0) pfirePageStart = 0;
                
                this.Print();
         }
    }

    // Start is called before the first frame update
    void Start()
    {
        hirePageStart = 0;
        firePageStart = 0;
    }

    private void OnEnable()
    {
        hireList = master.hirePeople;
        fireList = master.people;
        //this.memory.Capacity = 7;
        Print();
    }

    internal void Print()
    {
        //Debug.Log("히오스");

        List<Person> hirePage = new List<Person>(4);
        List<Person> firePage = new List<Person>(3);
        int hireCount = System.Math.Min(hireList.Count, 4);
        int fireCount = System.Math.Min(fireList.Count, 3);
        int memoryCount = memory.Count;
        for(int i = 0; i < memoryCount; i++)
        {
            Destroy(memory[i]);
        }
        memory.Clear();

        for(int i = 0; i < hireCount; i++)
        {
            //Debug.Log(i + hirePageStart);
            hirePage.Add(hireList[i + hirePageStart]);
            GameObject tempProfile = Instantiate(profile);
            PersonProfile tempInfo = tempProfile.GetComponent<PersonProfile>();
            tempProfile.transform.SetParent(this.transform ,false);
            tempProfile.transform.localPosition = new Vector3(-180f, 200 - (i * 85), -20);
            tempInfo.me = master.hirePeople[i + hirePageStart];
            tempInfo.masterPage = this;
            tempInfo.textPrint();
            tempProfile.transform.SetParent(this.transform.GetChild(0), true);
            memory.Add(tempProfile);
        }
        for (int i = 0; i < fireCount; i++)
        {
            //Debug.Log(i + firePageStart);
            firePage.Add(fireList[i + firePageStart]);
            GameObject tempProfile = Instantiate(profile);
            PersonProfile tempInfo = tempProfile.GetComponent<PersonProfile>();
            tempProfile.transform.SetParent(this.transform, false);
            tempProfile.transform.localPosition = new Vector3(175f, 200 - (85 * i), -20);
            tempInfo.me = master.people[i + firePageStart];
            tempInfo.masterPage = this;
            tempInfo.textPrint();
            tempProfile.transform.SetParent(this.transform.GetChild(0), true);
            memory.Add(tempProfile);
        }
        hireCount = 0;
        fireCount = fireList.Count;
        for(int i = 0; i < fireCount; i++)
        {
            hireCount += fireList[i].giveMoney;
        }
        totalPay.text = "총 임금: "+hireCount;
    }

    public void nextUpHirePage()
    {
        hirePageStart++;
    }    
    public void nextDownHirePage()
    {
        hirePageStart--;
    }
    public void nextUpFirePage()
    {
        firePageStart++;
    }
    public void nextDownFirePage()
    {
        firePageStart--;
    }

    internal void setPopUp()
    {
        popUp.SetActive(true);
        face.sprite = selectProfile.face.sprite;
        popUpName.text = selectProfile.personName.text;
        popUpHireMoney.text = "고용비: "+selectProfile.me.hireMoney;
        popUpPay.text = "급여: "+selectProfile.me.giveMoney;
        if (selectProfile.is_hire)
        {
            hireOrKill.interactable = true;
            hireOrKill.onClick.AddListener(selectProfile.kill);
            worning.text = "해고하시겠습니까?";
            hireOrKillText.text = "해고한다";
        }
        else
        {
            int hireMoney = selectProfile.me.hireMoney;
            float masterGold = master.gold;
            if (hireMoney > masterGold)
            {
                hireOrKill.interactable = false;
                worning.text = "고용비가 부족합니다!";
                hireOrKillText.text = "돈이없다!";
            }
            else
            {
                hireOrKill.interactable = true;
                hireOrKill.onClick.AddListener(selectProfile.Hire);
                worning.text = "고용하시겠습니까?";
                hireOrKillText.text = "고용한다";
            }
        }
        hireOrKill.onClick.AddListener(() => popUp.SetActive(false));
        hireOrKill.onClick.AddListener(() => personList.Print());
        hireOrKill.onClick.AddListener(() => hireOrKill.onClick.RemoveAllListeners());
    }

    public void clearListeners()
    {
        hireOrKill.onClick.RemoveAllListeners();
    }
}

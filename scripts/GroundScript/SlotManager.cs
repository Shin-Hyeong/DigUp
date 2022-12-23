using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using ESlot;
using EGround;
using EPerson;

public class SlotManager : MonoBehaviour
{
    internal Slot slot;
    internal Person person;
    static internal SlotManager slotTemp;
    internal Vector3 screenPosition;    //화면에 보이는 좌표

    [SerializeField] internal GameObject getIcon;
    internal PersonList tmpPerson;

    List<GameObject> employeeInSlot = new List<GameObject>(5);
    List<GameObject> memoryList = new List<GameObject>(5);

    float[] sortSlotX = { 8.7f, 55.8f, -12f, 12.7f, 37.6f };

    Ground ggnd;
    RectTransform slotSize;

    internal Ground gnd
    {
        get { return ggnd; }
        set 
        { 
            ggnd = value;
            slot = ggnd.slot;
        }
    }
    
    private void Start()
    {
        tmpPerson = GameManager.gameManager.gameInfo.transform.GetChild(1).GetComponent<PersonList>();
        Debug.Log(tmpPerson);
    }
    internal void ShowSlot()    //현재슬롯 표시
    {
        if (ggnd != null)
        {
            int maxSlotSize = slot.slotUse;
            memoryList.ForEach(v => { GameObject.Destroy(v); });
            memoryList.Clear();
            if(tmpPerson != null)
                tmpPerson.Print();
            for (int i = 0; i < maxSlotSize; i++)
            {
                if (slot.slotItem[i] != null)
                {
                    GameObject tmpPerson = Instantiate(getIcon);
                    memoryList.Add(tmpPerson);
                    tmpPerson.GetComponent<PersonIcon>().me = slot.slotItem[i];
                    tmpPerson.transform.SetParent(this.transform, false);
                    tmpPerson.GetComponent<RectTransform>().anchoredPosition = new Vector2(sortSlotX[i], 30);
                    tmpPerson.SetActive(true);
                    tmpPerson.GetComponent<PersonIcon>().Print();
                }
            }
        }
    }

    public void MouseEnter()
    {
        slotTemp = this;
    }
    internal IEnumerator ExitCoroutine()
    {
        
        yield return null;
        slotTemp = null;
        yield break;
    }
    public void MouseExit()
    {
        StartCoroutine(ExitCoroutine());
    }
}
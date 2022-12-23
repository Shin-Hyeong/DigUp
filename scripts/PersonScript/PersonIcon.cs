using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EPerson;

public class PersonIcon : MonoBehaviour
{
    internal Person me;
    [SerializeField]
    internal GameObject profile;
    [SerializeField] internal Image face;
    static GameObject alreadyProfile;
    [SerializeField] internal GameObject Action, property;
    internal static Person personTemp;
    private bool isDrag;
    private Vector3 originalPoint;
    private RectTransform recttransform;
    internal void PopUpPrint()
    {
        if(alreadyProfile == null)
        {
            alreadyProfile = Instantiate(profile);
        }
        alreadyProfile.GetComponent<PersonStat>().me = this.me;
        alreadyProfile.GetComponent<PersonStat>().textprint();
        alreadyProfile.transform.SetParent(transform.parent, false);
        
        alreadyProfile.SetActive(true);
        alreadyProfile.transform.position = gameObject.transform.position;

    }
    internal void Print()
    {
        int action;
        face.sprite = GameManager.gameManager.PersonFaceImageList[me.faceGraphic];
        action = me.GetAction();
        if (action == -1)
        {
            Action.SetActive(false);
        }
        else
        {
            Action.GetComponent<Image>().sprite = GameManager.gameManager.PersonActionImageList[action];
            Action.SetActive(true);
        }

        action = me.getPropertyGoodCount();
        if (action == 0)
        {
            property.SetActive(false);
        }
        else
        {
            if (action < 0)
                property.GetComponent<Image>().color = new Color(1, 0, 0, 1);
            else
                property.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            property.SetActive(true);
        }
    }
    private void Start()
    {
        GetComponent<Image>().color = me.color;
        transform.GetComponent<Button>().onClick.AddListener(PopUpPrint);
        recttransform = GetComponent<RectTransform>();
    }

    public void DragStart() //Mouse Down
    {
        originalPoint = this.transform.position;
        isDrag = true;
        personTemp = me;
    }
    public void DragEnd()   //Mouse Up
    {
        isDrag = false; 
        this.recttransform.position = originalPoint;
        if (SlotManager.slotTemp != null) 
        {
            //Debug.Log("This");
            SlotManager.slotTemp.slot.PutPerson(me.ID);
            SlotManager.slotTemp.ShowSlot();
            //transform.SetParent(GameObject.Find("personInSlot").transform, false);
        }
    }

    private void FixedUpdate()
    {
        if (isDrag)
        {
            Vector2 temp = new Vector2(Input.mousePosition.x - 25, Input.mousePosition.y);
            recttransform.anchoredPosition = temp;
        }
    }
}

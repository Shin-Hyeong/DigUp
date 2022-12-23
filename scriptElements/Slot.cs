using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EElements;
using EPerson;

namespace ESlot
{

    [System.Serializable]
    [SerializeField]
    internal class Slot : Elements
    {
        const int RmaxSlotSize = 5;

        internal Person[] slotItem = new Person[RmaxSlotSize];
        [SerializeField] internal int[] slotItemID = new int[RmaxSlotSize];
        [SerializeField] internal int slotUse = 2; //사용 가능한 슬롯 개수
        //slotid는 언제나 0초과이기 때문에 0인경우는 빈 슬롯으로 취급
        [SerializeField] internal personStatCategory useStat = 0; //사용하는 스텟

        internal Slot() { this.elementsMode = Emode.슬롯; }
        internal void PutPerson(int _ID)
        {
            int _temp = 0;
            if (!isAlreadyPerson(_ID)) return;
            for (_temp = 0; _temp < slotUse; _temp++)
            {
                if (slotItem[_temp] == null) break;
            }
            if (_temp < slotUse)
            {
                slotItem[_temp] = (Person)DataBases.allData[_ID];
                slotItemID[_temp] = _ID;
                for (int i = 0; i < slotUse; i++)
                {
                    if (slotItem[i] != null) slotItem[i].EventPutSlot(this);
                }
            }
        }

        internal void PopPerson(int _ID)
        {
            int _temp = 0;
            for (_temp = 0; _temp < slotUse; _temp++)
            {
                if (slotItemID[_temp] == _ID)
                {
                    for (int i = 0; i < slotUse; i++)
                    {
                        slotItem[i].EventPopSlot(this);
                    }
                    slotItem[_temp] = null;
                    slotItemID[_temp] = 0;
                    break;
                }
            }
        }

        internal bool isAlreadyPerson(int _ID)
        {
            foreach (int ID in slotItemID)
            {
                if (ID == _ID) return false;
            }
            return true;
        }

        internal void SetUseSize(int _size)
        {
            int size = _size;
            if (size > RmaxSlotSize) size = RmaxSlotSize;
            if (size < 0) size = 0;

            if (slotUse > size)
            {
                for (int i = slotUse; i < RmaxSlotSize; i++)
                {
                    slotItem[i] = null;
                    slotItemID[i] = 0;

                }
            }

            slotUse = size;
        }

        internal float GetSpeed()
        {
            float result = 0f;
            foreach (Person v in slotItem)
            {
                if (v != null) result += v.GetSpeed(this.useStat);
            }
            return result;
        }

        internal int[] GetBurff()
        {
            int[] result = new int[2];

            if (useStat == personStatCategory.작업능력)
            {
                foreach (Person v in slotItem)
                {
                    if (v != null) { result[0] += v.groundBuff; result[1] += v.miningBuff; }
                }
            }
            else if (useStat == personStatCategory.두뇌)
            {
                foreach (Person v in slotItem)
                {
                    if (v != null) result[0] += v.reserchPoint;
                }
            }
            else if (useStat == personStatCategory.첩보)
            {
                foreach (Person v in slotItem)
                {
                    if (v != null) result[0] += v.spyBuff;
                }
            }

            return result;
        }

        internal override void fLoad()
        {
            for (int i = 0; i < RmaxSlotSize; i++)
            {
                if (slotItemID[i] == 0) slotItem[i] = null;
                else slotItem[i] = (Person)DataBases.allData[slotItemID[i]];
            }
        }

        internal override void fSave()
        {
            for (int i = 0; i < RmaxSlotSize; i++)
            {
                if (slotItem[i] == null)
                {
                    slotItemID[i] = 0;
                }
                else
                {
                    slotItemID[i] = slotItem[i].ID;
                }
            }

            //slotData = JsonSerializer.Serialize(this, typeof(object));
            //return slotData;
        }
    }

}
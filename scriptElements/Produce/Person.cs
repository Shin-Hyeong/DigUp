using System.Collections;
using UnityEngine;
using EProduce;
using ESlot;
using System.Collections.Generic;
using EInfluence;



namespace EElements
{
    using EPerson;
    internal partial class DataBases
    {
        [SerializeField] internal static string[] nameListF = {
            "김", "이", "임", "강", "홍", "탁"
        };
        [SerializeField] internal static string[] nameListL =
        {
            "홍인", "동인", "동건", "신형", "도완", "혁재"
        };
        //static [SerializeField] internal List<Person> PersonData = new List<Person>();
        //static [SerializeField] internal int personLength = 0;
    }

    [SerializeField] internal enum personEffectNameList //직원 특성 상태
    {
        // 버프
        재치있는 = 1, // 같은 【지형】에 배치된 【직원】의 수가 3 일때 【작업능력】 1 증가
        성실한 = 2, // 배치된 【지형】의 자원량 5%p 증가. 【연구 포인트】 3 추가 획득
        조직적인 = 4, // 같은 【지형】에 배치된 【직원】의 수가 2 명이상 일때 같은 【지형】에배치된 【직원】 한명당 【작업능력】 5 증가 (최대 10)
        재능있는 = 8, // 고용하고 5 【턴】 이후 채광효율, 첩보효율 15%p 증가 【연구포인트】 10 추가 획득 x
        알뜰한 = 16, //【급여】 30%p 감소
        소박한 = 32, //【고용비】 50%p 감소
        전문적인 = 64, // 채광 효율, 첩보효율 10%p 증가. 【연구포인트】 5 추가획득
        끈기있는 = 128, // 동일한 【슬롯】에 【턴】이 지날 수록 채광효율 3%p 증가(최대 5 스택) x
        베태랑 = 256, // 모든 【능력】 1 만큼 상승
        열정적인 = 512, // 1 【턴】 동안 모든 【능력】 1 만큼 상승 x
                    // 너프
        개인적인 = 1024, // 같은 【지형】에 배치된 【직원】이 3 이상일 때 【작업능력】 1 감소g
        게으른 = 2048, // 채광 효율, 첩보효율 10%p 감소. 【연구포인트】 획득량 5 감소g
        내성적인 = 4096, // 같은 【지형】에 배치된 직원이 수가 2 명 이상 일때 같은 【지형】에 배치된【직원】 한명당 【작업능력】 5 감소 (최대 10)g
        변덕적인 = 8192, // 4【턴】에 한번씩 일을 안함. 단 【능력】 총합 수치 15 이상g
        도벽걸린 = 16384, // 2【턴】마다 유저의 【골드】를 50G 씩 훔쳐감g
        비겁한 = 32768, // 배치된【지형】의 자원량 5%p 감소.【연구 포인트】 획득량 3 감소g
        무례한 = 65536, // 같은【지형】에 배치된 【직원】이 매【턴】 5%확률로 부상g
        낭비가심한 = 131072, //【연구포인트】 획득량 5 감소. 침투력 획득량 10 감소-
    }
    [SerializeField] internal enum personStatCategory //스탯   슬롯스크립트에 참조가능(SlotMode)
    {
        작업능력,
        두뇌,
        첩보
    }
    [SerializeField] internal enum personReliStack
    {
        불신 = 0,
        의심 = 5,
        보통 = 10,
        믿음 = 15,
        신뢰 = 20
    }
    [SerializeField] internal enum crowdControl // 상태 이상(모든 이상은 【골드】로 대처 가능. 단 진행된 【턴】수, 이상에 따라 금액 차이)
    {
        건강함, // 기본 상태
        부상,  // 1【턴】 동안 【직원】의 모든【능력】 2 만큼 하락
        중상,  // 3【턴】 동안 【직원】의 행동 불가
        질병,  // 2【턴】 동안 【직원】의 모든【능력】 1 만큼 하락
        전염병, // 5【턴】 동안 【직원】의 모든【능력】 2 만큼 하락. 같은 【지형】 혹은 시설에 배치된 【직원】이 전염
    }
}

//인부
namespace EPerson
{
    using EElements;

    //using System;

    [System.Serializable]
    [SerializeField] internal class Person : Produce
    {
        internal const int RdiseasePercent = 50; // 전염 확률
        internal const int RmaxFaceNumber = 1;
        internal Influence master; //자기가 면접보러간 세력
        [SerializeField] internal int masterID;
        internal Slot mySlot;
        [SerializeField] internal int mySlotID = -1;
        [SerializeField] internal int reliAbility;
        private int preliAbility
        {
            get { return reliAbility; }
            set
            {
                if (value < 0) reliAbility = 0;
                else if (value > 25) reliAbility = 25;
                else reliAbility = value;
                //reliAbility = preliAbility;
            }
        } //신뢰도
        [SerializeField] internal int injuryAbility; //부상도

        [SerializeField] internal int enable; //활성화 여부 1이상이면 그 턴만큼 비활성화 비활성화 되었는데 슬롯 안에 있으면 그 슬롯에서 자체적으로 빼고 이벤트 수행해주세요~
        [SerializeField] internal crowdControl crowd; // 어떤 병에 걸렸는지

        [SerializeField] internal int giveMoney; //급여
        [SerializeField] internal int hireMoney; //고용비

        [SerializeField] internal int totalStat; //종합수치
        [SerializeField] internal int countTurn; // 고용된 이후의 턴 
        [SerializeField] internal int[] stat = new int[3]; //스탯
        [SerializeField] internal int property = 0; //직원 특성
        [SerializeField] internal int Bproperty = 0;

        [SerializeField] internal int reserchPoint; //연구포인트 획득량 증가 (백분율 아님)
        [SerializeField] internal int miningBuff; //채광 효율 집중적으로 캐는거 효율 더 증가
        [SerializeField] internal int spyBuff;    //첩보 효율 침투력획득량 증가
        [SerializeField] internal int groundBuff; //지형 자원량 지형에서 나오는 자원량 자체가 증가

        [SerializeField] internal int NesungStack; //내성적인 스택
        [SerializeField] internal int groupStack; //조직적인 스택
        [SerializeField] internal int injuryPercent; //부상확률
        [SerializeField] internal bool bokji; //복지증진 트리거
        [SerializeField] internal bool makeInterview = false; //면접 특성부여 제한
        [SerializeField] internal bool makeproperty = false; //홍보 특성부여 제한
        [SerializeField] internal bool isArbrt = false;

        [SerializeField] internal bool isHire = false;

        [SerializeField] internal Color color = new Color(1,1,1,1);

        [SerializeField] internal int faceGraphic;

        internal Person() { this.elementsMode = Emode.직원; }

        internal int GetAction()
        {
            int result = -1;
            if(mySlot == null)
            {
                if(enable > 0)
                {
                    if(crowd == crowdControl.중상)
                    {
                        result = 3; //중상
                    }
                    else
                    {
                        result = 3; //휴가를 간 상태
                    }
                }
            }
            else
            {
                result = (int)mySlot.useStat;
            }
            Debug.Log(result);
            return result;
        }

        internal void SetFaceGraphic()
        {
            this.faceGraphic = Random.Range(1, RmaxFaceNumber);
        }

        internal void MakePersonIsArbrt() //일하러 온 일용직 생성
        {
            faceGraphic = 0;
            this.hireMoney = 10;
            for (int i = 0; i < 3; i++) this.stat[i] = 30;
            isArbrt = true;
        }

        internal void Hire() //고용할때 수행
        {
            if (master.gold >= this.hireMoney)
            {
                master.gold -= this.hireMoney;
                master.people.Add(this);
                master.hirePeople.Remove(this);
                this.isHire = true;
            }

        }

        [SerializeField] internal void MakePerson(Influence _master)//면접보러온 직원 생성 
        {
            int temp = 0;
            totalStat = Random.Range(30, 301);
            totalStat = totalStat + _master.newComerLevel;
            master = _master;
            preliAbility = (int)personReliStack.보통 + 3;
            bokji = false;
            this.Name = DataBases.nameListF[Random.Range(0, DataBases.nameListF.Length)] + DataBases.nameListL[Random.Range(0, DataBases.nameListL.Length)];
            SetFaceGraphic();
            if (totalStat > 300)
            {
                totalStat = 300;
            }
            if (totalStat < 30)
            {
                totalStat = 30;
            }
            // newComerLevel
            // emploeeCost
            // emploeeCostPercent
            for (int i = 0; i < totalStat; i++)
            {
                stat[Random.Range(0, 3)]++;
            }
            if (totalStat < 50)
            {
                giveMoney = Random.Range(5, 11);
            }
            else if (totalStat < 100)
            {
                giveMoney = Random.Range(15, 26);
            }
            else if (totalStat < 150)
            {
                giveMoney = Random.Range(30, 41);
            }
            else if (totalStat < 200)
            {
                giveMoney = Random.Range(50, 71);
            }
            else if (totalStat < 250)
            {
                giveMoney = Random.Range(80, 101);
            }
            else if (totalStat < 300)
            {
                giveMoney = Random.Range(120, 141);
            }
            else
            {
                giveMoney = Random.Range(200, 301);
            }
            hireMoney = (giveMoney * 2 + _master.emploeeCost) + (((giveMoney * 2 + _master.emploeeCost) / 100) * _master.emploeeCostPercent);
            giveMoney = (giveMoney + _master.emploeePay) + ((giveMoney + _master.emploeePay) / 100 * _master.emploeePayPercent);

            //특성부여부분

            temp = Random.Range(0, 10);
            if (temp == 0)
            {
                temp = Random.Range(0, 18);
                temp = 1 << temp;
                this.property = temp;
                if ((personEffectNameList)temp == personEffectNameList.알뜰한) giveMoney = giveMoney - (int)(giveMoney * 0.3);
                if ((personEffectNameList)temp == personEffectNameList.소박한) hireMoney = hireMoney / 2;
                if ((personEffectNameList)temp == personEffectNameList.베태랑)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] += 10;
                        if (this.stat[i] > 100)
                        {
                            this.stat[i] = 100;
                        }
                    }
                }
                if ((personEffectNameList)temp == personEffectNameList.변덕적인) temp -= (int)personEffectNameList.변덕적인;
                if ((personEffectNameList)temp == personEffectNameList.게으른)
                {
                    miningBuff -= 20;
                    spyBuff -= 20;
                    reserchPoint -= 5;
                }
                if ((personEffectNameList)temp == personEffectNameList.비겁한)
                {
                    reserchPoint -= 3;
                    groundBuff -= 5;
                }
                if ((personEffectNameList)temp == personEffectNameList.낭비가심한)
                {
                    reserchPoint -= 5;
                }
                if ((personEffectNameList)temp == personEffectNameList.전문적인)
                {
                    miningBuff += 20;
                    spyBuff += 20;
                    reserchPoint += 5;
                }
                if ((personEffectNameList)temp == personEffectNameList.성실한)
                {
                    reserchPoint += 3;
                    groundBuff += 5;
                }
                //RcanHireSize
                //RnewComerLevel
            }
            foreach (influenceEffectNameList v in master.myEffect)
            {
                switch (v)
                {
                    case influenceEffectNameList.응원:
                        reliAbilityApply((int)personReliStack.믿음);
                        break;
                }
            }
        } 
        //스탯
        [SerializeField] internal bool injuryApply(int baseP) //부상확률
        {
            int _injury = Random.Range(0, 101);
            int _tInjury = baseP;

            foreach (influenceEffectNameList v in master.myEffect)
            {
                switch (v)
                {
                    case influenceEffectNameList.강압:
                        if ((this.preliAbility / 5) != 4) _tInjury += 10;
                        break;
                    case influenceEffectNameList.당근과채찍:
                        if ((this.preliAbility / 5) != 4) _tInjury += 5;
                        break;
                    case influenceEffectNameList.보안교육:
                        _tInjury -= 10;
                        break;
                    case influenceEffectNameList.제압:
                        _tInjury -= 5;
                        break;
                }
            }

            if (_injury <= _tInjury)
            {
                return true;
            }
            return false;
        }

        [SerializeField] internal bool seriousInjuryApply(int baseP) //중상확률
        {
            int _injury = Random.Range(0, 101);
            int _tInjury = baseP;

            foreach (influenceEffectNameList v in master.myEffect)
            {
                switch (v)
                {
                    case influenceEffectNameList.강압:
                        if ((this.preliAbility / 5) != 4) _tInjury += 10;
                        break;
                    case influenceEffectNameList.당근과채찍:
                        if ((this.preliAbility / 5) != 4) _tInjury += 5;
                        break;
                    case influenceEffectNameList.보안교육:
                        _tInjury -= 10;
                        break;
                    case influenceEffectNameList.제압:
                        _tInjury -= 5;
                        break;
                }
            }

            if (_injury <= _tInjury)
            {
                return true;
            }
            return false;
        }

        internal bool masterGetAddber()
        {
            foreach (influenceEffectNameList v in master.myEffect)
            {
                if (v == influenceEffectNameList.홍보 && !makeproperty) return true;
            }
            return false;
        } //마스터가 홍보를 가지고 있는지

        internal bool masterGetInterview()
        {
            foreach (influenceEffectNameList v in master.myEffect)
            {
                if (v == influenceEffectNameList.면접) return true;
            }
            return false;
        } //마스터가 면접을 가지고있는지

        internal string getProperty()
        {
            string result = "";
            int count = 1;
            int propertyLength = System.Enum.GetValues(typeof(personEffectNameList)).Length;

            if(this.property == 0)
            {
                return "없음";
            }

            for(int i = 0; i < propertyLength; i++)
            {
                if ((this.property & count) == count)
                {
                    result += " ["+ System.Enum.GetName(typeof(personEffectNameList), count) + "]";
                }
                count = count << 1;
            }
            return result;
        }

        internal int getPropertyGoodCount()
        {
            int result = 0;
            int count = 1;
            int propertyLength = System.Enum.GetValues(typeof(personEffectNameList)).Length;

            if (this.property == 0)
            {
                return 0;
            }

            for (int i = 0; i < propertyLength; i++)
            {
                if ((this.property & count) == count)
                {
                    if(count > 512)
                    {
                        result--;
                    }
                    else
                    {
                        result++;
                    }
                }
                count = count << 1;
            }

            return result;
        }
        internal string getReliy()
        {
            int tempReli = this.reliAbility;
            if (tempReli <= 0)
                return " 불신";
            else if (tempReli <= 5)
                return "의심";
            else if (tempReli <= 10)
                return "보통";
            else if (tempReli <= 15)
                return "믿음";
            else if (tempReli <= 20)
                return "신뢰";

            return "??";
        }

        internal string getCrowd() 
        {
            return System.Enum.GetName(typeof(crowdControl), this.crowd);
        }

        internal Color getCrowdColor()
        {
            Color result = new Color(1,1,1,1);

            switch (this.crowd)
            {
                case crowdControl.부상:
                    result = new Color(1f,0.8f,0.8f,1);
                    break;
                case crowdControl.전염병:
                    result = new Color(0.3f, 0.3f, 0.8f,1);
                    break;
                case crowdControl.중상:
                    result = new Color(1f, 0.4f, 0.4f,1);
                    break;
                case crowdControl.질병:
                    result = new Color(0.4f, 0.4f, 0.8f,1);
                    break;
            }
            return result;
        }
        internal void CanHireAd()   // 홍보 수행함수
        {
            int temp;

            foreach (influenceEffectNameList H in master.myEffect)
            {
                switch (H)
                {

                    case influenceEffectNameList.홍보:
                        if (master.gold - hireMoney < 0 && makeproperty)
                        {
                            break;
                        }
                        if (master.gold - hireMoney > 0 && makeproperty == false)
                        {
                            temp = Random.Range(0, 18);
                            temp = 1 << temp;
                            this.property = temp;
                            master.gold -= hireMoney;
                            makeproperty = true;
                        }
                        break;

                }
            }

        }
        [SerializeField] internal void CanHireInterview()  //면접 수행함수
        {
            foreach (influenceEffectNameList H in master.myEffect)
            {
                switch (H)
                {
                    case influenceEffectNameList.면접:
                        if (master.gold - giveMoney < 0)
                        {
                            break;
                        }
                        if (master.gold - giveMoney > 0 && makeInterview == false)
                        {
                            master.gold -= giveMoney;

                            this.stat[(int)personStatCategory.두뇌] += 10;
                            this.stat[(int)personStatCategory.작업능력] += 10;
                            this.stat[(int)personStatCategory.첩보] += 10;
                            makeInterview = true;
                        }
                        break;
                }
            }
        }

        [SerializeField] internal void clearProperty(int value)
        {
            value = property & value;
            if ((value & (int)personEffectNameList.알뜰한) == (int)personEffectNameList.알뜰한) giveMoney = giveMoney / 7 * 10;
            if ((value & (int)personEffectNameList.소박한) == (int)personEffectNameList.소박한) hireMoney = hireMoney * 2;
            if ((value & (int)personEffectNameList.베태랑) == (int)personEffectNameList.베태랑)
            {
                for (int i = 0; i < 3; i++)
                {
                    this.stat[i] = System.Math.Max(0, stat[i] - 10);
                }
            }
            if ((value & (int)personEffectNameList.게으른) == (int)personEffectNameList.게으른)
            {
                miningBuff += 20;
                spyBuff += 20;
                reserchPoint += 5;
            }
            if ((value & (int)personEffectNameList.비겁한) == (int)personEffectNameList.비겁한)
            {
                reserchPoint += 3;
                groundBuff += 5;
            }
            if ((value & (int)personEffectNameList.낭비가심한) == (int)personEffectNameList.낭비가심한)
            {
                reserchPoint += 5;
            }
            if ((value & (int)personEffectNameList.전문적인) == (int)personEffectNameList.전문적인)
            {
                miningBuff -= 20;
                spyBuff -= 20;
                reserchPoint -= 5;
            }
            if ((value & (int)personEffectNameList.성실한) == (int)personEffectNameList.성실한)
            {
                reserchPoint -= 3;
                groundBuff -= 5;
            }

            property -= value;

            if (mySlot != null)
            {
                EventPopSlot(mySlot);
                EventPutSlot(mySlot);
            }
        } //value에 해당하는 특성 제거

        [SerializeField] internal void EventPutSlot(Slot e) //자기 혹은 다른 직원이 해당하는 슬롯에 들어왔을 경우
        {
            //슬롯에 해당 직원이 배치 된 이후에 수행되는 함수
            mySlot = e;
            int _pCount = 0;
            for (int i = 0; i < e.slotUse; i++)
            {
                if (e.slotItem[i] != null) _pCount++;
            }


            if (((int)personEffectNameList.재치있는 & this.property) == (int)personEffectNameList.재치있는)
            {
                if (_pCount >= 3 && (this.Bproperty & (int)personEffectNameList.재치있는) == 0)
                {
                    this.Bproperty += (int)personEffectNameList.재치있는;
                    this.stat[(int)personStatCategory.작업능력] += 10;
                }
            }

            if (((int)personEffectNameList.개인적인 & this.property) == (int)personEffectNameList.개인적인)
            {
                if (_pCount >= 3 && (this.Bproperty & (int)personEffectNameList.개인적인) == 0)
                {
                    this.Bproperty += (int)personEffectNameList.개인적인;
                    this.stat[(int)personStatCategory.작업능력] -= 10;
                }
            }

            if (((int)personEffectNameList.내성적인 & this.property) == (int)personEffectNameList.내성적인)
            {
                if (_pCount >= 2 && this.NesungStack < 2)
                {
                    this.NesungStack++;
                    this.stat[(int)personStatCategory.작업능력] -= 5;
                }
            }

            if (((int)personEffectNameList.무례한 & this.property) == (int)personEffectNameList.무례한)
            {
                for (int i = 0; i < e.slotUse; i++)
                {
                    if ((e.slotItem[i].Bproperty & (int)personEffectNameList.무례한) == 0)
                    {
                        e.slotItem[i].Bproperty += (int)personEffectNameList.무례한;
                        e.slotItem[i].injuryPercent += 5;
                    }
                }
            }

            if (((int)personEffectNameList.조직적인 & this.property) == (int)personEffectNameList.조직적인)
            {
                if (_pCount >= 2 && this.groupStack < 2)
                {
                    groupStack++;
                    this.stat[(int)personStatCategory.작업능력] += 5;
                }
            }

        }
        [SerializeField] internal void EventPopSlot(Slot e) //자기 혹은 다른 직원이 해당하는 슬롯에 빠질 경우
        {
            //슬롯에 해당 직원이 빠지기 전에 수행되는 함수
            mySlot = null;
            int _pCount = -1;
            for (int i = 0; i < e.slotUse; i++)
            {
                if (e.slotItem[i] != null) _pCount++;
            }
            if (((int)personEffectNameList.재치있는 & this.property) == (int)personEffectNameList.재치있는)
            {
                if (_pCount < 3 && (this.Bproperty & (int)personEffectNameList.재치있는) == (int)personEffectNameList.재치있는)
                {
                    this.Bproperty -= (int)personEffectNameList.재치있는;
                    this.stat[(int)personStatCategory.작업능력] -= 10;
                }
            }

            if (((int)personEffectNameList.개인적인 & this.property) == (int)personEffectNameList.개인적인)
            {
                if (_pCount < 3 && (this.Bproperty & (int)personEffectNameList.개인적인) == (int)personEffectNameList.개인적인)
                {
                    this.Bproperty -= (int)personEffectNameList.개인적인;
                    this.stat[(int)personStatCategory.작업능력] += 10;
                }
            }

            if (((int)personEffectNameList.내성적인 & this.property) == (int)personEffectNameList.내성적인)
            {
                if (_pCount >= 2 && this.NesungStack > 0)
                {
                    this.NesungStack--;
                    this.stat[(int)personStatCategory.작업능력] += 5;
                }
            }

            if (((int)personEffectNameList.무례한 & this.property) == (int)personEffectNameList.무례한)
            {
                for (int i = 0; i < e.slotUse; i++)
                {
                    if ((e.slotItem[i].Bproperty & (int)personEffectNameList.무례한) == (int)personEffectNameList.무례한)
                    {
                        e.slotItem[i].Bproperty -= (int)personEffectNameList.무례한;
                        e.slotItem[i].injuryPercent -= 5;
                    }
                }
            }

            if (((int)personEffectNameList.조직적인 & this.property) == (int)personEffectNameList.조직적인)
            {
                if (_pCount >= 2 && this.groupStack > 0)
                {
                    groupStack--;
                    this.stat[(int)personStatCategory.작업능력] -= 5;
                }
            }

        }

        [SerializeField] internal void killSelf() //죽을때
        {
            DataBases.nullIDList.Add(this.ID);
            DataBases.allData[this.ID] = null;
            master.killPerson(this);
        }

        [SerializeField] internal void reliAbilityApply(int _after) //신뢰도를 _after로 변경함
        {
            int _befor = this.preliAbility;
            int bStack = System.Math.Max(1, _befor) / 5;
            int aStack = System.Math.Max(1, _after) / 5;
            int temp;
            if (_befor == _after) return;

            switch (bStack) //전에 가지고 있던 효과를 제거
            {
                case 0: //불신

                    break;
                case 1: //의심
                    temp = bStack - 4;
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] += temp;
                    }
                    break;
                case 2: //보통

                    break;
                case 3: //믿음
                    temp = bStack - 14;
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] -= temp;
                    }
                    break;
                case 4: //신뢰

                    break;
            }

            switch (aStack) //효과 부여
            {
                case 0: //불신
                    clearProperty(1023); //버프 제거
                    break;
                case 1: //의심
                    temp = bStack - 4;
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] -= temp;
                    }
                    break;
                case 2: //보통

                    break;
                case 3: //믿음
                    temp = bStack - 14;
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] += temp;
                    }
                    break;
                case 4: //신뢰
                    clearProperty(131071); //불신 제거
                    break;
            }
        }
        [SerializeField] internal void crowdControlApply()   //상태이상 효과
        {

            switch (crowd)
            {
                case crowdControl.건강함:
                    break;
                case crowdControl.부상:
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] -= 20;
                        if (stat[i] < 0)
                        {
                            stat[i] = 0;
                        }
                    }
                    injuryAbility = 1;
                    break;
                case crowdControl.중상:
                    this.enable = 3;
                    injuryAbility = 3;
                    break;
                case crowdControl.질병:
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] -= 10;
                        if (stat[i] < 0)
                        {
                            stat[i] = 0;
                        }
                    }
                    injuryAbility = 2;
                    break;
                case crowdControl.전염병:
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] -= 20;
                        if (stat[i] < 0)
                        {
                            stat[i] = 0;
                        }
                    }
                    injuryAbility = 5;
                    break;
            }
        }
        [SerializeField] internal void removeCrowd()   // 상태이상 제거
        {
            switch (crowd)
            {
                case crowdControl.건강함:
                    break;
                case crowdControl.부상:
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] += 20;
                        if (stat[i] > 100)
                        {
                            stat[i] = 100;
                        }
                    }

                    break;
                case crowdControl.중상:

                    break;
                case crowdControl.질병:
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] += 10;
                        if (stat[i] > 100)
                        {
                            stat[i] = 100;
                        }
                    }

                    break;
                case crowdControl.전염병:
                    for (int i = 0; i < 3; i++)
                    {
                        this.stat[i] += 20;
                        if (stat[i] > 100)
                        {
                            stat[i] = 100;
                        }
                    }

                    break;
            }
        }

        [SerializeField] internal void turnEnd() //턴이 끝나고 수행 될 함수
        {
            countTurn++;
            if (this.enable > 0)
            {
                this.enable--; //휴가 복귀 턴
            }

            if (this.injuryAbility > 0)
            {
                this.injuryAbility--;
                if (this.injuryAbility == 0)
                {
                    removeCrowd();
                    crowd = crowdControl.건강함;
                }
            }

            if (isArbrt)
            {
                killSelf();
            }
            /*   
           if(System.Math.Max(this.reliAbility,1) / 5 == (int)personReliStack.불신)
           {
               this.reliAbility = (int)personReliStack.의심 + 4;
           }
           if(System.Math.Max(this.reliAbility, 1) / 5 == (int)personReliStack.신뢰)
           {
               this.reliAbility = (int)personReliStack.믿음 + 4;
           }
            */
            if (countTurn % 2 == 0 && ((this.property & (int)personEffectNameList.도벽걸린) == (int)personEffectNameList.도벽걸린))
            {
                master.gold -= 50;
                if (master.gold < 0)
                {
                    master.gold = 0;
                }
            }

            if (countTurn % 4 == 0 && this.enable == 0 && ((this.property & (int)personEffectNameList.변덕적인) == (int)personEffectNameList.변덕적인))
            {
                this.enable = 1;
            }

            if (countTurn % 7 == 0)
            {
                foreach (influenceEffectNameList v in master.myEffect)
                {
                    switch (v)
                    {
                        case influenceEffectNameList.리더쉽:
                            preliAbility = System.Math.Min(preliAbility + 1, 25);
                            break;
                        case influenceEffectNameList.복지증진:
                            if (!this.bokji)
                            {
                                int temp = 0;
                                int id = 0;
                                for (int i = 0; i < 3; i++)
                                {
                                    if (temp < this.stat[i])
                                    {
                                        temp = this.stat[i];
                                        id = i;
                                    }
                                }
                                this.stat[id] += 10;
                                bokji = true;
                            }
                            break;
                    }
                }
            }
            foreach (influenceEffectNameList H in master.myEffect)  // 면접 효과 제거
            {
                switch (H)
                {
                    case influenceEffectNameList.면접:
                        if (master.gold - giveMoney > 0 && makeInterview == true)
                        {
                            this.stat[(int)personStatCategory.두뇌] -= 10;
                            this.stat[(int)personStatCategory.작업능력] -= 10;
                            this.stat[(int)personStatCategory.첩보] -= 10;
                            makeInterview = false;
                        }

                        break;
                }
            }
            //이벤트 턴엔드에서 턴이 끝나고 부상당할 확률 예약
        }

        [SerializeField] internal void turnStart() //턴이 시작할때 수행될 함수
        {
            /*
            if (System.Math.Max(this.reliAbility, 1) / 5 == (int)personReliStack.불신)
            {
                this.reliAbility = (int)personReliStack.의심 + 4;
            }
            if (System.Math.Max(this.reliAbility, 1) / 5 == (int)personReliStack.신뢰)
            {
                this.reliAbility = (int)personReliStack.믿음 + 4;
            }
            */
        }
        [SerializeField] internal float GetSpeed(personStatCategory _stat) //속도계산
        {
            float t = 0.0f;
            int gstat = (int)_stat;

            t = System.Math.Max(this.stat[gstat], 1);

            if ((this.master.emploeeBuff[gstat] < 0)) {
                if((preliAbility / 5) != 4)
                    t += System.Math.Max(this.master.emploeeBuff[gstat], 1);
            }
            else t += System.Math.Max(this.master.emploeeBuff[gstat], 1);
            t = t  / 100 * 90; //10분의 1

            return t;
        }
        [SerializeField] internal override void Producing()
        {
            //생산하는 함수인데... 뭘 생산하는지는 슬롯을 물고있는 땅이나 테크, 첩보가 물고 있는데...
        }

        [SerializeField] internal override void fSave()
        {
            masterID = master.ID;
            if(mySlot == null)
                mySlotID = -1;
            else
                mySlotID = mySlot.ID;
            //saveData = JsonSerializer.Serialize<object>(this);
            //return saveData;
        }

        [SerializeField] internal override void fLoad()
        {
            master = (Influence)DataBases.allData[masterID];
            if (mySlotID == -1)
                mySlot = null;
            else
                mySlot = (Slot)DataBases.allData[mySlotID];
        }
    }

}


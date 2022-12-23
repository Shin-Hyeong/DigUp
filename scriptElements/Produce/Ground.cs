using System.Collections.Generic;
using UnityEngine;
using EElements;
using EProduce;
using ESlot;
using EInfluence;

namespace EElements
{
    //지형의 열거형들
    [SerializeField] internal enum groundType    //땅 형태
    {   
        평지, 산
    }
}
namespace EGround
{
    [System.Serializable]
    [SerializeField] internal class Ground : Produce
    {
        internal Ground() { this.elementsMode = Emode.지형; }

        internal Influence master;
        internal Slot slot;  //현재 땅에서 일하는 인부들

        const int maxResouceCount = 3;  //최대 자원종류 개수
        const int maxGroundLevel = 10;  //최대 땅의 단계

        //속성
        [SerializeField] internal int masterGroundID; //소유 ID
        [SerializeField] internal int slot_ID;
        [SerializeField] internal float time; //게이지 시간
        [SerializeField] internal bool enableGround = false;  //땅의 활성화

        [SerializeField] internal groundType thisGroundType; //현재 땅의 유형
        [SerializeField] internal int thisGroundIndex;  //산 및 평지 타일 고유 인덱스 (산 : 6개, 평지 : 4개)
        [SerializeField] internal int SthisGroundIndex; //저장할 타일 인덱스

        [SerializeField] internal int exploerLevel = 0; //현재 탐사 단계
        [SerializeField] internal int SexploerLevel;    //저장할 탐사 단계


        [SerializeField] internal int nowGroundLevel = 1; //현재 땅의 단계
        [SerializeField] internal int[] SnowGroundLevel;    //저장할 땅의 단계
        [SerializeField] internal int effectMine = 0;   //캐려는 자원의 채광효율 (인덱스)

        [SerializeField] internal List<valueCategory> resouceInGround = new();   //땅에 있는 자원들 3개 


        private int maxGroundTypes = System.Enum.GetValues(typeof(groundType)).Length;  //최대 지형의 형태 개수
        /*위에 있는 속성들에서 저장할 것들
        소유 ID, 땅의 활성화, 현재 땅의 유형, 현재 탐사 단계, 현재 땅의 단계
        */
        [SerializeField] internal List<string> minigameList; //실행할 미니게임의 씬 이름
        //땅을 생성
        internal void CreateGround(int _mode = 0) //지형을 초기
        {

            slot = new Slot();

            slot.useStat = personStatCategory.작업능력;

            //처음일 때 지형은 산이고 자원은 구리랑 철로
            if (_mode == 1)
            {
                SetGroundType(groundType.산);
                InTheResouce(1);
            }
            else
            {
                SetGroundType((groundType)Random.Range(0, maxGroundTypes + 1));
                InTheResouce();
            }
        }
        private void InTheResouce(int _mode = 0)    //땅에 있는 자원들 추가 최대 3개
        {
            //처음에는 구리와 철광석만 나오게
            if (_mode == 1)
            {
                resouceInGround.Add(valueCategory.구리);
                resouceInGround.Add(valueCategory.철광석);
            }
            else
            {
                int addCount = nowGroundLevel >= 3 ? 1 : 0;
                int maxTemp = Random.Range(2, maxResouceCount + addCount);

                //땅에 단계에 따라 자원 인덱스를 추가
                int maxResouceIndex = nowGroundLevel >= 2 ? (int)valueCategory.텅스텐 : (int)valueCategory.은;

                for (int i = 0; i < maxTemp; i++)
                {
                    resouceInGround.Add((valueCategory)Random.Range(0, maxResouceIndex + 1));   //지형에 자원종류를 추가
                    // 중복검사
                    if (i > 0)
                        for (int j = i; j <= i; j++)
                            if (resouceInGround[i - j].Equals(resouceInGround[i]))
                                resouceInGround[i] = (valueCategory)Random.Range(0, maxResouceIndex + 1);
                    if (thisGroundType == groundType.산)
                    {
                        if (resouceInGround.Contains(valueCategory.보크사이트)) { resouceInGround.Remove(valueCategory.보크사이트); i--; }
                        if (resouceInGround.Contains(valueCategory.석유)) { resouceInGround.Remove(valueCategory.석유); i--; }
                        if (resouceInGround.Contains(valueCategory.천연가스)) { resouceInGround.Remove(valueCategory.천연가스); i--; }
                    }
                    else if (thisGroundType == groundType.평지)
                    {
                        if (resouceInGround.Contains(valueCategory.구리)) { resouceInGround.Remove(valueCategory.구리); i--; }
                        if (resouceInGround.Contains(valueCategory.철광석)) { resouceInGround.Remove(valueCategory.철광석); i--; }
                        if (resouceInGround.Contains(valueCategory.석탄)) { resouceInGround.Remove(valueCategory.석탄); i--; }
                        if (resouceInGround.Contains(valueCategory.텅스텐)) { resouceInGround.Remove(valueCategory.텅스텐); i--; }
                    }
                }
            }
        }
        private void SetGroundType(groundType _groundType) //땅의 유형을 지정
        {
            if (_groundType == groundType.산)
            {
                thisGroundType = groundType.산;
                thisGroundIndex = Random.Range(0, 6);   //타일에 표시할 스프라이트 배열에 인덱스를 랜덤으로 가져옴
            }
            else if (_groundType == groundType.평지)
            {
                thisGroundType = groundType.평지;
                thisGroundIndex = Random.Range(0, 4);
            }
        }
        internal int GetResourceProduce(valueCategory _valueCategory) //자원의 생산량 반환
        {
            switch(_valueCategory)
            {
                case valueCategory.구리:
                return exploerLevel >= 0 ? 3 : 0;
                case valueCategory.철광석:
                return exploerLevel >= 0 ? 2 : 0;

                case valueCategory.금:
                return exploerLevel >= 1 ? 1 : 0;
                case valueCategory.은:
                return exploerLevel >= 1 ? 2 : 0;

                case valueCategory.석탄:
                return exploerLevel >= 2 ? 2 : 0;
                case valueCategory.보크사이트:
                return exploerLevel >= 2 ? 1 : 0;

                case valueCategory.석유:
                return exploerLevel >= 3 ? 2 : 0;
                case valueCategory.천연가스:
                return exploerLevel >= 3 ? 1 : 0;

                case valueCategory.텅스텐:
                return exploerLevel >= 4 ? 1 : 0;
                default:
                return 0;
            }
        }
        internal int SetImportantMine() //캐려는 자원을 중심으로 캐기 
        {
            //채광 효율을 현재 땅이 갖고 있는 자원의 인덱스롤 받아옴
            valueCategory getResourceBuff = resouceInGround[effectMine];  //땅에 있는 자원을 받아옴

            int resultResource; //총 자원 생산량
            int produceAlpha = 1;  //추가 생산량 (우선 1로 해놨음)

            int produceDefault = GetResourceProduce(getResourceBuff); //기본 생산량
            int miningBuff = slot.GetBurff()[1];   //채광효율

            resultResource = produceDefault + produceAlpha + (produceDefault * miningBuff);

            float percent = resultResource / 100 * (miningBuff + master.miningSpeedPercent);

            return resultResource + (int)percent;
        }
        internal void SetProducingValue()    //자원을 산출하기
        {
            int[] temp = new int[resouceInGround.Count];

            for (int i = 0; i < resouceInGround.Count; i++)
            {
                //해당 인덱스가 캐려는 인덱스가 적합하면
                if ((int)resouceInGround[i] == effectMine)
                    temp[i] = SetImportantMine();
                else
                    temp[i] = GetResourceProduce(resouceInGround[i]);

                master.resorce[(int)resouceInGround[i]] += temp[i];
            }
        }
        internal override void Producing()
        {
            //생산중으로 게이지를 채우기(?)
            //작업할 때 직원이 캐낸 자원 수(?)
            //게이지 차면 일정확률로 미니게임
        }
        //속성 다 만들고 지정하자
        internal override void fSave()
        {
            Debug.Log(master);
            Debug.Log(ID);
            masterGroundID = master.ID;
            if (slot == null) slot_ID = -1;
            else slot_ID = slot.ID;
            // 탐사 및 땅 단계, 자원요소 저장

            //groundData = JsonSerializer.Serialize<object>(this);
            //return groundData;
        }
        internal override void fLoad()
        {
            if (slot_ID == -1) slot = null;
            else slot = (Slot)DataBases.allData[slot_ID];
            master = (Influence)DataBases.allData[masterGroundID];
        }
    }
}
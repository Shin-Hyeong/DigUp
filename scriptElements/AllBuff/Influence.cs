using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EAllbuff;

using ELeader;
using EPerson;
using EGround;


namespace EElements{
    using EInfluence;

    [SerializeField] internal partial class DataBases
    {
        // static private string influenceJsonData;
        [SerializeField] static internal int influenceLength = 0;
    }

    [SerializeField] internal enum influenceEffectNameList
    {
        암시장,//(버프) : 【시세】가 5%p 증가
        좋지않은시선,// : 첩보활동이 발각시 【관계도】 30% 추가 감소
        무기선호,// : 철광석, 철재(철 관련 자원) 【시세】 5%p 증가, AI 는 【무역】에서 【구매희망】에서 철 관련 자원 출현 빈도 증가.
        금은돌과같다,//금, 은의 【시세】가 15%p 감소
        강압,// : 첩보 효율, 채광 효율 15%p 증가. 부상 및 중상 발생 확률 10%p 증가
        당근과채찍,// : 첩보 효율, 채광 효율 10%p 증가, 부상 및 중상 발생 확률 5%p 증가
        보안교육,// : 부상 및 중상 발생 확률 10%p 감소
        제압,// : 부상 및 중상 발생 확률 5% 감소, 첩보 효율, 채광 효율 10%p 증가
        범죄,// : 1【턴】에 1 번 도둑질 대신 노략질로 변경.
        준비성,// : 【지도자】에게 첩보를 첫 배치시 침투력 10%p 획득
        욕심,// : 도둑질을 통한 보상 증가. 발각시 【관계도】 5%p 추가 감소
        은밀,// : 도둑질을 통한 보상 감소. 발각될 확률 5%p 감소
        산개,// : 1【턴】에 3 명의 【지도자】에게 첩보를 보내면 모든 【지도자】의 침투력10%p 획득
        집중,// : 매【턴】마다 동일 【지도자】에게 첩보활동을 하면 보상 증가. 유지된 【턴】수에 비례해서 보상 증가
        이기적거래,//(버프) : 해당【세력】이 무역을 통한 금&은 이나 보석을 구매시금&은 이나 보석의 【거래 시세】가 N%p 싸지고, 판매시 【거래 시세】가 N%p 비싸짐.
        절약,//(버프) : 각【직원】의 【급여】가 1G 씩 감소.
        멸시,//(디버프) : 철광석, 철재(철 관련 자원)의 【시세】가 10%p 감소.
        홍보,// : 【고용 가능한 직원】의 인원 수 2 증가.
        면접,// : 【고용 가능한 직원】의 【능력】이 총합 수치가 2 증가
        인건비절약,// : 【급여】 70% 감소
        엘리트,// : 같은 【지형】에 배치된 【직원】의 【능력】의 총합 수치가 N 이상일 때 효율 증가
        자원의무기화,//(버프) : 【지도자】가 가지지 않는 자원을 판매 할때 【거래 시세】가 N%p 비싸게 측정.
        연구재료,//(디 버프) : 【지도자】에게 석탄, 석유를 구매 할때 【거래 시세】 N%p 만큼 비싸게 측정.
        원시,// : 6 단계 이후 연구의 필요 【연구포인트】 10%p 감소
        근시,// : 연구 5 단계 이전의 연구 효과 50%p 증가
        최첨단,// : 10 단계 연구 완료후 【연구 포인트】로 【골드】 획득.(행정 10 - 과학【분야】와 중첩가능) 연구포인트 N 당 1G 로 변환
        탑시크릿,// : 행정 8 - 과학【분야】의 고급자원개발에 필요한 【연구 포인트】 N%p 감소
        정치,//(버프) : 다른 【지도자】와의 거래에서 판매시 【거래 시세】가 비싸게, 구매시 【거래 시세】가 싸짐.
        산업지원,// : 【채굴 게이지】가 5% 빨리 참.
        산업보안,// : 방첩 확률 5%p 증가 (기본 방첩 확률 5%p)
        자유시장,// : 판매에 대한 금액에 10%p 추가【골드】 획득
        복지증진,// : 모든【직원】의 가장 높은 【능력】이 1 증가
        외교,// : 관세 1%p 감소
        불합리,// : 상대【지도자】보다 【순위】가 높을 때 【관세】 1%p 감소
        역차별,// : 상대【지도자】보다 【순위】가 낮을 때 【관세】 1%p 감소
        평화협정,// : 다른【지도자】와 서로 시세조작, 생산마비 활동이 불가능해짐.
        거짓평화,// : 다른【지도자】와 도둑질에 필요한 침투력 50 으로 변경
        장인정신,//(버프) : 광석에 대한 판매시 5%p 증가
        최고의품질,//(버프) : 다른【지도자】와 광석을 거래할 때 판매시 【거래 시세】가N%p 비싸게 측정. 
        대량생산,//(버프) : 자원의 생산량 3%p 증가
        뇌근육,//(디버프) : 【직원】의 【두뇌】, 【첩보】 2 감소
        광질,// : 【채굴 게이지】 10%p 빠르게 상승
        응원,// : 【직원】의 상태를 믿음 1 스택를 기본값으로 함.
        요령,// : 채광 효율 20%p 증가
        리더쉽,// : 7 턴 마다 【직원】의 상태를 1 턴동안 믿음 1 스택를 올림.
        탐사,// : 미니게임 발생 확률 5%p 증가, 보석 【시세】 10% 증가
        교활,// : 다른【지도자】의 【관계도】와 침투력 10% 획득
        십일조,// : 매【턴】 마다 【급여】의 10%p 만큼 【골드】 획득
        기도,// : 매【턴】 마다 【급여】의 10%p 만큼 【골드】 추가획득
        맹신,// : 도둑질이후 침투력 20 회복, 발각 확률 5%p 증가
        구원,// : 【직원】의 【능력】감소 상태에 대한 면역
        광신,// : 도둑질로 얻는 보상 50% 증가, 발각 확률 10%p 증가
    }
    [SerializeField] internal enum valueCategory //자원, 무역에서도 사용 가능
    {
        //광물
        구리, 
        철광석,
        금,
        은,
        석탄,
        보크사이트, 
        석유,
        천연가스,
        텅스텐,

        //가공된거
        청동,
        강철,
        알루미늄,
        텅스텐합금,

        //보석
        다이아,
        루비,
        사파이어,
        에메랄드,
        토파즈
    }
    [SerializeField] internal enum influenceInfo //세력정보 무역할 때 참조 가능
    {
        친밀도,
        침투력
    }

    [SerializeField] internal enum leaderNameList
    {
        브라이언, 크루아, 파쿨람, 월렛, 에녹, 하워드, 킴, 제임스, 유진 = 9, 헬리오스 = 11
    }
    [SerializeField] internal enum influenceType
    {
        블랙맘바, 골든코퍼레이션, 지니어스오브투르스, 파얼러멘트, 팀웍리더스, 슬라이인사이티어
    }
}

namespace EInfluence //세력
{
    using EElements;
    using System;

    [System.Serializable]
    [SerializeField] internal class Influence : AllBuff
    {
        internal Influence() { this.elementsMode = Emode.세력; }

        const int RfriendShipEfficiencyPercent = 0; //친밀도 증가 효율
        const int RspyEfficiencyPercent = 0; //침투력 증가 효율 
        const int RourSpyDetectedPercent = 0; //아군 스파이 발각 확률
        const int RenemySpyDetectedPercent = 0; //적 스파이 발각 확률
        const int RresorceTraidBaseQuotePercent = 10;  //판매 거래시세
        const int RotherResorceTraidBaseQuotePercent = 10; //구매 거래시세
        const int RcanHireSize = 10; //고용가능한 직원 수
        const int RnewComerLevel = 0; //고용하러 오는 직원의 수준
        const int RresorceBaseQuotePercent = 0; //창고시세

        const int RemploeeCost = 0; //직원 고용비 증감
        const int RemploeeCostPercent = 0; //직원 고용비 증감(퍼센트)
        const int RemploeePay = 0; //직원 유지비 증감
        const int RemploeePayPercent = 0; //직원 유지비 증감(퍼센트)

        const int RbaseCustomsPercent = 0; //기본관세(퍼센트)
        const int RemploeeBuff = 0; //직원 스텟 버프
        const int RminingSpeedPercent = 0;// 채광 게이지 증가 속도 버프 (퍼센트)
        const int RminingSpeed = 0; //채광 게이지 증가 속도 버프
        const int RresearchSpeedPercent = 0;// 연구 게이지 증가 속도 버프 (퍼센트)
        const int RresearchSpeed = 0; //연구 게이지 증가 속도 버프
        const int RspySpeedPercent = 0;// 첩보 게이지 증가 속도 버프 (퍼센트)
        const int RspySpeed = 0; //첩보 게이지 증가 속도 버프

        const int RgameScore = 10;

        //[SerializeField] internal int SleaderID; //지도자 아이디
        //[SerializeField] internal Leader leader; // 지도자
         internal influenceType influenceName;

        [SerializeField] internal leaderNameList leader;


        //이것들은 숫자로 셀건지?
        internal List<Person> people = new List<Person>(45); //직원
        [SerializeField] internal List<int> SpeopleID = new(45);
        internal List<Person> hirePeople = new List<Person>(10); //고용 목록
        [SerializeField] internal List<int> ShirePeopleID = new(10);
        internal List<Ground> grounds = new List<Ground>(9); //지형
        [SerializeField] internal List<int> SgroundsID = new(9);

        
        [SerializeField] internal List<influenceEffectNameList> myEffect = new List<influenceEffectNameList>();

        //AI 한번 알아야함 어떻게 만들건지

        [SerializeField] internal int[] valueInfo = new int[System.Enum.GetValues(typeof(valueCategory)).Length];

        [SerializeField] internal int[] wantBuy = new int[System.Enum.GetValues(typeof(valueCategory)).Length];
        [SerializeField] internal int[] wantSell = new int[System.Enum.GetValues(typeof(valueCategory)).Length];

        [SerializeField] internal int[] wantBuyOfInfluenceFriends = new int[DataBases.influenceLength]; //사고싶은 세력의 친밀도
        [SerializeField] internal int[] wantSellOfInfluenceFriends = new int[DataBases.influenceLength]; //팔고싶은 세력의 친밀도
        [SerializeField] internal int[] wantBuyOfInfluencePenetration = new int[DataBases.influenceLength]; //사고싶은 세력의 침투력
        [SerializeField] internal int[] wantSellOfInfluencePenetration = new int[DataBases.influenceLength]; //팔고싶은 세력의 침투력

        internal int[,] otherInfluenceInfo = new int[DataBases.influenceLength,2]; //influenceInfo.친밀도 식으로 구분
        [SerializeField] internal int[] SotherInfluenceInfo;

        [SerializeField] internal int tradeCount; //무역 횟수

        [SerializeField] internal int friendShipEfficiencyPercent; //친밀도 증가 효율 (퍼센트)
        [SerializeField] internal int spyEfficiencyPercent; //침투력 증가 효율 (퍼센트)
        [SerializeField] internal int ourSpyDetectedPercent; //아군 스파이 발각 확률 (퍼센트)

        internal int[,] resorceTraidBaseQuotePercent = 
            new int[DataBases.influenceLength, System.Enum.GetValues(typeof(valueCategory)).Length];  //판매 거래시세 (퍼센트)
        [SerializeField] internal int[] SresorceTraidBaseQuotePercent; //저장 시 

        internal int[,] otherResorceTraidBaseQuotePercent = 
            new int[DataBases.influenceLength, System.Enum.GetValues(typeof(valueCategory)).Length];  //구매 거래시세 (퍼센트)
        [SerializeField] internal int[] SotherResorceTraidBaseQuotePercent; //저장 시 

        [SerializeField] internal int canHireSize; //고용가능한 직원 수
        [SerializeField] internal int newComerLevel; //고용하러 오는 직원의 총합
        [SerializeField] internal int[] resorceBaseQuotePercent = new int[System.Enum.GetValues(typeof(valueCategory)).Length]; //기본 창고시세 (퍼센트)

        [SerializeField] internal int emploeeCost; //직원 고용비 증감
        [SerializeField] internal int emploeePay; //직원 유지비 증감
        [SerializeField] internal int emploeeCostPercent; //직원 고용비 증감(퍼센트)
        [SerializeField] internal int emploeePayPercent; //직원 유지비 증감(퍼센트)

        [SerializeField] internal int enemySpyDetectedPercent; //적 스파이 발각 확률 (퍼센트)
        [SerializeField] internal int[] emploeeBuff = new int[3]; //모든 직원 능력치 보너스
        [SerializeField] internal int baseCostomsPercent; //기본관세

        [SerializeField] internal int miningSpeedPercent;// 채광 게이지 증가 속도 버프 (퍼센트)
        [SerializeField] internal int miningSpeed; //채광 게이지 증가 속도 버프
        [SerializeField] internal int researchSpeedPercent;// 연구 게이지 증가 속도 버프 (퍼센트)
        [SerializeField] internal int researchSpeed; //연구 게이지 증가 속도 버프
        [SerializeField] internal int spySpeedPercent;// 첩보 게이지 증가 속도 버프 (퍼센트)
        [SerializeField] internal int spySpeed; //첩보 게이지 증가 속도 버프
        // int buffStack; //효과 스텍

        [SerializeField] internal float gold;//자원
        [SerializeField] internal int techStep; //테크개수
        [SerializeField] internal bool isPlayer = false; //이게 플레이어냐

        [SerializeField] internal int[] resorce = new int[System.Enum.GetValues(typeof(valueCategory)).Length]; //valueCategor y 형식으로 구분
        [SerializeField] internal float ChoicingBase(Influence _targetInfluence) //상대 세력
        {
            //무역으로 가기 전 세력의 친밀도와 침투력을 기반으로 한 기본 시작값을 전달함
            int _friendShip = this.otherInfluenceInfo[ _targetInfluence.ID, (int)influenceInfo.친밀도]; //친밀도
            int _penetrationPoint = this.otherInfluenceInfo[_targetInfluence.ID, (int)influenceInfo.침투력]; //침투력



            return 0f;
        }

        internal void RestHirePeople() //안쓰는 함수 인듯
        {
            this.hirePeople.Clear();
            this.hirePeople.Capacity = canHireSize;
            for(int i = 0; i < canHireSize; i++)
            {
                Person temp = new Person();
                temp.MakePerson(this);
                if(DataBases.nullIDList.Count == 0)
                {
                    temp.ID = DataBases.allData.Count;
                    DataBases.allData.Add(temp);
                }
                else
                {
                    temp.ID = DataBases.nullIDList[0];
                    DataBases.nullIDList.RemoveAt(0);
                    DataBases.allData[temp.ID] = temp;
                }
                this.hirePeople.Add(temp);
            }
        }

        internal override void EffectApply() //효과 정의
        {
            friendShipEfficiencyPercent = RfriendShipEfficiencyPercent; //친밀도 증가 효율 (퍼센트)
            spyEfficiencyPercent = RspyEfficiencyPercent; //침투력 증가 효율 (퍼센트)
            ourSpyDetectedPercent = RourSpyDetectedPercent; //아군 스파이 발각 확률 (퍼센트)
            enemySpyDetectedPercent = RenemySpyDetectedPercent; //적 스파이 발각확률 (퍼센트)
            resorceTraidBaseQuotePercent = new int[DataBases.influenceLength, System.Enum.GetValues(typeof(valueCategory)).Length]; //판매 거래시세 (퍼센트)
            for (int i = 0; i < resorceTraidBaseQuotePercent.GetLength(0); i++)
            {
                for(int j = 0; j < resorceTraidBaseQuotePercent.GetLength(1); j++)
                    resorceTraidBaseQuotePercent[i,j] = RresorceTraidBaseQuotePercent;
            }
            otherResorceTraidBaseQuotePercent = new int[DataBases.influenceLength, System.Enum.GetValues(typeof(valueCategory)).Length]; //판매 거래시세 (퍼센트)
            for (int i = 0; i < otherResorceTraidBaseQuotePercent.GetLength(0); i++)
            {
                for (int j = 0; j < otherResorceTraidBaseQuotePercent.GetLength(1); j++)
                    otherResorceTraidBaseQuotePercent[i, j] = RotherResorceTraidBaseQuotePercent;
            }
            canHireSize = RcanHireSize; //고용가능한 직원 수
            newComerLevel = RnewComerLevel; //고용하러 오는 직원의 총합 
            resorceBaseQuotePercent = new int[System.Enum.GetValues(typeof(valueCategory)).Length]; //기본 창고시세 (퍼센트)
            for (int i = 0; i < resorceBaseQuotePercent.Length; i++)
            {
                resorceBaseQuotePercent[i] = RresorceBaseQuotePercent;
            }
            miningSpeedPercent = RminingSpeedPercent;// 채광 게이지 증가 속도 버프 (퍼센트)
            miningSpeed = RminingSpeed; //채광 게이지 증가 속도 버프
            researchSpeedPercent = RresearchSpeedPercent;// 연구 게이지 증가 속도 버프 (퍼센트)
            researchSpeed = RresearchSpeed; //연구 게이지 증가 속도 버프
            spySpeedPercent = RspySpeedPercent;// 첩보 게이지 증가 속도 버프 (퍼센트)
            spySpeed = RspySpeed; //첩보 게이지 증가 속도 버프


            float[] emploeeBuff = new float[3];
            for(int i = 0; i < emploeeBuff.Length; i++)
            {
                emploeeBuff[i] = RemploeeBuff;
            }
            baseCostomsPercent = RbaseCustomsPercent; //기본관세 (퍼센트)

            emploeeCost = RemploeeCost;
            emploeePay = RemploeePay;
            emploeeCostPercent = RemploeeCostPercent;
            emploeePayPercent = RemploeePayPercent;

            for (int i = 0; i < myEffect.Count; i++)
            {
                switch (myEffect[i])
                {
                    case influenceEffectNameList.암시장:
                        for (int j = 0; j < resorceBaseQuotePercent.Length; j++)
                        {
                            resorceBaseQuotePercent[j] += 5;
                        }
                        break;
                    case influenceEffectNameList.좋지않은시선:
                        //첩보활동이 발각시 【관계도】 30% 추가 감소
                        break;
                    case influenceEffectNameList.무기선호:
                        resorceBaseQuotePercent[(int)valueCategory.철광석] += 5;
                        //resorceBaseQuote[(int)valueCategory.철재] += 5;
                        break;
                    case influenceEffectNameList.금은돌과같다:
                        resorceBaseQuotePercent[(int)valueCategory.금] += 5;
                        resorceBaseQuotePercent[(int)valueCategory.은] += 5;
                        break;
                    case influenceEffectNameList.강압:
                        //첩보 효율, 채광 효율 15%p 증가. 부상 및 중상 발생 확률 10%p 증가
                        //첩보(첩보효율), 인게임(채광효율), 인부(부상 및 중상)측에서 할 것
                        for(int j = 0; j < 3; j++)
                        {
                            emploeeBuff[j] += 20;
                        }
                        break;
                    case influenceEffectNameList.당근과채찍:
                        //첩보 효율, 채광 효율 10%p 증가, 부상 및 중상 발생 확률 5%p 증가
                        break;
                    case influenceEffectNameList.보안교육:
                        //부상 및 중상 발생 확률 10 % p 감소
                        break ;
                    case influenceEffectNameList.제압:
                        //부상 및 중상 발생 확률 5% 감소, 첩보 효율, 채광 효율 10%p 증가
                        break;
                    case influenceEffectNameList.범죄:
                        //1【턴】에 1 번 도둑질 대신 노략질로 변경. (이벤트에서 할 것)
                        emploeeBuff[(int)personStatCategory.첩보] += 30;
                        break;
                    case influenceEffectNameList.준비성:
                        //: 【지도자】에게 첩보를 첫 배치시 침투력 10%p 획득 (첩보에서 할 것)
                        emploeeBuff[(int)personStatCategory.첩보] += 10;
                        break;
                    case influenceEffectNameList.욕심:
                        //도둑질을 통한 보상 증가. 발각시 【관계도】 5%p 추가 감소
                        break;
                    case influenceEffectNameList.은밀:
                        //도둑질을 통한 보상 감소.
                        ourSpyDetectedPercent -= 5; //발각 확률 5%감소
                        break;
                    case influenceEffectNameList.산개:
                        //1【턴】에 3 명의 【지도자】에게 첩보를 보내면 모든 【지도자】의 침투력 10 % p 획득
                        break;
                    case influenceEffectNameList.집중:
                        //매【턴】마다 동일 【지도자】에게 첩보활동을 하면 보상 증가. 유지된 【턴】수에 비례해서 보상 증가
                        break;
                    case influenceEffectNameList.이기적거래:
                        for(int j = 0; j < resorceTraidBaseQuotePercent.GetLength(0); j++)
                        {
                            resorceTraidBaseQuotePercent[j, (int)valueCategory.금] += 5; //N
                            resorceTraidBaseQuotePercent[j, (int)valueCategory.은] += 5; //N
                            resorceTraidBaseQuotePercent[j, (int)valueCategory.루비] += 5; //N
                            //기타 보석들
                        }
                        for(int j = 0; j < otherResorceTraidBaseQuotePercent.GetLength(0); j++)
                        {
                            otherResorceTraidBaseQuotePercent[j, (int)valueCategory.금] += 5; //N
                            otherResorceTraidBaseQuotePercent[j, (int)valueCategory.은] += 5; //N
                            otherResorceTraidBaseQuotePercent[j, (int)valueCategory.루비] += 5; //N
                            //기타 보석들
                        }
                        break;
                    case influenceEffectNameList.절약:
                        emploeePay -= 1; 
                        break;
                    case influenceEffectNameList.멸시:
                        resorceBaseQuotePercent[(int)valueCategory.철광석] += 10;
                        //철제시세
                        break;
                    case influenceEffectNameList.홍보:
                        canHireSize += 2;
                        break;
                    case influenceEffectNameList.면접:
                        newComerLevel += 20;
                        break;
                    case influenceEffectNameList.인건비절약:
                        emploeePayPercent -= 70; 
                        break;
                    case influenceEffectNameList.엘리트:
                        //같은 【지형】에 배치된 【직원】의 【능력】의 총합 수치가 N 이상일 때 효율 증가. (슬롯배치때 판정가능)
                        break;
                    case influenceEffectNameList.자원의무기화:
                        // 【지도자】가 가지지 않는 자원을 판매 할때 【거래 시세】가 N % p 비싸게 측정. (무역 상대측을 알때 판정 가능)
                        break;
                    case influenceEffectNameList.연구재료:
                        //【지도자】에게 석탄, 석유를 구매 할때 【거래 시세】 N % p 만큼 비싸게 측정.
                        for (int j = 0; j < resorceTraidBaseQuotePercent.GetLength(0); j++)
                        {
                            resorceTraidBaseQuotePercent[j, (int)valueCategory.석탄] += 10; //N
                            resorceTraidBaseQuotePercent[j, (int)valueCategory.석유] += 10; //N
                        }
                        break;
                    case influenceEffectNameList.원시:
                        // 6 단계 이후 연구의 필요 【연구포인트】 10%p 감소
                        //테크에서 해결해주세요
                        break;
                    case influenceEffectNameList.근시:
                        //연구 5 단계 이전의 연구 효과 50%p 증가
                        //테크에서 해결해주세요
                        break;
                    case influenceEffectNameList.최첨단:
                        //10 단계 연구 완료후 【연구 포인트】로 【골드】 획득.(행정 10 -  과학【분야】와 중첩가능) 연구포인트 N 당 1G 로 변환
                        //테크에서 기능 구현하고 이걸 기준으로 해제해주세요
                        break;
                    case influenceEffectNameList.탑시크릿:
                        //행정 8 - 과학【분야】의 고급자원개발에 필요한 【연구 포인트】 N % p 감소.
                        //각 테크에 대한 가격설정은 테크에서 해주세용
                        break;
                    case influenceEffectNameList.정치:    //정치(버프) : 다른 【지도자】와의 거래에서 판매시 【거래 시세】가 비싸게, 구매시【거래 시세】가 싸짐
                        break;
                    case influenceEffectNameList.산업지원: //산업지원 :【채굴 게이지】가 5% 빨리 참. -> 채굴게이지 함수 채광쪽일.
                        miningSpeedPercent += 5;
                        emploeeBuff[(int)personStatCategory.작업능력] += 5;
                        break;
                    case influenceEffectNameList.산업보안: //방첩 확률 5%p 증가 (기본 방첩 확률 5%p
                        enemySpyDetectedPercent += 5;
                        break;
                    case influenceEffectNameList.자유시장: //판매에 대한 금액에 10%p 추가【골드】 획득
                        for (int j = 0; j < resorceTraidBaseQuotePercent.GetLength(0); j++)
                        {
                            for(int k = 0; k < resorceTraidBaseQuotePercent.GetLength(1); k++)
                                resorceTraidBaseQuotePercent[j,k] += 10;
                        }
                        for (int j = 0; j < resorceBaseQuotePercent.Length; j++)
                            resorceBaseQuotePercent[j] += 10;
                        break;
                    case influenceEffectNameList.복지증진: // 모든【직원】의 가장 높은 【능력】이 1 증가 -> 능력증가 인부쪽.
                        break;
                    case influenceEffectNameList.외교: //  관세 1%p 감소
                        baseCostomsPercent -= 1;
                        break;
                    case influenceEffectNameList.불합리: // 상대【지도자】보다 【순위】가 높을 때 【관세】 1%p 감소 -> 순위 조건부 
                        // 순위 조건부 : baseCostoms -= 1;
                        break;
                    case influenceEffectNameList.역차별: //  상대【지도자】보다 【순위】가 낮을 때 【관세】 1%p 감소 -> 순위 조건부
                        // 순위 조건부 : baseCostoms -= 1;
                        break;
                    case influenceEffectNameList.평화협정: // 다른【지도자】와 서로 시세조작, 생산마비 활동이 불가능해짐. ?? -> ??
                        break;
                    case influenceEffectNameList.거짓평화: // 다른【지도자】와 도둑질에 필요한 침투력 50 으로 변경 -> 기준이 필요
                        // 침투력 기준점 -> 50으로 변경
                        break;
                    case influenceEffectNameList.장인정신: // 광석에 대한 판매시 5%p 증가
                        for (int j = 0; j < resorceBaseQuotePercent.Length; j++)
                        {
                            resorceBaseQuotePercent[j] += 5;
                        }
                        break;
                    case influenceEffectNameList.최고의품질: // 다른【지도자】와 광석을 거래할 때 판매시 【거래 시세】가 N % p 비싸게 측정.
                        for (int j = 0; j < resorceTraidBaseQuotePercent.GetLength(0); j++)
                        {
                            //resorceTraidBaseQuotePercent[j] += N;
                        }
                        break;
                    case influenceEffectNameList.대량생산: //  자원의 생산량 3%p 증가 -> 게이지가 완료되었을때 나오는 자원의 량 -> 인부, 땅에서 구현
                        break;
                    case influenceEffectNameList.뇌근육: // 【직원】의 【두뇌】, 【첩보】 2 감소 -> 능력증가 인주쪽.
                        emploeeBuff[(int)personStatCategory.두뇌] -= 20;
                        emploeeBuff[(int)personStatCategory.첩보] -= 20;
                        break;
                    case influenceEffectNameList.광질: // 【채굴 게이지】 10%p 빠르게 상승 -> 게이지함수족
                        miningSpeedPercent += 10;
                        break;
                    case influenceEffectNameList.응원: // 【직원】의 상태를 믿음 1 스택를 기본값으로 함. -> 기본값 기준치 필요 1증가
                        break;
                    case influenceEffectNameList.요령: //  채광 효율 20%p 증가 -> 효율적인 자원을 위주로 채굴??
                        emploeeBuff[(int)personStatCategory.작업능력] += 5;
                        break;
                    case influenceEffectNameList.리더쉽: // 7 턴 마다 【직원】의 상태를 1 턴동안 믿음 1 스택를 올림. -> 조건부 스택
                        break;
                    case influenceEffectNameList.탐사: // 미니게임 발생 확률 5%p 증가 -> , 보석 【시세】 10% 증가
                        //미니게임 발생확률 5% 증가 -> 이벤트에서 할지 여기서 할지??
                        for (int j = 0; j < resorceBaseQuotePercent.Length; j++)
                        {
                            resorceBaseQuotePercent[j] += 10;
                        }
                        break;
                    case influenceEffectNameList.교활: //  시작 할 때 다른 모든【지도자】의 【관계도】와 침투력 10% 획득
                        //spyEfficiencyPercent += 10;  //일단 못함
                        break;
                    case influenceEffectNameList.십일조: //  매【턴】 마다 【급여】의 10%p 만큼 【골드】 획득 -> 조건부 급여관련
                        break;
                    case influenceEffectNameList.기도: // 매【턴】 마다 【급여】의 10%p 만큼 【골드】 추가획득 -> 조건부 급여관련
                        break;
                    case influenceEffectNameList.맹신: //도둑질이후 침투력 20 회복 , 발각 확률 5%p 증가  -> 조건부
                        //도둑질 이후 조건부
                        //spyEfficiencyPercent += 20; //이건 증가효율
                        ourSpyDetectedPercent += 5;
                        break;
                    case influenceEffectNameList.구원: // : 【직원】의 【능력】감소 상태에 대한 면역
                        break;
                    case influenceEffectNameList.광신: // 도둑질로 얻는 보상 50% 증가, 발각 확률 10%p 증가
                        //도둑질로 얻는 보상 50%증가
                        ourSpyDetectedPercent += 10;
                        break;
                }
            }
        }

        internal void killPerson(Person person)
        {
            people.Remove(person);
            hirePeople.Remove(person);
            SpeopleID.Remove(person.ID);
            
            //이 이후는 가비지 컬렉터에게 맡긴다.
        }

        internal float GetGameScore() //점수 환산
        {
            float result = 0;
            result = this.gold;
            result += (this.techStep * RgameScore);
            return result;
        }

        internal override void fSave()
        {
            SresorceTraidBaseQuotePercent = DataBases.Make1DArray<int>(resorceTraidBaseQuotePercent);
            SotherResorceTraidBaseQuotePercent = DataBases.Make1DArray<int>(otherResorceTraidBaseQuotePercent);
            SotherInfluenceInfo = DataBases.Make1DArray<int>(otherInfluenceInfo);

            SgroundsID = new List<int>();
            SgroundsID.Capacity = grounds.Count;
            for (int i = 0; i < grounds.Count; i++)
            {
                SgroundsID.Add(grounds[i].ID);
            }
            SpeopleID = new List<int>();
            SpeopleID.Capacity = people.Count;
            for(int i = 0; i < people.Count; i++)
            {
                SpeopleID.Add(people[i].ID);
            }
            ShirePeopleID = new List<int>();
            ShirePeopleID.Capacity = people.Count;
            for(int i = 0; i < hirePeople.Count; i++)
            {
                ShirePeopleID.Add(hirePeople[i].ID);
            }
            //SleaderID = leader.ID;

            //influenceData = JsonSerializer.Serialize<object>(this);
            //Debug.Log(ID);
            //Debug.Log(influenceData);
            //return influenceData;
        }
        internal override void fLoad()
        {
            //JsonUtility.FromJson<Influence>(_loadData);
            //leader = DataBases.leaderData[SleaderID];

            grounds = new List<Ground>();
            int count;
            count = SgroundsID.Count;
            grounds.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                grounds.Add((Ground)DataBases.allData[SgroundsID[i]]); 
            }
            people = new List<Person>();
            count = SpeopleID.Count;
            people.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                people.Add((Person)DataBases.allData[SpeopleID[i]]); 
            }
            count = ShirePeopleID.Count;
            for(int i = 0; i < count; i++)
            {
                hirePeople.Add((Person)DataBases.allData[ShirePeopleID[i]]);
            }
            resorceTraidBaseQuotePercent = 
                DataBases.Make2DArray<int>(SresorceTraidBaseQuotePercent, DataBases.influenceLength, System.Enum.GetValues(typeof(valueCategory)).Length);
            otherInfluenceInfo =
                DataBases.Make2DArray<int>(SotherInfluenceInfo ,DataBases.influenceLength, 2);
            otherResorceTraidBaseQuotePercent =
                DataBases.Make2DArray<int>(SotherResorceTraidBaseQuotePercent, DataBases.influenceLength, System.Enum.GetValues(typeof(valueCategory)).Length);
        }
    }

}
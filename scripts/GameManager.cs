using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using UnityEngine;
using EElements;
using EInfluence;
using EGround;
using EPerson;
using EStorage;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    internal DataBases DB;

    int InfluenceSize = System.Enum.GetValues(typeof(influenceType)).Length;
    int countroll = 0;

    [SerializeField] internal Transform Canvase;

    [SerializeField] internal PersonHireMenuControll PersonHireMenu;
    [SerializeField] internal GameObject PersonIcon;
    [SerializeField] internal GameObject PersonProfile;
    [SerializeField] internal GameObject PersonStat;
    [SerializeField] internal GameInfo gameInfo;
    [SerializeField] internal PersonList personList;
    [SerializeField] internal SellOption sellOption;
    [SerializeField] internal GroundManager groundManager;
    [SerializeField] internal Button managerButton;
    [SerializeField] internal Button gameButton;
    [SerializeField] public List<Sprite> PersonFaceImageList = new(10);
    [SerializeField] public List<Sprite> PersonActionImageList = new(4);
    [SerializeField] public List<Sprite> StorageItemImageList = new List<Sprite>(19);
    [SerializeField] public List<Sprite> MountainTilesList = new List<Sprite>(6);
    [SerializeField] public List<Sprite> FieldTilesList = new List<Sprite>(4);
    [SerializeField] public List<Sprite> MiningItemList = new List<Sprite>(9);

    public static GameManager gameManager;

    public bool isGame = false;

    // Start is called before the first frame update
    void Start()
    {
        DB = new DataBases();
        gameManager = this;
        DontDestroyOnLoad(this.gameObject);
        GameStart(0);
        ManagerPhase();
    }

    [SerializeField] internal void GameStart(int _slot) //게임 시작
    {
        int slot = _slot;
        int influenceTypeLength = System.Enum.GetValues(typeof(influenceType)).Length;
        //Debug.Log(influenceTypeLength);
        List<Influence> newInfluence = new List<Influence>(influenceTypeLength);

        DataBases.allData.Clear();
        DataBases.nullIDList.Clear();

        FileInfo fi = new FileInfo(Application.dataPath + "/save/save" + slot + ".save");

        if (!fi.Exists)  //세이브 파일이 없는경우
        {
            DataBases.turn = 0; //턴 초기화

            //원래는 세력과 지도자 선택을 하던지 랜덤으로 지정해주는 ui생성
            for (int i = 0; i < influenceTypeLength; i++)
            {
                Influence tempInf;
                tempInf = new Influence();

                tempInf.gold = 100000; //초기 골드설정
                tempInf.influenceName = (influenceType)i; //세력 설정
                for(int j = 0; j < 9; j++)
                {
                    Ground tempgd = new Ground();
                    tempgd.master = tempInf;

                    if (j == 4) //가운데땅을 기본초기
                        tempgd.CreateGround(1);
                    else
                        tempgd.CreateGround(0);

                    tempInf.grounds.Add(tempgd);
                    
                }
                tempInf.grounds[4].enableGround = true; //가운데 땅 활성화
                
                {
                    int tempLeader = UnityEngine.Random.Range(i * 2, i * 2 + 2);//세력에 대한 지도자 지정
                    if (tempLeader == 8 || tempLeader == 10) //세력에 해당되는 지도자가 하나라서 만들어진 공백 이름에 들어간 경우 +1
                    {
                        tempLeader++;
                    }
                    tempInf.leader = (leaderNameList)tempLeader;
                }
                newInfluence.Add(tempInf);
            }
            newInfluence[UnityEngine.Random.Range(0, influenceTypeLength)].isPlayer = true; //랜덤으로 한 세력이 플레이어가 됨
            {
                Storage tempst = new Storage(); //창고 생성 창고는 하나만 있으면 됨
                tempst.ResorceBasedvalue();
            }
            //Debug.Log(newInfluence[0].Save());
            DataBases.Save(slot); //세이브 파일 생성
        }

        DataBases.Load(slot); //세이브 파일 로드
    }

    [SerializeField] internal void ManagerPhase() //관리 페이즈 _influe = 관리페이즈에 들어간 세력
    {
        Influence turnInfluence = (Influence)DataBases.allData[countroll*19];

        turnInfluence.EffectApply();

        int hirePCount = turnInfluence.canHireSize;
        Debug.Log(hirePCount);

        //면접보러온 직원 설정
        //turnInfluence.hirePeople.ForEach(v => { v.killSelf(); });
        int hirePeopleCount = turnInfluence.hirePeople.Count;
        for(int i = 0; i < hirePeopleCount; i++)
        {
            turnInfluence.hirePeople[0].killSelf();
        }
        turnInfluence.hirePeople.Clear();
        turnInfluence.ShirePeopleID.Clear();

        for(int i = 0; i < hirePCount; i++)
        {
            Person Ptemp;
            Ptemp = new Person();
            Ptemp.MakePerson(turnInfluence);
            turnInfluence.hirePeople.Add(Ptemp);
        }
        List<Person> arbrtList = new List<Person>(45);
        turnInfluence.people.ForEach(p => { if (p.isArbrt) arbrtList.Add(p); });
        arbrtList.ForEach(p => { p.killSelf(); });

        arbrtList.Clear();

        if (turnInfluence.isPlayer)
        {
            //플레이어인 경우
            PersonHireMenu.master = turnInfluence;
            personList.master = turnInfluence;
            personList.Print();
            gameInfo.me = turnInfluence;

            int temp = turnInfluence.resorce.Length;

            sellOption.Storage = (Storage)DataBases.allData[114]; //창고 설정
            sellOption.Influence = turnInfluence;
            StartCoroutine(sellOption.Born());


            //Debug.Log("히오스좋아");
        }
        else
        {
            StartCoroutine(nextManager());
        }
    }

    [SerializeField]
    public IEnumerator nextManager()
    {
        countroll++;
        if (countroll < InfluenceSize)
            ManagerPhase();
        else
        {
            isGame = true;
            countroll = 0;

            SceneManager.LoadScene("MineScene");
            yield return null;
            gameManager.gameInfo.gameObject.GetComponent<Canvas>().worldCamera
                = GameObject.Find("Main Camera").GetComponent<Camera>();
            managerButton.gameObject.SetActive(false);
            gameButton.gameObject.SetActive(true);
            GamePhase();
        }
        yield break;
    }

    [SerializeField] internal void GamePhase() //게임 페이즈
    {
        //Debug.Log(countroll);
        Influence turnInfluence = (Influence)DataBases.allData[countroll * 19];

        if (turnInfluence.isPlayer)
        {
            for (int i = 0; i < DataBases.allData.Count; i++)
            {
                if (DataBases.allData[i] is Person)
                {
                    Person tempp = (Person)DataBases.allData[i];
                    Debug.Log(tempp.master);
                }
            }
            //플레이어인 경우
            gameInfo.me = turnInfluence;
            //Debug.Log("게임 페이즈 시작");
            groundManager.guiInf = turnInfluence;
            groundManager.SettingGndManager();
            //Debug.Log("히오스좋아");
            //StartCoroutine(nextGame());

        }
        else
        {
            StartCoroutine(nextGame());
        }
    }

    public void NextGameButton()
    {
        StartCoroutine(nextGame());
    }

    public void NextManagerButton()
    {
        StartCoroutine(nextManager());
    }

    [SerializeField]
    public IEnumerator nextGame()
    {
        
        countroll++;
        if (countroll < InfluenceSize)
            GamePhase();
        else
        {
            isGame = false;
            countroll = 0;
            DataBases.Save(0);
            SceneManager.LoadScene("ManageScene");
            managerButton.gameObject.SetActive(true);
            gameButton.gameObject.SetActive(false);
            yield return null;
            Destroy(gameInfo.gameObject);
            Destroy(this.gameObject);
            ManagerPhase();
        }
        yield break;
    }

    public void testTurnUp()
    {
        DataBases.turn++;
        for(int i = 0; i < System.Enum.GetValues(typeof(valueCategory)).Length; i++)
        {
            sellOption.Storage.MarketPriceChagne(i);
        }
        
    }
}
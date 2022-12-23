using System.IO;
using System.Collections.Generic;
using UnityEngine;
using EInfluence;
using EGround;
using ELeader;
using EPerson;
using EProduce;
using EResearch;
using ESlot;
using EStorage;

//요소
namespace EElements
{
    internal partial class DataBases {
        internal static int turn;
        internal static List<Elements> allData = new List<Elements>(100);
        internal static List<int> nullIDList = new List<int>(20);
        internal static bool loading = false;
        public static T[,] Make2DArray<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                    //Debug.Log(output[i, j]);
                }
            }
            return output;
        }
        public static T[] Make1DArray<T>(T[,] input)
        {
            T[] output = new T[input.Length];
            for(int i = 0; i < input.GetLength(0); i++)
            {
                for(int j = 0; j < input.GetLength(1); j++)
                {
                    output[i * input.GetLength(1) + j] = input[i, j];
                }
            }
            return output;
        }

        internal static void Save(int _slot) //저장 번호
        {
            int _dataCount = allData.Count;
            string _saveData = turn+"\nTURNEND\n";
            for(int i = 0; i < _dataCount-1; i++)
            {
                if (allData[i] == null) _saveData = _saveData + "\n";
                else _saveData = _saveData + allData[i].Save() + "\n";
            }
            _saveData = _saveData + allData[_dataCount - 1].Save();

            using (StreamWriter sw = new StreamWriter(Application.dataPath + "/save/save" + _slot + ".save", false, System.Text.Encoding.UTF8))
            {
                sw.Write(_saveData);
            }
        }
        internal static void Load(int _slot)
        {
            //List<Influence> influenceData = new List<Influence>();
            //List<string> leaderData = new List<string>();
            //List<string> groundData = new List<string>();
            //List<string> slotData = new List<string>();
            //List<string> personData = new List<string>();
            loading = true;
            string[] data;
            int _count;
            using(StreamReader sr = new StreamReader(Application.dataPath + "/save/save" + _slot + ".save", System.Text.Encoding.UTF8))
            {
                data = sr.ReadToEnd().Split("\nTURNEND\n");
                turn = int.Parse(data[0]);
                data = data[1].Split('\n');
            }

            DataBases.allData.Clear();
            DataBases.nullIDList.Clear();
            
            _count = data.Length;
            allData.Capacity = _count;
            nullIDList.Capacity = 20;
            for(int i = 0; i < _count; i++)
            {
                Elements temp = null;
                if (data[i] == "")
                {
                    allData.Add(null);
                    continue;
                }
                else
                {
                    //Debug.Log(data[i]);
                    temp = JsonUtility.FromJson<Elements>(data[i]);
                    DataBases.allData.RemoveAt(temp.ID);
                }
                    

                Elements result;
                switch (temp.elementsMode)
                {
                    case Elements.Emode.세력:
                        result = JsonUtility.FromJson<Influence>(data[i]);
                        //allData.Add(result);
                        break;
                    case Elements.Emode.지형:
                        result = JsonUtility.FromJson<Ground>(data[i]);
                        //allData.Add(result);
                        //Debug.Log(JsonUtility.ToJson(allData[allData.Count - 1]));
                        break;
                    case Elements.Emode.슬롯:
                        result = JsonUtility.FromJson<Slot>(data[i]);
                        //allData.Add(result);
                        break;
                    case Elements.Emode.직원:
                        result = JsonUtility.FromJson<Person>(data[i]);
                        //allData.Add(result);
                        break;                    
                    case Elements.Emode.창고:
                        result = JsonUtility.FromJson<Storage>(data[i]);
                        //allData.Add(result);
                        break;
                }
            }
            for(int j = 0; j < allData.Count; j++)
            {
                if(allData[j] != null) allData[j].Load();
            }
            loading = false;
        }
    } //namespace DataBase

    [System.Serializable]
    internal class Elements
    {
        internal Elements()
        {
            //if (!DataBases.loading)
            {
                int lastNull = DataBases.nullIDList.Count;
                if (lastNull > 0)
                {
                    ID = DataBases.nullIDList[lastNull - 1];
                    DataBases.nullIDList.RemoveAt(lastNull - 1);
                    DataBases.allData[ID] = this;
                }
                else
                {
                    ID = DataBases.allData.Count;
                    DataBases.allData.Add(this);
                    //Debug.Log(ID);
                }
                
            }
        }
        internal enum Emode
        {
            세력,
            지도자,
            슬롯,
            지형,
            직원,
            창고
            //등등...
        }
        [SerializeField] internal int ID;
        [SerializeField] internal Emode elementsMode;
        [SerializeField] internal string Name;
        [SerializeField] internal string Story;

        internal virtual void fSave() { }
        internal virtual void fLoad() { }

        internal string Save()
        {
            fSave();
            return JsonUtility.ToJson(this);
        }
        internal void Load()
        {
            fLoad();
        }
    }

    

    internal class UserManage 
    {
        //시세, 자금, 턴, 자원

        void Saving(){
            //json
            //요소 ID값을 받아옴
        }
        void Creating()
        {
            
        }
    }
}
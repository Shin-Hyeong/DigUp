using EContentControll;
using EElements;
using System;

using EInfluence;

namespace EStorage
{
    using EInfluence;
    using ESlot;
    using UnityEngine;

    [System.Serializable]
    internal class Storage : ContentControll
    {
        // 난수를 만들기 위한 선언.
        System.Random rand = new System.Random(unchecked((int)DateTime.Now.Ticks));
        [SerializeField] internal readonly float[] basedResorcePrice = { 10, 15, 30, 20, 30, 40, 40, 60, 80, 30, 30, 60, 140, 300, 200, 100, 100, 50 };
        // 확률 변동을 위한 생 기본시세
        internal float[,] marketPrice = new float[20, System.Enum.GetValues(typeof(valueCategory)).Length];
        // 창고 판매 시세, 2차원 배열인 이유는 그래야 그래프를 그릴 수 있기 때문
        [SerializeField] internal float[] SmarketPrice;
        [SerializeField] internal float[] sellingItemNumber = new float[System.Enum.GetValues(typeof(valueCategory)).Length];
        // 아이템을 판 개수 아이템 시세 변경 메소드에서 사용됨.

        [SerializeField] internal Storage()
        { 
            this.elementsMode = Emode.창고;
        }



        internal override void fLoad()
        {
            //Debug.Log("fLoad 수행" + ID);
            marketPrice = DataBases.Make2DArray<float>(SmarketPrice, 20, System.Enum.GetValues(typeof(valueCategory)).Length);
            rand = new System.Random(unchecked((int)DateTime.Now.Ticks));
            //Debug.Log(marketPrice[0, 0]);
        }

        internal override void Printing() 
        {

        }

        internal override void fSave()
        {
            SmarketPrice = DataBases.Make1DArray<float>(marketPrice);
            //saveData = JsonUtility.ToJson(this);
            //return saveData;
        }



        // 아이템을 판매하는 메소드
        internal float Selling(valueCategory _sellingItem, int sellingNumber, int sellerNum)
        {
            Influence _Seller = (Influence)DataBases.allData[sellerNum];
            float cost = 0;
            // cost 는 아이템을 판매하는 전체 가격을 명시한다.
            
            int nowResorce = 0;
            nowResorce = _Seller.resorce[(int)_sellingItem];
            // nawResorce는 현재 가지고 있는 아이템의 수를 명시한다.


            if (nowResorce >= sellingNumber)
            // 현재 가지고 있는 아이템의 수가 팔 수보다 많거나 같다면
            {
                int tempTrun;
                // 여기에서 판매한 개수를 저장시킨다.
                // 이 변수는 시세 변동 메소드에 사용된다.

                //sellingItemNumber[(int)_sellingItem] += sellingNumber;

                // 여기서 판매한 전체 가격을 cost에 저장한다.
                tempTrun = System.Math.Min(DataBases.turn, 10);



                cost = sellingNumber * (marketPrice[tempTrun, (int)_sellingItem] +
                    marketPrice[tempTrun, (int)_sellingItem] / 100 * _Seller.resorceBaseQuotePercent[(int)_sellingItem]);
                _Seller.resorce[(int)_sellingItem] -= sellingNumber; //Seller의 수량 제거
            }
            else
            {
                cost = 0;
            }
            return cost;
        }



        // 게임을 시작할 때에 초기 값을 marketPrice[첫째턴, 아이템종류]에 저장시키기 위한 메소드
        internal void ResorceBasedvalue()
        {
            for (int _b = 0; _b < System.Enum.GetValues(typeof(valueCategory)).Length; _b++)
            {
                Debug.Log(DataBases.turn+"   "+ _b);
                marketPrice[DataBases.turn, _b] = basedResorcePrice[_b];
            }
        }


        // 시세 변동 메소드
        internal void MarketPriceChagne(int changeNumber)
        {
            int tempTrun = DataBases.turn;
            if (tempTrun > 19)
            {
                tempTrun = 19;
                for (int i = 0; i < 19; i++)
                {
                    marketPrice[i, changeNumber] = marketPrice[i + 1, changeNumber];
                }
            }
            int _changeDecide;
            // 이전턴의 가격을 기본 가격의 백분율을 구한 값을 changeDecide에 저장.
            _changeDecide = (int)(basedResorcePrice[changeNumber] / marketPrice[tempTrun - 1, changeNumber] * 100);

            // a = 1 or -1 으로 저장되어 정해진 변동폭을 더할지 뺄지에 대한 식을 더해주는 함수이다.
            int _a = 1;

            // _chagnePrice 는 변동폭이다.
            int _changePrice = 0;
            // _changePercent는 변동폭을 정하는 난수 값이다.
            int _changePercent = rand.Next(0, 99);

            // changerange 는 변동을 결정하는 난수 값이다.
            int changerange = rand.Next(0, 9);
            Debug.Log("변동폭" + changerange + "변동확률" + _changePercent);
            // 
            if (_changeDecide > (sellingItemNumber[changeNumber] / tempTrun))
            {
                if (changerange > 1)
                {
                    _a = 1;
                }
                else
                    _a = -1;
            }
            if (_changeDecide < (sellingItemNumber[changeNumber] / tempTrun))
            {
                if (changerange > 6)
                {
                    _a = 1;
                }
                else
                    _a = -1;
            }

            // 시세 변동폭

            if (_changePercent < 5)
            {
                _changePrice = 0;
            }
            else if (5 <= _changePercent && _changePercent < 20)
            {
                _changePrice = 1;
            }
            else if (20 <= _changePercent && _changePercent < 45)
            {
                _changePrice = 2;
            }
            else if (45 <= _changePercent && _changePercent < 60)
            {
                _changePrice = 3;
            }
            else if (60 <= _changePercent && _changePercent < 70)
            {
                _changePrice = 4;
            }
            else if (70 <= _changePercent && _changePercent < 80)
            {
                _changePrice = 5;
            }
            else if (80 <= _changePercent && _changePercent < 86)
            {
                _changePrice = 6;
            }
            else if (86 <= _changePercent && _changePercent < 91)
            {
                _changePrice = 7;
            }
            else if (91 <= _changePercent && _changePercent < 94)
            {
                _changePrice = 8;
            }
            else if (94 <= _changePercent && _changePercent < 97)
            {
                _changePrice = 9;
            }
            else if (97 <= _changePercent && _changePercent < 99)
            {
                _changePrice = 10;
            }
            else if (99 <= _changePercent && _changePercent < 100)
            {
                _changePrice = 15;
            }

            for (int _i = 0; _i < sellingItemNumber.Length; _i++)
            {
                sellingItemNumber[_i] = 0;
            }

            


            // marketPrice 현재 턴의 값에 (marketPrice 이전 턴의 값 * 변동 폭)을 저장한다.
            marketPrice[tempTrun, changeNumber] = marketPrice[tempTrun - 1, changeNumber] + ((marketPrice[tempTrun - 1, changeNumber] * _changePrice / 100) * _a);

        }

    }
}

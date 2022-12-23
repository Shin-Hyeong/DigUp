/**********************************
*****************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EStorage;
using EElements;
using UnityEditorInternal;
using System.Diagnostics.CodeAnalysis;

using UnityEngine.UI;
using System;
using System.Data;

public class DrawGraph : MonoBehaviour
{
    internal EStorage.Storage storage;
    float[] Price = new float[50];
    float maxPrice, minPrice;
    int x = System.Math.Min(DataBases.turn, 20);
    public GameObject RedPoint;
    public GameObject BluePoint;
    public Transform PointGroup;
    RectTransform Graph;
    public Color Blue;
    public Color Red;

    int Itemtype;

    List<GameObject> pointList = new List<GameObject>(20);

    private float graph_Width;
    private float graph_Height;

    //public Button detailButton;

    GameObject graph;



    // Start is called before the first frame update
    private void Start()
    {
        Graph = GetComponent<RectTransform>();
        graph = gameObject;
        graph_Width = Graph.rect.width;
        graph_Height = Graph.rect.height;
    }

    public void priceMaxAndMin()
    {
        maxPrice = 0;
        minPrice = 0;

        Debug.Log("x는 시발: " + x);

        for (int i = 0; i < x; i++)
        {
            float v = storage.marketPrice[i, Itemtype];
            Price[i] = v - storage.basedResorcePrice[Itemtype];

            Debug.Log("가격" + storage.marketPrice[i, Itemtype]);
            if (maxPrice < Price[i])
            {
                maxPrice = Price[i];
            }
            if (minPrice > Price[i])
            {
                minPrice = Price[i];
            }

        }
    }


    public void DrawthisGraph()
    {

        int pointMemory = pointList.Count;

        for (int i = 0; i < pointMemory; i++)
        {
            Destroy(pointList[i]);
        }
        pointList.Clear();

        float startPosition = (-graph_Width / 2) + 10;
        // 그래프 영역의 길이 / 2에 -를 붙이면 시작위치가 된다.

        float maxYPosition = graph_Height / 2;
        // 그래프 영역의 높이 / 2 => 점을 찍을 최대 높이
        // ---
        // 격차 컬렉션 생성 및 저장, 최대값 저장...(위에꺼)
        // ---
        Vector3[] pointPosition = new Vector3[20];
        for (int i = 0; i < x; i++)
        {
            // 포인트 오브젝트 생성 및 부모 설정, 각 컴포넌트 가져오기

            if (maxPrice == minPrice)
            {
                pointPosition[i] = new Vector3((startPosition + (i * (graph_Width) / x)), 0, -1);
            }
            else
            {
                pointPosition[i] = minPrice < 0 ? new Vector3((startPosition + (i * (graph_Width) / x)), (((Price[i] - (maxPrice + minPrice) / 2) * (graph_Height - 5)) / (maxPrice - minPrice)), -1)
            : new Vector3((startPosition + (i * (graph_Width) / x)), (((Price[i] - (maxPrice - minPrice) / 2) * (graph_Height - 5)) / (maxPrice - minPrice)), -1);
            }

            Debug.Log("최댓값" + maxPrice + "최솟값" + minPrice);
            Debug.Log("가격" + Price[i]);
            Debug.Log("높이" + graph_Height);

            Debug.Log("턴수" + x + "사용 턴수" + DataBases.turn);



            if (i == 0)
            {
                GameObject point = Instantiate(RedPoint);
                pointList.Add(point);
                point.transform.SetParent(graph.transform, false);
                point.transform.localPosition = pointPosition[i];
                point.GetComponent<PointLine>().value = storage.marketPrice[i, Itemtype];
            }
            else if (i > 0)
            {
                GameObject point = Price[i] >= Price[i - 1] ? Instantiate(RedPoint) : Instantiate(BluePoint);
                point.transform.SetParent(graph.transform, false);
                point.transform.localPosition = pointPosition[i];
                pointList.Add(point);
                point.GetComponent<PointLine>().nextPoint = pointList[i - 1];
                point.GetComponent<PointLine>().value = storage.marketPrice[i, Itemtype];
            }
        }
        pointList.ForEach(v => { v.GetComponent<PointLine>().Print(); });
    }



    public void showthisgraph(int _itemtype)
    {
        Itemtype = _itemtype;
        gameObject.SetActive(true);
        x = System.Math.Min(DataBases.turn, 20);
        priceMaxAndMin();
        DrawthisGraph();
    }
}

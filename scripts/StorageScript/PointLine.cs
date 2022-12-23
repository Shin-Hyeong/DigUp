using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PointLine : MonoBehaviour
{
    LineRenderer lr;
    internal GameObject nextPoint;
    internal float value;

    [SerializeField] internal Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Print()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        if (nextPoint != null)
        {
            lr.SetPosition(0, this.transform.position);
            lr.SetPosition(1, this.nextPoint.transform.position);
        }
    }

    public void show()
    {
        text.text = value + "";
    }
    
     
}

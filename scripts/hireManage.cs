using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hireManage : MonoBehaviour
{
    public string employeeName;
    public int gold, ability, knowledge, spy;

    Text name;

    private void Start()
    {
        name = GetComponent<Text>();

        name.text = employeeName + "\n±Þ¿© : " + gold + "G";

    }
}

abstract class Employee
{
    int ability, knowledge, spy;
}
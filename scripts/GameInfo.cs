using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EElements;

public class GameInfo : MonoBehaviour
{
    [SerializeField] internal Text gold, turn;
    internal EInfluence.Influence me;

    Canvas canvas;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void FixedUpdate()
    {
        if (me != null)
        {
            gold.text = "������: " + me.gold;
            turn.text = DataBases.turn + " ��";
        }
    }

    
}

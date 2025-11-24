using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : EnemyManager
{
    public TextMeshPro ArmyNoTxt;

    void Start() 
    {
        ArmyNoTxt = transform.GetChild(0).GetComponent<TextMeshPro>();

        ArmyNoTxt.text = ArmNo.ToString();
    }
}

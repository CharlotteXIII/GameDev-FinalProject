using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class EnemyManager : MonoBehaviour
{
    public int ArmNo,PlayerArmyNo;
    public SpriteRenderer TerritorySprite;

    void Awake()
    {
        ArmNo = Random.Range(10 , 25);
    }

    public void UnderAttack(TextMeshPro ArmyNoTxt)
    {
       if(ArmNo > 0)
       {
          ArmyNoTxt.text = (ArmNo--).ToString();
       }
       else
       {
          ArmyNoTxt.text = (PlayerArmyNo++).ToString();
       } 

       if(ArmNo == 0)
       {
         // TerritorySprite.color = new Color(0.4f , 0.2f , 0.2f);

         if()
         TerritorySprite.color = new Color(0.35f, 1f, 0f);
         gameObject.GetComponent<SpriteRenderer>().color = new Color(0.45f , 0.95f , 0.7f);
       }


    }

    
}

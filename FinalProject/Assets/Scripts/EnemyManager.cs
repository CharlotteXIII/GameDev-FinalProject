using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class EnemyManager : MonoBehaviour
{
    public int ArmNo,PlayerArmyNo;
    public SpriteRenderer TerritorySprite;

    public string currentOwner = "Neutral"; // เริ่มต้นเป็นกลาง

    void Awake()
    {
        ArmNo = Random.Range(10 , 25);
        TerritorySprite.color = new Color32(255, 255, 255, 255);
    }

   //  public void UnderAttack(TextMeshPro ArmyNoTxt)
   // public void UnderAttack(TextMeshPro ArmyNoTxt, string attackerTeam, Color attackerColor)
   //  {
   //     if(ArmNo > 0)
   //     {
   //        ArmyNoTxt.text = (ArmNo--).ToString();
   //     }
   //     else
   //     {
   //        ArmyNoTxt.text = (PlayerArmyNo++).ToString();
   //     } 

   //     if(ArmNo == 0)
   //     {
   //       // TerritorySprite.color = new Color(0.4f , 0.2f , 0.2f);

   //       // if()
   //       TerritorySprite.color = new Color(0.35f, 1f, 0f);
   //       gameObject.GetComponent<SpriteRenderer>().color = new Color(0.45f , 0.95f , 0.7f);
   //     }
   //  }
   public void UnderAttack(TextMeshPro ArmyNoTxt, string attackerTeam, Color attackerColor)
    {
        // กรณีที่ 1: พวกเดียวกันมาเติมของ (Reinforce)
        if (attackerTeam == currentOwner)
        {
            ArmNo++; 
        }
        // กรณีที่ 2: ศัตรูมาโจมตี (Attack)
        else
        {
            ArmNo--;

            // ถ้าโดนตีจนหมดหลอด (เปลี่ยนเจ้าของ!)
            if (ArmNo < 0)
            {
                currentOwner = attackerTeam;    // เปลี่ยนชื่อเจ้าของใหม่
                TerritorySprite.color = attackerColor; // เปลี่ยนสีพื้นที่
                ArmNo = 1; // เริ่มนับ 1 ใหม่ในฐานะเจ้าของใหม่
            }
        }

        // อัปเดตตัวเลขบนหน้าจอ
        ArmyNoTxt.text = ArmNo.ToString();
    }
}

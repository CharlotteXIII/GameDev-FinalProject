using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier2 : Attack2
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("enemy") && other.gameObject.name == PlayerManager2.Instance.enemy.name)
        {
           other.GetComponent<Enemy>().UnderAttack(other.GetComponent<Enemy>().ArmyNoTxt); 
           gameObject.SetActive(false); 
        }
    }
}

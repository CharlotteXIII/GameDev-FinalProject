using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyManager : MonoBehaviour
{
    public int ArmNo;
    public SpriteRenderer TerritorySprite;

    void Awake()
    {
        ArmNo = Random.Range(10,25);
    }
}

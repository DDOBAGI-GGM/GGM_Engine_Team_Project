using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyEnum
{
    Charging = 0,
    HorizontalArea,
    VerticalArea
}

public class EnemyManager : MonoBehaviour
{
    ChargingMonster chargingMonster;
    HorizontalAreaMonsters horseMonsters;
    VerticalAreaMonsters verticalMonsters;

    private void Awake()
    {
        chargingMonster = GetComponent<ChargingMonster>();
        horseMonsters = GetComponent<HorizontalAreaMonsters>();
        verticalMonsters = GetComponent<VerticalAreaMonsters>();
    }

    public void Enemy(EnemyEnum enemy)
    {
        switch (enemy)
        {
            case EnemyEnum.Charging:
                chargingMonster.ChargeTowardsPlayer();
            break;
            case EnemyEnum.HorizontalArea:
                horseMonsters.Attack();
            break;
            case EnemyEnum.VerticalArea:
                verticalMonsters.Attack();
            break;
        }
    }
}

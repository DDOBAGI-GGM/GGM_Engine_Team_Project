using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyEnum
{
    None = -1,
    Charging = 0,
    HorizontalArea,
    VerticalArea
}

public class EnemyManager : SINGLETON<EnemyManager> 
{
    ChargingMonster chargingMonster;
    HorizontalAreaMonsters horseMonsters;
    VerticalAreaMonsters verticalMonsters;

    private void Awake()
    {
        chargingMonster = FindObjectOfType<ChargingMonster>();
        horseMonsters = FindObjectOfType<HorizontalAreaMonsters>();
        verticalMonsters = FindObjectOfType<VerticalAreaMonsters>();
    }

    public void EnemyAttack(EnemyEnum enemy)
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

    public void EnemyDamage(EnemyEnum enemy)
    {
        switch (enemy)
        {
            case EnemyEnum.Charging:
                chargingMonster.GetDamage();
                break;
            case EnemyEnum.HorizontalArea:
                horseMonsters.GetDamage();
                break;
            case EnemyEnum.VerticalArea:
                verticalMonsters.GetDamage();
                break;
        }
    }
}

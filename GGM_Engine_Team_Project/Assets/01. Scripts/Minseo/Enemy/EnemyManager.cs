using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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

    public int enemyAttackDanamge = 1; 
    

    [SerializeField] private float radius = 2.2f; 

    private void Update()
    {
        RaycastHit2D hit;   
        hit = Physics2D.CircleCast(transform.position, radius, Vector3.forward, 0f);

        if (hit.collider != null)
        {
            if (!hit.collider.CompareTag("Player"))
            {
                // �浹�� ��ü ��������
                GameObject collidedObject = hit.collider.gameObject;

                switch (collidedObject.tag)
                {
                    case "CEnemy":
                        chargingMonster = collidedObject.GetComponent<ChargingMonster>();
                        //Debug.Log(collidedObject.name);
                        break;
                    case "HEnemy":
                        horseMonsters = collidedObject.GetComponent<HorizontalAreaMonsters>();
                        //Debug.Log(collidedObject.name);
                        break;
                    case "VEnemy":
                        verticalMonsters = collidedObject.GetComponent<VerticalAreaMonsters>();
                        //Debug.Log(collidedObject.name);
                        break;
                }
            }

           
        }
    }

    public void EnemyAttack(EnemyEnum enemy)
    {
        Debug.Log(enemy);
        switch (enemy)
        {
            case EnemyEnum.Charging:
                if(chargingMonster != null)
                    chargingMonster.Attack();
            break;
            case EnemyEnum.HorizontalArea:
                if(horseMonsters != null)
                    horseMonsters.Attack();
            break;
            case EnemyEnum.VerticalArea:
                if (verticalMonsters != null)
                    verticalMonsters.Attack();
            break;
        }
    }

    public void EnemyDamage(EnemyEnum enemy)
    {
        Debug.Log(enemy);
        switch (enemy)
        {
            case EnemyEnum.Charging:
                if (chargingMonster != null)
                    chargingMonster.GetDamage();    
                break;
            case EnemyEnum.HorizontalArea:
                if (horseMonsters != null)
                    horseMonsters.GetDamage(enemyAttackDanamge);
                break;
            case EnemyEnum.VerticalArea:
                if (verticalMonsters != null)
                    verticalMonsters.GetDamage(enemyAttackDanamge);
                break;
        }
    }
    public int GetEnemyHp(EnemyEnum enemyType)
    {
        switch (enemyType)
        {
            case EnemyEnum.HorizontalArea:
                if (horseMonsters != null)
                    return horseMonsters.M_HP;
                break;
            case EnemyEnum.VerticalArea:
                if (verticalMonsters != null)
                    return verticalMonsters.M_HP;
                break;
        }
        return 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}

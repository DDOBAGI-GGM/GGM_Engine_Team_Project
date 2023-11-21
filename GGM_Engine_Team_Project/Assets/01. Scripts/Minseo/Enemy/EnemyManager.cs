using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
                // 충돌한 객체 가져오기
                GameObject collidedObject = hit.collider.gameObject;

                switch (collidedObject.tag)
                {
                    case "CEnemy":
                        chargingMonster = collidedObject.GetComponent<ChargingMonster>();
                        Debug.Log(collidedObject.name);
                        break;
                    case "HEnemy":
                        horseMonsters = collidedObject.GetComponent<HorizontalAreaMonsters>();
                        Debug.Log(collidedObject.name);
                        break;
                    case "VEnemy":
                        verticalMonsters = collidedObject.GetComponent<VerticalAreaMonsters>();
                        Debug.Log(collidedObject.name);
                        break;
                }
            }

           
        }
    }

    public void EnemyAttack(EnemyEnum enemy)
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

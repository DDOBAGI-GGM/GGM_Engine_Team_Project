using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    

    [SerializeField] private float radius = 2.0f; 
    private void Update()
    {
        RaycastHit2D hit;   
        hit = Physics2D.CircleCast(transform.position, radius, Vector3.forward, 0f);

        if (hit.collider != null)
        {
            // 충돌한 객체 가져오기
            GameObject collidedObject = hit.collider.gameObject;

            switch (collidedObject.tag)
            {
                case "CEnemy":
                    chargingMonster = collidedObject.GetComponent<ChargingMonster>();
                    Debug.Log("c");
                    break;
                case "HEnemy":
                    horseMonsters = collidedObject.GetComponent<HorizontalAreaMonsters>();
                    Debug.Log("h" + collidedObject.name);
                    break;
                case "VEnemy":
                    verticalMonsters = collidedObject.GetComponent<VerticalAreaMonsters>();
                    break;
            }
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

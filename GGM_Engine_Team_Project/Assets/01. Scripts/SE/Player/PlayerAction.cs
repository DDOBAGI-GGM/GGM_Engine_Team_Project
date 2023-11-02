using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerActionEnum
{
    Jump = 0,
    Attack = 1,
    Avoidance = 2,      // ÇÇÇÏ±â
}

public interface IPlayerAction
{
    void Jump();
    void Attack();
    void Avoidance();
}

public class PlayerAction : MonoBehaviour
{
    private IPlayerAction[] actions = new IPlayerAction[2];

    private void Awake()
    {
        actions[0] =  GetComponent<PlayerMovement>();
        actions[1] = GetComponent<PlayerAnimation>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actions.ToList().ForEach(x => x.Jump());
        }
    }
#endif

    public void action(PlayerActionEnum action)
    {
        switch (action)
        {
            case PlayerActionEnum.Jump:
                actions.ToList().ForEach(x => x.Jump());
                break;
            case PlayerActionEnum.Attack:
                actions.ToList().ForEach(x => x.Attack());
                break;
            case PlayerActionEnum.Avoidance:
                actions.ToList().ForEach(x => x.Avoidance());
                break;
            default:
                break;
        }
    }
}

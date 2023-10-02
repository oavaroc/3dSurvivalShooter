using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter Chase State");
    }

    public override void Exit()
    {
        Debug.Log("Exit Chase State");
    }

    public override void Update()
    {
        Debug.Log("Update Chase State");
        _enemyAI.CalculateAIMovement();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter Attack State");
    }

    public override void Exit()
    {
        Debug.Log("Exit Attack State");
    }

    public override void Update()
    {
        Debug.Log("Update Attack State");
    }

}

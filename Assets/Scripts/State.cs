using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyAI _enemyAI;

    public State(EnemyAI enemyAI)
    {
        this._enemyAI = enemyAI;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

using System;
using UnityEngine;

public class PlayerState : State
{
    protected Player _player => _owner as Player;

    public PlayerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }
}
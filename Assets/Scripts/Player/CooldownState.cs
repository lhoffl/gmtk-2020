using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownState : IState {

    Entity _entity;

    private int _cooldown = 100;

    public void HandleInput(Vector3 position, bool leftMouseClick) {
        if(leftMouseClick) {
            _entity.EnterState(new MovingState());
        }
    }
    
    public void Enter(Entity entity) { 
        _entity = entity;
    }

    public void Exit() {}

    public void Update() {
        _cooldown--;
        _entity.cooldownTracker = _cooldown;
        if(_cooldown <= 0) {
            _entity.cooldownTracker = 0;
            _entity.EnterState(new IdleState());
        }
    }
    
}

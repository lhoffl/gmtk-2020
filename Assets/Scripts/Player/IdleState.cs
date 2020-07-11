using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState {

    Entity _entity;

    public void HandleInput(Vector3 position, bool leftMouseClick) {
        if(leftMouseClick) {
            _entity.EnterState(new MovingState());
        }
    }
    
    public void Enter(Entity entity) { 
        _entity = entity;
    }

    public void Exit() {}

    public void Update() {}
    
}

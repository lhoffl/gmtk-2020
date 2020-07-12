using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {

    void HandleInput(Vector3 position, bool leftMouseClick);
    void Enter(Entity entity);
    void Exit();
    void Update();
}

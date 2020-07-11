using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingState : IState {

    Entity _entity;

    private Vector3 _targetLocation;
    private Vector3 _startLocation;

    private Vector3 _currentTarget;
    private Vector3 _previousTarget;

    private float _distanceThreshold = 0.015f;

    private float _speedModifier = 0.5f;

    public void HandleInput(Vector3 position, bool leftMouseClick) {

    }

    public void Enter(Entity entity) {
        Debug.Log(entity.gameObject.name + " is roaming");
        _entity = entity;
        _startLocation = _entity.transform.position;
        _targetLocation = GetValidateAlternatePoint();

        _currentTarget = _targetLocation;
        _previousTarget = _startLocation;
    }

    public void Exit() {}

    public void Update() {
        Roam();
    }

    private Vector3 GetValidateAlternatePoint() {
        
        Vector3 target = new Vector3(-1, -1, -1);

        while(!GameManager.Instance.ValidPosition(target)) {
            int randomDirection = Random.Range(0,4);
        
            if(randomDirection == 0) {
                target = _startLocation + new Vector3(0, randomDirection, 0);
            }
            if(randomDirection == 1) {
                target = _startLocation + new Vector3(randomDirection, 0, 0);
            }
            if(randomDirection == 2) {
                target = _startLocation + new Vector3(-randomDirection, 0, 0);
            }
            if(randomDirection == 3) {
                target = _startLocation + new Vector3(0, -randomDirection, 0);
            }
        }
        Debug.Log(target);
        return target;
    }

    private void Roam() {

            // If there's still distance to the target location within some threshold
            if(Vector3.Distance(_entity.transform.position, _currentTarget) > _distanceThreshold) {
                Vector3 moveDirection = (_currentTarget - _entity.transform.position).normalized;
                _entity.transform.position = _entity.transform.position + moveDirection * _entity.MovementSpeed * _speedModifier * Time.deltaTime;
            }

            // We've reached the target 
            else {
                
                // snap to the grid
                _entity.transform.position = _currentTarget;
                Vector3 temp = _currentTarget;
                _currentTarget = _previousTarget;
                _previousTarget= temp;
            }
    }
}

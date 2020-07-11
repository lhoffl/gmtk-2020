using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IState {

    Entity _entity;
    private int _movementDirection;

    private Vector3 _location;

    private int _currentPathIndex = 0;
    List<Vector3> _pathVectorList;

    private Vector3 _currentTarget;

    private Vector3 _targetLocation;
    private Vector3 _startLocation;

    private float _distanceThreshold = 0.015f;

    public void HandleInput(Vector3 position, bool leftMouseClick) {

        Debug.Log("Recieved " + position);

        if(_targetLocation != position) {
            _targetLocation = position;
            _pathVectorList = Pathfinding.Instance.FindVectorPath(_startLocation, _targetLocation);
        
            if(_pathVectorList != null && _pathVectorList.Count > 1) {
                _pathVectorList.RemoveAt(0);
            }
        }
    }

    public void Enter(Entity entity) {
        _entity = entity;
        _startLocation = _entity.transform.position;
    }

    public void Exit() {}

    public void Update() {
        Move();
    }

    private void Move() {

        if(_pathVectorList != null) {
            _currentTarget = _pathVectorList[_currentPathIndex];
            
            // If there's still distance to the target location within some threshold
            if(Vector3.Distance(_entity.transform.position, _currentTarget) > _distanceThreshold) {
                Vector3 moveDirection = (_currentTarget - _entity.transform.position).normalized;
                _entity.transform.position = _entity.transform.position + moveDirection * _entity.MovementSpeed * Time.deltaTime;
            }

            // We've reached the target 
            else {
                
                // snap to the grid
                _entity.transform.position = _currentTarget; 
                _currentPathIndex++;
            
                if(_currentPathIndex >= _pathVectorList.Count) {
                    _pathVectorList = null;
                    _entity.EnterState(new IdleState());
                }
            }
        }
        else {
            _entity.EnterState(new IdleState());
        }
    }
}

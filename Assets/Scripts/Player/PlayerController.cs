using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    Entity _currentEntity;

    [SerializeField]
    public List<Entity> _playerEntities;
    
    private bool _leftMouseClick, _rightMouseClick, _space;
    private Vector3 _selectedLocation;

    public static PlayerController Instance { get; private set;}

    void Awake() {

        Instance = this;
        _currentEntity = _playerEntities[0];
        _currentEntity.IsSelected = true;
    }
 
    void Update() {
        ReadInput();
        HandleInput();
        ResetInput();
    }

    private void ReadInput() {

        if(Input.GetMouseButtonDown(0)) {
            _leftMouseClick = true;
        }
        if(Input.GetMouseButtonDown(1)) {
            _rightMouseClick = true;
        }

        if(Input.GetKey(KeyCode.Space)){
            _space = true;
        }

        if(_leftMouseClick || _rightMouseClick || _space) {
            _selectedLocation =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _selectedLocation.x = Mathf.Floor(_selectedLocation.x);
            _selectedLocation.y = Mathf.Floor(_selectedLocation.y);
            _selectedLocation.z = 0;
        }
    }

    private void ResetInput() {
        _leftMouseClick = false;
        _rightMouseClick = false;
        _space = false;
        _selectedLocation = Vector3.zero;
    }

    private void HandleInput() {

        Entity selectedEntity = null;

        if(_rightMouseClick) {
            selectedEntity = RayCastFromMouse();
            if (selectedEntity!= null) {
                _currentEntity = selectedEntity;

                foreach(Entity entity in _playerEntities) {
                    entity.IsSelected = false;
                }
                
                _currentEntity.IsSelected = true;
            }
        }

        if(_leftMouseClick) {
            if(_currentEntity != null) {
                if(!typeof(MovingState).IsInstanceOfType(_currentEntity.CurrentState())) {
                    _currentEntity.EnterState(new MovingState());
                    _currentEntity.HandleInput(_selectedLocation);
                }
            }
        }

        if(_space){
            //selectedEntity = RayCastFromMouse();
            if(_currentEntity != null){
                if(!typeof(MovingState).IsInstanceOfType(_currentEntity.CurrentState())) {
                    if(!typeof(CooldownState).IsInstanceOfType(_currentEntity.CurrentState())) {
                        _currentEntity.EnterState(new AttackState());
                        _currentEntity.HandleInput(_selectedLocation);
                    }
                }
            }
        }
    }

    private Entity RayCastFromMouse() {
        
        Vector2 location2D = new Vector2(_selectedLocation.x, _selectedLocation.y);
        RaycastHit2D[] hitEntities = Physics2D.RaycastAll(location2D, Vector2.zero);

        foreach(RaycastHit2D hit in hitEntities) {
            if(hit.collider != null) {
                if(hit.collider.gameObject.GetComponent<Entity>().IsPlayerEntity) {
                    Debug.Log("Selected Unit: " + hit.collider.gameObject.name);
                    return hit.collider.gameObject.GetComponent<Entity>();
                }
            }
        }

        return null;
    }

    private void CheckForOnePlayerLeft() {
        int count = _playerEntities.Count;
        foreach(Entity entity in _playerEntities) {
            if(!entity.isActiveAndEnabled) {
                count--;
            }
        }
    }
}
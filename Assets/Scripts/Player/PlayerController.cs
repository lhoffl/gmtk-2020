using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    Entity _currentEntity;
    
    private bool _leftMouseClick, _rightMouseClick, _space;
    private Vector3 _selectedLocation;
 
    void Update() {
        ReadInput();
        HandleInput();
        ResetInput();
    }

    private void ReadInput() {

        _selectedLocation =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _selectedLocation.x = Mathf.Floor(_selectedLocation.x);
        _selectedLocation.y = Mathf.Floor(_selectedLocation.y);
        _selectedLocation.z = 0;

        if(Input.GetMouseButtonDown(0)) {
            _leftMouseClick = true;
        }
        if(Input.GetMouseButtonDown(1)) {
            _rightMouseClick = true;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            _selectedLocation.x = Mathf.Floor(_selectedLocation.x);
            _selectedLocation.y = Mathf.Floor(_selectedLocation.y);
            _space = true;
        }

        /*
        if(_leftMouseClick || _rightMouseClick) {
            _selectedLocation =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _selectedLocation.x = Mathf.Floor(_selectedLocation.x);
            _selectedLocation.y = Mathf.Floor(_selectedLocation.y);
            _selectedLocation.z = 0;
            Debug.Log("Selected: " + _selectedLocation);
        }
        */
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
                if (selectedEntity!= null) _currentEntity = selectedEntity;
        }

        if(_leftMouseClick) {
            if(_currentEntity != null) {
                _currentEntity.EnterState(new MovingState());
                _currentEntity.HandleInput(_selectedLocation);
            }
        }

        if(_space){
            selectedEntity = RayCastFromMouse();
            if(selectedEntity == null && _currentEntity != null){
                _currentEntity.EnterState(new AttackState());
                _currentEntity.HandleInput(_selectedLocation);
            }
        }
    }

    private Entity RayCastFromMouse() {
        
        Vector2 location2D = new Vector2(_selectedLocation.x, _selectedLocation.y);
        RaycastHit2D hit = Physics2D.Raycast(location2D, Vector2.zero);

        if(hit.collider != null) {
            Debug.Log("Selected Unit: " + hit.collider.gameObject.name);
            return hit.collider.gameObject.GetComponent<Entity>();
        }

        return null;
    }
}
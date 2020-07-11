using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    
    [SerializeField]
    private int _baseMovementSpeed = 5;
    
    private int _currentMovementSpeed;
    public int MovementSpeed => _currentMovementSpeed;
    
    [SerializeField]
    private IState _currentState;

    private Rigidbody2D _rigidbody;

    private Vector3 _location;

    private bool _leftMouseClick = false;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentMovementSpeed = _baseMovementSpeed;
    }

    private void Start() {
        _currentState = new IdleState();
        _currentState.Enter(this);
    }

    private void Update() {
        ReadInput();
        HandleInput();
        _currentState.Update();
    }

    private void ReadInput() {
        
        if(Input.GetMouseButtonDown(0)) {

            _leftMouseClick = true;
            _location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _location.x = Mathf.Floor(_location.x);
            _location.y = Mathf.Floor(_location.y);
            Debug.Log("Selected: " + _location);
            _location.z = 0;
        }
        else {
            _leftMouseClick = false;
        }
    }

    public void HandleInput() {
        _currentState.HandleInput(_location, _leftMouseClick);
    }

    public void EnterState(IState state) {
        Debug.Log("Entering " + _currentState);
        
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter(this);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    
    [SerializeField]
    private int _baseMovementSpeed = 5;
    public int maxHealth = 10;
    public int currentHealth;

    public int cooldownTracker = 0;

    [SerializeField]
    private bool _isPlayerEntity = true;
    public bool IsPlayerEntity => _isPlayerEntity;
    
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
        currentHealth = maxHealth;
    }

    private void Update() {
        HandleInput(_location);
        _currentState.Update();
    }

    public void HandleInput(Vector3 location) {
        _location = location;
        _currentState.HandleInput(_location, _leftMouseClick);
    }

    public void EnterState(IState state) {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter(this);
    }

    public IState CurrentState() {
        return _currentState;
    }

    public void CheckForDeath(){
        if (currentHealth <= 0) gameObject.active = false;
    }

    public void ModifyHealth(int healthMod){
        currentHealth += healthMod;
        CheckForDeath();
    }
}

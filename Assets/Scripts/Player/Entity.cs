using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    
    [SerializeField]
    private int _baseMovementSpeed = 5;
    public int maxHealth = 10;
    public int currentHealth;

    [SerializeField]
    private bool _isPlayerEntity = true;
    public bool IsPlayerEntity => _isPlayerEntity;
    
    private int _currentMovementSpeed;
    public int MovementSpeed => _currentMovementSpeed;

    [SerializeField]
    private SpellType _type;
    public SpellType Type => _type;

    public SpellType StatusEffect { get; set; }

    private int _statusEffectTimer = 30;
    private int _count = 0;
    
    [SerializeField]
    private IState _currentState;

    private Rigidbody2D _rigidbody;

    private Vector3 _location;

    private bool _leftMouseClick = false;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentMovementSpeed = _baseMovementSpeed;
        StatusEffect = SpellType.None;
    }

    private void Start() {
        _currentState = new IdleState();
        _currentState.Enter(this);
        currentHealth = maxHealth;
    }

    private void Update() {
        HandleInput(_location);
        _currentState.Update();
        TickStatusEffect();
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

    public void TickStatusEffect() {
        if(StatusEffect == SpellType.None) {
            return;
        }

        if(StatusEffect == SpellType.Fire) {
            ModifyHealth(-1);
            _count++;
        }

        if(StatusEffect == SpellType.Ice) {
            _currentMovementSpeed = _baseMovementSpeed / 2;
            _count++;
        }

        if(StatusEffect == SpellType.Healing) {
            ModifyHealth(1);
            _count++;
        }

        if(StatusEffect == SpellType.Lightning) {
            _currentMovementSpeed = _baseMovementSpeed * 2;
            _count++;
        }

        if(_count >= _statusEffectTimer) {
            _count = 0;
            StatusEffect = SpellType.None;
            _currentMovementSpeed = _baseMovementSpeed;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int secondsToLive;
    public float speed;
    public int damage;

    public SpellType Type { get; set; }

    public Entity Caster { get; set; }

    public Color[] color;

    private CircleCollider2D _collider;
    private int _timeAlive;

    private int _criticalDamageModifier = 12;
    private int _normalDamageModifier = 8;
    private int _ineffectiveDamageModifier = 4;

    private Vector3 _previousLocation = new Vector3(10000,10000,10000);

    private ParticleSystem ps;
    private ParticleSystem.ColorOverLifetimeModule colorModule;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable() {
        gameObject.GetComponent<Rigidbody2D>().velocity *= speed;
        _collider = GetComponent<CircleCollider2D>();
        _timeAlive = 0;
        
    }

    void FixedUpdate() {

        if(_timeAlive >= secondsToLive * 95) {
            PlayerSpellPool.Instance.ReturnSpell(this);
        }

        if(Vector3.Distance(transform.position, _previousLocation) < 0.001f) {
            PlayerSpellPool.Instance.ReturnSpell(this);
        }

        _previousLocation = transform.position;

        _timeAlive++;

        color = new Color[2];
        this.SetSpellColorByType();
        Debug.Log("Spell type: " + Type.ToString());
        ps = GetComponentsInChildren<ParticleSystem>()[0];
        colorModule = ps.colorOverLifetime;
        if(ps != null){
            var main = ps.main;
            //main.startColor = this.color;
            colorModule.color = new ParticleSystem.MinMaxGradient(color[0], color[1]);
            GetComponent<SpriteRenderer>().color = this.color[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.GetComponent<Entity>() != Caster) {
            if(other.gameObject.GetComponent<Spell>() == null) {
                Entity hitEntity = other.gameObject.GetComponent<Entity>();

                if (hitEntity != null) {
                    hitEntity.ModifyHealth((damage * -1 * GetDamageModifier(hitEntity.Type)));
                    hitEntity.StatusEffect = this.Type;
                    //Debug.Log("Spell cast by " + Caster + " hit " + other.gameObject.name + " for " + damage * -1 * GetDamageModifier(hitEntity.Type));
                    PlayerSpellPool.Instance.ReturnSpell(this);
                }
            }
        }
    }

    private int GetDamageModifier(SpellType type) {

        int damageMod = _normalDamageModifier;

        if(GameManager.Instance.OnePlayerRemaining) {
            return _criticalDamageModifier * _criticalDamageModifier;
        }

        switch (type) {
            case SpellType.Fire:
                if(this.Type == SpellType.Ice) {
                    return _ineffectiveDamageModifier;
                }
                if(this.Type == SpellType.Lightning) {
                    return _criticalDamageModifier;
                }
                break;
            case SpellType.Ice:
                if(this.Type == SpellType.Fire) {
                    return _criticalDamageModifier;    
                }
                if(this.Type == SpellType.Lightning) {
                    return _ineffectiveDamageModifier;
                }
                break;
            case SpellType.Lightning:
                if(this.Type == SpellType.Ice) {
                    return _criticalDamageModifier;
                }
                if(this.Type == SpellType.Fire) {
                    return _ineffectiveDamageModifier;
                }
                break;
            case SpellType.Healing:
                if(this.Type == SpellType.Healing) {
                    return _normalDamageModifier * -1;
                }
                return _normalDamageModifier;
        }

        return damageMod;
    }
    
    public void SetSpellColorByType(){
        color[0] = Color.magenta;
        color[1] = Color.white;

        switch (Type)
        {
            case SpellType.Fire:
                this.color[0] = Color.red;
                this.color[1] = Color.yellow;
                break;
            case SpellType.Ice:
                this.color[0] = Color.cyan;
                this.color[1] = Color.white;
                break;
            case SpellType.Healing:
                this.color[0] = Color.green;
                this.color[1] = Color.yellow;
                break;
            case SpellType.Lightning:
                this.color[0] = Color.yellow;
                this.color[1] = Color.white;
                break;
        }
    }
}

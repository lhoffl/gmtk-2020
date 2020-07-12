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

    private CircleCollider2D _collider;
    private int _timeAlive;

    private int _criticalDamageModifier = 4;
    private int _normalDamageModifier = 2;
    private int _ineffectiveDamageModifier = 1;

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

        if(_timeAlive >= secondsToLive * 10) {
            PlayerSpellPool.Instance.ReturnSpell(this);
        }

        _timeAlive++;
    }

   private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.GetComponent<Entity>() != Caster) {
            if(other.gameObject.GetComponent<Spell>() == null) {
                Entity hitEntity = other.gameObject.GetComponent<Entity>();

                if (hitEntity != null) {
                    hitEntity.ModifyHealth((damage * -1 * GetDamageModifier(hitEntity.Type)));
                    hitEntity.StatusEffect = this.Type;
                    Debug.Log("Spell cast by " + Caster + " hit " + other.gameObject.name + " for " + damage * -1 * GetDamageModifier(hitEntity.Type));
                    PlayerSpellPool.Instance.ReturnSpell(this);
                }
            }
        }
   }

   private int GetDamageModifier(SpellType type) {

        if(type == this.Type) {
            return _normalDamageModifier;
        }

        int damageMod = _normalDamageModifier;

        switch (type) {
            case SpellType.Fire:
                if(this.Type == SpellType.Ice) {
                    return _ineffectiveDamageModifier;
                }
                break;
            case SpellType.Ice:
                if(this.Type == SpellType.Fire) {
                    return _criticalDamageModifier;    
                }
                break;
            case SpellType.Healing:
                return _normalDamageModifier * -1;
        }

        return damageMod;
   }
    
}

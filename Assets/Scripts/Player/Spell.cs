using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public int secondsToLive;
    public float speed;
    public int damage;

    public Entity Caster { get; set; }

    private CircleCollider2D _collider;
    private int _timeAlive;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable() {
        gameObject.GetComponent<Rigidbody2D>().velocity *= speed;
        _collider = GetComponent<CircleCollider2D>();
        _timeAlive = 0;
    }

    void Update() {

        if(_timeAlive >= secondsToLive * 360) {
            PlayerSpellPool.Instance.ReturnSpell(this);
        }

        _timeAlive++;
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log("Projectile collided with: " + collision);
        Destroy(gameObject);
        if(other.gameObject.GetComponent<Entity>() != Caster) {
            if(other.gameObject.GetComponent<Spell>() == null) {
                Debug.Log("Spell cast by " + Caster + " hit " + other.gameObject.name);
                Entity hitEntity = other.gameObject.GetComponent<Entity>();
                if (hitEntity != null) hitEntity.ModifyHealth((damage * -1));
                PlayerSpellPool.Instance.ReturnSpell(this);
            }
        }
    }
}

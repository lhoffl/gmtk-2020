using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public int secondsToLive;
    public float speed;

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

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.GetComponent<Entity>() != Caster) {
            if(other.gameObject.GetComponent<Spell>() == null) {
                Debug.Log("Spell cast by " + Caster + " hit " + other.gameObject.name);
                PlayerSpellPool.Instance.ReturnSpell(this);
            }
        }
    }
}

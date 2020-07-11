using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public int secondsToLive;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity *= speed;
        DestroyObjectDelayed();
    }

    void DestroyObjectDelayed()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, secondsToLive);
    }

    // Update is called once per frame
    void Update()
    {
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

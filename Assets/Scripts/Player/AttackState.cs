using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    Entity _entity;

    float cooldownTime = 3.0f;

    public void HandleInput(Vector3 position, bool leftMouseClick) {
        shoot(position);
    }
    
    public void Enter(Entity entity) { 
        _entity = entity;
    }

    public void Exit() {
        //After attacking a character cools down
        //_entity.StartCoroutine(Cooldown());  
    }

    public void Update() {

    }

    private void shoot(Vector3 position){
        var heading = (position - new Vector3(_entity.transform.position.x, _entity.transform.position.y, 0));
        var direction = heading / heading.magnitude;
        direction.Normalize();
        float rotationZ = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
        /*
        var distance = heading.magnitude;
        var direction = heading / distance;
        */
        Debug.Log("Shooting: " + heading);
        GameObject projectile = (GameObject)Object.Instantiate(_entity.spell);
        projectile.transform.position = _entity.transform.GetComponent<Renderer>().bounds.center;
        projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        projectile.GetComponent<Rigidbody2D>().velocity = direction; 
        _entity.EnterState(new IdleState());
    }
    
    private IEnumerator Cooldown(){
            yield return new WaitForSeconds(cooldownTime);
            Debug.Log("Cooldown finished");
        }
}

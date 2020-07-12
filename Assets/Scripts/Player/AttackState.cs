using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    Entity _entity;

    public void HandleInput(Vector3 position, bool leftMouseClick) {
        shoot(position);
    }
    
    public void Enter(Entity entity) { 
        _entity = entity;
    }

    public void Exit() {
    }

    public void Update() {

    }

    private void shoot(Vector3 position){
        var heading = (position - new Vector3(_entity.transform.position.x, _entity.transform.position.y, 0));
        var direction = heading / heading.magnitude;
        direction.Normalize();
        float rotationZ = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;

        Spell projectile = PlayerSpellPool.Instance.GetSpell();
        if(projectile != null) {
            projectile.gameObject.SetActive(true);
            projectile.Type = _entity.Type;
            projectile.transform.position = _entity.transform.GetComponent<Renderer>().bounds.center;
            projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * 5; 
            projectile.Caster = _entity;
        }

        _entity.EnterState(new CooldownState());
    }
}

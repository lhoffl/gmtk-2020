using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    [SerializeField]
    public List<Entity> _enemyList;

    [SerializeField]
    private float _enemySightRange = 3f;

    void Update() {
        foreach(Entity enemy in _enemyList) {

                Entity playerEntity = FindClosestPlayerEntity(enemy);

                if(playerEntity != null) {
                    MoveToEntity(enemy, playerEntity);
                    AttackEntity(enemy, playerEntity);
                }
                else {

                    if(!typeof(RoamingState).IsInstanceOfType(enemy.CurrentState())) {
                        enemy.EnterState(new RoamingState());
                    }
                }
            }
    }

    private Entity FindClosestPlayerEntity(Entity enemy) {

        Vector3 enemyPosition = enemy.transform.position;
        Vector2 enemyPosition2D = new Vector2(enemyPosition.x, enemyPosition.y);
        
        Collider2D[] results = Physics2D.OverlapCircleAll(enemyPosition2D, _enemySightRange);

        Entity closestPlayerEntity = null;
        float closestPlayerDistance = float.MaxValue;

        foreach(Collider2D collider in results) {
            if(collider.gameObject.GetComponent<Entity>() != null) {
                Entity hitEntity = collider.gameObject.GetComponent<Entity>();

                if(hitEntity.IsPlayerEntity) {
                    
                    float distance = Vector3.Distance(hitEntity.transform.position, enemyPosition);
                    if(distance < closestPlayerDistance) {
                        closestPlayerDistance = distance;
                        closestPlayerEntity = hitEntity;
                    }
                }
            }
        }

        return closestPlayerEntity;
    }

    private void MoveToEntity(Entity enemy, Entity entity) {

        if(entity != null) {
        
            int randomOffset = Random.Range(1, 3);
            int xOffset = Random.Range(0,2);
            int sign = Random.Range(0,2);
            
            Vector3 offsetVector;

            if(xOffset != 0) {
                offsetVector = new Vector3(0, randomOffset, 0);
            } else {
                offsetVector = new Vector3(randomOffset, 0, 0);
            }

            if(sign != 0) {
                offsetVector = -offsetVector;
            }

            if(!typeof(CooldownState).IsInstanceOfType(enemy.CurrentState())) {
                if(!typeof(MovingState).IsInstanceOfType(enemy.CurrentState()))
                    enemy.EnterState(new MovingState());
            }
            
            Vector3 targetLocation = entity.transform.position + offsetVector;
            if(GameManager.Instance.ValidPosition(targetLocation)) {
                enemy.HandleInput(entity.transform.position + offsetVector);
            }
            else {
                enemy.HandleInput(enemy.transform.position);
            }
        } 
        else {
            enemy.EnterState(new RoamingState());
        }
    }

    private void AttackEntity(Entity enemy, Entity entity) {
    
        if(Vector3.Distance(enemy.transform.position, entity.transform.position) <= _enemySightRange / 2) {
            if(!typeof(CooldownState).IsInstanceOfType(enemy.CurrentState()))
                enemy.EnterState(new AttackState());
        }
    }
}

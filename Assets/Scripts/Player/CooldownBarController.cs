using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBarController : MonoBehaviour
{
    Vector3 localScale;

    Entity _entity;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        _entity = transform.parent.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = (float)_entity.cooldownTracker / 100;
        transform.localScale = localScale;
    }
}

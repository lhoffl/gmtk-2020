using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{


    public float duration = 1f;
    public float range_mod = 50;
    private float amplitude = 0;
    
    // Update is called once per frame
    void Update()
    {
        ListAimlessly();
    }

    void changePos(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }

    void ListAimlessly()
    {
        float phi = Time.time / duration * 2;// * Mathf.PI;
        amplitude = Mathf.Sin(phi);
        changePos(0, (amplitude/range_mod));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public MapInfo map;

    void OnMouseUp(){
        Debug.Log("Clicked " + "X: " + tileX + ", Y: " + tileY + "!");

        map.GeneratePathTo(tileX, tileY);

    }
}

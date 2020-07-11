using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingTerrain : MonoBehaviour
{
    
    private Pathfinding _pathFinding;
    Grid<GameObject> _objectGrid;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldCoordinates = gameObject.transform.position;
        Vector2Int gridCoordinates = _pathFinding.GetGrid().GetGridCoordinates(worldCoordinates);
        PathNode nodeAtLocation = _pathFinding.GetNode(gridCoordinates.x, gridCoordinates.y);
        nodeAtLocation.SetIsWalkable(false);
    }
}

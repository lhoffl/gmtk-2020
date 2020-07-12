using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateWalls : MonoBehaviour {

    [SerializeField]
    public Tilemap _walls, _floors;

    Grid<GameObject> _objectGrid;
    private Pathfinding _pathFinding;

    private void Start() {

        Debug.Log(_walls.cellBounds);
        Debug.Log(_floors.cellBounds);

        _objectGrid = new Grid<GameObject>(_walls.cellBounds.xMax, _walls.cellBounds.yMax, Vector3.zero, 1, (Grid<GameObject> grid, int x, int y) => null);
        _pathFinding = new Pathfinding(_walls.cellBounds.xMax, _walls.cellBounds.yMax);

        BoundsInt wallBounds = _walls.cellBounds;
        for(int x = 0; x < wallBounds.xMax; x++) {
            for(int y = 0; y < wallBounds.yMax; y++) {
                if(_walls.GetTile(new Vector3Int(x,y,0))) {
                    PathNode nodeAtLocation = _pathFinding.GetNode(x,y);
                    nodeAtLocation.SetIsWalkable(false);
                    Debug.Log(x + ", " + y);
                }
            }

        }
    }

}

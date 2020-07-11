using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public TileType[] tileTypes;
    public int mapSizeX = 10;
    public int mapSizeY = 10;
    // Start is called before the first frame update Grid<GameObject> _objectGrid;
    Grid<GameObject> _objectGrid;
    private Pathfinding _pathFinding;

    //[SerializeField]
    //private GameObject _box;

    // Start is called before the first frame update
    void Start()
    {
        _objectGrid = new Grid<GameObject>(mapSizeX,mapSizeY, Vector3.zero, 1, (Grid<GameObject> grid, int x, int y) => null);
        _pathFinding = new Pathfinding(mapSizeX, mapSizeY);

        BasicMap();
        BuildMap();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridCoordinates = _objectGrid.GetGridCoordinates(worldCoordinates);

            GameObject box = Instantiate(_box, new Vector3(gridCoordinates.x + 1, gridCoordinates.y, 0), Quaternion.identity);
            _objectGrid.SetValue(worldCoordinates, box);

            gridCoordinates = _pathFinding.GetGrid().GetGridCoordinates(worldCoordinates); 
            PathNode nodeAtLocation = _pathFinding.GetNode(gridCoordinates.x, gridCoordinates.y);

            nodeAtLocation.SetIsWalkable(!nodeAtLocation.isWalkable);
            
        }*/
    }

    void BuildMap(){
        for (int x = 0; x < mapSizeX; x++){
            for (int y = 0; y < mapSizeY; y++){
                Instantiate(tileTypes[_objectGrid.GetTileType(x,y)].tileVisualPrefab, new Vector3(x, y, .1f), Quaternion.identity);
                PathNode nodeAtLocation = _pathFinding.GetNode(x,y);
                nodeAtLocation.SetIsWalkable(tileTypes[_objectGrid.GetTileType(x,y)].passable);
            }
        }
    }

    void BasicMap(){
        //Basic walls, diamond in center
        for (int x = 0; x < mapSizeX; x++){
            for (int y = 0; y < mapSizeY; y++){
                if (x == 0 || x == (mapSizeX - 1) || y == 0 || y == (mapSizeY - 1)){
                    _objectGrid.SetTileType(x,y,1);
                    //Debug.Log("X: " + x + ", Y: " + y + ", Type: 1");
                }
                else{
                    _objectGrid.SetTileType(x,y,0);
                    //Debug.Log("X: " + x + ", Y: " + y + ", Type: 0");
                }

                if (x == 4 && y == 4) _objectGrid.SetTileType(x,y,5);
                if (x == 4 && y == 5) _objectGrid.SetTileType(x,y,3);
                if (x == 5 && y == 4) _objectGrid.SetTileType(x,y,4);
                if (x == 5 && y == 5) _objectGrid.SetTileType(x,y,2);
            }
        }
        
    }

}
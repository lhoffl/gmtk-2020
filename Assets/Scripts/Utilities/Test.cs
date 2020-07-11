using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    Grid<GameObject> _objectGrid;
    private Pathfinding _pathFinding;

    [SerializeField]
    private GameObject _box;

    // Start is called before the first frame update
    void Start()
    {
       _objectGrid = GameManager.Instance.GetObjectGrid();
       _pathFinding = new Pathfinding(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridCoordinates = _objectGrid.GetGridCoordinates(worldCoordinates);

            GameObject box = Instantiate(_box, new Vector3(gridCoordinates.x + 1, gridCoordinates.y, 0), Quaternion.identity);
            _objectGrid.SetValue(worldCoordinates, box);

            gridCoordinates = _pathFinding.GetGrid().GetGridCoordinates(worldCoordinates); 
            PathNode nodeAtLocation = _pathFinding.GetNode(gridCoordinates.x, gridCoordinates.y);

            nodeAtLocation.SetIsWalkable(!nodeAtLocation.isWalkable);
        }
    }
}

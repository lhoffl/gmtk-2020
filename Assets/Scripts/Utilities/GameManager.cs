using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    private int _width, _height, _cellSize;

    private Grid<GameObject> _objectGrid;
    private Grid<GameObject> _backgroundObjectGrid;
    private Grid<Entity> _entityGrid;

    public static GameManager Instance { get; private set; }

    public bool OnePlayerRemaining { get; set; }

    void Awake() {
        Instance = this;

        OnePlayerRemaining = false;

        _objectGrid = new Grid<GameObject>(_width, _height, Vector3.zero, _cellSize, 
            (Grid<GameObject> grid, int x, int y) => null);
        
        _backgroundObjectGrid = new Grid<GameObject>(_width, _height, Vector3.zero, _cellSize, 
            (Grid<GameObject> grid, int x, int y) => null);
        
        _entityGrid = new Grid<Entity>(_width, _height, Vector3.zero, _cellSize, 
            (Grid<Entity> grid, int x, int y) => null);
    }

    void Start() {

    }

    void Update() {
        
    }

    public Grid<GameObject> GetObjectGrid() {
        return _objectGrid;
    }

    public bool ValidPosition(Vector3 position) {
        if(position.x < 0 || position.x > _width) {
            return false;
        }

        if(position.y < 0 || position.y > _height) {
            return false;
        }

        return true;
    }
}

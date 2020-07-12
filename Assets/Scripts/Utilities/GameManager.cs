using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine; using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    private int _width, _height, _cellSize;

    private Grid<GameObject> _objectGrid;
    private Grid<GameObject> _backgroundObjectGrid;
    private Grid<Entity> _entityGrid;

    public static GameManager Instance { get; private set; }

    public int PlayersRemaining { get; set; }
    public int EnemiesRemaining { get; set; }

    void Awake() {
        Instance = this;

        PlayersRemaining = PlayerController.Instance._playerEntities.Count;
        EnemiesRemaining = EnemyController.Instance._enemyList.Count;

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
        if(IsGameOver()) {
            Debug.Log("You lost");
            SceneManager.LoadScene("GameOver-Lose"); 
        }

        if(IsGameWon()) {
            Debug.Log("you winrar");
            SceneManager.LoadScene("GameOver-Win"); 
        }
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

    public bool IsGameOver() {
        return (PlayersRemaining <= 0);
    }

    public bool IsGameWon() {
        return (EnemiesRemaining <= 0);
    }
}

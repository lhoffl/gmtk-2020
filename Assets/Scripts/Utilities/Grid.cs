using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T> {

    private int _width;
    private int _height;

    private int _cellSize;

    private T[,] _grid;

    private Vector3 _origin;

    public Grid(int width, int height, Vector3 origin, int cellSize, Func<Grid<T>, int, int, T> createGridObject) {

        _width = width;
        _height = height;
        _cellSize = cellSize;

        _grid = new T[_width, _height];

        _origin = origin;

        for(int x = 0; x < _grid.GetLength(0); x++) {
            for(int y = 0; y < _grid.GetLength(1); y++) {
                _grid[x,y] = createGridObject(this, x, y);
            }
        }
        
        DrawGrid();
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * _cellSize + _origin;
    }

    public Vector2Int GetGridCoordinates(Vector3 worldCoordinates) {
        int x = Mathf.FloorToInt((worldCoordinates.x - _origin.x)  / _cellSize);
        int y = Mathf.FloorToInt((worldCoordinates.y - _origin.y) / _cellSize);

        return new Vector2Int(x, y);
    }

    private void DrawGrid() {
        for(int x = 0; x < _grid.GetLength(0); x++) {
            for(int y = 0; y < _grid.GetLength(1); y++) {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 1000f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 1000f);
            }
        }
    }

    public void SetValue(int x, int y, T value) { 
        if(x < 0 || y < 0 || x > _width || y > _height)
            return;
        
        _grid[x,y] = value;
        Debug.Log(x + ", " + y + ": " + value);
    }

    public void SetValue(Vector3 worldCoordinates, T value) {
        Vector2Int gridCoordinates = GetGridCoordinates(worldCoordinates);
        SetValue(gridCoordinates.x, gridCoordinates.y, value);
    }

    public T GetValue(int x, int y) {
        return _grid[x, y];
    }

    public T GetValue(Vector3 worldCoordinates) {
        Vector2Int gridCoordinates = GetGridCoordinates(worldCoordinates);
        return GetValue(gridCoordinates.x, gridCoordinates.y);
    }

    public int GetWidth() {
        return _width;
    }

    public int GetHeight() {
        return _height;
    }

    public int GetCellSize() {
        return _cellSize;
    }
}

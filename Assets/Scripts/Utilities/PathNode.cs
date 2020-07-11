using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private Grid<PathNode> _grid;
    public int x;
    public int y;

    public int gCost, hCost, fCost;

    public bool isWalkable;
    public PathNode previousNode;

    public PathNode(Grid<PathNode> grid, int x, int y) {
        _grid = grid;
        this.x = x;
        this.y = y;

        isWalkable = true;
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
    }

    public string toString() {
        return (x + ", " + y);
    }
}

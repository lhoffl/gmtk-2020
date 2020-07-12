using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    private Grid<PathNode> _grid;
    private List<PathNode> _openList;
    private List<PathNode> _closedList;

    public Pathfinding(int width, int height) {
        Instance = this;
        _grid = new Grid<PathNode>(width, height, Vector3.zero, 1, (Grid<PathNode> grid, int x, int y) => new PathNode(grid, x, y)); 
    }

    public List<Vector3> FindVectorPath(Vector3 startWorldPosition, Vector3 targetWorldPosition) {
        Vector2Int startGridPosition = _grid.GetGridCoordinates(startWorldPosition);
        Vector2Int targetGridPosition = _grid.GetGridCoordinates(targetWorldPosition);

        List<PathNode> path = FindPath(startWorldPosition, targetWorldPosition);

        if(path == null) {
            return null;
        }
        else {
            List<Vector3> vectorPath = new List<Vector3>();

            foreach(PathNode pathNode in path) {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y));
            }

            return vectorPath;
        }
    }

    public List<PathNode> FindPath(Vector3 initialPosition, Vector3 targetPosition) {

        PathNode startNode = _grid.GetValue(initialPosition);
        PathNode endNode = _grid.GetValue(targetPosition);

        _openList = new List<PathNode> { startNode };
        _closedList = new List<PathNode>();

        for(int x = 0; x < _grid.GetWidth(); x++) {
            for(int y = 0; y < _grid.GetHeight(); y++) {

                PathNode pathNode = _grid.GetValue(x, y);

                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();

                pathNode.previousNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while(_openList.Count > 0) {

            PathNode currentNode = GetLowestFCostNode(_openList);
            if(currentNode == endNode) {
                return CalculatePath(endNode);
            }

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach(PathNode neighborNode in GetNeighborList(currentNode)) {
                
                if(_closedList.Contains(neighborNode)) continue;
                if(!neighborNode.isWalkable) {
                    _closedList.Add(neighborNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighborNode);
                if(tentativeGCost < neighborNode.gCost) {
                    neighborNode.previousNode = currentNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistance(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if(!_openList.Contains(neighborNode)) {
                        _openList.Add(neighborNode);
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> GetNeighborList(PathNode node) {
        List<PathNode> neighborList = new List<PathNode>();

        if(node.x - 1 >= 0) {
            neighborList.Add(GetNode(node.x - 1, node.y));

            if(node.y - 1 >= 0) {
                neighborList.Add(GetNode(node.x - 1, node.y - 1));
            }
            
            if(node.y + 1 < _grid.GetHeight()) {
                neighborList.Add(GetNode(node.x - 1, node.y + 1));
            }
        }

        if(node.x + 1 < _grid.GetWidth()) {
            neighborList.Add(GetNode(node.x + 1, node.y));
            
            if(node.y - 1 >= 0) {
                neighborList.Add(GetNode(node.x + 1, node.y - 1));
            }
            
            if(node.y + 1 < _grid.GetHeight()) {
                neighborList.Add(GetNode(node.x + 1, node.y + 1));
            }
        }

        if(node.y - 1 >= 0) {
            neighborList.Add(GetNode(node.x, node.y - 1));
        }
        
        if(node.y + 1 < _grid.GetHeight()) {
            neighborList.Add(GetNode(node.x, node.y + 1));
        }

        return neighborList;
    }

    public PathNode GetNode(int x, int y) {
        return _grid.GetValue(x, y);
    }

    private List<PathNode> CalculatePath(PathNode node) {
        List<PathNode> path = new List<PathNode>();
        path.Add(node);
        
        PathNode currentNode = node;

        while(currentNode.previousNode != null) {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistance(PathNode a, PathNode b) {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++) {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }

    public Grid<PathNode> GetGrid() {
        return _grid;
    }
}
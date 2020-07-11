using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapInfo : MonoBehaviour
{
    public TileType[] tileType;

    //This code is working off the assumption of one unit
    public GameObject selectedUnit;

    int[,] tiles;
    Node[,] graph;


    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start(){

        //Setup the selectedUnit's position
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;

        GenerateMapData();
        GenerateRandomMap();

        GeneratePathFindingGraph();

        //Spawn visual prefabs (render map)
        GenerateMapVisual();
    }

    void GenerateMapData(){
        //Allocate map tiles
        tiles = new int[mapSizeX,mapSizeY];

        //Initialize tiles to grass
        for(int x=0; x < mapSizeX; x++){
            for(int y=0; y < mapSizeY; y++){
                tiles[x,y] = 0;
            }
        }

        // Create U-shaped wall
        tiles[4,4] = 2;
        tiles[5,4] = 2;
        tiles[6,4] = 2;
        tiles[7,4] = 2;
        tiles[8,4] = 2;

        tiles[4,5] = 2;
        tiles[4,6] = 2;
        tiles[8,5] = 2;
        tiles[8,6] = 2;

        //mud for the mud god
        for(int x=3; x <= 5; x++){
            for(int y=0; y < 4; y++){
                tiles[x,y] = 1;
            }
        }

    }

    void GenerateRandomMap(){
        //Randomize tiles, favoring grass
        System.Random rnd = new System.Random();
        for(int x=0; x < mapSizeX; x++){
            for(int y=0; y < mapSizeY; y++){
                int probability  = rnd.Next(1, 101);
                if (probability < 20) tiles[x,y] = 2;
                else if (probability < 40) tiles[x,y] = 1;
                else tiles[x,y] = 0;
            }
        }
    }

    float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY){
        TileType tt = tileType[tiles[targetX, targetY]];

        //Don't use impassable tiles in pathfinding
        if(!UnitCanEnterTile(targetX, targetY)) return Mathf.Infinity;

        float cost = tt.moveCost;

        if (sourceX!=targetX && sourceY!=targetY){
            //we are moving diagonally. we want to break ties in favor for aesthetics
            cost += 0.001f;
        }

        return cost;
    }

    void GeneratePathFindingGraph(){
        //initialize the array
        graph = new Node[mapSizeX,mapSizeY];

        //initialize a spot for each node in array
        for(int x=0; x < mapSizeX; x++){
            for(int y=0; y < mapSizeY; y++){
                graph[x,y] = new Node();

                graph[x,y].x = x;
                graph[x,y].y = y;
            }
        }

        //find neighbors
        for(int x=0; x < mapSizeX; x++){
            for(int y=0; y < mapSizeY; y++){
                /*
                // Orthagonal edges
                if (x > 0) graph[x,y].edges.Add(graph[x-1,y]); //west
                if (x < (mapSizeX - 1)) graph[x,y].edges.Add(graph[x+1,y]); //east
                if (y > 0) graph[x,y].edges.Add(graph[x,y-1]); //south
                if (y < (mapSizeY - 1)) graph[x,y].edges.Add(graph[x,y+1]); //north
                */

                // 8-way edges (diagonals)
				// Try left
				if(x > 0) {
					graph[x,y].edges.Add( graph[x-1, y] );
					if(y > 0)
						graph[x,y].edges.Add( graph[x-1, y-1] );
					if(y < mapSizeY-1)
						graph[x,y].edges.Add( graph[x-1, y+1] );
				}

				// Try Right
				if(x < mapSizeX-1) {
					graph[x,y].edges.Add( graph[x+1, y] );
					if(y > 0)
						graph[x,y].edges.Add( graph[x+1, y-1] );
					if(y < mapSizeY-1)
						graph[x,y].edges.Add( graph[x+1, y+1] );
				}

				// Try straight up and down
				if(y > 0)
					graph[x,y].edges.Add( graph[x, y-1] );
				if(y < mapSizeY-1)
					graph[x,y].edges.Add( graph[x, y+1] );
            }
        }
    }

    void GenerateMapVisual(){
        for(int x=0; x < mapSizeX; x++){
            for(int y=0; y < mapSizeY; y++){
                TileType tt = tileType[tiles[x,y]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x,y,0), Quaternion.identity);

                //teach the tile its coordiantes and map
                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y){
        return new Vector3(x, y, 0);
    }

    public bool UnitCanEnterTile(int x, int y){
        return tileType[tiles[x,y]].passable;
    }

    public void GeneratePathTo(int x, int y){
        selectedUnit.GetComponent<Unit>().currentPath = null;

        //User clicked on a mountin
        if(!UnitCanEnterTile(x,y)) return;

        Dictionary<Node, float> distance = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();

        //List of Nodes we have not checked yet
        List<Node> unvisited = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];
        Node target = graph[x,y];

        distance[source] = 0;
        previous[source] = null;

        //Initialize everything to have infinity distance
        //Possible to have some nodes unreachable
        foreach(Node node in graph){
            if (node != source){
                distance[node] = Mathf.Infinity;
                previous[node] = null;
            }

            unvisited.Add(node);
        }

        while(unvisited.Count > 0){
            //u is the unvisited ndoe with the smallest distance
            Node u = null;

            foreach(Node potentialU in unvisited){
                if (u == null || distance[potentialU] < distance[u]) u = potentialU;
            }

            if(u == target) break; //we got em

            unvisited.Remove(u);

            foreach(Node node in u.edges){
                //float alt = distance[u] + u.DistanceTo(node);
                float alt = distance[u] + CostToEnterTile(u.x, u.y, node.x, node.y);
                if (alt < distance[node]){
                    distance[node] = alt;
                    previous[node] = u;
                }
            }
        }

        //if we get there, then we either found the shortest route to the target
        //or there is no route at all

        if(previous[target] == null){
            //No route between source and target
            return;
        }

        //step through previous sequence change and add to our path
        List<Node> currentPath = new List<Node>();

        Node current = target;

        while(current != null){
            currentPath.Add(current);
            current = previous[current];
        }

        //now, currentPath describes a path from target to source
        //we need to invert it
        currentPath.Reverse();
        
        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}

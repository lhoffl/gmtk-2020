using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public MapInfo map;
    public List<Node> currentPath = null;

    public int speed = 2;

    void Update(){
        if (currentPath != null){
            int currNode = 0;

            while(currNode < currentPath.Count-1){
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) + new Vector3(0,0,-1);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode+1].x, currentPath[currNode+1].y) + new Vector3(0,0,-1);
                
                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }
    }

    public void ProcessEndOfTurn(){
        for(int x = 0; x < this.speed; x++){
            MoveNextTile();
        }
    }

    void MoveNextTile(){
        if(currentPath==null) return;

        //Remove the old current/first node from the path
        currentPath.RemoveAt(0);

        //Grab new first node and move there
        transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
        this.tileX = (int)transform.position.x;
        this.tileY = (int)transform.position.y;

        if(currentPath.Count == 1){
            //only one tile left, we must be at our ultimate destination
            //clear path info
            currentPath = null;
        }

    }

}

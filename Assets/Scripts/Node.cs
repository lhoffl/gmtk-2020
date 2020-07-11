﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public List<Node> edges;
    public int x;
    public int y;

    public Node(){
        edges = new List<Node>();
    }

    public float DistanceTo(Node node){
        return Vector2.Distance(
            new Vector2(x,y),
            new Vector2(node.x, node.y));
    }
}
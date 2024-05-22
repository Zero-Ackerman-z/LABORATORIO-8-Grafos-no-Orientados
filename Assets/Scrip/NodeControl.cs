using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour
{
    public SimpleList<Connection> adjacentNodes;

    void Awake()
    {
        adjacentNodes = new SimpleList<Connection>();
    }

    public void AddAdjacentNode(NodeControl node, float weight)
    {
        adjacentNodes.Add(new Connection { Node = node, Weight = weight });
    }

    public Connection SelectRandomAdjacent()
    {
        int index = Random.Range(0, adjacentNodes.Count);
        return adjacentNodes.Get(index);
    }

    public class Connection
    {
        public NodeControl Node;
        public float Weight;
    }
}

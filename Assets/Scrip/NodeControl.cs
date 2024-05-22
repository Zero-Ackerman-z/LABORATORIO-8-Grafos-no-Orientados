using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour
{
    public List<NodeControl> adjacentNodes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddAdjacenNode(NodeControl node)
    {
        adjacentNodes.Add(node);
    }
    public NodeControl SelectRandomAdjacen()
    {
        int index = Random.Range(0, adjacentNodes.Count);
        return adjacentNodes[index];
    }
}

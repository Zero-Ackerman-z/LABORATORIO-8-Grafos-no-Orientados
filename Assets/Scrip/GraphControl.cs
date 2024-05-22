using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour
{
    public GameObject nodePrefab;
    public TextAsset nodePositionsTxt;
    public string[] arrayNodePositions;
    public string[] currentNodePositions;
    public SimpleList<GameObject> allNodes;
    public TextAsset nodeConnectionsTxt;
    public string[] arrayNodeConnections;
    public string[] currentNodeConnections;
    public EnemyControl enemy;
    // Start is called before the first frame update
    void Start()
    {
        allNodes = new SimpleList<GameObject>();
        CreateNodes();
        CreateConnections();
        SelectInitialNode();
    }

    void CreateNodes()
    {
        if (nodePositionsTxt != null)
        {
            arrayNodePositions = nodePositionsTxt.text.Split('\n');
            for (int i = 0; i < arrayNodePositions.Length; i++)
            {
                currentNodePositions = arrayNodePositions[i].Split(',');
                if (currentNodePositions.Length < 2)
                {
                    Debug.LogError("Invalid node position data at line " + (i + 1));
                    continue;
                }

                float x = float.Parse(currentNodePositions[0]);
                float y = float.Parse(currentNodePositions[1]);

                Vector2 position = new Vector2(x, y);
                GameObject tmp = Instantiate(nodePrefab, position, transform.rotation);
                allNodes.Add(tmp);
            }
        }
    }

    void CreateConnections()
    {
        if (nodeConnectionsTxt != null)
        {
            arrayNodeConnections = nodeConnectionsTxt.text.Split('\n');
            for (int i = 0; i < arrayNodeConnections.Length; ++i)
            {
                currentNodeConnections = arrayNodeConnections[i].Split(',');
                NodeControl nodeControl = allNodes.Get(i).GetComponent<NodeControl>();
                for (int j = 0; j < currentNodeConnections.Length; ++j)
                {
                    int connectionIndex = int.Parse(currentNodeConnections[j]);
                    if (connectionIndex < allNodes.Count)
                    {
                        NodeControl targetNode = allNodes.Get(connectionIndex).GetComponent<NodeControl>();
                        nodeControl.AddAdjacentNode(targetNode, 10.0f); // Asigna un peso de 10.0
                        targetNode.AddAdjacentNode(nodeControl, 10.0f); // Crea una conexión bidireccional con peso de 10.0
                    }
                    else
                    {
                        Debug.LogError("Invalid node connection data at line " + (i + 1));
                    }
                }
            }
        }
    }


    void SelectInitialNode()
    {
        if (allNodes.Count == 0)
        {
            Debug.LogError("No nodes available to select initial node.");
            return;
        }

        int index = Random.Range(0, allNodes.Count);
        enemy.objective = allNodes.Get(index);
    }
}

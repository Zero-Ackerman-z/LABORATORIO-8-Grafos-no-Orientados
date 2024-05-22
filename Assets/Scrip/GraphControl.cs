using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour
{
    public GameObject nodePrefab;
    public TextAsset nodePositionsTxt;
    public string[] arrayNodePositions;
    public string[] currentNodePositions;
    public List<GameObject> allNodes;
    public TextAsset nodeConectionsTxt;
    public string[] arrayNodeConections;
    public string[] currentNodeConections;
    public EnemyControl enemy;
    // Start is called before the first frame update
    void Start()
    {
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
                Vector2 position = new Vector2(float.Parse(currentNodePositions[0]), float.Parse(currentNodePositions[1]));
                GameObject tmp = Instantiate(nodePrefab, position, transform.rotation);
                allNodes.Add(tmp);

            }
        }
    }
    void CreateConnections()
    {
        if(nodeConectionsTxt != null)
        {
            arrayNodeConections = nodeConectionsTxt.text.Split('\n');
            for(int i = 0; i < arrayNodeConections.Length; ++i)
            {
                currentNodeConections = arrayNodeConections[i].Split(",");
                for(int j = 0; j < currentNodeConections.Length; ++j)
                {
                    allNodes[i].GetComponent<NodeControl>().AddAdjacenNode(allNodes[int.Parse(currentNodeConections[j])].GetComponent<NodeControl>());
                }
            }
        }
    }
    void SelectInitialNode()
    {
        int index = Random.Range(0, allNodes.Count);
        enemy.objective = allNodes[index];
    }
// Update is called once per frame
    void Update()
    {
        
    }
}

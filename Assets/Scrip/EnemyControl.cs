using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyControl : MonoBehaviour
{
    public GameObject objective;
    public Vector2 speedReference;
    public float energy;
    public float maxEnergy = 100f;
    public float restTime = 5f;
    private float currentRestTime = 0f;
    private bool isResting = false;
    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
        if (objective == null)
        {
            Debug.LogError("Objective not set in EnemyControl.");
        }
    }

    void Update()
    {
        if (isResting)
        {
            currentRestTime -= Time.deltaTime;
            if (currentRestTime <= 0)
            {
                isResting = false;
                energy = maxEnergy;
            }
            return;
        }

        if (objective != null)
        {
            transform.position = Vector2.SmoothDamp(transform.position, objective.transform.position, ref speedReference, 0.5f);
        }
        else
        {
            Debug.LogWarning("Objective is null in EnemyControl.Update");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node")
        {
            NodeControl nodeControl = collision.gameObject.GetComponent<NodeControl>();
            if (nodeControl != null)
            {
                NodeControl.Connection newObjective = nodeControl.SelectRandomAdjacent();
                if (newObjective != null)
                {
                    objective = newObjective.Node.gameObject;
                    energy -= newObjective.Weight;
                    if (energy <= 0)
                    {
                        isResting = true;
                        currentRestTime = restTime;
                    }
                }
                else
                {
                    Debug.LogWarning("No adjacent nodes available in NodeControl.");
                }
            }
            else
            {
                Debug.LogError("NodeControl component not found on collision object.");
            }
        }
    }
}

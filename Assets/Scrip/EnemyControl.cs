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

    public float visionRange = 5f;
    public float visionAngle = 45f;
    public LineRenderer visionCone;
    private GameObject player;

    void Start()
    {
        energy = maxEnergy;
        if (objective == null)
        {
            Debug.LogError("Objective not set in EnemyControl.");
        }

        player = GameObject.FindGameObjectWithTag("Player");
        SetupVisionCone();
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

        if (IsPlayerInVision())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        UpdateVisionCone();
    }

    private void Patrol()
    {
        if (objective != null)
        {
            transform.position = Vector2.SmoothDamp(transform.position, objective.transform.position, ref speedReference, 0.5f);
        }
        else
        {
            Debug.LogWarning("Objective is null in EnemyControl.Update");
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;
            transform.position = Vector2.SmoothDamp(transform.position, playerPosition, ref speedReference, 0.5f);
            energy -= Time.deltaTime * 10;
            if (energy <= 0)
            {
                isResting = true;
                currentRestTime = restTime;
            }
        }
    }

    private bool IsPlayerInVision()
    {
        if (player == null)
        {
            return false;
        }

        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);

        if (angleToPlayer < visionAngle / 2 && Vector2.Distance(transform.position, player.transform.position) < visionRange)
        {
            return true;
        }
        return false;
    }

    private void SetupVisionCone()
    {
        if (visionCone == null)
        {
            visionCone = gameObject.AddComponent<LineRenderer>();
        }

        visionCone.positionCount = 4;
        visionCone.startWidth = 0.1f;
        visionCone.endWidth = 0.1f;
        visionCone.loop = true;
        visionCone.useWorldSpace = false;

        UpdateVisionCone();
    }

    private void UpdateVisionCone()
    {
        Vector2 moveDirection = speedReference.normalized;

        Vector3[] visionVertices = new Vector3[4];
        visionVertices[0] = Vector3.zero;
        visionVertices[1] = Quaternion.Euler(0, 0, visionAngle / 2) * moveDirection * visionRange;
        visionVertices[2] = moveDirection * visionRange;
        visionVertices[3] = Quaternion.Euler(0, 0, -visionAngle / 2) * moveDirection * visionRange;

        visionCone.SetPositions(visionVertices);
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


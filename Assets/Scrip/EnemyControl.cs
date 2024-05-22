using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject objective;
    public Vector2 speedReference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position,objective.transform.position,ref speedReference ,0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Node")
        {
            objective = collision.gameObject.GetComponent<NodeControl>().SelectRandomAdjacen().gameObject;
        }
    }
}

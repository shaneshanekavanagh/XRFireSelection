using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    public List<GameObject> objectsToAvoid;

    public float avoidanceRadius = 0.3f;    
    public float sphereRadius = 1.8f;
    public float moveSpeed = 2f;

    private Vector3 randomDirection;

    void Start()
    {
        randomDirection = Random.onUnitSphere;
    }

    void Update()
    {           
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(Vector3.zero, transform.position) > sphereRadius)
        {
            randomDirection = -randomDirection;
        }
                
        if (Random.value < 0.00001f)
        {
            randomDirection = Random.onUnitSphere;
        }        
        
        for (int i = 0; i < objectsToAvoid.Count; i++)
        {
            Vector3 avoidanceDirection = transform.position - objectsToAvoid[i].transform.position;
            float distance = avoidanceDirection.magnitude;

            if (distance < avoidanceRadius)
            {
                //randomDirection = -randomDirection;
                randomDirection = Random.onUnitSphere;
            }
        }

        if((transform.position.y > 2f)||(transform.position.y < 0.5f))
        {
            randomDirection = -randomDirection;
        }
    }
}

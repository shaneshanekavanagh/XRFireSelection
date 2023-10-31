using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovementv2 : MonoBehaviour
{

    public List<GameObject> objectsToAvoid;

    public GameObject objectB;

    public GameObject objectC;

    public float avoidanceRadius = 1f;
   
    public float moveSpeed = 2f;

    public float rotationSpeed = 3f;

    public bool avoiding = false;

    public bool flying = true;

    void Awake()
    {
        //myTransform = transform;
    }

    void Start()
    {
        avoidanceRadius = Random.Range(avoidanceRadius - 0.25f,avoidanceRadius + 0.2f);
        moveSpeed = Random.Range(moveSpeed - 0.04f, moveSpeed + 0.08f);
        rotationSpeed = Random.Range(rotationSpeed - 0.04f, rotationSpeed + 0.08f);

        if (objectC == null){ objectC = GameObject.FindGameObjectWithTag("objectC"); }

        if(objectB == null){ objectB = GameObject.FindGameObjectWithTag("objectB"); }

        StartCoroutine(BuildI());
    }

    IEnumerator BuildI()
    {
        while(flying)
        {
            if (Vector3.Distance(objectC.transform.position, transform.position) > avoidanceRadius)
            {
                avoiding = false;
            }
            else
            {
                avoiding = true;
            }

            for (int i = 0; i < objectsToAvoid.Count; i++)
            {
                if (objectsToAvoid[i] != gameObject)
                {
                    Vector3 avoidanceDirection = transform.position - objectsToAvoid[i].transform.position;

                    float distance = avoidanceDirection.magnitude;

                    if(distance < (avoidanceRadius/10))
                    {
                        avoiding = true;
                        yield return null;
                    }
                    yield return null;
                }
            }
            yield return null;
        }
    }

    void Update()
    {

        if(avoiding)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(objectC.transform.position + transform.position), rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * moveSpeed * Time.deltaTime * 2;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(objectB.transform.position - transform.position), rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
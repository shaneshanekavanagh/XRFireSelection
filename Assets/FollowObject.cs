using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public float followDistance = 2.0f;
    public float maxSpeed = 5.0f;

    void Start()
    {
        followDistance = Random.Range(10.0f,30.0f);
        maxSpeed = Random.Range(1.0f,2.0f);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;
            float speed = Mathf.Clamp(distanceToTarget / followDistance, 0f, maxSpeed);
            Vector3 newPosition = transform.position + directionToTarget.normalized * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
        }
    }
}
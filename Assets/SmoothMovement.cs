using System.Collections;
using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;   // Total distance to move up and down.
    public float moveSpeed = 0.1f;     // Speed of the movement.

    private Vector3 initialPosition;
    private bool movingUp = true;

    public bool UpAndDown = true;

    private void Start()
    {
        initialPosition = transform.position;

        if(UpAndDown)
        {
            StartCoroutine(MoveUpDown());
        }
        else
        {
            StartCoroutine(MoveLeftRight());
        }
    }

    private IEnumerator MoveUpDown()
    {
        while (true)
        {
            Vector3 targetPosition = movingUp ? initialPosition + Vector3.up * moveDistance : initialPosition;

            float distance = Vector3.Distance(transform.position, targetPosition);

            while (distance > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, targetPosition);
                yield return null;
            }

            movingUp = !movingUp;

            // Pause for a moment at the top and bottom positions (you can adjust this duration).
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator MoveLeftRight()
    {
        while (true)
        {
            Vector3 targetPosition = movingUp ? initialPosition + Vector3.right * moveDistance : initialPosition;

            float distance = Vector3.Distance(transform.position, targetPosition);

            while (distance > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, targetPosition);
                yield return null;
            }

            movingUp = !movingUp;

            // Pause for a moment at the top and bottom positions (you can adjust this duration).
            yield return new WaitForSeconds(1.0f);
        }
    }
}
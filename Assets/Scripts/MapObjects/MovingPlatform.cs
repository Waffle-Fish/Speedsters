using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;
    private Transform originalTransform;
    private int currentWaypointIndex = 0;

    

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.0001f)
        {
            ++currentWaypointIndex;
                if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(transform.position, waypoints[0].transform.position);
        Gizmos.DrawLine(transform.position, waypoints[1].transform.position);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        originalTransform = collision.transform.parent;
        collision.gameObject.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.SetParent(originalTransform);
    }
}

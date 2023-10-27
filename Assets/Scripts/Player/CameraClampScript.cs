using UnityEngine;

//Script responsible for camera clamp (restrictive camera view based on edges)
public class CameraClampScript : MonoBehaviour
{
    [SerializeField]
    GameObject playerPos;

    //Sets mins and maxes for camera, once reached, camera stops moving in the direction it reached the min/max for
    [SerializeField]
    float xMin;
    [SerializeField]
    float xMax;
    [SerializeField]
    float yMin;
    [SerializeField]
    float yMax;

    void Update()
    {
        gameObject.transform.position = new Vector3
             (
                 Mathf.Clamp(playerPos.transform.position.x, xMin, xMax),
                 Mathf.Clamp(playerPos.transform.position.y, yMin, yMax),
                 gameObject.transform.position.z
            );
    }
}

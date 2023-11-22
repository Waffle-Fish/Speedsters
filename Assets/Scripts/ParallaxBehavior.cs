using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class ParallaxBehavior : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    Transform playerTrans;
    [SerializeField]
    float yOffest = 0;
    
    Vector2 startPosition;
    float startZ;

    Vector2 Travel => new(cam.transform.position.x - startPosition.x, cam.transform.position.y + yOffest - startPosition.y);
    float DistanceFromSubject => transform.position.z - playerTrans.position.z;
    float ClippingPlane => (cam.transform.position.z + (DistanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float ParrallaxFactor => Mathf.Abs(DistanceFromSubject) / ClippingPlane;

    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    void Update()
    {
        Vector2 newPos = startPosition + Travel * ParrallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}

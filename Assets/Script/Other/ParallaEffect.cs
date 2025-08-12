using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;



    float startingz;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingz = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newposition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newposition.x, newposition.y, startingz);
    }
}

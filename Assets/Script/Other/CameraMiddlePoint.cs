using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMiddlePoint : MonoBehaviour
{
    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private Transform dronePosition;
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 droneScreenPosition = Camera.main.ScreenToWorldPoint(dronePosition.transform.position);
        //Vector2 playerScreenPosition = Camera.main.ScreenToWorldPoint(playerPosition.transform.position);
        
        //Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseWorld = Input.mousePosition;
        Vector3 player = Camera.main.WorldToScreenPoint(playerPosition.transform.position);
        Vector3 middle = Camera.main.ScreenToWorldPoint((3 * player + mouseWorld) / 4f);
        middle.z = -10f;
        Debug.Log(middle);
        transform.position = middle + offset;
        //Debug.Log(transform.position);
    }
}

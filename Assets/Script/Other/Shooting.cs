using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    private float timeBetweenFiring;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector2 mouse = Input.mousePosition;
        Vector2 diraction = (mouse - playerScreenPosition).normalized;
        Quaternion originalRotarion = transform.rotation;

        float angle = Mathf.Atan2(diraction.y, diraction.x) * Mathf.Rad2Deg;


        Quaternion rotation = Quaternion.AngleAxis(angle - 0f, Vector3.forward);

        this.transform.rotation = rotation;
        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}

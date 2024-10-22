using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Speed variable of 8
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        // LASER UPWARD MOVEMENT
        // translate laser up
        Vector3 direction = new Vector3(0, 1, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        // LASER GARBAGE COLLECTOR
        // if laser position is greater than 8 on the y
        // destroy the object
        // destroy method can also be used with a second value, being the time
        // destroy(gameObject, after how many seconds);
        if (transform.position.y >= 8f)
        {
            Destroy(this.gameObject);
        }
    }
}

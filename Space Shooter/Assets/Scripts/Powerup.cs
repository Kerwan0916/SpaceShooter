using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 3.0f;
    // ID for powerups 
    // 0 = triple shot
    // 1 = speed
    // 2 = shields
    [SerializeField]
    private int powerupID;


    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3 (adjust in the inspector)
        // when leave the screen, destroy this object
        transform.Translate(Vector3.down * _speed  * Time.deltaTime);
        
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    // on triggerCollision
    // only be collectable by the player (tags)
    // on collected, destroy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // communicate 
            // create a handle to the component i want
            // assign the handle to the component
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                if (powerupID == 0)
                {
                    player.TripleShotActive();
                }//else if (powerupID == 1) {
                 // player.Speed
                // ekse if (powerupID == 2) { player.shieldsactivate
            }
            Destroy(this.gameObject);
        }
    }

}

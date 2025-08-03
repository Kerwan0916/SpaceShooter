using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 3.0f;
    [SerializeField] // 0 = triple shot 1 = speed 2 = shields
    private int powerupID;

    [SerializeField] private AudioClip _audioClip;


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

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            if (player != null)
            {
                //    if (powerupID == 0)
                //    {
                //        player.TripleShotActive();
                //    }
                //    else if (powerupID == 1)
                //    {
                //        //player.SpeedBoostActive();
                //        Debug.Log("Collected Speed Boost");
                //    }
                //    else if (powerupID == 2)
                //    {
                //        Debug.Log("Shields Collected");
                //    }
                //}

                switch (powerupID)
                {
                    case 0:
                        Debug.Log("TripleShot Collected");
                        player.TripleShotActive();
                        break;
                    case 1:
                        Debug.Log("Collected Speed Boost");
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Shields Collected");
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default State");
                        break;
                }
                Destroy(this.gameObject);
            }
        }
    }

}

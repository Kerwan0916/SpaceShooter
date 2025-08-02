using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    // handle to animator component
    private Animator enemyAnimator;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 7.5f, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        // null check player
        if ( _player != null )
        {
            Debug.LogError("The Player is NULL");
        }
        // assign the component to Anim
        enemyAnimator = GetComponent<Animator>();
        if ( enemyAnimator != null )
        {
            Debug.LogError("Animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4 m/s
        Vector3 direction = new Vector3(0, -1, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // if off bottom of the screen
        // respawn back at the top with a new random x position
        if (transform.position.y <= -5.4f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is player
        // damage the player
        // destroy us
        if (other.tag == "Player")
        {
            // damage player
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            // trigger the anim
            enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.6f);
        }

        // if other is laser
        // laser
        // destroy us
        if (other.tag == "Laser")
        {
            //Destroy(Laser);
            Destroy(other.gameObject);

            // add 10 to score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            // trigger the anim
            enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.6f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data type (int, float, bool, string)
    // every variable has a name
    // optional value assigned
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0,0,0)
        transform.position = new Vector3(0, 0, 0);

        // find the object, get the component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }



    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // MOVEMENT SPEED AND INPUTS
        // new Vector3(1,0,0) * 0 * 3.5f * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        // optimized version
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);



        // MOVEMENT LIMITS AND MAP BOUNDS
        // if player.position.y > 0
        // then y position = 0
        // else if position on the y is less than -3.8f
        // then y = pos. -3.8f

        //if (transform.position.y >= 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0, 0);
        //}
        //else if (transform.position.y <= -3.8f)
        //{
        //    transform.position = new Vector3(transform.position.x, -3.8f, 0);
        //}

        // optimized version
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);


        if (transform.position.x >= 11.28f)
        {
            transform.position = new Vector3(-11.28f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.28f)
        {
            transform.position = new Vector3(11.28f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        // offset for laser spawning, +0.8 on y axis
        Vector3 offset = new Vector3(0, 1.05f, 0);

        // if i hit the space key
        // spawn gameObject (laser prefab)

        _canFire = Time.time + _fireRate;
        // we set a default spawn position and rotation to the laser
        Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);

    }

    public void Damage()
    {
        // removes a life
        // _lives -= 1; also works
        _lives--;

        // check if dead
        if (_lives < 1)
        {
            // communicate with spawn manager
            // let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning;
    [SerializeField]
    private GameObject[] powerups;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Spawn game objects every 5 seconds
    // Create a coroutine of type IEnumerator -- Yield Events
    // While loop
    IEnumerator SpawnEnemyRoutine()
    {
        // while loop (infinite loop)
        // instantiante enemy prefab
        // yield wait for 5 seconds


        // change while loop to only while player is alive
        while (_stopSpawning == false)
        {
            // Mathf.Clamp(transform.position.x,-8f,8f) also works for the x range
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            // every 3-7 seconds, spawn in a powerup
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);

            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

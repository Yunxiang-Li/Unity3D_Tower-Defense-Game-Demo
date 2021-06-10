using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents an object pool's behaviors which helps us instantiate the enemy stream.
 */
public class ObjectPool : MonoBehaviour
{   
    // Store the enemy prefab to be spawned.
    [SerializeField] private GameObject enemyPrefab;
    // Store the spawn time interval.
    [SerializeField] [Range(0.1f, 30f)]private float spawnTime = 1f;
    // Store the size of object pool.
    [SerializeField] [Range(0, 50)]private int poolSize = 5;
    // Store the object pool(GameObject array).
    private GameObject[] objectPool;
    
    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start the SpawnEnemy coroutine function.
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /**
     * A coroutine function to spawn an enemy prefab per spawn time.
     */
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Active first inactive object in object pool.
            EnableObjectInPool();
            // Wait for spawn time.
            yield return new WaitForSeconds(spawnTime);
        }
    }

    /**
     * Instantiate and de-active all objects in object pool.
     */
    private void PopulatePool()
    {
        // Initialize the object pool.
        objectPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            // Instantiate and de-active each object from object pool.
            objectPool[i] = Instantiate(enemyPrefab, transform);
            objectPool[i].SetActive(false);
        }
    }

    /**
     * Active first inactive object in object pool.
     */
    private void EnableObjectInPool()
    {
        // Try to active the first inactive object in the object pool.
        for (int i = 0; i < poolSize; i++)
        {
            if (objectPool[i].activeInHierarchy == false)
            {
                objectPool[i].SetActive(true);
                return;
            }
        }
    }
}

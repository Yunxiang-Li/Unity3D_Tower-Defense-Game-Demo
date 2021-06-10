using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents each enemy object's health behaviors.
 */
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    // Store the enemy object's current hit times and max hit times.
    [SerializeField] private int maxHitTimes = 5;
    [SerializeField] private int currHitTimes = 0;
    // Store the factor of difficulty.
    [Tooltip("Add certain enemy max hit points when enemy died.")]
    [SerializeField] private int difficultyFactor = 1;
    
    // Store the enemy ref;
    private Enemy enemy;
    
    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        // Initialize enemy object's current hit times as max hit times.
        currHitTimes = maxHitTimes;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the enemy ref.
        enemy = FindObjectOfType<Enemy>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * process when enemy's particle system hit by one projectile.
     */
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    /**
     * Handle with hit situation.
     */
    private void ProcessHit()
    {
        // If current hit time is less than or equal to 0.
        if (--currHitTimes <= 0)
        {
            // de-active the enemy object.
            gameObject.SetActive(false);
            // Reward player certain balance to the bank.
            enemy.RewardBalance();
            // Increments the enemy game object's max hit time each time that enemy died.
            maxHitTimes += difficultyFactor;
        }
    }
}

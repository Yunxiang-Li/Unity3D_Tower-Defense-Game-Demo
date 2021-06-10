using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents tower object's aiming at a specific target's transform with its weapon's behavior.
 */
public class TargetLocator : MonoBehaviour
{
    // Store the tower weapon and enemy's transforms.
    [SerializeField] private Transform weaponTransform;
    // Store the tower's fire range.
    [SerializeField] private float fireRange = 15f;
    // Store the tower's projectile particle system.
    [SerializeField] private ParticleSystem projectileParticles;
    // Store the closest enemy's transform
    private Transform enemyTransform;

    // Update is called once per frame
    void Update()
    {
        // Try to find the closest enemy.
        FindClosestEnemy();
        // Keep aiming at the enemy object.
        AimAtEnemy();
    }

    /**
     * Set the tower weapon's transform aiming at enemy object's transform if within tower's fire range.
     */
    private void AimAtEnemy()
    {
        if (enemyTransform == null) return;
        // Let tower always face at the closest enemy.
        weaponTransform.LookAt(enemyTransform);
        // Calculate distance between closest enemy and current tower.
        float distance = Vector3.Distance(transform.position, enemyTransform.position);
        // Attack(play particle effects) if distance is in tower's fire range.
        Attack(distance <= fireRange);
    }
    
    /**
     * Find the closest enemy object of current tower.
     */
    private void FindClosestEnemy()
    {   
        // Store all Enemy game objects.
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        // Initialize a Transform object to store the closest enemy object's transform.
        Transform closestEnemyTransform = null;
        // Initialize the min distance as infinity.
        float minDistance = Mathf.Infinity;
        
        // For each enemy object.
        foreach (Enemy enemy in enemies)
        {
            // Calculate its distance towards current tower.
            float currDistance = Vector3.Distance(transform.position, enemy.transform.position);
            
            // If current distance is less than the min distance.
            if (currDistance < minDistance)
            {
                // Reset the min distance as current distance.
                minDistance = currDistance;
                // Reset the closest enemy transform as current enemy transform.
                closestEnemyTransform = enemy.transform;
            }
        }
        
        // Pass the closest enemy transform to the field.
        enemyTransform = closestEnemyTransform;
    }

    /**
     * Let current tower attack(play particle effect) when input boolean is true.
     */
    private void Attack(bool isActive)
    {
        // Store the projectileParticles emission module.
        ParticleSystem.EmissionModule em = projectileParticles.emission;
        
        // Set emission module's active state according to the input parameter.
        em.enabled = isActive;
    }
}

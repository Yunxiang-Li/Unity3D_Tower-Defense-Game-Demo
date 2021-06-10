using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents all tower's balance behaviors.
 */
public class Tower : MonoBehaviour
{
    // Store the balance of a tower.
    [SerializeField] private int towerBalance = 75;
    // Store the build delay time.
    [SerializeField] private float buildDelay = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Call the coroutine function Build.
        StartCoroutine(Build());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Try to create a tower prefab at a certain position.
     */
    public bool CreateTower(Tower towerPrefab, Vector3 pos)
    {
        // Find the bank game object in the game.
        Bank bank = FindObjectOfType<Bank>();
        // If no bank ref then return false.
        if (bank == null)
            return false;
        // If current balance is greater than or equal to the tower balance.
        if (bank.CurrBalance >= towerBalance)
        {
            // Subtract tower balance from the current balance.
            bank.Withdraw(towerBalance);
            // Instantiate a tower game object.
            Instantiate(towerPrefab, pos, Quaternion.identity);
            return true;
        }
        // If current balance is less than the tower balance then just return false.
        return false;
    }

    /**
     * De-active all tower children and grandchildren then active them per build delay.
     */
    private IEnumerator Build()
    {
        // De-active all tower children and grandchildren. 
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(false);

            foreach (Transform grandchildTransform in childTransform)
            {
                grandchildTransform.gameObject.SetActive(false);
            }
        }
        
        // Active all tower children and grandchildren. 
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(true);

            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandchildTransform in childTransform)
            {
                grandchildTransform.gameObject.SetActive(true);
            }
        }
    }
}

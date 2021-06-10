using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents all enemy balance behaviors.
 */
public class Enemy : MonoBehaviour
{
    // Store the enemy reward and penalty.
    [SerializeField] private int reward = 25;
    [SerializeField] private int penalty = 25;
    // Store the Bank ref;
    private Bank bank;
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the bank ref.
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Reward player a certain balance.
     */
    public void RewardBalance()
    {
        if (bank == null)
            return;
        bank.Deposit(reward);
    }

    /**
     * Steal player a certain balance.
     */
    public void StealBalance()
    {
        if (bank == null)
            return;
        bank.Withdraw(reward);
    }
}

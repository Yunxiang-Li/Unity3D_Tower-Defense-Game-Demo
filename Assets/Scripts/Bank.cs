using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/**
 * This class plays as a bank role in the game which controls resources to place towers.
 */
public class Bank : MonoBehaviour
{
    // Store the starting balance.
    [SerializeField] private int startingBalance = 150;
    // Store the current balance.
    [SerializeField] private int currBalance;
    // Store the TextMeshPro ref.
    [SerializeField] private TextMeshProUGUI balanceDisplay;
    
    // A getter function of currBalance field.
    public int CurrBalance => currBalance;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Initialize the current balance and update the display.
        currBalance = startingBalance;
        UpdateDisplay();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Add balance to current balance according to input and update the display.
     */
    public void Deposit(int amount)
    {
        currBalance += Mathf.Abs(amount);
        // Update the display.
        UpdateDisplay();
    }
    
    /**
     * Subtract balance from current balance according to input and handle lose situation.
     */
    public void Withdraw(int amount)
    {
        currBalance -= Mathf.Abs(amount);
        // Update the display.
        UpdateDisplay();
        
        // Handle lose situation.
        if (currBalance < 0)
        {
            ReloadScene();
        }
    }

    /**
     * Reload the current active scene when lose the game.
     */
    private void ReloadScene()
    {
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.buildIndex);
    }

    /**
     * Update the text content in the TextMeshPro component.
     */
    private void UpdateDisplay()
    {
        balanceDisplay.text = "Gold: " + currBalance;
    }
}

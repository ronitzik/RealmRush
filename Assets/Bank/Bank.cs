using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    [SerializeField] int curBalance;
    [SerializeField] TextMeshProUGUI displayBalance;


    public int CurBalance { get { return curBalance; } }

    void Awake()
    {
        curBalance = startingBalance;
        UpdateDisplay();

    }

    public void Deposit(int amount)
    {
        curBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        curBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if (curBalance < 0)
        {
            //Lose the game;
            ReloadScene();
        }
    }
    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + curBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

    }
}


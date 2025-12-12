using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlayerController player;
    public int numOfHearts;
    public Image[] hearts;

    public Image ability1;
    public Image ability2;
    public Image ability3;

    public TMP_Text healthText;
    public TMP_Text waveCounter;

    public GameObject winScreen;
    public GameObject loseScreen;



        
    


    public void RemoveHeart()
    {
        if (numOfHearts > 0)
        {
            numOfHearts--;
            hearts[numOfHearts].enabled = false;
        }
    }


    public void ResetHealthBar()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = true;
        }
        numOfHearts = hearts.Length;
    }

    public void ChangeHealthText(float playerHealth)
    {
        if (healthText != null)
        {
            Debug.Log("null");
            return;
        }
        healthText.text = "Health: " + playerHealth.ToString();
    }

    public void UpdateWaveCounter(int waveNumber)
    {

        waveCounter.text = "Wave" + waveNumber.ToString();
        Debug.Log(waveCounter.text);
    }

    private void ShowCoolDown(int abilityNum, float coolDownLength)
    {
        
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
    }

    public void RestartButton()
    {
        GameManager.instance.ResetGame();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

    }


    private void CloseGame()
    {
        Application.Quit();
    }
}

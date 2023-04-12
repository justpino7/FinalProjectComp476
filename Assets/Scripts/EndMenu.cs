using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI endText;

    public HealthManager healthManager;

    public GameObject gameUI;

    // Update is called once per frame
    void Update()
    {
        if (healthManager.health <= 0)
        {
            endText.text = "Game Over";
        } 
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
        gameUI.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

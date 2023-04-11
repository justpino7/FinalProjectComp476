using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Allows the cursor to be always visible when the main menu opens
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void Pause()
    {
        Debug.Log("Pausing game for menu");

        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0) / 2;

        gameManager.Pause();
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Debug.Log("Resuming game from menu");

        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0);

        gameManager.Resume();
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager.GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

}

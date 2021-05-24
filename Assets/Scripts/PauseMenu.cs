using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject playerLook;

    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        playerLook = GameObject.FindGameObjectWithTag("Player").transform
            .Find("First person camera").gameObject;
    }

    public void Pause()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0) / 2;

        playerLook.GetComponent<FirstPersonLook>().enabled = false; 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        gameManager.Pause();
    }

    public void Resume()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0);

        playerLook.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        gameManager.Resume();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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

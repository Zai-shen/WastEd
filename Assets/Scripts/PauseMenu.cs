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
        Debug.Log("Hello from pausemenu");
        gameManager = GameObject.FindObjectOfType<GameManager>();
        playerLook = GameObject.FindGameObjectWithTag("Player").transform
            .Find("First person camera").gameObject;
    }

    public void Pause()
    {
        Debug.Log("Pausing game for menu");

        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0) / 2;

        playerLook.GetComponent<FirstPersonLook>().enabled = false; 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        gameManager.Pause();
    }

    public void Resume()
    {
        Debug.Log("Resuming game from menu");

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
                Debug.Log("Resuming");
                Resume();
            }
            else
            {
                Debug.Log("Pausing");
                Pause();
            }
        }
    }

}

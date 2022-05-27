using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSettings : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Player1UI;
    public GameObject Player2UI;

    void Start()
    {
        Player1UI.gameObject.SetActive(true);
        Player2UI.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
    }
}

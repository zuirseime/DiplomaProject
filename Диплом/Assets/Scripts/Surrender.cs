using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrender : MonoBehaviour
{
    public GameObject player1UI, player2UI;
    public void OnClick()
    {
        if (player1UI.activeSelf)
        {
            Debug.Log("Гравець <color=blue>USERNAME</color> переміг");
            Time.timeScale = 0f;
        }
        else if (player2UI.activeSelf)
        {
            Debug.Log("Гравець <color=red>USERNAME</color> переміг");
            Time.timeScale = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrender : MonoBehaviour
{
    public GameObject player1UI, player2UI, Player1Win, Player2Win;
    public void OnClick()
    {
        if (player1UI.activeSelf)
        {
            Time.timeScale = 0f;
            Player2Win.SetActive(true);
            Player1Win.SetActive(false);
            Time.timeScale = 0f;
        }
        else if (player2UI.activeSelf)
        {
            Time.timeScale = 0f;
            Player1Win.SetActive(true);
            Player2Win.SetActive(false);
            Time.timeScale = 0f;
        }

    }
}

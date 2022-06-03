using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] public MouseController movesValue;
    private bool isMovesLeft;
    public GameObject cameraRotator, player1UI, player2UI, playerSwap, redPlayerZone, bluePlayerZone, hud, roundCounter;
    public float roundNumber = 0.5f;

    public void PlayerSpawer()
    {
        roundNumber += 0.5f;
        isMovesLeft = movesValue.isMovesPossible;
        cameraRotator.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + 180, 0));

        if (roundNumber % 1 == 0)
        {
            player1UI.SetActive(false);
            player2UI.SetActive(true);
        }
        else
        {
            player2UI.SetActive(false);
            player1UI.SetActive(true);
            hud.GetComponent<ScoreBoard>().ScoreWrite();
            hud.GetComponent<ScoreBoard>().RoundWrite();
        }

        if (playerSwap.activeSelf)
            playerSwap.SetActive(false);
        isMovesLeft = true;
        movesValue.IsMovesPossible(isMovesLeft);
    }
}

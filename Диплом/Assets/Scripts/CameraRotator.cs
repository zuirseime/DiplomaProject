using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private MouseController movesCount;
    private int tileCount;
    public GameObject cameraRotator, player1UI, player2UI, playerSwap, gameArea;

    public void PlayerSpawer()
    {
        cameraRotator.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + 180, 0));
        playerSwap.SetActive(false);
        tileCount = gameArea.transform.childCount;

        if (gameArea.transform.GetChild(tileCount - 1).name == "PlayerBlue")
        {
            player1UI.SetActive(true);
            player2UI.SetActive(false);
        }
        else if (gameArea.transform.GetChild(tileCount - 1).name == "PlayerRed")
        {
            player1UI.SetActive(false);
            player2UI.SetActive(true);
        }

        playerSwap.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public GameObject redPlayerZone, bluePlayerZone, redScore, blueScore, roundCounter;
    private int tempRedScore, tempBlueScore, tempRoundCounter;

    public void RoundWrite()
    {
        tempRoundCounter = int.Parse(roundCounter.GetComponent<Text>().text);
        roundCounter.GetComponent<Text>().text = ((int)(tempRoundCounter)).ToString();
    }

    public void ScoreWrite()
    {
        for (int i = 0; i < redPlayerZone.transform.childCount; i++)
        {
            tempRedScore = int.Parse(redScore.GetComponent<Text>().text);
            redScore.GetComponent<Text>().text = (tempRedScore + redPlayerZone.transform.GetChild(i).GetComponent<VoxelTile>().tilePrice).ToString();
        }
        for (int i = 0; i < bluePlayerZone.transform.childCount; i++)
        {
            tempBlueScore = int.Parse(blueScore.GetComponent<Text>().text);
            blueScore.GetComponent<Text>().text = (tempBlueScore + bluePlayerZone.transform.GetChild(i).GetComponent<VoxelTile>().tilePrice).ToString();
        }
    }
}

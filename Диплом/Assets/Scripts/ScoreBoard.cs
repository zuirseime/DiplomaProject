using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public GameObject redPlayerZone, bluePlayerZone, redScore, blueScore, roundCounter, Player1Pos, Player2Pos, winPanel, Player1Win, Player2Win;
    private int tempRedScore, tempBlueScore, tempRoundCounter = 0;
    private Text RedScoreText, BlueScoreText;
    public void OnEnable()
    {
        RedScoreText = redScore.GetComponent<Text>();
        BlueScoreText = blueScore.GetComponent<Text>();
    }

    public void RoundWrite()
    {
        tempRoundCounter += 1;
        roundCounter.GetComponent<Text>().text = (tempRoundCounter).ToString();
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
    public void Update()
    {
        if (int.Parse(RedScoreText.text) >= 10000)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
            Player1Win.SetActive(true);
            Player2Win.SetActive(false);
        }
        if (int.Parse(BlueScoreText.text) >= 10000)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
            Player2Win.SetActive(true);
            Player1Win.SetActive(false);
        }

        if (int.Parse(RedScoreText.text) < -100)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
            Player1Win.SetActive(true);
            Player2Win.SetActive(false);
        }
        if (int.Parse(BlueScoreText.text) < -100)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
            Player2Win.SetActive(true);
            Player1Win.SetActive(false);
        }

        if (winPanel.activeSelf)
        {
            for (int i = 0; i < redPlayerZone.transform.childCount; i++)
                redPlayerZone.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 0;
            for (int i = 0; i < bluePlayerZone.transform.childCount; i++)    
                bluePlayerZone.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 0;
        }
        else
        {
            for (int i = 0; i < redPlayerZone.transform.childCount; i++)
                redPlayerZone.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 4;
            for (int i = 0; i < bluePlayerZone.transform.childCount; i++)    
                bluePlayerZone.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 4;
        }

        if (int.Parse(RedScoreText.text) > int.Parse(BlueScoreText.text))
        {
            Player1Pos.transform.localPosition = new Vector3 (-100f, 170f, 0);
            Player2Pos.transform.localPosition = new Vector3 (-100f, 91f, 0);

            Player1Pos.transform.Find("Pos").GetComponent<Text>().text = "1st";
            Player2Pos.transform.Find("Pos").GetComponent<Text>().text = "2nd";

            Player1Pos.transform.Find("Pos").GetComponent<Text>().color = new Color(255, 230, 0, 255);
            Player2Pos.transform.Find("Pos").GetComponent<Text>().color = new Color(217, 212, 203, 255);
        }
        else if (int.Parse(BlueScoreText.text) > int.Parse(RedScoreText.text))
        {
            Player2Pos.transform.localPosition = new Vector3 (-100f, 170f, 0);
            Player1Pos.transform.localPosition = new Vector3 (-100f, 91f, 0);

            Player2Pos.transform.Find("Pos").GetComponent<Text>().text = "1st";
            Player1Pos.transform.Find("Pos").GetComponent<Text>().text = "2nd";

            Player2Pos.transform.Find("Pos").GetComponent<Text>().color = new Color(255, 230, 0, 255);
            Player1Pos.transform.Find("Pos").GetComponent<Text>().color = new Color(217, 212, 203, 255);
        }
    }
}

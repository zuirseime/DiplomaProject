using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public GameObject[] Player1UI;
    public GameObject[] Player2UI;
    

    void Start()
    {
        Player1UI = GameObject.FindGameObjectsWithTag("Player1UI");
        Player2UI = GameObject.FindGameObjectsWithTag("Player2UI");
        
        for (int i = 0; i < Player1UI.Length; i++)
            {
                Player1UI[i].SetActive(true);
            }

        for (int i = 0; i < Player2UI.Length; i++)
            {
                Player2UI[i].SetActive(false);
            }
    }   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + 180, 0));

            for (int i = 0; i < Player1UI.Length; i++)
            {
                Player1UI[i].SetActive(!Player1UI[i].activeSelf);
            }

            for (int i = 0; i < Player2UI.Length; i++)
            {
                Player2UI[i].SetActive(!Player2UI[i].activeSelf);
            }
        }
    }
}

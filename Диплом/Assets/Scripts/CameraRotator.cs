using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public GameObject Player1UI;
    public GameObject Player2UI; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + 180, 0));

            Player1UI.SetActive(!Player1UI.activeSelf);
            Player2UI.SetActive(!Player2UI.activeSelf);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private bool RoundEnd = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (RoundEnd == false)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                RoundEnd = true;
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                RoundEnd = false;
            }
        }
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed;
    private float step = 0.5f;
    public float moveDistance;

    private void Start()
    {
    }

    void CameraMove(int minValue, int maxValue)
    {
        if (transform.position.x >= minValue && transform.position.z >= minValue && transform.position.x <= maxValue && transform.position.z <= maxValue)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x + (Time.deltaTime * moveDistance), transform.position.y, transform.position.z + (Time.deltaTime * moveDistance));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x - (Time.deltaTime * moveDistance), transform.position.y, transform.position.z - (Time.deltaTime * moveDistance));
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - (Time.deltaTime * moveDistance), transform.position.y, transform.position.z + (Time.deltaTime * moveDistance));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + (Time.deltaTime * moveDistance), transform.position.y, transform.position.z - (Time.deltaTime * moveDistance));
            }
        }
        else if (transform.position.x < minValue)
        {
            transform.position = new Vector3(transform.position.x + (Time.deltaTime * moveDistance), transform.position.y, transform.position.z);
        }
        else if (transform.position.z < minValue)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (Time.deltaTime * moveDistance));
        }
        else if (transform.position.x > maxValue)
        {
            transform.position = new Vector3(transform.position.x - (Time.deltaTime * moveDistance), transform.position.y, transform.position.z);
        }
        else if (transform.position.z > maxValue)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (Time.deltaTime * moveDistance));
        }
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (transform.position.y >= 3 && transform.position.y <= 10)
        {
            transform.position += transform.forward * scroll * zoomSpeed;
        }
        else if (transform.position.y < 3)
        {
            transform.position = new Vector3(transform.position.x - step, 3, transform.position.z - step);
        }
        else if (transform.position.y > 10)
        {
            transform.position = new Vector3(transform.position.x + step, 10, transform.position.z + step);
        }

        if (transform.position.y <= 4)
        {
            int minValue = -8;
            int maxValue = 5;
            CameraMove(minValue, maxValue);
        }
        else if (transform.position.y > 4)
        {
            int minValue = -10;
            int maxValue = 0;
            CameraMove(minValue, maxValue);
        }
    }
}

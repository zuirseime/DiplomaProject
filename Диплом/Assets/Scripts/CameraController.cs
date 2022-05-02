using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed;
    public float moveDistance;
    private Camera MainCamera;

    private GameObject Hand;

    private void Start()
    {
        MainCamera = GetComponent<Camera>();
        Hand = GameObject.Find("Hand");
    }

    void CameraMove(float minCorner, float maxCorner)
    {
        if (transform.position.x >= minCorner && transform.position.z >= minCorner && transform.position.x <= maxCorner && transform.position.z <= maxCorner)
        {
            if (Input.GetKey(KeyCode.W))
                transform.position = new Vector3(transform.position.x + Time.deltaTime * moveDistance, transform.position.y, transform.position.z + Time.deltaTime * moveDistance);
            if (Input.GetKey(KeyCode.S))
                transform.position = new Vector3(transform.position.x - Time.deltaTime * moveDistance, transform.position.y, transform.position.z - Time.deltaTime * moveDistance);
            if (Input.GetKey(KeyCode.A))
                transform.position = new Vector3(transform.position.x - Time.deltaTime * moveDistance, transform.position.y, transform.position.z + Time.deltaTime * moveDistance);
            if (Input.GetKey(KeyCode.D))
                transform.position = new Vector3(transform.position.x + Time.deltaTime * moveDistance, transform.position.y, transform.position.z - Time.deltaTime * moveDistance);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCorner, maxCorner), transform.position.y, Mathf.Clamp(transform.localPosition.z, minCorner, maxCorner));
        Hand.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Update()
    {
        int minSize = 1;
        int maxSize = 5;

        float minCorner = -6f;
        float maxCorner = 6f;

        if (Input.mouseScrollDelta.y != 0 && MainCamera.orthographicSize >= minSize && MainCamera.orthographicSize <= maxSize) 
            MainCamera.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
        else
            MainCamera.orthographicSize = Mathf.Clamp(MainCamera.orthographicSize, minSize, maxSize);

        CameraMove(minCorner, maxCorner);
    }
}

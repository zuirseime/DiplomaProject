using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 offset;
    Vector3 lastPosition;
    public string destinationTag = "GameArea";

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
        Cursor.visible = false;
        lastPosition = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 worldPos = MouseWorldPosition();
        transform.position = new Vector3(worldPos.x, 1f, worldPos.z) + offset;
        if (Input.GetMouseButtonDown(1))
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90f, transform.rotation.eulerAngles.z));
        }
    }

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            if (hit.transform.tag == destinationTag)
            {
                transform.position = hit.transform.position;
                transform.tag = "Lying";
            }
            else
            {
                transform.position = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z);
                transform.GetComponent<Collider>().enabled = true;
            }
        }
        Cursor.visible = true;
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}

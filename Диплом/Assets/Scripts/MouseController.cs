using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 lastPosition;
    private Vector3 lastRotation;
    private string newTag = "Lying";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = CastRay();
            if (selectedObject == null)
            {
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag") && !hit.collider.CompareTag("TownHall"))
                    {
                        return;
                    }
                    else{
                        selectedObject = hit.collider.gameObject;
                        selectedObject.transform.GetComponent<MeshCollider>().enabled = false;
                        lastPosition = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
                        Cursor.visible = false;
                    }
                }
            }
            else 
            {
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("GameArea"))
                    {
                        selectedObject.transform.position = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z);
                        selectedObject.transform.GetComponent<MeshCollider>().enabled = true;
                    }
                    else{
                        //VoxelTile.IsAcceptable();
                        selectedObject.transform.position = hit.transform.position;
                        selectedObject.transform.SetParent(GameObject.FindGameObjectWithTag("Finish").transform);
                        selectedObject.transform.tag = newTag;
                        Cursor.visible = false;
                    }
                }
                selectedObject = null;
                Cursor.visible = true;
            }
        }
        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, lastPosition.y + .5f, worldPosition.z);
            if (Input.GetMouseButtonDown(1))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(selectedObject.transform.rotation.eulerAngles.x, selectedObject.transform.rotation.eulerAngles.y + 90f, selectedObject.transform.rotation.eulerAngles.z));
            }
        }      
    }

    private RaycastHit CastRay() 
    {
        Vector3 screenMousePositionFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePositionFar = Camera.main.ScreenToWorldPoint(screenMousePositionFar);
        Vector3 worldMousePositionNear = Camera.main.ScreenToWorldPoint(screenMousePositionNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePositionNear, worldMousePositionFar - worldMousePositionNear, out hit);

        return hit;
    }
}

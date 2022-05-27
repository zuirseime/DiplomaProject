using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 lastPosition;
    private Vector3 lastRotation;
    private string newTag = "Lying";
    private GameObject pauseMenu;
    public GameObject player1Zone;
    public GameObject player2Zone;
    private Outline outline;

    private void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        outline = GetComponent<Outline>();
    }
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
                    else
                    {
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
                    if (!pauseMenu.activeSelf)
                    {
                        if (!hit.collider.CompareTag("GameArea"))
                        {
                            selectedObject.transform.position = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z);
                            selectedObject.transform.GetComponent<MeshCollider>().enabled = true;
                        }
                        else{
                            Cursor.visible = false;

                            if (selectedObject.transform.parent == GameObject.Find("Player1CardArea").transform)
                            {
                                outline.OutlineColor = Color.red;
                                outline.OutlineWidth = 4;
                                
                            }
                            else if (selectedObject.transform.parent == GameObject.Find("Player2CardArea").transform)
                            {
                                outline.OutlineColor = Color.blue;
                                outline.OutlineWidth = 4;
                            }
                            selectedObject.transform.SetParent(GameObject.FindGameObjectWithTag("Finish").transform);
                            selectedObject.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + .05f, hit.transform.position.z);
                            selectedObject.transform.tag = newTag;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                selectedObject = null;
                Cursor.visible = true;
            }
        }
        if (selectedObject != null)
        {
            if (!pauseMenu.activeSelf)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, lastPosition.y + .5f, worldPosition.z);
                if (Input.GetMouseButtonDown(1))
                {
                    selectedObject.transform.rotation = Quaternion.Euler(new Vector3(selectedObject.transform.rotation.eulerAngles.x, selectedObject.transform.rotation.eulerAngles.y + 90f, selectedObject.transform.rotation.eulerAngles.z));
                }
            }
            else
            {
                selectedObject.transform.position = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z);
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
        Physics.Raycast(worldMousePositionNear, worldMousePositionFar - worldMousePositionNear, out hit, 100);

        return hit;
    }
}

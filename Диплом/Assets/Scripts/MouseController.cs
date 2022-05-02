using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private GameObject selectedObject;
    private GameObject SelectedCell;
    Transform DefaultParent, DefaultTempCardParent;

    private void Awake()
    {
        SelectedCell = GameObject.Find("SelectedCell");
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;

                    DefaultParent = DefaultTempCardParent = transform.parent;

                    SelectedCell.transform.SetParent(DefaultParent);

                    transform.SetParent(DefaultParent.parent);
                }
            }
            else 
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(Mathf.Round(worldPosition.x) - .5f, 0f, Mathf.Round(worldPosition.z) - .5f);

                selectedObject = null;
                Cursor.visible = true;

                transform.SetParent(DefaultParent);
                SelectedCell.transform.SetParent(GameObject.Find("GameArea").transform);
                SelectedCell.transform.localPosition = new Vector3(0, -1, 0);
            }
        }

        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, .3f, worldPosition.z);

            SelectedCell.transform.localPosition = new Vector3(Mathf.Round(worldPosition.x), 0f, Mathf.Round(worldPosition.z));

        
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

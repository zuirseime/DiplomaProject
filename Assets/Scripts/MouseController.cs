using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 lastPosition, lastRotation;
    private Outline outline;
    private VoxelTile[,] placedTiles;

    public GameObject player1UI, player2UI, playerSwap, pauseMenu, redPlayerZone, bluePlayerZone;
    [HideInInspector] public bool isMovesPossible = true;

    public void IsMovesPossible(bool newValue)
    {
        isMovesPossible = newValue;
    }
    private void Awake()
    {
        placedTiles = new VoxelTile[13, 13];
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMovesPossible)
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
                        outline = selectedObject.transform.GetComponent<Outline>();
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
                        PlaceNotAvailable();
                    }
                    else
                    {
                        Vector3 pos = hit.transform.position;
                        bool ForwardAvailable = false;
                        bool BackAvailable = false;
                        bool LeftAvailable = false;
                        bool RightAvailable = false;
                        if (!IsPlaceTaken((int)(pos.x), (int)(pos.z)))
                        {
                            if (placedTiles[(int)(pos.x + 1), (int)(pos.z)] != null)
                                if (selectedObject.GetComponent<VoxelTile>().Right != placedTiles[(int)(pos.x + 1), (int)(pos.z)].Left) RightAvailable = false;
                                else RightAvailable = true;
                            else RightAvailable = true;
                            if (placedTiles[(int)(pos.x - 1), (int)(pos.z)] != null)
                                if (selectedObject.GetComponent<VoxelTile>().Left != placedTiles[(int)(pos.x - 1), (int)(pos.z)].Right) LeftAvailable = false;
                                else LeftAvailable = true;
                            else LeftAvailable = true;
                            if (placedTiles[(int)(pos.x), (int)(pos.z + 1)] != null)
                                if (selectedObject.GetComponent<VoxelTile>().Forward != placedTiles[(int)(pos.x), (int)(pos.z + 1)].Back) ForwardAvailable = false;
                                else ForwardAvailable = true;
                            else ForwardAvailable = true;
                            if (placedTiles[(int)(pos.x), (int)(pos.z - 1)] != null)
                                if (selectedObject.GetComponent<VoxelTile>().Back != placedTiles[(int)(pos.x), (int)(pos.z - 1)].Forward) BackAvailable = false;
                                else BackAvailable = true;
                            else BackAvailable = true;
                            if (ForwardAvailable && BackAvailable && LeftAvailable && RightAvailable)
                            {
                                PlaceAvailable(pos, hit);
                            }
                            else return;
                        }
                        else return;
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
                selectedObject.GetComponent<VoxelTile>().TileRotator();
            }

            if (pauseMenu.activeSelf)
            {
                selectedObject.transform.position = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z);
                selectedObject.transform.GetComponent<MeshCollider>().enabled = true;
                selectedObject = null;
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

    private void PlaceNotAvailable()
    {
        selectedObject.transform.position = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z);
        selectedObject.transform.GetComponent<MeshCollider>().enabled = true;
    }

    private void PlaceAvailable(Vector3 pos, RaycastHit hit)
    {
        if (selectedObject.transform.parent == GameObject.Find("Player1CardArea").transform)
        {
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 4;
            selectedObject.transform.name += $" X: {(int)(pos.x)} Z: {(int)(pos.z)}";
            player1UI.SetActive(false);
            selectedObject.transform.SetParent(redPlayerZone.transform);
        }
        else if (selectedObject.transform.parent == GameObject.Find("Player2CardArea").transform)
        {
            outline.OutlineColor = Color.blue;
            outline.OutlineWidth = 4;
            selectedObject.transform.name += $" X: {(int)(pos.x)} Z: {(int)(pos.z)}";
            player2UI.SetActive(false);
            selectedObject.transform.SetParent(bluePlayerZone.transform);
        }
        selectedObject.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + .05f, hit.transform.position.z);
        placedTiles[(int)(pos.x), (int)(pos.z)] = selectedObject.GetComponent<VoxelTile>();
        playerSwap.SetActive(true);
        isMovesPossible = false;
    }

    private bool IsPlaceTaken(int x, int z)
    {
        if (placedTiles[x, z] != null) return true;
        return false;
    }
}

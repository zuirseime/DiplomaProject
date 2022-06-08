using System;
using UnityEngine;

public class VoxelTile : MonoBehaviour
{
    private Camera mainCamera;
    public Transform Forward, Back, Right, Left;
    private Transform tempForward, tempBack, tempRight, tempLeft;
    private Outline outline;
    private VoxelTile previousTile;
    public int tilePrice;

    public void TileRotator()
    {
        tempForward = Forward;
        tempBack = Back;
        tempRight = Right;
        tempLeft = Left;

        Forward = tempLeft;
        Left = tempBack;
        Back = tempRight;
        Right = tempForward;
    }

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
        outline.OutlineWidth = 0;
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 100))
        {
            var tile = hit.collider.GetComponent<VoxelTile>();
            if (tile != null)
            {
                if (tile == this && tile != previousTile)
                {
                    outline.OutlineWidth = 4;
                    previousTile = tile;
                }
            }
            else if (previousTile != null)
            {
                    outline.OutlineWidth = 0;
                    previousTile = null;
            }
        }
    }
}

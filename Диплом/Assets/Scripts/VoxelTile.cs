using System;
using UnityEngine;

public class VoxelTile : MonoBehaviour
{
    private Camera mainCamera;

    public Transform Forward;
    public Transform Back;
    public Transform Right;
    public Transform Left;

    private Transform tempForward;
    private Transform tempBack;
    private Transform tempRight;
    private Transform tempLeft;

    private Outline outline;
    private VoxelTile previousTile;

    public void OnHoverEnter()
    {
        outline.OutlineWidth = 4;
    }

    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
    }

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
                    OnHoverEnter();
                    previousTile = tile;
                }
            }
            else if (previousTile != null)
            {
                    OnHoverExit();
                    previousTile = null;
            }
        }
    }
}

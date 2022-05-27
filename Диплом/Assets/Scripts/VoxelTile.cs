using System;
using UnityEngine;

public class VoxelTile : MonoBehaviour
{
    private Camera mainCamera;

    public int TileWidth = 64;
    public int TileHeight = 6;
    public float VoxelSize = 1 / 64;

    [HideInInspector] public int[] ColorsForward;
    [HideInInspector] public int[] ColorsLeft;
    [HideInInspector] public int[] ColorsRight;
    [HideInInspector] public int[] ColorsBack;

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

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
        outline.OutlineWidth = 0;


        /*ColorsRight = new int[TileWidth];
        ColorsForward = new int[TileWidth];
        ColorsLeft = new int[TileWidth];
        ColorsBack = new int[TileWidth];

        for (int y = 5; y < TileHeight; y++)
        {
            for (int x = 0; x < TileWidth; x++)
            {
                ColorsRight[x] = (GetVoxelColor(y, x, Vector3.right));
                ColorsForward[x] = (GetVoxelColor(y, x, Vector3.forward));
                ColorsLeft[x] = (GetVoxelColor(y, x, Vector3.left));
                ColorsBack[x] = (GetVoxelColor(y, x, Vector3.back));
            }
        }

        Debug.Log("<color=blue>Right</color>\n" + string.Join(", ", ColorsRight), gameObject);
        Debug.Log("<color=blue>Forward</color>\n" + string.Join(", ", ColorsForward), gameObject);
        Debug.Log("<color=blue>Left</color>\n" + string.Join(", ", ColorsLeft), gameObject);
        Debug.Log("<color=blue>Back</color>\n" + string.Join(", ", ColorsBack), gameObject);*/
    }

    /*private int GetVoxelColor(int verticalLayer, int horizontalOffset, Vector3 direction)
    {
        var meshCollider = GetComponent<MeshCollider>();

        float vox = VoxelSize;
        float half = vox / 2;
        Vector3 rayStart;

        if (direction == Vector3.right)
        {
            rayStart = meshCollider.bounds.min + new Vector3(-half, 0, half + horizontalOffset * vox);
        }
        else if (direction == Vector3.forward)
        {
            rayStart = meshCollider.bounds.min + new Vector3(half + horizontalOffset * vox, 0, -half);
        }
        else if (direction == Vector3.left)
        {
            rayStart = meshCollider.bounds.min + new Vector3(half + vox * TileWidth, 0, -half + (TileWidth - horizontalOffset) * vox);
        }
        else if (direction == Vector3.back)
        {
            rayStart = meshCollider.bounds.min + new Vector3(-half + (TileWidth - horizontalOffset) * vox, 0, half + vox * TileWidth);
        }
        else
        {
            throw new ArgumentException("Wrong direction value, should be Vector.forward/left/right/back", nameof(direction));
        }

        rayStart.y = meshCollider.bounds.min.y + half + verticalLayer * vox;

        Debug.DrawRay(rayStart, direction*.01f, Color.blue, vox*2);

        if (Physics.Raycast(new Ray(rayStart, direction), out RaycastHit hit, vox*2))
        {
            Mesh mesh = meshCollider.sharedMesh;
            int hitTriangleVertex =  mesh.triangles[hit.triangleIndex*3+1];
            int colorIndex = (int)(mesh.uv[hitTriangleVertex].x * 256);
            return colorIndex;
        }

        return -1;
    }*/

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

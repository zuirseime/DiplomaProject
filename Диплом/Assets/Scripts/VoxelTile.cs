using System;
using UnityEngine;

public class VoxelTile : MonoBehaviour
{
    public void Start()
    {
        Debug.Log(GetVoxelColor(5, 0));
    }

    public float VoxelSize = 1/65f;
    public int TileSideVoxels = 65;

    private int GetVoxelColor(Int64 verticalLayer, Int64 horizontalOffset)
    {
        var meshCollider = GetComponentInChildren<MeshCollider>();

        Vector3 rayStart = meshCollider.bounds.min + new Vector3((.5f + horizontalOffset) * VoxelSize, .5f * VoxelSize + verticalLayer * VoxelSize, -.5f * VoxelSize);

        Debug.DrawRay(rayStart, Vector3.forward*.01f, Color.blue, 50);

        if (Physics.Raycast(new Ray(rayStart, Vector3.forward), out RaycastHit hit, VoxelSize))
        {
            Mesh mesh = meshCollider.sharedMesh;
            int hitTriangleVertex =  mesh.triangles[hit.triangleIndex*3];
            int colorIndex = (int)(mesh.uv[hitTriangleVertex].x * 256);
            return colorIndex;
        }

        return 256;
    }
}

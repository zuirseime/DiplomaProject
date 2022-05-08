using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Cell prefab;
    [SerializeField] private float offset;
    [SerializeField] private Transform parent;

    [ContextMenu("Generate grid")]

    private void GenerateGrid()
    {
        var cellsize = prefab.GetComponent<MeshRenderer>().bounds.size;

        for (int x = -gridSize.x/2; x < gridSize.x/2 + 1; x++)
        {
            for (int y = -gridSize.y/2; y < gridSize.y/2 + 1; y++)
            {
                var position = new Vector3(x * (cellsize.x + offset), -.5f, y * (cellsize.z + offset));
                var cell = Instantiate(prefab, position, Quaternion.identity, parent);
                cell.name = $"X: {x} Z: {y}";
            }
        }
    }
}

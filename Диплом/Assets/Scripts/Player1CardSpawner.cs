using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1CardSpawner : MonoBehaviour
{
    [SerializeField] private Vector2Int cardNum;
    [SerializeField] private float offset;
    [SerializeField] private Transform parent;
    [SerializeField] public GameObject[] objects;

    public void OnClick()
    {
        var cellsize = objects[0].GetComponent<MeshRenderer>().bounds.size;
 
        for (int x = -cardNum.x/2; x < cardNum.x/2 + 1; x++)
        {
            int rand = Random.Range(0, objects.Length - 1);
            var position = new Vector3(x * (cellsize.x + offset), parent.localPosition.y, parent.localPosition.z);
            var cell = Instantiate(objects[rand], position, Quaternion.identity, parent);
            cell.name = $"X: {x}";
        }
    }
}

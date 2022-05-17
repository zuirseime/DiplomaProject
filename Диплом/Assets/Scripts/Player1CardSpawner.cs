using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1CardSpawner : MonoBehaviour
{
    [SerializeField] private Vector2Int cardNum;
    [SerializeField] private float offset;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] objects;

    public void OnClick()
    {
        var cellsize = objects[0].GetComponentInChildren<MeshRenderer>().bounds.size;

        if (parent.childCount != 0)
        {
            for (int x = 0; x < cardNum.x; x++)
            {
                Destroy(parent.GetChild(x).gameObject);
            }
        }

        for (int x = -cardNum.x/2; x < cardNum.x/2 + 1; x++)
        {
            int rand = Random.Range(0, objects.Length - 1);
            int randAngle = Random.Range(0, 360);
            var position = new Vector3(x * (cellsize.x + offset), parent.localPosition.y, parent.localPosition.z);

            if (randAngle > 315 || randAngle <= 45)
            {
                var cell = Instantiate(objects[rand], position, Quaternion.Euler(0, 0, 0), parent);
                cell.name = $"X: {x}";
            }
            else if (randAngle > 45 || randAngle <= 135)
            {
                var cell = Instantiate(objects[rand], position, Quaternion.Euler(0, 90, 0), parent);
                cell.name = $"X: {x}";
            }
            else if (randAngle > 135 || randAngle <= 225)
            {
                var cell = Instantiate(objects[rand], position, Quaternion.Euler(0, 180, 0), parent);
                cell.name = $"X: {x}";
            }
            else if (randAngle > 225 || randAngle <= 315)
            {
                var cell = Instantiate(objects[rand], position, Quaternion.Euler(0, 270, 0), parent);
                cell.name = $"X: {x}";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1CardSpawner : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private GameObject TownHall;
    [SerializeField] private GameObject townHallWarning;

    private void Awake()
    {

    }

    public void OnClick()
    {
        var cellsize = objects[0].GetComponent<MeshRenderer>().bounds.size;

        if (TownHall.transform.parent != parent)
        {
            if (parent.childCount > 0)
            {
                for (int x = 0; x < parent.childCount; x++)
                {
                    Destroy(parent.GetChild(x).gameObject);
                }
            }
            for (int x = -2; x <= 2; x++)
            {
                int rand = Random.Range(0, objects.Length - 1);
                var position = new Vector3(parent.localPosition.x + x * (cellsize.x + offset), parent.localPosition.y, parent.localPosition.z);
                var cell = Instantiate(objects[rand], position, Quaternion.identity, parent);
            }
        }
        else
        {
            StartCoroutine(TownHallWarning());
        }  
    }

    private IEnumerator TownHallWarning()
    {
        townHallWarning.SetActive(true);
        yield return new WaitForSeconds(.75f);
        townHallWarning.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2CardSpawner : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private GameObject TownHall, townHallWarning, penalty, score;

    private void Awake()
    {
        
    }

    public void OnClick()
    {
        if (int.Parse(score.GetComponent<Text>().text) <= -100)
        {
            Debug.Log("Гравець <color=red>USERNAME</color> переміг");
            Time.timeScale = 0f;
        }
        var cellsize = objects[0].GetComponent<MeshRenderer>().bounds.size;

        if (TownHall.transform.parent != parent)
        {
            if (parent.childCount > 0)
            {
                penalty.GetComponent<Animation>().Play();
                score.GetComponent<Text>().text =  (int.Parse(score.GetComponent<Text>().text) - 25).ToString();
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

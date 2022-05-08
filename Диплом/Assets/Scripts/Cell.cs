using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Color standartColor;
    [SerializeField] private Color hoverColor;

    [SerializeField] private MeshRenderer meshRenderer;

    private void ChangeColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    private void OnMouseEnter()
    {
        ChangeColor(hoverColor);
    }
    private void OnMouseExit()
    {
        ChangeColor(standartColor);
    }
}

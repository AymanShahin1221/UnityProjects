using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMouseCoords : MonoBehaviour
{
    // mouse position text object
    public Text mousePosObj;

    void Update()
    {
        // display current mouse position
        Vector2 coords = Input.mousePosition;
        mousePosObj.text = "Mouse Coordinates (x, y): " + coords.ToString();
    }
}

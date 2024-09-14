using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using static System.Net.Mime.MediaTypeNames;
using Unity.Mathematics;

public class DrawShape : MonoBehaviour
{
    // current shape that will be drawn on the screen
    private string selectedShape;

    // current color that will be applied to the shapes
    private string selectedColor;

    // bool to keep track of erase mode
    private bool eraseMode;

    // default material for the shapes
    public Material defaultMaterial;

    // current scale factor of shapes
    public float currentSize;

    // bool to keep track of whether or not user want to randomize size
    public bool isScaleRandom;

    void Start()
    {
        // initialize first shape to be cube since it is first in the dropdown
        selectedShape = "cube";

        // initialize first color to be blue since it is first in the dropdown
        selectedColor = "blue";

        // bool to keep track of erase mode
        eraseMode = false;

        // by default, set to 1
        currentSize = 1.0f;

        // intialized to false since toggle is not checked
        isScaleRandom = false;
    }

    void Update()
    {
        // current mouse position
        Vector2 coords = Input.mousePosition;

        // draw shapes on screen when left mouse click is held
        if (Input.GetMouseButton(0) && !eraseMode)
        {
            switch (selectedShape)
            {
                case "cube":
                    drawCube(coords, currentSize);
                    break;

                case "cylinder":
                    drawCylinder(coords, currentSize);
                    break;

                case "capsule":
                    drawCapsule(coords, currentSize);
                    break;

                case "sphere":
                    drawSphere(coords, currentSize);
                    break;

                case "random":
                    drawRandom(coords);
                    break;
            }
        }

        // toggle erase mode on/off
        if (Input.GetMouseButtonDown(1))
            eraseMode = !eraseMode;

        // right click to enter erase mode
        // while in erase mode, hold left click to erase shapes
        if(eraseMode && Input.GetMouseButton(0))
            eraseShapes(coords);

        if (isScaleRandom)
        {
            System.Random random = new System.Random();
            currentSize = (float) (random.NextDouble() * 1.5) + 1f;
        }
    }

    public void handleDropdown(int choice)
    {
        // obtain selected shape from dropdown menu
        switch (choice)
        {
            case 0:
                selectedShape = "cube";
                break;

            case 1:
                selectedShape = "cylinder";
                break;

            case 2:
                selectedShape = "capsule";
                break;

            case 3:
                selectedShape = "sphere";
                break;

            case 4:
                selectedShape = "random";
                break;
        }
    }

    public void handleColor(int choice)
    {
        // obtain selected color from dropdown menu
        switch (choice)
        {
            case 0:
                selectedColor = "blue";
                break;

            case 1:
                selectedColor = "red";
                break;

            case 2:
                selectedColor = "purple";
                break;

            case 3:
                selectedColor = "yellow";
                break;

            case 4:
                selectedColor = "green";
                break;

            case 5:
                selectedColor = "orange";
                break;

            case 6:
                selectedColor = "random";
                break;
        }
    }

    // draw shape at the current mouse position
    private void drawCube(Vector2 coords, float scale)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        cube.AddComponent<BoxCollider>();

        cube.transform.localScale = new Vector3(scale, scale, scale);

        cube.GetComponent<Renderer>().material = defaultMaterial;
        setColor(cube.GetComponent<Renderer>());

        cube.tag = "DrawnCube";
    }

    private void drawCylinder(Vector2 coords, float scale)
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        cylinder.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        cylinder.AddComponent<BoxCollider>();

        cylinder.transform.localScale = new Vector3(scale, scale, scale);

        cylinder.GetComponent<Renderer>().material = defaultMaterial;
        setColor(cylinder.GetComponent<Renderer>());

        cylinder.tag = "DrawnCylinder";
    }

    private void drawCapsule(Vector2 coords, float scale)
    {
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        capsule.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        capsule.AddComponent<BoxCollider>();

        capsule.transform.localScale = new Vector3(scale, scale, scale);

        capsule.GetComponent<Renderer>().material = defaultMaterial;
        setColor(capsule.GetComponent<Renderer>());

        capsule.tag = "DrawnCapsule";
    }

    private void drawSphere(Vector2 coords, float scale)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        sphere.AddComponent<BoxCollider>();

        sphere.transform.localScale = new Vector3(scale, scale, scale);

        sphere.GetComponent<Renderer>().material = defaultMaterial;
        setColor(sphere.GetComponent<Renderer>());

        sphere.tag = "DrawnSphere";
    }

    private void drawRandom(Vector2 coords)
    {
        string[] shapes = { "cube", "cylinder", "capsule", "sphere" };

        // generate random index and assign a random shape
        System.Random rand = new System.Random();
        int randIndex = rand.Next(0, shapes.Length);
        string randShape = shapes[randIndex];

        // call the function for corresponding random shape
        switch (randShape)
        {
            case "cube":
                drawCube(coords, currentSize);
                break;

            case "cylinder":
                drawCylinder(coords, currentSize);
                break;

            case "capsule":
                drawCapsule(coords, currentSize);
                break;

            case "sphere":
                drawSphere(coords, currentSize);
                break;
        }
    }

    private void setColor(Renderer shapeRenderer)
    {
        switch (selectedColor)
        {
            case "blue":
                shapeRenderer.material.color = Color.blue;
                break;

            case "red":
                shapeRenderer.material.color = Color.red;
                break;

            case "purple":
                shapeRenderer.material.color = new Color(0.5f, 0, 0.5f);
                break;

            case "yellow":
                shapeRenderer.material.color = Color.yellow;
                break;

            case "green":
                shapeRenderer.material.color = Color.green;
                break;

            case "orange":
                shapeRenderer.material.color = new Color(1, 0.5f, 0);
                break;

            case "random":
                System.Random rand = new System.Random();
                shapeRenderer.material.color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
                break;
        }
    }

    public void adjustSize(string scaleText)
    {
        if(scaleText != "")
            currentSize = float.Parse(scaleText);

        else
            currentSize = 1.0f;
    }

    public void randomizeSize(bool randomize)
    {
        if (randomize)
        {
            isScaleRandom = true;
        }
        else 
        {
            isScaleRandom = false;
            currentSize = 1.0f;
        }
    }

    private void eraseShapes(Vector2 coords)
    {
        Ray ray = Camera.main.ScreenPointToRay(coords);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}

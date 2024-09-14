using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using Color = UnityEngine.Color;
using UnityEditor;
using Unity.VisualScripting;
using System.Collections.Generic;


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

    // bool to keep track of whether or not user wants to randomize size
    public bool isScaleRandom;

    // default background
    public string selectedBackground;

    // input fields for rotation
    public InputField xRotationField;
    public InputField yRotationField;
    public InputField zRotationField;

    // current rotation
    private float currentXRotation;
    private float currentYRotation;
    private float currentZRotation;

    // current eraseSize
    private float eraseSize;

    // bool to keep track of whether or not user wants to randomize rotation values 
    public bool isRotationRandom;

    public bool enableDynamicBackground;

    public Dropdown preDrawnShapesDropdown;

    public GameObject housePrefab;
    public GameObject skyscraperPrefab;

    public bool housePrefabInstantiated;
    public bool skyscraperPrefabInstantiated;

    public bool animate;

    public Canvas canvas;

    public float currentAnimationSpeed;


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

        isRotationRandom = false;
        
        // initialize current background
        selectedBackground = "White";
        
        currentXRotation = 0.0f;
        currentYRotation = 0.0f;
        currentZRotation = 0.0f;

        eraseSize =  0.5f;

        housePrefabInstantiated = false;
        skyscraperPrefabInstantiated = false;

        animate = false;

        currentAnimationSpeed = 1.0f;
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
                    drawCube(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
                    housePrefabInstantiated = false;
                    skyscraperPrefabInstantiated = false;
                    break;

                case "cylinder":
                    drawCylinder(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
                    housePrefabInstantiated = false;
                    skyscraperPrefabInstantiated = false;
                    break;

                case "capsule":
                    drawCapsule(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
                    housePrefabInstantiated = false;
                    skyscraperPrefabInstantiated = false;
                    break;

                case "sphere":
                    drawSphere(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
                    housePrefabInstantiated = false;
                    skyscraperPrefabInstantiated = false;
                    break;

                case "random":
                    drawRandom(coords);
                    housePrefabInstantiated = false;
                    skyscraperPrefabInstantiated = false;
                    break;

                case "housePrefab":
                    if (Input.GetMouseButtonDown(0))
                    {
                        InstantiatePrefabAtMousePosition(housePrefab);
                        housePrefabInstantiated = true;
                    }
                    break;

                case "skyscraperPrefab":
                    if (Input.GetMouseButtonDown(0))
                    {
                        InstantiatePrefabAtMousePosition(skyscraperPrefab);
                        skyscraperPrefabInstantiated = true;
                    }
                    break;
            }
        }

        //Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(coords);
        RaycastHit hit;

        // toggle erase mode on/off when user right clicks
        if (Input.GetMouseButtonDown(1))
            eraseMode = !eraseMode;

        // right click to enter erase mode
        // while in erase mode, hold left click to erase shapes
        // check if there are any hits
        if (Physics.Raycast(ray, out hit))
        {
            if (eraseMode && Input.GetMouseButton(0))
            {
                eraseShapes(hit.point, eraseSize);
            }
        }

        if (isScaleRandom)
        {
            System.Random random = new System.Random();
            currentSize = (float)(random.NextDouble() * 0.5) + 0.1f;
        }

        if (isRotationRandom)
        {
            currentXRotation = Random.Range(-180f, 180f);
            currentYRotation = Random.Range(-180f, 180f);
            currentZRotation = Random.Range(-180f, 180f);
        }
        
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        Renderer backgroundRenderer = background.GetComponent<Renderer>();
        switch (selectedBackground)
        {
            case "White":
                backgroundRenderer.material.color = Color.white;
                break;

            case "Black":
                backgroundRenderer.material.color = Color.black;
                break;

            case "Sky Blue":
                backgroundRenderer.material.color = new Color(0.529f, 0.808f, 0.922f);
                break;

            case "dynamic":
                break;

        }
        //if (preDrawnShapesDropdown != null && Input.GetMouseButtonDown(0) && preDrawnShapesDropdown.value != 0 && !eraseMode)
        //{
        //    switch (preDrawnShapesDropdown.value)
        //    {
        //        case 1:
        //            InstantiatePrefabAtMousePosition(housePrefab);
        //            break;

        //        case 2:
        //            InstantiatePrefabAtMousePosition(skyscraperPrefab);
        //            break;
        //    }
        //}
    }

    public void handleShape(int choice)
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

            case 5:
                selectedShape = "housePrefab";
                housePrefabInstantiated = true;
                break;

            case 6:
                selectedShape = "skyscraperPrefab";
                skyscraperPrefabInstantiated = true;
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

    public void handleBackground(int choice)
    {
        switch (choice)
        {
            case 0:
                selectedBackground = "White";
                break;

            case 1:
                selectedBackground = "Black";
                break;

            case 2:
                selectedBackground = "Sky Blue";
                break;
        }
    }


    // draw shape at the current mouse position
    private void drawCube(Vector2 coords, float scale, float xRotation, float yRotation, float zRotation)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        cube.AddComponent<MeshCollider>();

        cube.transform.localScale = new Vector3(scale, scale, scale);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        cube.transform.rotation = rotation;

        cube.GetComponent<Renderer>().material = defaultMaterial;
        setColor(cube.GetComponent<Renderer>());

        cube.tag = "DrawnCube";
    }

    private void drawCylinder(Vector2 coords, float scale, float xRotation, float yRotation, float zRotation)
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        cylinder.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        cylinder.AddComponent<MeshCollider>();

        cylinder.transform.localScale = new Vector3(scale, scale, scale);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        cylinder.transform.rotation = rotation;

        cylinder.GetComponent<Renderer>().material = defaultMaterial;
        setColor(cylinder.GetComponent<Renderer>());

        cylinder.tag = "DrawnCylinder";
    }

    private void drawCapsule(Vector2 coords, float scale, float xRotation, float yRotation, float zRotation)
    {
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        capsule.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        capsule.AddComponent<MeshCollider>();

        capsule.transform.localScale = new Vector3(scale, scale, scale);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        capsule.transform.rotation = rotation;

        capsule.GetComponent<Renderer>().material = defaultMaterial;
        setColor(capsule.GetComponent<Renderer>());

        capsule.tag = "DrawnCapsule";
    }

    private void drawSphere(Vector2 coords, float scale, float xRotation, float yRotation, float zRotation)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(coords.x, coords.y, 10));
        sphere.AddComponent<MeshCollider>();

        sphere.transform.localScale = new Vector3(scale, scale, scale);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        sphere.transform.rotation = rotation;

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
                drawCube(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
                break;

            case "cylinder":
                drawCylinder(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
                break;

            case "capsule":
                drawCapsule(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
                break;

            case "sphere":
                drawSphere(coords, currentSize, currentXRotation, currentYRotation, currentZRotation);
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
        if (scaleText != "")
            currentSize = float.Parse(scaleText);

        else
            currentSize = 1.0f;
    }

    public void adjustRotation()
    {

        if (xRotationField.text != "")
            currentXRotation = float.Parse(xRotationField.text);

        else 
            currentXRotation = 0.0f;
        
        if (yRotationField.text != "")
            currentYRotation = float.Parse(yRotationField.text);

        else
            currentYRotation = 0.0f;
        
        if (zRotationField.text != "")
            currentZRotation = float.Parse(zRotationField.text);

        else
            currentZRotation = 0.0f;
    }

    public void adjustEraseSize(float size)
    {
        eraseSize = size;
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

    public void randomizeRotation(bool randomize)
    {
        if(randomize)
        {
            isRotationRandom = true;
        }
        else
        {
            isRotationRandom = false;
            currentXRotation = 1.0f;
            currentYRotation = 1.0f;
            currentZRotation = 1.0f;
        }
    }

    //private void eraseShapes(Vector2 coords)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(coords);
    //    RaycastHit[] hits = Physics.RaycastAll(ray);

    //    foreach (RaycastHit hit in hits)
    //    {
    //        if (hit.collider != null)
    //        {
    //            if (!hit.collider.CompareTag("Background"))
    //            {
    //                Destroy(hit.collider.gameObject);
    //            }
    //        }
    //    }
    //}

    // erase shapes with given radius
    // larger radius means more shapes get erases
    //private void eraseShapes(Vector3 coords, float eraseRadius)
    //{
    //    Collider[] colliders = Physics.OverlapSphere(coords, eraseRadius);

    //    foreach (Collider col in colliders)
    //    {
    //        if (PrefabUtility.IsPartOfPrefabAsset(col.gameObject))
    //        {
    //            DestroyImmediate(col.gameObject, true);
    //        }

    //        if (!col.CompareTag("Background"))
    //        {
    //            Destroy(col.gameObject);
    //        }
    //    }
    //}
    private void eraseShapes(Vector3 coords, float eraseRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(coords, eraseRadius);

        foreach (Collider col in colliders)
        {
            if(col.CompareTag("DrawnPrefab"))
            {
                Destroy(col.transform.parent.gameObject);    
            }

            if (!col.CompareTag("Background"))
            {
                Destroy(col.gameObject); 
            }  
        }
    }


    // use time to allow user to change background
    // Morning --> random light color (6:00 - 11:59)
    // Afternoon --> random intermediate color (12:00 - 17:59)
    // Evening --> random dark color (18:00 - 23:59)
    // Midnight --> random darker color (0:00 - 5:59)

    // change background based on current time
    public void dynamicBackground(bool enable)
    {

        GameObject background = GameObject.FindGameObjectWithTag("Background");
        Renderer backgroundRenderer = background.GetComponent<Renderer>();
        if (enable)
        {
            DateTime currentTime = DateTime.Now;
            int currentHour = currentTime.Hour;

            Color color;
            if (currentHour >= 6 && currentHour < 12) // Morning 
                color = randColor(0.8f, 1f);

            else if (currentHour >= 12 && currentHour < 18) // Afternoon
                color = randColor(0.4f, 0.8f);

            else if (currentHour >= 18 && currentHour < 24) // Evening 
                color = randColor(0.2f, 0.4f);

            else // Night 
                color = randColor(0f, 0.2f);

            backgroundRenderer.material.color = color;

            selectedBackground = "dynamic";
        }
        else
        {
            backgroundRenderer.material.color = Color.white;
        }
    }

    //public void handlePreDrawnObject(int choice)
    //{
    //    switch (choice)
    //    {
    //        case 0:
    //            housePrefabInstantiated = false;
    //            housePrefabInstantiated = false;
    //            break;

    //        case 1:
    //            housePrefabInstantiated = true;
    //            skyscraperPrefabInstantiated = false;
    //            break;

    //        case 2:
    //            skyscraperPrefabInstantiated = true;
    //            housePrefabInstantiated = false;
    //            break;
    //    }
    //}

    public void animatePrefabs(bool status)
    {
        if (status)
            animate = true;
        else
            animate = false;
    }

    private void InstantiatePrefabAtMousePosition(GameObject prefab)
    {
        float canvasWidth = canvas.pixelRect.width;
        float canvasHeight = canvas.pixelRect.height;

        if (canvasWidth == 960 && canvasHeight == 600)
        {
            if(housePrefabInstantiated)
                prefab.transform.localScale = new Vector3(50, 50, 50);

            else if(skyscraperPrefabInstantiated)
            {
                prefab.transform.localScale = new Vector3(6f, 6f, 6f);
            }

        }
        
        else if (canvasWidth == 3840 && canvasHeight == 2160)
        {
            if (housePrefabInstantiated)
                prefab.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

            else if (skyscraperPrefabInstantiated)
                prefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }



        // Get the current mouse position
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(prefab, hit.point, Quaternion.identity);
            prefab.tag = "DrawnPrefab";
        }
    }

    public void changeAnimationSpeed(float speed)
    {
        currentAnimationSpeed = speed;
    }

    // creates random color with brightness range
    Color randColor(float min, float max)
    {
        float r = UnityEngine.Random.Range(min, max);
        float g = UnityEngine.Random.Range(min, max);
        float b = UnityEngine.Random.Range(min, max);

        return new Color(r, g, b); 
    }
}










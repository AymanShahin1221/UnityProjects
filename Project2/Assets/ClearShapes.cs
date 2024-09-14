using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClearShapes : MonoBehaviour
{
    public void clear(int choice)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        GameObject[] drawnShapes = allObjects.Where(obj => obj.tag.StartsWith("Drawn")).ToArray();

        switch (choice)
        {
            case 0:
                //
                break;

            case 1:
                deleteShapesByTagname(drawnShapes, "DrawnCube");
                break;

            case 2:
                deleteShapesByTagname(drawnShapes, "DrawnCylinder");
                break;

            case 3:
                deleteShapesByTagname(drawnShapes, "DrawnCapsule");
                break;

            case 4:
                deleteShapesByTagname(drawnShapes, "DrawnSphere");
                break;

            case 5:
                foreach (GameObject shape in drawnShapes)
                {
                    Destroy(shape);
                }
                break;
        }
    }

    private void deleteShapesByTagname(GameObject[] shapes, string tag)
    {
        GameObject[] shapesToDelete = shapes.Where(obj => obj.tag == tag).ToArray();
        foreach (GameObject shape in shapesToDelete)
        {
            Destroy(shape);
        }
    }
}

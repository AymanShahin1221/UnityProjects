using UnityEngine;

public class Scaler : MonoBehaviour
{
    public Canvas canvas;
    void Update()
    {
        float canvasRatio = (float)canvas.pixelRect.width / (float)canvas.pixelRect.height;
        transform.localScale = new Vector3(canvasRatio * 300f, canvasRatio * 300f, canvasRatio * 300f);
    }
}
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Texture2DEditor : MonoBehaviour
{
    Texture2D texture2D;
    Vector2 mousePos = new Vector2();
    RectTransform rectTransform;
    [SerializeField] Button clearButton;
    [SerializeField] Button ExtractionButton;
    public Color drawColor = new Color(0, 0, 0, 0);
    int width = 0;
    int height = 0;
    public int penRadius = 10;
    public Vector2 lastMousePos;

    void Start()
    {
        var rawImage = GetComponent<RawImage>();
        rectTransform = rawImage.GetComponent<RectTransform>();

        width = (int) rectTransform.rect.width;
        height = (int) rectTransform.rect.height;

        texture2D = rawImage.texture as Texture2D;

        var pixelData = texture2D.GetPixels();

        //Debug.Log(pixelData.Length);
    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out mousePos);

        mousePos.x = width - (width / 2 - mousePos.x);
        mousePos.y = Mathf.Abs((height / 2 - mousePos.y) - height);

        //print(mousePos.x + " " + mousePos.y);

        if (Input.GetMouseButton(0)) {
            float quality = 20;
            for (int i = 0; i <= quality; i++)
            {
                Vector2 _mousePos;
                _mousePos.x = Mathf.Lerp(lastMousePos.x, mousePos.x, i/quality);
                _mousePos.y = Mathf.Lerp(lastMousePos.y, mousePos.y, i/quality);
                paintCircle(texture2D, (int) _mousePos.x, (int) _mousePos.y, 10);
            }
            texture2D.Apply();
        }
        lastMousePos = mousePos;
    }

    public void clearCanvas()
    {
        for (int i = 0; i <= height - 1; i++)
        {
            for (int j = 0; j <= width - 1; j++)
            {
                texture2D.SetPixel(j, i, new Color(255, 255, 255, 255));
            }
        }
        texture2D.Apply();
    }

    void paintCircle(Texture2D texture, int i, int j, int radius)
    {   
        int a = Mathf.Clamp(i - radius, 0, width);
        int b = Mathf.Clamp(j - radius, 0, height);
        int c = Mathf.Clamp(i + radius, 0, width);
        int d = Mathf.Clamp(j + radius, 0, height);

        for (int y = b; y < d; y++)
        {
            for (int x = a; x < c; x++)
            {
                float distance = (x - i) * (x - i) + (y - j) * (y - j);
                if (distance < radius * radius)
                {
                    texture2D.SetPixel(x, y, new Color(0, 0, 0, 255));
                }
            }
        }
    }

    public void FeatureExtraction()
    {
        int width = texture2D.width;
        int height =texture2D.height;
        float[,] grayscale_data = Grayscale(texture2D, width, height);
        bool[,] biner_data = Biner(grayscale_data, width, height);
        Rect bounding_box = MinimalBoundingBox(biner_data, width, height);

        DrawRect(texture2D, bounding_box);
        print(bounding_box);
    }

    public float[,] Grayscale(Texture2D texture, int width, int height) {
        float[,] grayscale_data = new float[width, height];
        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                grayscale_data[x,y] = texture.GetPixel(x, y).grayscale;
            }
        }
        return grayscale_data;
    }

    public bool[,] Biner(float[,] grayscale_data, int width, int height) {
        bool[,] binary_data = new bool[width, height];
        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                binary_data[x,y] = (grayscale_data[x,y] < 0.5f);
            }
        }
        return binary_data;
    }
    
    public Rect MinimalBoundingBox(bool[,] binary_data, int width, int height) {
        Vector2 min_point = new Vector2(width, height);
        Vector2 max_point = new Vector2(0, 0);

        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                if(binary_data[x,y]) {
                    min_point.x = Mathf.Min(min_point.x, x);
                    min_point.y = Mathf.Min(min_point.y, y);
                    max_point.x = Mathf.Max(max_point.x, x);
                    max_point.y = Mathf.Max(max_point.y, y);
                }
            }
        }

        return new Rect(min_point, max_point-min_point);
    }

    public void DrawRect(Texture2D texture2D, Rect rect) {
        Vector2 p0 = new Vector2(rect.position.x            , rect.position.y);
        Vector2 p1 = new Vector2(rect.position.x+rect.size.x, rect.position.y);
        Vector2 p2 = new Vector2(rect.position.x            , rect.position.y+rect.size.y);
        Vector2 p3 = new Vector2(rect.position.x+rect.size.x, rect.position.y+rect.size.y);

        DrawLine(texture2D, p0, p1);
        DrawLine(texture2D, p0, p2);
        DrawLine(texture2D, p1, p3);
        DrawLine(texture2D, p2, p3);
    }

    public void DrawLine(Texture2D texture2D, Vector2 start, Vector2 end) {
        for(int y=(int)start.y; y<=end.y; y++) {
            for(int x=(int)start.x; x<=end.x; x++) {
                texture2D.SetPixel(x, y, new Color(0 ,0, 0, 255));
            }
        }
        texture2D.Apply();
    }
}

﻿using System.Collections;
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

        print(mousePos.x + " " + mousePos.y);

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
        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j <= width; j++)
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
}

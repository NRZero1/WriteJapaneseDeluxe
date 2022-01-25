using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Texture2DEditor : MonoBehaviour
{
    Texture2D texture2D;
    Vector2 mousePos = new Vector2();
    RectTransform rectTransform;
    [SerializeField] GameObject drawPanel;
    [SerializeField] Text inputField;
    public Color drawColor = new Color(0, 0, 0, 0);
    int width = 0;
    int height = 0;
    int penRadius = 5;
    public Vector2 lastMousePos;
    double[,] matrixPopulation;
    double aspectRatio;
    public string frequentCharacter;

    void Start()
    {
        var rawImage = GetComponent<RawImage>();
        rectTransform = rawImage.GetComponent<RectTransform>();

        width = (int) rectTransform.rect.width;
        height = (int) rectTransform.rect.height;

        texture2D = rawImage.texture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out mousePos);

        mousePos.x = width - (width / 2 - mousePos.x);
        mousePos.y = Mathf.Abs((height / 2 - mousePos.y) - height);

        if (Input.GetMouseButton(0)) {
            float quality = 20;
            for (int i = 0; i <= quality; i++)
            {
                Vector2 _mousePos;
                _mousePos.x = Mathf.Lerp(lastMousePos.x, mousePos.x, i/quality);
                _mousePos.y = Mathf.Lerp(lastMousePos.y, mousePos.y, i/quality);
                paintCircle(texture2D, (int) _mousePos.x, (int) _mousePos.y, penRadius);
            }
            texture2D.Apply();
        }
        lastMousePos = mousePos;

        if (Input.GetMouseButton(1))
        {
            clearCanvas();
        }

        if (drawPanel != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                drawPanel.SetActive(false);
            }
        }
    }

    public void clearCanvas()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
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

    private float[,] Grayscale(Texture2D texture, int width, int height) {
        float[,] grayscale_data = new float[width, height];
        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                grayscale_data[x,y] = texture.GetPixel(x, y).grayscale;
            }
        }
        return grayscale_data;
    }

    private bool[,] Biner(float[,] grayscale_data, int width, int height) {
        bool[,] binary_data = new bool[width, height];
        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                binary_data[x,y] = (grayscale_data[x,y] >= 0.5f);
            }
        }
        return binary_data;
    }
    
    private Rect MinimalBoundingBox(bool[,] binary_data, int width, int height) {
        Vector2 min_point = new Vector2(width, height);
        Vector2 max_point = new Vector2(0, 0);

        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                if(binary_data[x,y] == false) {
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

    private double[,] Population(Rect Boundary, bool[,] binary_data)
    {
        float matrixWidth;
        float matrixHeight;
        float characterWidth;
        float characterHeight;

        characterWidth = (Boundary.xMax - Boundary.xMin) + 1;
        characterHeight = (Boundary.yMax - Boundary.yMin) + 1;

        bool[,] matrixResult = new bool[(int)characterWidth, (int)characterHeight];
        double[,] result = new double[5, 5];

        for (int y = 0; y < 5; y ++)
        {
            for (int x = 0; x < 5; x ++)
            {
                result[x, y] = 0;
            }
        }

        matrixWidth = characterWidth / 5f;
        matrixHeight = characterHeight / 5f;
        aspectRatio = characterWidth / characterHeight;

        for (int y = 0; y < characterHeight; y ++)
        {
            for (int x = 0; x < characterWidth; x ++)
            {
                if (binary_data[x + (int)Boundary.xMin, y + (int)Boundary.yMin] == false)
                {
                    matrixResult[x, (int)characterHeight - 1 - y] = false;
                }
                else
                {
                    matrixResult[x, (int)characterHeight - 1 - y] = true;
                }
            }
        }

        for (int j = 0; j < 5; j ++)
        {
            for (int i = 0; i < 5; i ++)
            {
                int n = 0;
                for (int y = (int)Mathf.Round(j * matrixHeight); y < (int)Mathf.Round((j + 1) * matrixHeight); y ++)
                {
                    for (int x = (int)Mathf.Round(i * matrixWidth); x < (int)Mathf.Round((i + 1) * matrixWidth); x++)
                    {
                        if (matrixResult[x, y] == false)
                        {
                            result[i, j] += 1;
                        }
                        n += 1;
                    }
                }
                result[i, j] = (result[i, j] / (double)n) * 100.0;
            }
        }
        return result;
    }

    public void Classify()
    {        
        float[,] gray_data = Grayscale(texture2D, width, height);
        bool[,] biner_data = Biner(gray_data, width, height);
        Rect boundingBox = MinimalBoundingBox(biner_data, width, height);
        //DrawRect(texture2D, boundingBox);
        matrixPopulation = Population(boundingBox, biner_data);

        string CSVFileName = Path.Combine(Application.streamingAssetsPath, "Dataset.csv");
        string[] lines = System.IO.File.ReadAllLines(CSVFileName, System.Text.Encoding.GetEncoding(932));

        List<string> populationListShaped = new List<string>();
        List<double> AspectRatio = new List<double>();
        List<string> japaneseCharacter = new List<string>();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] rowData = lines[i].Split(',');

            double a = Convert.ToDouble(rowData[3]);
            populationListShaped.Add(rowData[2]);
            japaneseCharacter.Add(rowData[4]);
            AspectRatio.Add(a);
        }

        double[,,] populationMatrixShaped = new double[populationListShaped.Count, 5, 5];

        for (int n = 0; n < populationListShaped.Count; n++)
        {
            string[] temp = populationListShaped[n].Split(';');
            int indexCounter = 0;

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    //richTextBox1.Text += temp[indexCounter] + "\r\n";
                    double t = Convert.ToDouble(temp[indexCounter]);
                    populationMatrixShaped[n, i, j] = t;
                    indexCounter++;
                }
            }
        }

        double[] distanceList = new double[populationListShaped.Count];
        double distanceRatio;
        double[,] distanceMatrix = new double[5, 5];

        for (int n = 0; n < populationListShaped.Count; n++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    distanceMatrix[i, j] = Math.Abs(populationMatrixShaped[n, i, j] - matrixPopulation[i, j]);
                }
            }
            distanceRatio = Math.Abs(AspectRatio[n] - aspectRatio);

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    distanceList[n] += distanceMatrix[i, j];
                }    
            }
            distanceList[n] += distanceRatio;
        }

        int k = 3;

        double[] distanceListTemp = new double[distanceList.Length];
        int[] bestIndex = new int[k];

        Array.Copy(distanceList, distanceListTemp, distanceList.Length);

        Array.Sort(distanceListTemp);

        for (int x = 0; x < k; x ++)
        {
            bestIndex[x] = Array.IndexOf(distanceList, distanceListTemp[x]);
        }

        int count = 1;
        int tempCount;
        frequentCharacter = japaneseCharacter[bestIndex[0]];
        string tempCharacter;

        for (int i = 0; i < k; i ++)
        {
            tempCharacter = japaneseCharacter[bestIndex[i]];
            tempCount = 0;
            
            for (int j = 0; j < k; j ++)
            {
                if (tempCharacter == japaneseCharacter[bestIndex[j]])
                {
                    tempCount++;
                }
            }
            
            if (tempCount > count)
            {
                frequentCharacter = tempCharacter;
                count = tempCount;
            }
        }
    }
}

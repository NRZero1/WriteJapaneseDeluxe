using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    [SerializeField] GameObject darkOverlayPanel;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject savePanel;
    [SerializeField] GameObject stageClearPanel;
    [SerializeField] GameObject panelCanvas1;
    [SerializeField] GameObject panelCanvas2;
    [SerializeField] GameObject panelCanvas3;
    [SerializeField] GameObject panelCanvas4;
    [SerializeField] GameObject panelCanvas5;
    [SerializeField] GameObject panelCanvas6;
    [SerializeField] GameObject panelCanvas7;
    [SerializeField] Image lifePointTrue1;
    [SerializeField] Image lifePointTrue2;
    [SerializeField] Image lifePointTrue3;
    [SerializeField] Image lifePointTrue4;
    [SerializeField] RawImage drawCanvas1;
    [SerializeField] RawImage drawCanvas2;
    [SerializeField] RawImage drawCanvas3;
    [SerializeField] RawImage drawCanvas4;
    [SerializeField] RawImage drawCanvas5;
    [SerializeField] RawImage drawCanvas6;
    [SerializeField] RawImage drawCanvas7;
    [SerializeField] Button drawButton1;
    [SerializeField] Button drawButton2;
    [SerializeField] Button drawButton3;
    [SerializeField] Button drawButton4;
    [SerializeField] Button drawButton5;
    [SerializeField] Button drawButton6;
    [SerializeField] Button drawButton7;
    int width = 0;
    int height = 0;
    RectTransform rectTransform1;
    RectTransform rectTransform2;
    RectTransform rectTransform3;
    RectTransform rectTransform4;
    RectTransform rectTransform5;
    RectTransform rectTransform6;
    RectTransform rectTransform7;
    Texture2D texture2D1;
    Texture2D texture2D2;
    Texture2D texture2D3;
    Texture2D texture2D4;
    Texture2D texture2D5;
    Texture2D texture2D6;
    Texture2D texture2D7;
    double[,] matrixPopulation;
    double aspectRatio;
    Vector2 min_point;
    Vector2 max_point;

    void Start()
    {
        darkOverlayPanel.SetActive(false);
        leavePanel.SetActive(false);
        savePanel.SetActive(false);
        panelCanvas1.SetActive(false);
        panelCanvas2.SetActive(false);
        panelCanvas3.SetActive(false);
        panelCanvas4.SetActive(false);
        panelCanvas5.SetActive(false);
        panelCanvas6.SetActive(false);
        panelCanvas7.SetActive(false);
        stageClearPanel.SetActive(false);
        
        drawCanvas1 = GetComponent<RawImage>();
        drawCanvas2 = GetComponent<RawImage>();
        drawCanvas3 = GetComponent<RawImage>();
        drawCanvas4 = GetComponent<RawImage>();
        drawCanvas5 = GetComponent<RawImage>();
        drawCanvas6 = GetComponent<RawImage>();
        drawCanvas7 = GetComponent<RawImage>();

        rectTransform1 = drawCanvas1.GetComponent<RectTransform>();
        rectTransform2 = drawCanvas2.GetComponent<RectTransform>();
        rectTransform3 = drawCanvas3.GetComponent<RectTransform>();
        rectTransform4 = drawCanvas4.GetComponent<RectTransform>();
        rectTransform5 = drawCanvas5.GetComponent<RectTransform>();
        rectTransform6 = drawCanvas6.GetComponent<RectTransform>();
        rectTransform7 = drawCanvas7.GetComponent<RectTransform>();

        width = (int) rectTransform1.rect.width;
        height = (int) rectTransform1.rect.height;

        texture2D1 = drawCanvas1.texture as Texture2D;
        texture2D2 = drawCanvas2.texture as Texture2D;
        texture2D3 = drawCanvas3.texture as Texture2D;
        texture2D4 = drawCanvas4.texture as Texture2D;
        texture2D5 = drawCanvas5.texture as Texture2D;
        texture2D6 = drawCanvas6.texture as Texture2D;
        texture2D7 = drawCanvas7.texture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void submitButtonClicked()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                texture2D1.SetPixel(y, x, new Color(255, 255, 255, 255));
                texture2D2.SetPixel(y, x, new Color(255, 255, 255, 255));
                texture2D3.SetPixel(y, x, new Color(255, 255, 255, 255));
                texture2D4.SetPixel(y, x, new Color(255, 255, 255, 255));
                texture2D5.SetPixel(y, x, new Color(255, 255, 255, 255));
                texture2D6.SetPixel(y, x, new Color(255, 255, 255, 255));
                texture2D7.SetPixel(y, x, new Color(255, 255, 255, 255));
            }
        }
        texture2D1.Apply();
        texture2D2.Apply();
        texture2D3.Apply();
        texture2D4.Apply();
        texture2D5.Apply();
        texture2D6.Apply();
        texture2D7.Apply();

        /*int width = texture2D1.width;
        int height = texture2D1.height;
        float[,] grayscale_data = Grayscale(texture2D, width, height);
        bool[,] biner_data = Biner(grayscale_data, width, height);
        Rect bounding_box = MinimalBoundingBox(biner_data, width, height);*/

        //DrawRect(texture2D, bounding_box);
        //print(bounding_box);
    }

    public void stageClear()
    {

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
                binary_data[x,y] = (grayscale_data[x,y] < 0.5f);
            }
        }
        return binary_data;
    }
    
    private Rect MinimalBoundingBox(bool[,] binary_data, int width, int height) {
        min_point = new Vector2(width, height);
        max_point = new Vector2(0, 0);

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

    private void Population()
    {
        /*int CharacterWidth = (int) Mathf.Abs(bounding_box.xMax - bounding_box.xMin) + 1;
        int CharacterHeight = (int) Mathf.Abs(bounding_box.yMax - bounding_box.yMin) + 1;
        bool[,] MatriksResult = new bool[CharacterWidth, CharacterHeight];
        double[,] Result = new double[5, 5];*/
        //Rect boundingBox = MinimalBoundingBox(biner_data, width, height);

        float matrixWidth;
        float matrixHeight;
        float characterWidth;
        float characterHeight;

        characterWidth = (max_point.x - min_point.x);
        characterHeight = (max_point.y - min_point.y);

        float[,] matrixResult = new float[(int)characterWidth, (int)characterHeight];
        double[,] result = new double[5, 5];

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                result[x, y] = 0;
            }
        }

        matrixWidth = characterWidth / 5;
        matrixHeight = characterHeight / 5;
        aspectRatio = characterWidth / characterHeight;
    }

    private void Classify()
    {
        
    }

    public void drawButton1Clicked()
    {
        panelCanvas1.SetActive(true);
    }

    public void drawButton2Clicked()
    {
        panelCanvas2.SetActive(true);
    }

    public void drawButton3Clicked()
    {
        panelCanvas3.SetActive(true);
    }

    public void drawButton4Clicked()
    {
        panelCanvas4.SetActive(true);
    }

    public void drawButton5Clicked()
    {
        panelCanvas5.SetActive(true);
    }

    public void drawButton6Clicked()
    {
        panelCanvas6.SetActive(true);
    }

    public void drawButton7Clicked()
    {
        panelCanvas7.SetActive(true);
    }
}

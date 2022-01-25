using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnHiraganaController : MonoBehaviour
{
    [SerializeField] GameObject hiraganaMap;
    [SerializeField] RawImage rawImage;
    [SerializeField] Text detectedText;
    Texture2DEditor texture2DEditor;
    [SerializeField] Text hiraganaText;
    [SerializeField] GameObject showTextPanel;
    bool mapToggle = false;
    [SerializeField] GameObject drawCanvas;
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject darkOverlayPanel;
    void Start()
    {
        texture2DEditor = rawImage.GetComponent<Texture2DEditor>();
        showTextPanel.SetActive(false);
        hiraganaMap.SetActive(false);
        exitPanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            texture2DEditor.Classify();
            detectedText.text = texture2DEditor.frequentCharacter;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (drawCanvas.activeSelf == false)
            {
                showTextPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mapToggle = !mapToggle;
            if (mapToggle)
            {
                hiraganaMap.SetActive(true);
                drawCanvas.SetActive(false);
            }
            else
            {
                hiraganaMap.SetActive(false);
                showTextPanel.SetActive(false);
                drawCanvas.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            darkOverlayPanel.SetActive(true);
            exitPanel.SetActive(true);
        }
    }
    public void noButtonPressed()
    {
        exitPanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
    }

    public void hiraganaA()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "あ";
    }

    public void hiraganaI()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "い";
    }

    public void hiraganaU()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "う";
    }

    public void hiraganaE()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "え";
    }

    public void hiraganaO()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "お";
    }

    public void hiraganaKa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "か";
    }

    public void hiraganaKi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "き";
    }

    public void hiraganaKu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "く";
    }

    public void hiraganaKe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "け";
    }

    public void hiraganaKo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "こ";
    }

    public void hiraganaSa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "さ";
    }

    public void hiraganaShi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "し";
    }

    public void hiraganaSu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "す";
    }

    public void hiraganaSe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "せ";
    }

    public void hiraganaSo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "そ";
    }

    public void hiraganaTa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "た";
    }

    public void hiraganaChi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ち";
    }

    public void hiraganaTsu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "つ";
    }

    public void hiraganaTe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "て";
    }
    
    public void hiraganaTo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "と";
    }

    public void hiraganaNa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "な";
    }

    public void hiraganaNi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "に";
    }

    public void hiraganaNu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぬ";
    }

    public void hiraganaNe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ね";
    }

    public void hiraganaNo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "の";
    }

    public void hiraganaHa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "は";
    }

    public void hiraganaHi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ひ";
    }

    public void hiraganaFu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ふ";
    }

    public void hiraganaHe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "へ";
    }

    public void hiraganaHo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ほ";
    }

    public void hiraganaMa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ま";
    }

    public void hiraganaMi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "み";
    }

    public void hiraganaMu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "む";
    }

    public void hiraganaMe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "め";
    }

    public void hiraganaMo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "も";
    }

    public void hiraganaYa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "や";
    }

    public void hiraganaYu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ゆ";
    }

    public void hiraganaYo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "よ";
    }

    public void hiraganaRa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ら";
    }

    public void hiraganaRi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "り";
    }

    public void hiraganaRu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "る";
    }

    public void hiraganaRe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "れ";
    }

    public void hiraganaRo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ろ";
    }

    public void hiraganaWa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "わ";
    }

    public void hiraganaWo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "を";
    }

    public void hiraganaN()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ん";
    }

    public void hiraganaGa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "が";
    }

    public void hiraganaGi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぎ";
    }

    public void hiraganaGu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぐ";
    }

    public void hiraganaGe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "げ";
    }

    public void hiraganaGo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ご";
    }

    public void hiraganaZa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ざ";
    }

    public void hiraganaJi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "じ";
    }

    public void hiraganaZu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ず";
    }

    public void hiraganaZe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぜ";
    }

    public void hiraganaZo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぞ";
    }

    public void hiraganaDa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "だ";
    }

    public void hiraganaDji()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぢ";
    }

    public void hiraganaDzu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "づ";
    }

    public void hiraganaDe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "で";
    }

    public void hiraganaDo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ど";
    }

    public void hiraganaBa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ば";
    }

    public void hiraganaBi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "び";
    }

    public void hiraganaBu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぶ";
    }

    public void hiraganaBe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "べ";
    }

    public void hiraganaBo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぼ";
    }

    public void hiraganaPa()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぱ";
    }

    public void hiraganaPi()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぴ";
    }

    public void hiraganaPu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぷ";
    }

    public void hiraganaPe()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぺ";
    }

    public void hiraganaPo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぽ";
    }

    public void hiraganaKya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "きゃ";
    }

    public void hiraganaKyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "きゅ";
    }

    public void hiraganaKyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "きょ";
    }

    public void hiraganaSha()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "しゃ";
    }

    public void hiraganaShu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "しゅ";
    }

    public void hiraganaSho()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "しょ";
    }

    public void hiraganaCha()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ちゃ";
    }

    public void hiraganaChu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ちゅ";
    }

    public void hiraganaCho()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ちょ";
    }

    public void hiraganaNya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "にゃ";
    }

    public void hiraganaNyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "にゅ";
    }

    public void hiraganaNyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "にょ";
    }

    public void hiraganaHya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ひゃ";
    }

    public void hiraganaHyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ひゅ";
    }

    public void hiraganaHyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ひょ";
    }

    public void hiraganaMya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "みゃ";
    }

    public void hiraganaMyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "みゅ";
    }

    public void hiraganaMyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "みょ";
    }

    public void hiraganaRya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "りゃ";
    }

    public void hiraganaRyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "りゅ";
    }

    public void hiraganaRyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "りょ";
    }

    public void hiraganaGya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぎゃ";
    }

    public void hiraganaGyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぎゅ";
    }

    public void hiraganaGyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぎょ";
    }

    public void hiraganaJya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "じゃ";
    }

    public void hiraganaJyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "じゅ";
    }

    public void hiraganaJyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "じょ";
    }

    public void hiraganaBya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "びゃ";
    }

    public void hiraganaByu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "びゅ";
    }

    public void hiraganaByo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "びょ";
    }

    public void hiraganaPya()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぴゃ";
    }

    public void hiraganaPyu()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぴゅ";
    }

    public void hiraganaPyo()
    {
        showTextPanel.SetActive(true);
        hiraganaText.text = "ぴょ";
    }
}

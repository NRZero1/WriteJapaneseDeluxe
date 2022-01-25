using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    [SerializeField] GameObject darkOverlayPanel;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject savePanel;
    [SerializeField] GameObject stageClearPanel;
    [SerializeField] GameObject levelClearPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameClearPanel;
    [SerializeField] GameObject[] panelCanvas;
    [SerializeField] GameObject[] lifePointTrue;
    [SerializeField] RawImage[] drawCanvas;
    [SerializeField] GameObject[] drawButton;
    [SerializeField] InputField inputField;
    [SerializeField] GameObject materialPanel;
    [SerializeField] Material[] materialLevel1 = new Material[10];
    [SerializeField] Material[] materialLevel2 = new Material[10];
    [SerializeField] Material[] materialLevel3 = new Material[10];
    public Material[] currentLevel = new Material[10];
    public int stageCount = 0;
    public int falseCount = 0;
    [SerializeField] Text question;
    [SerializeField] Text levelNumber;
    [SerializeField] Image[] materialImage = new Image[10];
    [SerializeField] Image[] stageImage = new Image[10];
    Level levelConfig = new Level();
    
    void Start()
    {
        Animator[] anim = new Animator[4];
        darkOverlayPanel.SetActive(false);
        leavePanel.SetActive(false);
        savePanel.SetActive(false);
        stageClearPanel.SetActive(false);
        levelClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        materialPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        inputField.interactable = false;

        for (int i = 0; i < anim.Length; i ++)
        {
            lifePointTrue[i].SetActive(false);
            anim[i] = lifePointTrue[i].GetComponent<Animator>();
        }

        for (int i = 0; i < panelCanvas.Length; i ++)
        {
            panelCanvas[i].SetActive(false);
        }

        if (LoadState.isLoad == true)
        {
            levelConfig.loadLevel();
            generateLevel();
        }
        else
        {
            levelConfig.shuffleLevel();
            generateLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SearchLevel()
    {
        switch (levelConfig.level[levelConfig.levelCount])
        {
            case 1: {
                for (int i = 0; i < materialImage.Length; i ++)
                {
                    materialImage[i].sprite = materialLevel1[i].illustration;
                }
                currentLevel = materialLevel1;
                break;
            }
            case 2: {
                for (int i = 0; i < materialImage.Length; i ++)
                {
                    materialImage[i].sprite = materialLevel2[i].illustration;
                }
                currentLevel = materialLevel2;
                break;
            }
            case 3:{
                for (int i = 0; i < materialImage.Length; i ++)
                {
                    materialImage[i].sprite = materialLevel3[i].illustration;
                }
                currentLevel = materialLevel3;
                break;
            }
        }
    }

    private void generateLevel()
    {
        Random random = new Random();
        SearchLevel();
        currentLevel = currentLevel.OrderBy(Material => random.Next()).ToArray();
        levelNumber.text = "Level - " + (levelConfig.levelCount + 1).ToString();
        materialPanel.SetActive(true);
    }

    private int stringCheck()
    {
        int find = 0;
        string[] excludeCharacter = new string[33];
        int characterCount = currentLevel[stageCount].hiraganaName.Length;

        excludeCharacter[0] = "きゃ";
        excludeCharacter[1] = "きゅ";
        excludeCharacter[2] = "きょ";
        excludeCharacter[3] = "しゃ";
        excludeCharacter[4] = "しゅ";
        excludeCharacter[5] = "しょ";
        excludeCharacter[6] = "ちゃ";
        excludeCharacter[7] = "ちゅ";
        excludeCharacter[8] = "ちょ";
        excludeCharacter[9] = "にゃ";
        excludeCharacter[10] = "にゅ";
        excludeCharacter[11] = "にょ";
        excludeCharacter[12] = "ひゃ";
        excludeCharacter[13] = "ひゅ";
        excludeCharacter[14] = "ひょ";
        excludeCharacter[15] = "みゃ";
        excludeCharacter[16] = "みゅ";
        excludeCharacter[17] = "みょ";
        excludeCharacter[18] = "りゃ";
        excludeCharacter[19] = "りゅ";
        excludeCharacter[20] = "りょ";
        excludeCharacter[21] = "ぎゃ";
        excludeCharacter[22] = "ぎゅ";
        excludeCharacter[23] = "ぎょ";
        excludeCharacter[24] = "じゃ";
        excludeCharacter[25] = "じゅ";
        excludeCharacter[26] = "じょ";
        excludeCharacter[27] = "びゃ";
        excludeCharacter[28] = "びゅ";
        excludeCharacter[29] = "びょ";
        excludeCharacter[30] = "ぴゃ";
        excludeCharacter[31] = "ぴゅ";
        excludeCharacter[32] = "ぴょ";

        int i = 0;

        while(i < excludeCharacter.Length)
        {
            Regex regex = new Regex("("+excludeCharacter[i]+")");
            if (regex.IsMatch(currentLevel[stageCount].hiraganaName))
            {
                find++;
            }
            i++;
        }
        return find;
    }

    private void generateStage()
    {
        Texture2DEditor[] texture2DEditor = new Texture2DEditor[10];
        int characterCount = currentLevel[stageCount].hiraganaName.Length;
        
        for (int i = 0; i < characterCount; i ++)
        {
            texture2DEditor[i] = drawCanvas[i].GetComponent<Texture2DEditor>();
        }

        for (int i = 0; i < characterCount; i ++)
        {
            texture2DEditor[i].clearCanvas();
        }

        for (int i = 0; i < drawButton.Length; i ++)
        {
            drawButton[i].SetActive(false);
        }
        
        characterCount = characterCount - stringCheck();

        for (int i = 0; i < characterCount; i ++)
        {
            drawButton[i].SetActive(true);
        }

        question.text = currentLevel[stageCount].meaning;
    }

    private void stageClear()
    {
        stageImage[stageCount].GetComponent<Image>().color = new Color32(140, 180, 34, 255);
        stageCount++;
        if (stageCount < 10)
        {
            darkOverlayPanel.SetActive(true);
            stageClearPanel.SetActive(true);
        }
        else
        {
            stageCount = 0;
            falseCount = 0;
            levelConfig.levelCount++;
            for (int i = 0; i < stageImage.Length; i ++)
            {
                stageImage[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }

            Animator[] anim = new Animator[4];

            for (int i = 0; i < anim.Length; i ++)
            {
                anim[i] = lifePointTrue[i].GetComponent<Animator>();
            }

            for (int i = 0; i < anim.Length; i ++)
            {
                anim[i].SetBool("isFalse", false);
            }

            for (int i = 0; i < lifePointTrue.Length; i ++)
            {
                lifePointTrue[i].SetActive(false);
            }

            darkOverlayPanel.SetActive(true);
            if (levelConfig.levelCount == 4)
            {
                gameClearPanel.SetActive(true);
            }
            else
            {
                levelClearPanel.SetActive(true);
            }
        }
    }

    public void submitButtonClicked()
    {
        Animator[] anim = new Animator[4];

        for (int i = 0; i < anim.Length; i ++)
        {
            anim[i] = lifePointTrue[i].GetComponent<Animator>();
        }
        
        if (inputField.text.Equals(currentLevel[stageCount].hiraganaName))
        {
            inputField.text = "";
            stageClear();
        }
        else
        {
            lifePointTrue[falseCount].SetActive(true);
            anim[falseCount].SetBool("isFalse", true);
            falseCount++;
        }

        if (falseCount == 4)
        {
            darkOverlayPanel.SetActive(true);
            gameOverPanel.SetActive(true);
        }
    }

    public void checkButtonClicked()
    {
        Texture2DEditor[] texture2DEditor = new Texture2DEditor[10];
        int characterCount = currentLevel[stageCount].hiraganaName.Length;

        characterCount = characterCount - stringCheck();
        
        inputField.text = "";

        for (int i = 0; i < characterCount; i ++)
        {
            texture2DEditor[i] = drawCanvas[i].GetComponent<Texture2DEditor>();
        }

        for (int i = 0; i < characterCount; i ++)
        {
            texture2DEditor[i].Classify();
            inputField.text += texture2DEditor[i].frequentCharacter;
        }
    }

    public void continueButtonClicked()
    {
        materialPanel.SetActive(false);
        generateStage();
    }

    public void stageClearOKClicked()
    {
        generateStage();
        stageClearPanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
    }

    public void levelClearOKClicked()
    {
        generateLevel();
        levelClearPanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
    }

    public void gameClearOrOverOKClicked()
    {
        if (SaveSystem.checkSaveFile())
        {
            SaveSystem.delete();
        }
    }

    public void leaveButtonClicked()
    {
        darkOverlayPanel.SetActive(true);
        leavePanel.SetActive(true);
    }

    public void leaveYesButtonClicked()
    {
        savePanel.SetActive(true);
    }

    public void leaveNoButtonClicked()
    {
        leavePanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
    }

    public void saveYesButtonClicked()
    {
        if (SaveSystem.checkSaveFile())
        {
            SaveSystem.delete();
        }
        levelConfig.saveLevel();
    }

    public void saveNoButtonClicked()
    {
        LoadState.isLoad = false;
        if (SaveSystem.checkSaveFile())
        {
            SaveSystem.delete();
        }
        //SceneController sceneController = new SceneController();
        //sceneController.SceneLoad("MainMenuScene");
    }

    public void drawButton1Clicked()
    {
        panelCanvas[0].SetActive(true);
    }

    public void drawButton2Clicked()
    {
        panelCanvas[1].SetActive(true);
    }

    public void drawButton3Clicked()
    {
        panelCanvas[2].SetActive(true);
    }

    public void drawButton4Clicked()
    {
        panelCanvas[3].SetActive(true);
    }

    public void drawButton5Clicked()
    {
        panelCanvas[4].SetActive(true);
    }

    public void drawButton6Clicked()
    {
        panelCanvas[5].SetActive(true);
    }

    public void drawButton7Clicked()
    {
        panelCanvas[6].SetActive(true);
    }
}

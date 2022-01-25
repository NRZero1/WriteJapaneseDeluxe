using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject darkOverlayPanel;
    [SerializeField] Button continueButton;

    void Start()
    {
        exitPanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
        if (SaveSystem.checkSaveFile())
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void continueButtonClicked()
    {
        Level levelConfig = new Level();
        LoadState.isLoad = true;
    }

    public void exitButtonPressed()
    {
        darkOverlayPanel.SetActive(true);
        exitPanel.SetActive(true);
    }

    public void exitConfirm()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void exitCancel()
    {
        exitPanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
    }
}

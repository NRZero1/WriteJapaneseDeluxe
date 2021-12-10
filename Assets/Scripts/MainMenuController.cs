using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject darkOverlayPanel;

    void Start()
    {
        exitPanel.SetActive(false);
        darkOverlayPanel.SetActive(false);
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

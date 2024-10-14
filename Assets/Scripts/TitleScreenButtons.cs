using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public GameObject helpPage;
    public GameObject mainPage;

    public GameObject objectiveText;
    public GameObject mechanicsText;
    public GameObject controlsText;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SwitchToMain()
    {
        mainPage.SetActive(true);
        helpPage.SetActive(false);
    }

    public void SwitchToHelp()
    {
        helpPage.SetActive(true);
        mainPage.SetActive(false);
        objectiveText.SetActive(true);
        mechanicsText.SetActive(false);
        controlsText.SetActive(false);
    }

    public void ObjectiveText()
    {
        objectiveText.SetActive(true);
        mechanicsText.SetActive(false);
        controlsText.SetActive(false);
    }

    public void MechanicsText()
    {
        objectiveText.SetActive(false);
        mechanicsText.SetActive(true);
        controlsText.SetActive(false);
    }

    public void ControlsText()
    {
        objectiveText.SetActive(false);
        mechanicsText.SetActive(false);
        controlsText.SetActive(true);
    }
}

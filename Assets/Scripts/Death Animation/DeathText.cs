using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathText : MonoBehaviour
{
    private DeathScreenController deathScreenController;
    private TextMeshProUGUI textMeshPro;

    private RespawnText respawnText;

    private bool stillRestartScreen;

    public bool switchToRespawnText;
    public bool fadeIn;
    public bool fadeOut;
    private float fadeAmount;

    public bool beginFadeIn;
    // Start is called before the first frame update
    void Start()
    {
        stillRestartScreen = true;
        deathScreenController = GameObject.Find("Death Screen").GetComponent<DeathScreenController>();
        switchToRespawnText = true;
        beginFadeIn = false;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        respawnText = GameObject.Find("Respawn Text").GetComponent<RespawnText>();
        fadeAmount = 0;
        textMeshPro.color = new Color32(156, 7, 7, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut && textMeshPro.color.a <= 0 && deathScreenController.resetScene && stillRestartScreen)
        {
            StartCoroutine(ChangeScene());
        }

        if (deathScreenController.resetScene)
        {
            fadeIn = false;
            fadeOut = true;
        }

        if (fadeOut && textMeshPro.color.a == 0 && switchToRespawnText)
        {
            stillRestartScreen = false;
            respawnText.beginFadeIn = true;
            switchToRespawnText = false;
        }

        if (beginFadeIn)
        {
            StartCoroutine(WaitToFadeIn());
        }

        if (fadeAmount >= 200)
        {
            fadeIn = false;
            fadeOut = true;
        }

        if (fadeIn && fadeAmount < 200)
        {
            fadeAmount += 0.3f;

            textMeshPro.color = new Color32(156, 7, 7, (byte)fadeAmount);
        }

        if (fadeOut && fadeAmount > 0)
        {
            fadeAmount -= 0.3f;

            textMeshPro.color = new Color32(156, 7, 7, (byte)fadeAmount);
        }
    }

    IEnumerator WaitToFadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        fadeIn = true;
        beginFadeIn = false;
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level1");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RespawnText : MonoBehaviour
{
    private DeathScreenController deathScreenController;

    private TextMeshProUGUI textMeshPro;

    public bool fadeIn;
    private float fadeAmount;
    public bool fadeOut;

    public bool beginFadeIn;
    // Start is called before the first frame update
    void Start()
    {
        deathScreenController = GameObject.Find("Death Screen").GetComponent<DeathScreenController>();
        beginFadeIn = false;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        fadeAmount = 0;
        textMeshPro.color = new Color32(0, 97, 106, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut && textMeshPro.color.a <= 0 && deathScreenController.resetScene)
        {
            StartCoroutine(ChangeScene());
        }

        if (deathScreenController.resetScene)
        {
            fadeIn = false;
            fadeOut = true;
        }

        if (beginFadeIn)
        {
            StartCoroutine(WaitToFadeIn());
        }

        if (fadeAmount >= 200)
        {
            fadeIn = false;
        }

        if (fadeIn && fadeAmount < 200)
        {
            fadeAmount += 1f;

            textMeshPro.color = new Color32(0, 97, 106, (byte)fadeAmount);
        }

        if (fadeOut && fadeAmount > 0)
        {
            fadeAmount -= 0.5f;

            textMeshPro.color = new Color32(0, 97, 106, (byte)fadeAmount);
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

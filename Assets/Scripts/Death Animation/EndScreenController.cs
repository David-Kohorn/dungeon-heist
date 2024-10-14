using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndScreenController : MonoBehaviour
{
    //Message Stuff
    private TextMeshProUGUI textMeshPro;
    public bool fadeIn;
    private float fadeAmount;

    //Button Stuff
    public Button button;
    public TextMeshProUGUI textMeshProButton;
    public bool fadeInButton;
    // Start is called before the first frame update
    void Start()
    {
        fadeInButton = false;
        fadeIn = false;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        fadeAmount = 0;
        textMeshPro.color = new Color32(96, 178, 90, 0);
        button.image.color = new Color32(111, 155, 115, 0);
        textMeshProButton.color = new Color32(12, 60, 13, 0);
        StartCoroutine(WaitToFadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn && fadeAmount < 200)
        {
            fadeAmount += 0.3f;

            textMeshPro.color = new Color32(96, 178, 90, (byte)fadeAmount);
        }

        if (fadeInButton && fadeAmount < 255)
        {
            fadeAmount += 0.3f;
            button.image.color = new Color32(111, 155, 115, (byte)fadeAmount);
            textMeshProButton.color = new Color32(12, 60, 13, (byte)fadeAmount);
        }

        if (fadeAmount > 199 && !fadeInButton)
        {
            fadeIn = false;
            fadeInButton = true;
            fadeAmount = 0;
        }
    }

    IEnumerator WaitToFadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        fadeIn = true;
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }
}

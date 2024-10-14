using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public bool resetScene;

    private SpriteRenderer spriteRenderer;

    private PlayerHealth playerHealth;

    private bool switchToDeathText;

    private bool fadeOut;
    public bool fadeIn;
    private float fadeAmount;

    private DeathText deathText;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        resetScene = false;
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        deathText = GameObject.Find("Death Text").GetComponent<DeathText>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        switchToDeathText = true;
        fadeIn = false;
        fadeOut = true;
        fadeAmount = 1;
        spriteRenderer.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn && playerHealth.playerHealth == 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(ChangeScene());
            }
        }

        if (spriteRenderer.color.a <= 0 && fadeOut)
        {
            fadeOut = false;
            spriteRenderer.color = new Color(0, 0, 0, 0);
            fadeAmount = 0;
        }

        if (fadeOut && spriteRenderer.color.a > 0)
        {
            fadeAmount -= 0.005f;

            spriteRenderer.color = new Color(0, 0, 0, fadeAmount);
        }

        if (fadeIn)
        {
            if (Input.GetKey(KeyCode.R))
            {
                resetScene = true;
            }
        }

        if (spriteRenderer.color.a >= 1 && switchToDeathText && !fadeOut)
        {
            deathText.beginFadeIn = true;
            switchToDeathText = false;
        }

        transform.position = player.transform.position;
        if (fadeIn)
        {
            fadeAmount += 0.003f;

            spriteRenderer.color = new Color(0, 0, 0, fadeAmount);
        }
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }

}

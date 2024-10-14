using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;
    private SpriteRenderer spriteRenderer;
    private Color red;
    private Color white;

    private DeathScreenController deathScreenController;
    // Start is called before the first frame update
    void Start()
    {
        deathScreenController = GameObject.Find("Death Screen").GetComponent<DeathScreenController>();
        playerHealth = 3;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        red = new Color(1, 0.4f, 0.4f, 1);
        white = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
        {
            deathScreenController.fadeIn = true;
        }
    }

    public void damagePlayer()
    {
        playerHealth -= 1;
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = red;
        yield return new WaitForSeconds(0.12f);
        spriteRenderer.color = white;
    }
}

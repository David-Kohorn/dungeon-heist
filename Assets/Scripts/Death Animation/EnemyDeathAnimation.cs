using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private EnemyController enemyController;
    private bool stopFlicker;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyController = gameObject.GetComponentInParent<EnemyController>();
        spriteRenderer.enabled = !spriteRenderer.enabled;
        stopFlicker = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.enemyHealth == 0 && !stopFlicker)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            stopFlicker = true;
        }
    }
}

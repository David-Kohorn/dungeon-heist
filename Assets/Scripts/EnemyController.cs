using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int turnOrderNum;
    //Global variable for tracking turns
    public bool isPlayerTurn;

    private EnemyManager enemyManager;

    private bool validRandomPoint;
    public LayerMask whatStopsMovement;

    private Color red;
    private Color white;

    private SpriteRenderer spriteRenderer;

    public int enemyHealth;

    public bool destroyEnemy;

    private Vector3 upPoint;
    private Vector3 downPoint;
    private Vector3 leftPoint;
    private Vector3 rightPoint;

    private Vector3 movePoint;
    public GameObject player;

    public Transform transformPoint;
    private float moveSpeed = 5;

    private PlayerHealth playerHealthScript;
    private PlayerController playerController;
    private CursorController cursorController;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyHealth = 2;
        transformPoint.parent = null;
        cursorController = GameObject.Find("Cursor").GetComponent<CursorController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemyManager = GameObject.Find("Main Camera").GetComponent<EnemyManager>();

        red = new Color(1, 0.4f, 0.4f, 1);
        white = new Color(1, 1, 1, 1);

        destroyEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth == 0)
        {
            StartCoroutine(DestroyEnemy());
        }

        if (destroyEnemy)
        {
            Destroy(gameObject);
        }

        // Move sprite towards movePoint location
        transform.position = Vector3.MoveTowards(transform.position, transformPoint.position, moveSpeed * Time.deltaTime);

        //Find which move point is closest to player
        movePoint = FindClosestPoint();

        // If the sprite has caught up to the movepoint, allow movement
        if (Vector3.Distance(transform.position, transformPoint.position) <= 0.05f)
        {
            if (!playerController.isPlayerTurn && enemyHealth != 0 && enemyManager.currentEnemyTurn == turnOrderNum)
            {
                // Decide if enemy should move to or attack player based on distance (1 tile away horizontal or diagonal)
                if (Vector3.Distance(transform.position, player.transform.position) <= Mathf.Sqrt(2))
                {
                    playerHealthScript.damagePlayer();
                }
                else if (Vector3.Distance(transform.position, player.transform.position) <= 4 * Mathf.Sqrt(2))
                {
                    transformPoint.position = movePoint;
                }

                else
                {
                    validRandomPoint = false;
                    while (!validRandomPoint)
                    {
                        movePoint = ChooseRandomPoint();
                        if (!Physics2D.OverlapCircle(movePoint, 0.2f, whatStopsMovement) && WillNotHitOtherEnemy())
                        {
                            validRandomPoint = true;
                        }
                    }
                    
                    transformPoint.position = movePoint;
                }

                enemyManager.currentEnemyTurn++;
                if (enemyManager.currentEnemyTurn == enemyManager.enemies.Count)
                {
                    playerController.isPlayerTurn = true;
                    playerController.playerCanMove = true;
                    cursorController.canAttack = true;
                }
            }
        }

        //Movepoints are always a tile in each direction from enemy
        upPoint = transform.position + new Vector3(0, 1, 0);
        downPoint = transform.position - new Vector3(0, 1, 0);
        rightPoint = transform.position + new Vector3(1, 0, 0);
        leftPoint = transform.position - new Vector3(1, 0, 0);
    }

    private Vector3 FindClosestPoint()
    {
        Vector3 findPoint = downPoint;
        Vector3 playerPos = player.transform.position;
        float distanceUpPoint = Vector3.Distance(playerPos, upPoint);
        float distanceDownPoint = Vector3.Distance(playerPos, downPoint);
        float distanceLeftPoint = Vector3.Distance(playerPos, leftPoint);
        float distanceRightPoint = Vector3.Distance(playerPos, rightPoint);

        if (distanceUpPoint < Vector3.Distance(playerPos, findPoint))
        {
            findPoint = upPoint;
        }

        if (distanceLeftPoint < Vector3.Distance(playerPos, findPoint))
        {
            findPoint = leftPoint;
        }

        if (distanceRightPoint < Vector3.Distance(playerPos, findPoint))
        {
            findPoint = rightPoint;
        }


        return findPoint;
    }

    private Vector3 ChooseRandomPoint()
    {
        Vector3 randPoint;
        int randNum = Random.Range(1, 5);

        if (randNum == 1)
        {
            randPoint = downPoint;
        }
        else if (randNum == 2)
        {
            randPoint = upPoint;
        }
        else if (randNum == 3)
        {
            randPoint = leftPoint;
        }
        else
        {
            randPoint = rightPoint;
        }

        return randPoint;
    }

    public void damageEnemy()
    {
        enemyHealth -= 1;
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = red;
        yield return new WaitForSeconds(0.12f);
        spriteRenderer.color = white;
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(0.8f);
        destroyEnemy = true;
    }

    private bool WillNotHitOtherEnemy()
    {
        int samePointCount = 0;
        foreach (GameObject enemy in enemyManager.enemies)
        {
            if (enemy.transform.position == movePoint)
            {
                samePointCount++;
            }
        }

        if (samePointCount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

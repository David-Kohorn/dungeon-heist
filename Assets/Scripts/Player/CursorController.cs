using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    private GameObject enemyInContactWith;
    private Vector3 upPoint;
    private Vector3 downPoint;
    private Vector3 leftPoint;
    private Vector3 rightPoint;

    private Vector3 upRightPoint;
    private Vector3 upLeftPoint;
    private Vector3 downRightPoint;
    private Vector3 downLeftPoint;

    private PlayerHealth playerHealthScript;

    private DeathScreenController deathScreenController;

    private Vector3 movePoint;

    public bool canAttack;
    private bool enemyContact;

    private bool finishButtonContact;

    private PlayerController playerControllerScript;

    private Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        deathScreenController = GameObject.Find("Death Screen").GetComponent<DeathScreenController>();
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemyContact = false;
        finishButtonContact = false;
        canAttack = true;
        enemyInContactWith = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealthScript.playerHealth == 0)
        {
            enabled = !enabled;
        }

        //Movepoints are always a tile in each direction from player
        upPoint = playerPos.position + new Vector3(0, 1, 0);
        downPoint = playerPos.position - new Vector3(0, 1, 0);
        rightPoint = playerPos.position + new Vector3(1, 0, 0);
        leftPoint = playerPos.position - new Vector3(1, 0, 0);

        upRightPoint = playerPos.position + new Vector3(1, 1, 0);
        upLeftPoint = playerPos.position + new Vector3(-1, 1, 0);
        downRightPoint = playerPos.position + new Vector3(1, -1, 0);
        downLeftPoint = playerPos.position + new Vector3(-1, -1, 0);

        //Move cursor to point closest to mouse
        movePoint = FindClosestPoint();
        transform.position = movePoint;

        

        if (Input.GetMouseButtonDown(0) && playerControllerScript.movesLeft > 0)
        {
            if (finishButtonContact)
            {
                StartCoroutine(NextLevel());
            }

            else if (enemyContact && canAttack)
            {
                if (enemyInContactWith.GetComponent<EnemyController>().enemyHealth - 1 == 0)
                {
                    playerControllerScript.enemyCount--;
                }
                enemyInContactWith.GetComponent<EnemyController>().damageEnemy();
                playerControllerScript.movesLeft -= 1;
            }
            
        }
    }

    //Find which point is closest to mouse and return it
    private Vector3 FindClosestPoint()
    {
        Vector3 findPoint = downPoint;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distanceUpPoint = Vector3.Distance(mousePosition, upPoint);
        float distanceDownPoint = Vector3.Distance(mousePosition, downPoint);
        float distanceLeftPoint = Vector3.Distance(mousePosition, leftPoint);
        float distanceRightPoint = Vector3.Distance(mousePosition, rightPoint);

        float distanceUpRightPoint = Vector3.Distance(mousePosition, upRightPoint);
        float distanceUpLeftPoint = Vector3.Distance(mousePosition, upLeftPoint);
        float distanceDownRightPoint = Vector3.Distance(mousePosition, downRightPoint);
        float distanceDownLeftPoint = Vector3.Distance(mousePosition, downLeftPoint);

        if (distanceUpPoint < Vector3.Distance(mousePosition, findPoint))
        {
            findPoint = upPoint;
        }

        if (distanceLeftPoint < Vector3.Distance(mousePosition, findPoint))
        {
            findPoint = leftPoint;
        }

        if (distanceRightPoint < Vector3.Distance(mousePosition, findPoint))
        {
            findPoint = rightPoint;
        }

        if (distanceUpRightPoint < Vector3.Distance(mousePosition, findPoint))
        {
            findPoint = upRightPoint;
        }

        if (distanceUpLeftPoint < Vector3.Distance(mousePosition, findPoint))
        {
            findPoint = upLeftPoint;
        }

        if (distanceDownRightPoint < Vector3.Distance(mousePosition, findPoint))
        {
            findPoint = downRightPoint;
        }

        if (distanceDownLeftPoint < Vector3.Distance(mousePosition, findPoint))
        {
            findPoint = downLeftPoint;
        }

        return findPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Button" && playerControllerScript.isPlayerTurn)
        {
            finishButtonContact = true;
        }

        else if (collision.gameObject.tag == "Enemy" && playerControllerScript.isPlayerTurn)
        {
            enemyContact = true;
            enemyInContactWith = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Button" && playerControllerScript.isPlayerTurn)
        {
            finishButtonContact = false;
            
        }

        else if (collision.gameObject.tag == "Enemy" && playerControllerScript.isPlayerTurn)
        {
            enemyContact = false;
            enemyInContactWith = null;
        }
    }

    IEnumerator NextLevel()
    {
        deathScreenController.fadeIn = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

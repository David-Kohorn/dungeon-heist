using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Enemy managment 
    private EnemyManager enemyManager;

    public int enemyCount;

    public int movesLeft;
    public Transform movePoint;
    private float moveSpeed = 5;

    public LayerMask whatStopsMovement;

    private CursorController cursorController;
    private PlayerHealth playerHealth;

    public bool isPlayerTurn;
    public bool playerCanMove;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        movePoint.parent = null;
        isPlayerTurn = true;
        playerCanMove = true;

        enemyManager = GetComponentInChildren<EnemyManager>();
        cursorController = GameObject.Find("Cursor").GetComponent<CursorController>();
        playerHealth = gameObject.GetComponentInParent<PlayerHealth>();

        movesLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (movesLeft == 0)
        {
            movesLeft = 2;
            if (enemyCount > 0)
            {
                StartCoroutine(ChangeTurn());
            }
        }

        if (playerHealth.playerHealth == 0)
        {
            enabled = !enabled;
        }
        // Move sprite towards movePoint location
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // If the sprite has caught up to the movepoint, allow movement
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && movesLeft > 0)
        {
            if (isPlayerTurn && playerCanMove)
            {
                if (enemyCount > 0)
                {
                    RunPlayerMovementWithEnemy();
                }
                else
                {
                    RunPlayerMovementWithoutEnemy();
                }
            }
             
        }
    }

    private void RunPlayerMovementWithEnemy()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.2f, whatStopsMovement) && CheckForCollisionWithEnemy("Horizontal"))
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                movesLeft -= 1;
            }
            
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 0.2f, whatStopsMovement) && CheckForCollisionWithEnemy("Vertical"))
            {
                movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                movesLeft -= 1;
            }
            
        }
    }

    private void RunPlayerMovementWithoutEnemy()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                movesLeft -= 1;
            }

        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                movesLeft -= 1;
            }

        }
    }

    public IEnumerator ChangeTurn()
    {
        playerCanMove = false;
        cursorController.canAttack = false;
        yield return new WaitForSeconds(0.5f);
        isPlayerTurn = false;
        enemyManager.currentEnemyTurn = 0;
    }

    private bool CheckForCollisionWithEnemy(string axis)
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (axis == "Horizontal")
            {
                if (enemy.transform.position == movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0))
                {
                    return false;
                }
            }

            if (axis == "Vertical")
            {
                if (enemy.transform.position == movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0))
                {
                    return false;
                }
            }
        }

        return true;
    }
}

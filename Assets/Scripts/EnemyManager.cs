using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private PlayerController playerController;
    private EnemyController currentEnemyScript;
    private int turnOrderNum;
    public List<GameObject> enemies;

    public int currentEnemyTurn;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        RestackEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.enemyCount < enemies.Count)
        {
            RestackEnemies();
        }
    }

    private void RestackEnemies()
    {
        currentEnemyTurn = 0;
        enemies.Clear();
        turnOrderNum = 0;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
            currentEnemyScript = enemy.GetComponent<EnemyController>();
            currentEnemyScript.turnOrderNum = turnOrderNum;
            turnOrderNum++;
        }
    }
}

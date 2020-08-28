using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager _instance;
    public static EnemiesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EnemiesManager>();
            }

            return _instance;
        }
    }
    public GameObject enemyPrefab;


    private void Start()
    {
        //var go = Instantiate(enemyPrefab);
        //go.name = "Goblin";
        //BoardManager.Instance.AddToGameBoard(go.GetComponent<Enemy>(), new Vector3Int(1, 1, 0));

        //go = Instantiate(enemyPrefab);
        //go.name = "Billy";
        //go.transform.position = new Vector3(-3.5f, -4.5f, 0);
        //BoardManager.Instance.AddToGameBoard(go.GetComponent<Enemy>());
    }

    public void DoEnemyTurn()
    {
        foreach (var e in BoardManager.Instance.EnemiesOnBoard)
        {
            var enemy = (Enemy)e;
            enemy.DoTurn();
        }
    }

   
    private void AddEnemyToBoard()
    {
        var go = Instantiate(enemyPrefab);
        BoardManager.Instance.AddCharacterToGameBoard(go.GetComponent<Enemy>());
    }
}

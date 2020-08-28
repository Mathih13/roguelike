using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyData : Singleton<EnemyData>
{

    public List<SerializeableEnemy> Enemies { get; private set; }


    [SerializeField]
    private DataSource dataSource;

    public void LoadItems()
    {
        Enemies = new List<SerializeableEnemy>();

        var loader = new DataLoader<JsonDataContainer<SerializeableEnemy>>(dataSource);
        loader.LoadData();

        foreach (var item in loader.Memory.data)
        {
            Enemies.Add(item);
        }
    }

    public SerializeableEnemy GetEnemyData()
    {
        return Enemies[UnityEngine.Random.Range(0, Enemies.Count)];
    }

    public SerializeableEnemy GetEnemyData(string name)
    {
        return Enemies.FirstOrDefault(x => x.Name == name);
    }
}

using UnityEngine;
using System.Collections;
using Strata;

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    private BoardGenerator boardGenerator;

    public float Progress;
    public bool Loaded;

    private void Awake()
    {
        StartCoroutine(LoadAndBuild());
    }

    IEnumerator LoadAndBuild()
    {
        ItemData.Instance.LoadItems();
        Progress += 20;

        EnemyData.Instance.LoadItems();


        yield return boardGenerator.BuildLevel();

        BoardEventHub.Instance.BoardGenerated();
        yield return new WaitForSeconds(2f);
        Progress += 20; Loaded = true;
    }

}

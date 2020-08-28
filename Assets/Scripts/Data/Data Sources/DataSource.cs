using UnityEngine;
using System.Collections;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Data loading/Data source")]
public class DataSource : ScriptableObject, IDataSource
{
    public bool File;
    public bool Url;

    public string path;

    public DataSourceInfo GetData()
    {
        return new DataSourceInfo
        {
            File = File,
            Url = Url,
            Path = path
        };
    }
}

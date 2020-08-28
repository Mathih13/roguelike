using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataLoader<T>
{
    public T Memory;
    private IDataSource source;

    public DataLoader(IDataSource source)
    {
        this.source = source;
    }

    public void LoadData()
    {
        var info = source.GetData();

        if (info.Url)
        {
            LoadDataFromUrl(info.Path);
            return;
        }

        if (info.File)
        {
            LoadDataFromResourceFolder(info.Path);
            return;
        }

        Debug.LogError($"Unable to load data {info.Path}, source is neither file or URL");
    }

    private void LoadDataFromResourceFolder(string path)
    {
        string filePath = "Gamedata/" + path.Replace(".json", "");

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        var rawJson = targetFile.text;

        Memory = JsonUtility.FromJson<T>(rawJson);
    }

    private void LoadDataFromUrl(string path)
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class JsonDataContainer<T>
{
    public T[] data;
}

public interface IDataSource
{
    DataSourceInfo GetData();
}

public struct DataSourceInfo
{
    public bool Url { get; set; }
    public string Path { get; set; }
    public bool File { get; set; }
}
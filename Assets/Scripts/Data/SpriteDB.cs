using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class SpriteDB : Singleton<SpriteDB>
{
    Tile[] colored;
    Tile[] transparent;


    private void Start()
    {


        string path = "Tilemap/Tiles/";
        colored = Resources.LoadAll<Tile>(path + "Base/");
        transparent = Resources.LoadAll<Tile>(path + "Transparent/");
    }

    public Sprite GetSpriteFromName(string name)
    {

        bool isTransparent = name.Contains("transparent");

        if (isTransparent)
        {
            var result = transparent.FirstOrDefault(x => x.name == name);
            return result != null ? result.sprite : Resources.Load<Tile>("Tilemap/Tiles/Base/colored_0").sprite;
        }
        else
        {
            var result = colored.FirstOrDefault(x => x.name == name);
            return result != null ? result.sprite : Resources.Load<Tile>("Tilemap/Tiles/Base/colored_0").sprite;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{
    public Tilemap tilemap;
    public Vector3 offset = new Vector3(0,0.25f,0f);

    // Update is called once per frame
    void Update()
    {
        
    }
}

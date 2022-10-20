using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TileBase Ground1;
    [SerializeField] private TileBase Ground2;
    [SerializeField] private TileBase Air;

    [SerializeField] private Vector2Int Origo;

    [SerializeField] private int PlatformCount = 10;
    [SerializeField] private int PlatformLenght = 4;
    [SerializeField] private int PlatformDistance = 3;
    [SerializeField] private Vector2Int SafeZone = new Vector2Int (5, 0);

    // Start is called before the first frame update
    void Start()
    {
        int offset = SafeZone.x;
        for (int i = 0; i < PlatformCount; i++) {
            offset += PlatformDistance;
            for (int j = 0; j < PlatformLenght; j++) {
                tilemap.SetTile (new Vector3Int (offset + j, Origo.y, 0), Ground1);
            }
            offset += PlatformLenght;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

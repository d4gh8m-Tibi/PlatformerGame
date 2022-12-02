using Assets.Scripts.MapGen;
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

    [SerializeField] private Vector2Int chunkDimensions = new Vector2Int(10,10);
    [SerializeField] private int chunkSize = 10;
    private Chunk [,] chunkMap;

    // Start is called before the first frame update
    void Start()
    {
        TileFunctions.PlaceTiles (5, tilemap, Ground1, Vector2Int.zero, Vector2Int.left);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}

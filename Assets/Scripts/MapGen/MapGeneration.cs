using Assets.Scripts.MapGen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    public static MapGeneration instance;

    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TileBase Ground1;
    [SerializeField] private TileBase Ground2;
    [SerializeField] private TileBase Air;

    [SerializeField] private Vector2Int Origo;

    [SerializeField] private Vector2Int chunkDimensions = new Vector2Int(10,10);
    [SerializeField] private int chunkSize = 10;
    private Chunk [,] chunkMap;

    [SerializeField] private List<CheckPoint> checkPoints = new List<CheckPoint>();

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameController.instance.SetMapInstance();
        TileFunctions.PlaceTiles (2, tilemap, Ground1, new Vector2Int(Origo.x + 7,Origo.y - 1), Vector2Int.right);
    }

    public Vector3 GetCheckPointPositionById(int id)
    {
        if(id > checkPoints.Count || !checkPoints.Any())
        {
            return Vector3.zero;
        }

        CheckPoint checkPoint = checkPoints.FirstOrDefault(i => i.GetId() == id);
        if(checkPoint == null)
        {
            return Vector3.zero;
        }

        return checkPoint.Position;
    }
}

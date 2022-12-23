using Assets.Scripts.MapGen;
using System.CodeDom.Compiler;
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

    [SerializeField] private int pathLength = 15;
    private List<Chunk> chunkMap = new List<Chunk> ();

    [SerializeField] private List<CheckPoint> checkPoints = new List<CheckPoint> ();



    public void Awake () {
        instance = this;
    }

    void Start () {
        GameController.instance.SetMapInstance ();
        GenerateLayout ();
    }

    public Vector3 GetCheckPointPositionById (string id) {
        Vector3 startBase = new Vector3 (0, 2, 0);
        if (!checkPoints.Any ()) {
            return startBase;
        }

        CheckPoint checkPoint = checkPoints.FirstOrDefault (i => i.GetId ().Equals (id));
        if (checkPoint == null) {
            return startBase;
        }

        return checkPoint.Position;
    }


    private void GenerateLayout () {
        GeneratePath ();
        foreach (Chunk item in chunkMap) {
            GeneratePlatforms (item);
        }
    }


    private void GeneratePath () {
        chunkMap.Add (new Chunk (Origo, ChunkType.start, 10));
        Chunk nwChunk;
        for (int i = 0; i < pathLength; i++) {
            ChunkType type = (ChunkType) Random.Range (3, 4);
            int size;
            switch (type) {
                case ChunkType.start:
                size = 5;
                break;
                case ChunkType.end:
                size = 10;
                break;
                case ChunkType.rugged:
                size = 40;
                break;
                case ChunkType.twoLayer:
                size = 300;
                break;
                default:
                size = 0;
                break;
            }
            nwChunk = new Chunk (chunkMap [chunkMap.Count - 1].origo + Vector2Int.right * chunkMap [chunkMap.Count - 1].lenght, type, size);
            chunkMap.Add (nwChunk);
        }
        nwChunk = new Chunk (chunkMap [chunkMap.Count - 1].origo + Vector2Int.right * chunkMap [chunkMap.Count - 1].lenght, ChunkType.end, 10);
        chunkMap.Add (nwChunk);
    }

    private void GeneratePlatforms (Chunk chunk) {
        switch (chunk.type) {
            case ChunkType.start:
            break;
            case ChunkType.end:
            TileFunctions.PlaceTiles (10, tilemap, Ground1, chunk.origo, Vector2Int.up);
            break;
            case ChunkType.rugged:
            GenerateRugged (chunk);
            break;
            case ChunkType.twoLayer:
            GenerateTwoLayer (chunk);
            break;
            default:
            break;
        }
    }

    private void GenerateTwoLayer (Chunk chunk) {
        Random.InitState ((int)Time.time);
        int platformPadding = 2;
        int deflenght = 3;
        int platformDiff = 2;
        Vector2Int lastPosUp = chunk.origo + new Vector2Int (0, 3);
        Vector2Int lasPosDown = chunk.origo + new Vector2Int (0, -4);
        int lastLenghtUp = 0;
        int verticalDiffUp = 0;
        int lastLenghtDown = 0;
        int verticalDiffDown = 0;
        while (lastPosUp.x + lastLenghtUp - chunk.origo.x < chunk.lenght) {
            lastLenghtUp = Random.Range (deflenght, deflenght + platformDiff);
            if (verticalDiffUp > 15) {
                int diff = Random.Range (-3, -2);
                lastPosUp += new Vector2Int (0, diff);
                verticalDiffUp += diff;
            }
            else if (verticalDiffUp < -15) {
                int diff = Random.Range (1, 3);
                lastPosUp += new Vector2Int (0, diff);
                verticalDiffUp += diff;
            }
            else {
                int diff = Random.Range (-2, 3);
                lastPosUp += new Vector2Int (0, diff);
                verticalDiffUp += diff;
            }
            TileFunctions.PlaceTiles (lastLenghtUp, tilemap, Ground2, lastPosUp, Vector2Int.right);
            int padd = Random.Range (platformPadding, platformPadding + 2);
            lastPosUp += new Vector2Int (lastLenghtUp + padd, 0);
        }

        while (lasPosDown.x + lastLenghtDown - chunk.origo.x < chunk.lenght) {
            lastLenghtDown = Random.Range (deflenght, deflenght + platformDiff);
            if (verticalDiffDown > 15) {
                int diff = Random.Range (-3, -2);
                lasPosDown += new Vector2Int (0, diff);
                verticalDiffDown += diff;
            }
            else if (verticalDiffDown < -15) {
                int diff = Random.Range (1, 3);
                lasPosDown += new Vector2Int (0, diff);
                verticalDiffDown += diff;
            }
            else {
                int diff = Random.Range (-2, 3);
                lasPosDown += new Vector2Int (0, diff);
                verticalDiffDown += diff;
            }
            TileFunctions.PlaceTiles (lastLenghtDown, tilemap, Ground2, lasPosDown, Vector2Int.right);
            int padd = Random.Range (platformPadding, platformPadding + 2);
            lasPosDown += new Vector2Int (lastLenghtDown + padd, 0);
        }
    }

    private void GenerateRugged (Chunk chunk) {

    }
}

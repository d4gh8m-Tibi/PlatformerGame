using Assets.Scripts.MapGen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    public static MapGeneration instance;

    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TileBase Ground1;
    [SerializeField] private TileBase PlatformMaterial;
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
            ChunkType type = ChunkType.room;//(ChunkType) Random.Range (2, 4);
            int size;
            switch (type) {
                case ChunkType.start:
                size = 5;
                break;
                case ChunkType.end:
                size = 10;
                break;
                case ChunkType.room:
                size = 16;
                break;
                case ChunkType.twoLayer:
                size = 300;
                break;
                default:
                size = 0;
                break;
            }
            nwChunk = new Chunk (chunkMap.Last().origo + Vector2Int.right * chunkMap.Last().lenght, type, size);
            chunkMap.Add (nwChunk);
        }
        nwChunk = new Chunk (chunkMap.Last ().origo + Vector2Int.right * chunkMap.Last ().lenght, ChunkType.end, 10);
        chunkMap.Add (nwChunk);
    }

    private void GeneratePlatforms (Chunk chunk) {
        switch (chunk.type) {
            case ChunkType.start:
            break;
            case ChunkType.end:
            TileFunctions.PlaceTiles (10, tilemap, Ground1, chunk.origo, Vector2Int.up);
            break;
            case ChunkType.room:
            GenerateRoom (Random.Range(0,3), chunk);
            break;
            case ChunkType.twoLayer:
            GenerateTwoLayer (chunk);
            break;
            default:
            break;
        }
    }

    private void GenerateTwoLayer (Chunk chunk) {
        int [] verticalPos = new int [2] { 8, 0 };
        int [] horizontalPos = new int [2] { 0, 0 };
        int minPadding = 2;
        int maxPadding = 4;
        int midPlatformChance = 10;

        List<Platform> upperPlatformList = new List<Platform> ();
        List<Platform> lowerPlatformList = new List<Platform> ();
        upperPlatformList.Add (GenerateAPlatform (horizontalPos [0], verticalPos [0], 0, null));
        lowerPlatformList.Add (GenerateAPlatform (horizontalPos [1], verticalPos [1], 0, null));
        while (horizontalPos [0] < chunk.lenght || horizontalPos [1] < chunk.lenght) {

            int upperPadding = Random.Range (minPadding, maxPadding);
            int lowerPadding = Random.Range (minPadding, maxPadding);
            int upperVertical = verticalPos [0] + Random.Range (-2, 3);
            int lowerVertical = verticalPos [1] + Random.Range (-2, 3);

            int midplat = Random.Range (0, 100);

            if (midplat < midPlatformChance) {
                Platform upper = GenerateAPlatform (horizontalPos [0], +((upperVertical - lowerVertical) / 2), upperPadding, null);
                verticalPos [0] = upperVertical;
                horizontalPos [0] = upper.origo.x + upper.length;
                chunk.platforms.Add (upper);
                TileFunctions.PlaceTiles (upper.length, tilemap, PlatformMaterial, upper.origo, Vector2Int.right);
            }

            if (upperVertical > 8) {
                upperVertical = 8;
            }
            else if (upperVertical < 4) {
                upperVertical = 4;
            }
            if (lowerVertical > 2) {
                lowerVertical = 2;
            }
            else if (lowerVertical < -2) {
                lowerVertical = -2;
            }

            if (horizontalPos [0] < chunk.lenght) {
                Platform upper = GenerateAPlatform (horizontalPos [0], upperVertical, upperPadding, upperPlatformList.Last ());
                verticalPos [0] = upperVertical;
                horizontalPos [0] = upper.origo.x + upper.length;
                upperPlatformList.Add (upper);
                chunk.platforms.Add (upper);
                TileFunctions.PlaceTiles (upper.length, tilemap, PlatformMaterial, upper.origo, Vector2Int.right);
            }
            if (horizontalPos [1] < chunk.lenght) {
                Platform lower = GenerateAPlatform (horizontalPos [1], lowerVertical, lowerPadding, lowerPlatformList.Last ());
                verticalPos [1] = lowerVertical;
                horizontalPos [1] = lower.origo.x + lower.length;
                lowerPlatformList.Add (lower);
                chunk.platforms.Add (lower);
                TileFunctions.PlaceTiles (lower.length, tilemap, PlatformMaterial, lower.origo, Vector2Int.right);
            }
        }

    }

    private Platform GenerateAPlatform (int horizontalPos, int verticalPos, int padding, Platform previous) {
        return new Platform (new Vector2Int (horizontalPos + padding, verticalPos), Random.Range (1, 6), previous);
    }


    private void GenerateRoom (int id, Chunk chunk) {
        if (id == 0) {
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 21) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 20) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (7, tilemap, PlatformMaterial, new Vector2Int (3, 15) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (0, 13) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (15, 14) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (0, 11) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (9, 10) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (16, 9) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (0, 6) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (14, 6) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (0, 5) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (12, 5) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 4) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (14, tilemap, PlatformMaterial, new Vector2Int (2, 3) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (10, tilemap, PlatformMaterial, new Vector2Int (4, 2) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (10, 1) + chunk.origo, Vector2Int.right);
        }
        else if (id == 1) {
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 21) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 20) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (11, 15) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (5, 14) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (10, 14) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (4, 13) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (1, 10) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (1, 9) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (1, 8) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (1, 7) + chunk.origo, Vector2Int.right);

            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (11, 8) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (10, 7) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (9, 6) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (8, 5) + chunk.origo, Vector2Int.right);

            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (5, 3) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (6, tilemap, PlatformMaterial, new Vector2Int (0, 2) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (7, 2) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (14, 2) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (6, tilemap, PlatformMaterial, new Vector2Int (0, 1) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (7, 1) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (14, 1) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (1, 0) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (7, tilemap, PlatformMaterial, new Vector2Int (8, 0) + chunk.origo, Vector2Int.right);

        }
        else if (id == 2) {
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 21) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 20) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (0, 19) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (9, 19) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (14, 19) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (0, 18) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (9, 18) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (15, 18) + chunk.origo, Vector2Int.right);

            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (0, 14) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (4, 14) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (9, 14) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (9, 13) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (8, 12) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (6, tilemap, PlatformMaterial, new Vector2Int (7, 11) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (0, 10) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (0, 9) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (0, 8) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (0, 7) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (9, 10) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (9, 9) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (9, 8) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (9, 7) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (9, tilemap, PlatformMaterial, new Vector2Int (7, 6) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (6, tilemap, PlatformMaterial, new Vector2Int (9, 6) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (9, 5) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (7, tilemap, PlatformMaterial, new Vector2Int (6, 4) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (6, tilemap, PlatformMaterial, new Vector2Int (7, 3) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (8, 2) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (9, 1) + chunk.origo, Vector2Int.right);

            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (0, 3) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (0, 2) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (0, 1) + chunk.origo, Vector2Int.right);

        }
    }

    private void GenerateRugged (Chunk chunk) {

    }
}

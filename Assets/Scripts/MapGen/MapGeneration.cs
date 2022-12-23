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

    [SerializeField] private CheckPoint checkPointPrefab;
    [SerializeField] private GameObject chParent; 
    [SerializeField] private List<CheckPoint> checkPoints = new List<CheckPoint> ();
    [SerializeField] private GameObject deathzone;


    public void Awake () {
        instance = this;
    }

    void Start () {
        GameController.instance.SetMapInstance ();
        GenerateLayout ();
        Random.InitState ((int) System.DateTime.Now.Ticks);
        deathzone.transform.localScale = new Vector3 (100, 1, 1);
    }

    public Vector3 GetCheckPointPositionById (string id) {
        Vector3 startBase = new Vector3 (0, 2, 0);
        if (!checkPoints.Any ()) {
            return startBase;
        }
        foreach (var item in checkPoints) {
            Debug.Log (item.GetId ());
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
        Chunk start = new Chunk (Origo, ChunkType.start, 10);
        chunkMap.Add (start);
        GenerateStart (start);
        Chunk nwChunk;
        for (int i = 0; i < pathLength; i++) {
            ChunkType type = (ChunkType) Random.Range (2, 3);
            int size;
            switch (type) {
                case ChunkType.start:
                size = 10;
                break;
                case ChunkType.end:
                size = 10;
                break;
                case ChunkType.room:
                size = 16;
                break;
                case ChunkType.twoLayer:
                size = 60;
                break;
                default:
                size = 0;
                break;
            }
            nwChunk = new Chunk (chunkMap.Last ().origo + Vector2Int.right * chunkMap.Last ().lenght, type, size);
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
            GenerateEnd (chunk);
            break;
            case ChunkType.room:
            GenerateRoom (Random.Range (0, 3), chunk);
            break;
            case ChunkType.twoLayer:
            GenerateTwoLayer (chunk);
            break;
            default:
            break;
        }
    }

    private void GenerateTwoLayer (Chunk chunk) {
        Debug.Log ("asd");
        int vertoffset = 12;
        int [] verticalPos = new int [2] { chunk.origo.y + vertoffset, chunk.origo.y };
        int [] horizontalPos = new int [2] { chunk.origo.x, chunk.origo.x };
        int minPadding = 2;
        int maxPadding = 4;
        int midPlatformChance = 25;
        Debug.Log (horizontalPos [0]);
        List<Platform> upperPlatformList = new List<Platform> ();
        List<Platform> lowerPlatformList = new List<Platform> ();
        upperPlatformList.Add (GenerateAPlatform (horizontalPos [0], verticalPos [0], 0, null));
        lowerPlatformList.Add (GenerateAPlatform (horizontalPos [1], verticalPos [1], 0, null));
        while (horizontalPos [0] < chunk.origo.x + chunk.lenght || horizontalPos [1] < chunk.origo.x + chunk.lenght) {

            int upperPadding = Random.Range (minPadding, maxPadding);
            int lowerPadding = Random.Range (minPadding, maxPadding);
            int upperVertical = verticalPos [0] + Random.Range (-2, 3);
            int lowerVertical = verticalPos [1] + Random.Range (-2, 3);

            int midplat = Random.Range (0, 100);

            if (midplat < midPlatformChance) {
                Platform middle = GenerateAPlatform (horizontalPos [0], +((upperVertical - lowerVertical) / 2), upperPadding, null);
                chunk.platforms.Add (middle);
                TileFunctions.PlaceTiles (middle.length, tilemap, PlatformMaterial, middle.origo, Vector2Int.right);
            }

            if (upperVertical > 14) {
                upperVertical = 14;
            }
            else if (upperVertical < 4) {
                upperVertical = 4;
            }
            if (lowerVertical > 8) {
                lowerVertical = 8;
            }
            else if (lowerVertical < 0) {
                lowerVertical = 0;
            }

            if (horizontalPos [0] < chunk.origo.x + chunk.lenght) {
                Platform upper = GenerateAPlatform (horizontalPos [0], upperVertical, upperPadding, upperPlatformList.Last ());
                Debug.Log (upper.origo);
                verticalPos [0] = upperVertical;
                horizontalPos [0] = upper.origo.x + upper.length;
                upperPlatformList.Add (upper);
                chunk.platforms.Add (upper);
                TileFunctions.PlaceTiles (upper.length, tilemap, PlatformMaterial, upper.origo, Vector2Int.right);
            }
            if (horizontalPos [1] < chunk.origo.x + chunk.lenght) {
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

    private void GenerateStart (Chunk chunk) {
        TileFunctions.PlaceTiles (3, tilemap, Ground1, chunk.origo + new Vector2Int (4, 6), Vector2Int.right);
        TileFunctions.PlaceTiles (3, tilemap, Ground1, chunk.origo + new Vector2Int (2, 5), Vector2Int.right);
        TileFunctions.PlaceTiles (3, tilemap, Ground1, chunk.origo + new Vector2Int (0, 3), Vector2Int.right);
        TileFunctions.PlaceTiles (10, tilemap, Ground1, chunk.origo, Vector2Int.right);
    }

    private void GenerateEnd (Chunk chunk) {
        TileFunctions.PlaceTiles (chunk.lenght, tilemap, Ground1, chunk.origo, Vector2Int.right);
    }

    private void GenerateRoom (int id, Chunk chunk) {
        CheckPoint checkpoint = null;
        if (id == 0) {
            checkpoint = Instantiate (checkPointPrefab, new Vector3 (chunk.origo.x+3, chunk.origo.y+7, 0), Quaternion.identity, chParent.transform);
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 21) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 20) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (7, tilemap, PlatformMaterial, new Vector2Int (3, 15) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (0, 13) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (15, 14) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (0, 11) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (3, tilemap, PlatformMaterial, new Vector2Int (9, 10) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (16, 9) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (0, 6) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (1, tilemap, PlatformMaterial, new Vector2Int (14, 7) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (2, tilemap, PlatformMaterial, new Vector2Int (13, 6) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (5, tilemap, PlatformMaterial, new Vector2Int (0, 5) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (12, 5) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (16, tilemap, PlatformMaterial, new Vector2Int (0, 4) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (14, tilemap, PlatformMaterial, new Vector2Int (2, 3) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (10, tilemap, PlatformMaterial, new Vector2Int (4, 2) + chunk.origo, Vector2Int.right);
            TileFunctions.PlaceTiles (4, tilemap, PlatformMaterial, new Vector2Int (10, 1) + chunk.origo, Vector2Int.right);
        }
        else if (id == 1) {
            checkpoint = Instantiate (checkPointPrefab, new Vector3 (chunk.origo.x + 3, chunk.origo.y + 11, 0), Quaternion.identity, chParent.transform);
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
            checkpoint = Instantiate (checkPointPrefab, new Vector3 (chunk.origo.x + 3, chunk.origo.y + 11, 0), Quaternion.identity, chParent.transform);
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
        
        checkpoint.SetId (checkPoints.Count+2);
        checkpoint.name = checkpoint.GetId();
        checkPoints.Add (checkpoint);
    }
}

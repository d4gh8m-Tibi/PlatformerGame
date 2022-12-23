using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk
{
    public Vector2Int origo;
    public ChunkType type;
    public int lenght;

    public Chunk (Vector2Int origo, ChunkType type, int lenght) {
        this.origo = origo;
        this.type = type;
        this.lenght = lenght;
    }
}

public enum ChunkType
{
    start = 0,
    end = 1,
    rugged = 2,
    twoLayer = 3,
}


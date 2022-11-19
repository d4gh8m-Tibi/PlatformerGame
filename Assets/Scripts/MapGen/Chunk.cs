using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk
{
    public Vector2Int origo;
    public ChunkType type;

    public Chunk (Vector2Int origo, ChunkType type) {
        this.origo = origo;
        this.type = type;
    }
}

public enum ChunkType
{
    start = 0,
    end = 1,
    vTunnel = 2,
    hTunnel = 3,
    nwCorner = 4,
    swCorner = 5,
    neCorner = 6,
    seCorner = 7,
    nwTsection = 8,
    swTsection = 9,
    neTsection = 10,
    seTsection = 11,
}


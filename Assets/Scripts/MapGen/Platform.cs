using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform
{
    public Vector2Int origo;
    public int length;
    public Platform previousPlatform;

    public Platform(Vector2Int origo, int length) {
        this.origo = origo;
        this.length = length;
        previousPlatform = null;
    }

    public Platform(Vector2Int origo, int length, Platform previous) { 
        this.origo = origo;
        this.length = length;
        previousPlatform = previous;
    }
}

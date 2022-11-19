using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using UnityEngine;

namespace Assets.Scripts.MapGen
{
    internal class TileFunctions
    {
        public static void PlaceTiles (int number, Tilemap tilemap, 
            TileBase tileBase, Vector2Int start, Vector2Int direction) {
            for (int i = 0; i < number; i++) {
                tilemap.SetTile (((Vector3Int) start) + ((Vector3Int)direction * i), tileBase);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private float cycleTime;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private bool down = false;
    private Vector2Int direction;

    public void Start()
    {
        direction = down ? new Vector2Int(0,-1) : new Vector2Int(0,1);
        InvokeRepeating("SpawnBullets", 3, 2);
    }

    private void SpawnBullets()
    {         
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Init("Player", direction, 10);
    }

}

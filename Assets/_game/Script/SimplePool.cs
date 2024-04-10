using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePool : MonoBehaviour
{
    public static SimplePool instance;
    public GameObject bulletPrefabs;
    public GameObject explosion;
    public List<GameObject> bulletPool;
    public List<GameObject> enemyBulletPool;
    void Awake()
    {
        instance = this;
        SpawnAndPoolBullet();
    }
    void SpawnAndPoolBullet()
    {
        for(int i = 0; i < 30; i++)
        {
            GameObject bullet = Instantiate(bulletPrefabs,transform);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    void SpawnAndPoolEnemyBullet()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet = Instantiate(bulletPrefabs, transform);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    public GameObject GetPooledBullet()
    {
        foreach(GameObject bullet in bulletPool)
        {
            if(!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
    }
}

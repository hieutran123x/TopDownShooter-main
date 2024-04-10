using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtType3 : Enemy
{
    public float waitToShootTime = 0.75f;

    public override void Update()
    {
        base.Update();
        waitToShootTime -= Time.deltaTime;
        if(waitToShootTime <= 0)
        {
            Shoot();
            waitToShootTime = 0.75f;
        }
    }
    public void Shoot()
    {
        GameObject bullet = SimplePool.instance.GetPooledBullet();
        if(bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
}

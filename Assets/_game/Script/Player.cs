using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isDead => hp <= 0;
    public float speed = 5.0f;
    public float timeEachShoot = 0.5f;
    public int hp = 5;
    public int ammo = 7;
    public Joystick controller;
    public Animator anim;
    public static Player instance;
    public GameObject gunHolder;
    public GameObject enemy;
    public Rigidbody2D rb;
    public GameObject avatar;
    [SerializeField] string currentAnim="";
    void Start()
    {
        instance = this;
        FindNearestEnemy();
    }

    void Update()
    {
        RotateGunHolder();
        if (ammo != 0 && !isDead)
        {
            if (timeEachShoot <= 0)
            {
                Shoot();
                timeEachShoot = 0.5f;
                ammo--;
            }
            else
            {
                timeEachShoot -= Time.deltaTime;
            }
        }
        Move();
    }
    //void FixedUpdate()
    //{
    //    ScreenBoundary();
    //    Move();
    //}

    public void Move()
    {
        if(controller == null)
        {
            controller = GameObject.FindAnyObjectByType<Joystick>();
        }
        Vector2 input = new Vector2(controller.Horizontal, controller.Vertical);
        if(input != Vector2.zero && !isDead)
        {
            //if player moving to the left side, flip the player
            if(input.x < 0)
            {
                avatar.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if(input.x > 0)
            {
                avatar.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            ChangeAnim("run");
            transform.Translate(input * speed * Time.deltaTime);
        }
        else
        {
            if (!isDead)
                ChangeAnim("idle");
        }
        ScreenBoundary();
    }
    public void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        if(enemies.Length == 0)
        {
            enemy = null;
            return;
        }
        foreach(GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                this.enemy = enemy;
                distance = curDistance;
            }
        }
    }
    //add to update if player use gun

    public void RotateGunHolder()
    {
        //if ammo == 0 mak the gun holder spin 360 degree to looks like reload
        if (!isDead)
        {
            if (ammo == 0)
            {
                gunHolder.transform.GetChild(0).transform.Rotate(0, 0, 360 * Time.deltaTime);
                StartCoroutine(ShootAndReload());
                return;
            }
        }
        if (enemy != null)
        { 
            Vector3 diff = enemy.transform.position - gunHolder.transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            gunHolder.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            FindNearestEnemy();
        }
    }

    public void Shoot()
    {
        FindNearestEnemy();
        if (enemy == null)
            return;
        GameObject clonedBullet = SimplePool.instance.GetPooledBullet();
        if(clonedBullet != null)
        {
            //rotate the bullet to the same direction as the gun holder
            clonedBullet.transform.rotation = gunHolder.transform.GetChild(0).transform.rotation;
            clonedBullet.transform.position = gunHolder.transform.GetChild(0).transform.position;
            clonedBullet.SetActive(true);
        }
    }

    public IEnumerator ShootAndReload()
    {
        yield return new WaitForSeconds(1.5f);
        ammo = 7;
    }

    public void Hit()
    {
        //push player back a bit
        if(hp ==1)
        {
            Dead();
        }
        rb.AddForce(new Vector2(-transform.localScale.x, 1) * 5, ForceMode2D.Impulse);
        hp--;
        rb.velocity = Vector2.zero;
    }

    public void Dead()
    {
        hp = 0;
        ChangeAnim("dead");
        GameManagerController.instance.YouLose();
    }

    public void ChangeAnim(string newAnim)
    {
        if(currentAnim != "")
        {
            if(currentAnim.CompareTo(newAnim)!=0)
                anim.ResetTrigger(currentAnim);
        }
        currentAnim = newAnim;
        anim.SetTrigger(currentAnim);
    }

    public void AppliedSlowEffect()
    {
        speed -= 1.5f;
        timeEachShoot += 0.25f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Hit();
        }
    }

    void ScreenBoundary()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -10f, 10f),
            Mathf.Clamp(transform.position.y, -20f, 20f), transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ProjectilesScript : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float existanceTime = 2f;
    public int damage = 15;
    void Update()
    {
        
        if (existanceTime <= 0)
        {
            gameObject.SetActive(false);
            existanceTime = 2f;
        }
        else
        {
            existanceTime -= Time.deltaTime;
        }

        transform.transform.Translate(UnityEngine.Vector2.right * speed * Time.deltaTime);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            gameObject.SetActive(false);
            existanceTime = 2f;
        }        
    }
}

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWhenTouch : MonoBehaviour
{
    public float existanceTime = 0.5f;
    private void Update()
    {
        existanceTime -= Time.deltaTime;
        if (existanceTime <= 0)
        {
            gameObject.SetActive(false);
            existanceTime = 0.5f;
        }
    }
    //after active check if any game object in in the collider
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(5);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Hit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(5);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Hit();
        }
    }
}

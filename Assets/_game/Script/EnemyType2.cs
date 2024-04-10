using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : Enemy
{
    public GameObject explode;
    public override IEnumerator Die()
    {
        ChangeAnim("die");
        yield return new WaitForSeconds(0.2f);
        Debug.Log("run");
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

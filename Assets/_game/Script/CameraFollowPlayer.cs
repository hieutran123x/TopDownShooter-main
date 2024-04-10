using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
            player = GameObject.FindWithTag("Player");
        else
            transform.position = player.transform.position + new Vector3(0,0,-10);
    }
}

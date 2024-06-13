using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour
{

    GameObject player;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.LookAt(player.transform);
        Vector3 forceVector = transform.TransformDirection(new Vector3(0, 0, 100f * Speed));
        GetComponent<Rigidbody>().AddForce(new Vector3(forceVector.x, 0, forceVector.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

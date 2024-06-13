using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public int angle;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(Speed * angle, 0, Speed);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 45 * angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

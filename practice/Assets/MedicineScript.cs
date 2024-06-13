using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody Medicine = GetComponent<Rigidbody>();
        Medicine.velocity = new Vector3(0, 0, -30);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

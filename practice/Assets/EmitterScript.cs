using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    public GameObject[] Asteroids;
    public GameObject Medicine;
    public float minDelay, maxDelay;
    public float medicineDelay;

    float nextLaunchTime;
    float medicineLaunchTime;

    void Start() {
        medicineLaunchTime = Time.time + medicineDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.getIsStarted() ||
        GameControllerScript.instance.getIsPaused()) {
            return;
        } else {
            if (Time.time > nextLaunchTime) {
                float xPosition = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
                Instantiate(Asteroids[Random.Range(0, Asteroids.Length)],
                 new Vector3(xPosition, 10, transform.position.z),
                Quaternion.identity);
                nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
            }

            if (Time.time > medicineLaunchTime) {
                float xPosition = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
                Instantiate(Medicine, new Vector3(xPosition, 10, transform.position.z),
                Quaternion.identity);
                medicineLaunchTime = Time.time + medicineDelay;                
            }
        }
    }
}

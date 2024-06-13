using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public GameObject AsteroidExplosion;
    public GameObject PlayerExplosion;
    bool enter = false;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody Asteroid = GetComponent<Rigidbody>();
        Asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        float zSpeed = Random.Range(minSpeed, maxSpeed);
        Asteroid.velocity = new Vector3(0, 0, -zSpeed);      
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "GameBoundary"|| other.tag == "Medicine") {
            return;
        } else if (other.tag == "Enemy") {
            Instantiate(PlayerExplosion, other.transform.position, Quaternion.identity);
        } else if (other.tag == "Laser"){
            if (!enter) {
                GameControllerScript.instance.increaseScore(10);
            }
            Destroy(other.gameObject);
            enter = true;
        }
        Instantiate(AsteroidExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    float zSpeed;
    GameObject player;
    public float minSpeed, maxSpeed;
    public GameObject Explosion;
    public GameObject PlayerExplosion;
    Rigidbody Enemy;
    public GameObject LaserShot;
    public GameObject LaserGun;
    public float shotDelay;
    float nextShotTime;
    bool enter = false;
    

    // Start is called before the first frame update
    void Start()
    {
        nextShotTime = Time.time + 0.2f;        
        player = GameObject.FindWithTag("Player");
        Enemy = GetComponent<Rigidbody>();
        zSpeed = Random.Range(minSpeed, maxSpeed);
        Enemy.rotation = Quaternion.Euler(0, 180, 0);
    }

    void Update() {
        if (player != null) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, zSpeed * Time.deltaTime);
            transform.LookAt(player.transform);
        } else {
            Enemy.velocity = new Vector3(0, 0, -zSpeed); 
        }

        if (Time.time > nextShotTime && player != null) {
                Instantiate(LaserShot, LaserGun.transform.position, Quaternion.identity);
                nextShotTime = Time.time + shotDelay;
            }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "GameBoundary") {
            return;
        } else if (other.tag == "Player") {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        } else if (other.tag == "Laser"){
            if (!enter) {
                GameControllerScript.instance.increaseScore(20);
            }
            enter = true;
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        } else if (other.tag == "Asteroid" || other.tag == "Enemy") {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}

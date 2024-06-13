using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    Rigidbody Ship;
    public float Speed;
    public float xMin, xMax, zMin, zMax;
    public float tilt;
    public GameObject PlayerExplosion;

    public GameObject LaserShot;
    public GameObject LaserShotDiagonalLeft;
    public GameObject LaserShotDiagonalRight;
    public GameObject LaserGun;
    public GameObject LaserGun2;
    public GameObject LaserGun3;
    public GameObject Shield;
    public float shotDelay;
    float nextShotTime;

    int shields = 0;
    public static ShipScript instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Ship = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControllerScript.instance.getIsStarted() &&
        !GameControllerScript.instance.getIsPaused()) {
            float moveVertical = Input.GetAxis("Vertical");
            float moveHorizontal = Input.GetAxis("Horizontal");
            Ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * Speed;
        
            float restrictedX = Mathf.Clamp(Ship.position.x, xMin, xMax);
            float restricredZ = Mathf.Clamp(Ship.position.z, zMin, zMax);
            Ship.position = new Vector3(restrictedX, Ship.position.y, restricredZ);

            Ship.rotation = Quaternion.Euler(Ship.velocity.z * tilt, 0, -Ship.velocity.x * tilt);
        
            if (Time.time > nextShotTime && Input.GetButton("Fire1")) {
                Instantiate(LaserShot, LaserGun.transform.position, Quaternion.identity);
                nextShotTime = Time.time + shotDelay;
            } else if (Time.time > nextShotTime && Input.GetButton("Fire2")) {
                Instantiate(LaserShotDiagonalLeft, LaserGun2.transform.position, Quaternion.identity);
                Instantiate(LaserShotDiagonalRight, LaserGun3.transform.position, Quaternion.identity);
                nextShotTime = Time.time + shotDelay;
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EnemyLaser" || other.tag == "Enemy" || other.tag == "Asteroid"){
            if (shields == 0) {
                Instantiate(PlayerExplosion, other.transform.position, Quaternion.identity);
                Destroy(gameObject);
            } else {
                shields -= 1;
                Destroy(GameObject.FindWithTag("Shield"));
                Destroy(other.gameObject);
            }
        } else if (other.tag == "Medicine"){
            shields += 1;
            Instantiate(Shield, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    public int getShields() {
        return shields;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bullet;

    public Transform transformSpawn;

    public Vector3 bulletDirection;

    
    
    void Shoot()
    {
        Instantiate(bullet, transformSpawn);

        bullet.GetComponent<Rigidbody>().AddForceAtPosition(transform.position, bulletDirection, ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        Shoot();
    }

    public int counter = 0;

    // Update is called once per frame
    void Update()
    {
        if(counter % 100 == 0)
        {
            Shoot();
        }

        counter++;

        if (counter > 10000)
            counter = 0;
    }
}

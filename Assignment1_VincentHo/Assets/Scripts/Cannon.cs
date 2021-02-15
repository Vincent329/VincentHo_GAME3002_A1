using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public static Cannon sharedInstance;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        sharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    public void ChangeCannonAim(float hAngle, float vAngle)
    {
        //transform.rotation = Quaternion.Euler(vAngle, hAngle, 0.0f);
    }
    
    public void Spawn()
    {
        GameObject projectile = BulletPool.sharedInstance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = spawnPoint.transform.position;
            projectile.transform.forward = transform.forward;
            projectile.SetActive(true);
        }
    }
}

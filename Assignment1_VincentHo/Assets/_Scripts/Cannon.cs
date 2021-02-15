using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public static Cannon sharedInstance;
    public Transform spawnPoint; // where the bullet will spawn from upon launch

    [SerializeField]
    private float fVelocityMagnitude;

    [SerializeField]
    private float fTheta;

    [SerializeField]
    private float fPhi;

    [SerializeField]
    public Vector3 vInitialVelocity;

    public GameObject targetReticle; // this is what we'll use to indicate the landing area2
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

    public void Spawn()
    {
        GameObject projectile = BulletPool.sharedInstance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = spawnPoint.transform.position;
            projectile.transform.forward = transform.forward;
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * 8;
            projectile.SetActive(true);
        }
    }

    #region CONTROL
    public void ChangeCannonAim(float hAngle, float vAngle)
    {
        fTheta = hAngle;
        fPhi = vAngle;
        transform.rotation = Quaternion.Euler(hAngle, vAngle, 0.0f);
    }
  
    Vector3 calculateLaunchDistance()
    {
        return Vector3.zero;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public static Cannon sharedInstance;
    public Transform spawnPoint; // where the bullet will spawn from upon launch

    public Transform floor; // the plane for which the target reticle will be placed upon

    [SerializeField]
    private float fVelocityMagnitude = 1.0f;

    [SerializeField]
    private float fTheta;

    [SerializeField]
    private float fPhi;

    [SerializeField]
    public Vector3 vInitialVelocity;

    public GameObject targetReticle; // this is what we'll use for a reticle

    private Target landingMarker; // We'll use this as a temporary marker

    private bool bPowerBuild;
    
    // Start is called before the first frame update
    void Start()
    {
        sharedInstance = this;
        fVelocityMagnitude = 1.0f;
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space)) // depending how long we hold the button, this dictates the velocity power
        {
            // increase the velocity power
            fVelocityMagnitude += 0.07f;
        }
        if (Input.GetKeyUp(KeyCode.Space) )
        {
            Spawn();
            fVelocityMagnitude = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

    }

    public void Spawn()
    {
        GameObject projectile = BulletPool.sharedInstance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = spawnPoint.transform.position;
            projectile.transform.forward = transform.forward; // setting the forward vector of the projectile
            //projectile.GetComponent<Rigidbody>().velocity = transform.forward * fVelocityMagnitude; // upon activation, the ball will get propelled by some velocity
            Launch(projectile);
            projectile.SetActive(true);
        }
    }

    #region CONTROL
    public void ChangeCannonAim(float fYaw, float fPitch)
    {
        fTheta = fYaw;
        fPhi = fPitch;
        transform.rotation = Quaternion.Euler(fPhi, fTheta, 0.0f);
    }
  
    Vector3 calculateLaunchDistance()
    {
        // Here we get the distance of the 
        // time = 

        float vx = fVelocityMagnitude * Mathf.Sin(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);
        float vy = fVelocityMagnitude * -Mathf.Sin(Mathf.Deg2Rad * fPhi); // may have to shift this to negative
        float vz = fVelocityMagnitude * Mathf.Cos(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);

        // for debugging
        vInitialVelocity = new Vector3(vx, vy, vz);

        // t = 2* (vf - vy)/g........... But this only works with height displacement
        // 0 = h + vy*t + 0.5*a*t^2
        // 0 = -0.5*g*t^2 + vy*t + h
        // where g is the acceleration of gravity
        // t is the time
        // h is the displacement
        // vy is the vertical velocity
        // must multiply by -1 to avoid a negative number

        // t = (-vy (+ or -) Sqrt(vy^2 - 4gh)) / 2a 
        Vector3 offset = transform.position - spawnPoint.transform.position;

        float h = floor.transform.position.y - spawnPoint.transform.position.y;
        float a = Physics.gravity.y;

        float time = (-vy + Mathf.Sqrt((vy*vy) - 4*a*h))/ (2*a); // the returning value must be a positive number, debug this if need be

        float dX = vx * time + offset.x;
        float dZ = vz * time + offset.z;

        Vector3 distance = new Vector3(dX, h, dZ);
        //Debug.Log(vInitialVelocity);

        return distance;
    }

    void Launch(GameObject projectile)
    {
        // ISOLATING THE VELOCITY VALUES
        float vx = fVelocityMagnitude * Mathf.Sin(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);
        float vy = fVelocityMagnitude * -Mathf.Sin(Mathf.Deg2Rad* fPhi); // may have to shift this to negative
        float vz = fVelocityMagnitude * Mathf.Cos(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);

        // for debugging
        vInitialVelocity = new Vector3(vx, vy, vz);

        Debug.Log(vInitialVelocity);

        projectile.GetComponent<Rigidbody>().velocity = vInitialVelocity;
    }
    #endregion
}

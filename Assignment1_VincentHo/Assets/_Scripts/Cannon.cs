using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Bullet Pool
    public static Cannon sharedInstance;

    [SerializeField]
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
            fVelocityMagnitude += 0.05f;
            Debug.Log(calculateLaunchDistance());
            targetReticle.transform.position = calculateLaunchDistance();
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

        //float vx = fVelocityMagnitude * Mathf.Sin(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);
        //float vy = fVelocityMagnitude * -Mathf.Sin(Mathf.Deg2Rad * fPhi); // may have to shift this to negative
        //float vz = fVelocityMagnitude * Mathf.Cos(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);

        //// for debugging
        //vInitialVelocity = new Vector3(vx, vy, vz);

        VelocityGain();

        // t = 2* (vf - vy)/g........... But this only works with height displacement
        // 0 = h + vy*t + 0.5*a*t^2
        // 0 = -0.5*g*t^2 + vy*t + h
        // where g is the acceleration of gravity
        // t is the time
        // h is the displacement
        // vy is the vertical velocity
        // must multiply by -1 to avoid a negative number

        // 0 = h + vy * t + 0.5*g*t^2

        // t = (-vy (+ or -) Sqrt(vy^2 - 4gh)) / 2a 
        Vector3 offset = transform.position - spawnPoint.transform.position;
        float vx = vInitialVelocity.x;
        float vy = vInitialVelocity.y;
        float vz = vInitialVelocity.z;
        // yf - yi
        float h = spawnPoint.transform.position.y - floor.transform.position.y;
        
        float a = Physics.gravity.y; // - 9.81
        //Debug.Log(a);
        //Debug.Log(test);
        a *= 0.5f; // - 4.9 m/s^2

        // quadratic equation: 0 = h + vy*t + 0.5*g*t^2
        // we must solve for time, predict how long the ball will stay in the air
        // time = (- b (+ or -) Sqrt(b^2 - 4*a*c)) / 2(a)
        float time = (-vy - Mathf.Sqrt((vy*vy) - 4*a*h)) / (2*a); // the returning value must be a positive number, debug this if need be
        float dX = vx * time;
        float dZ = vz * time;

        Vector3 distance = new Vector3(dX - offset.x, -h + 2.5f, dZ - offset.z);
        //Debug.Log(distance);

        return distance;
    }

    private void VelocityGain()
    {
        // ISOLATING THE VELOCITY VALUES
        float vx = fVelocityMagnitude * Mathf.Sin(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);
        float vy = fVelocityMagnitude * -Mathf.Sin(Mathf.Deg2Rad * fPhi); // may have to shift this to negative
        float vz = fVelocityMagnitude * Mathf.Cos(Mathf.Deg2Rad * fTheta) * Mathf.Cos(Mathf.Deg2Rad * fPhi);

        // for debugging
        vInitialVelocity = new Vector3(vx, vy, vz);
    }
    void Launch(GameObject projectile)
    {
        VelocityGain();

        //Debug.Log(vInitialVelocity);

        projectile.GetComponent<Rigidbody>().velocity = vInitialVelocity;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    // Bullet Pool
    public static Cannon sharedInstance;

    [SerializeField]
    public Transform spawnPoint; // where the bullet will spawn from upon launch
    public Transform floor; // the plane for which the target reticle will be placed upon

    [SerializeField]
    private float fVelocityMagnitude = 1.0f; // base value of 1 to start

    [SerializeField]
    private float fTheta; // yaw

    [SerializeField]
    private float fPhi; // pitch

    [SerializeField]
    private Vector3 vInitialVelocity;

    [SerializeField]
    private float fVelocityIncrement;


    public GameObject targetReticle; // this is what we'll use for a reticle

    private bool bPowerBuildDir; // if we pass a threshold, this flag will switch and change if velocity increments or decrements

    // Get the Slider, make sure to import Unity.UI
    // for slider values
    public float max = 30.0f;
    public float min = 0.0f;

    public SliderScript powerBar;


    
    // Start is called before the first frame update
    void Start()
    {
        bPowerBuildDir = true;
        sharedInstance = this;
        fVelocityMagnitude = 1.0f;
        bPowerBuildDir = true;
        targetReticle = Instantiate(targetReticle, new Vector3(0.0f, floor.transform.position.y, 0.0f), Quaternion.identity);
        powerBar.SetMaxVelocity(max);
    }

    #region GETTERS
    public float GetYaw()
    {
        return fTheta;
    }
    public float GetPitch()
    {
        return -1*fPhi;
    }

    #endregion
    private void FlipVelocityPower()
    {
        if (fVelocityMagnitude >= max)
        {
            bPowerBuildDir = false;
        }
        else if (fVelocityMagnitude < min)
        {
            bPowerBuildDir = true;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space)) // depending how long we hold the button, this dictates the velocity power
        {
            // increase the velocity power
            FlipVelocityPower();
            if (bPowerBuildDir)
            {
                fVelocityMagnitude += fVelocityIncrement;
            } 
            else
            {
                fVelocityMagnitude -= fVelocityIncrement;
            }
            // updates the power bar
            powerBar.displayVelocity(fVelocityMagnitude);

            targetReticle.transform.position = calculateLaunchDistance();
            targetReticle.transform.rotation = Quaternion.Euler(0.0f, fTheta, 0.0f);
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
        VelocityGain();

        // t = 2* (vf - vy)/g........... But this only works with height displacement

        // Projectile Motion Equation
        // where g is the acceleration of gravity
        // t is the time
        // h is the displacement
        // vy is the vertical velocity

        // Quadratic Equation
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

        Vector3 distance = new Vector3(dX - offset.x, floor.transform.position.y, dZ - offset.z);
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
        projectile.GetComponent<Rigidbody>().velocity = vInitialVelocity;
    }
    #endregion

    public void displayVelocity(float value)
    {
        value = fVelocityMagnitude;
    }
}

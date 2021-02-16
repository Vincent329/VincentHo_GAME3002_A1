using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateBase(float hAngle)
    {
        transform.rotation = Quaternion.Euler(0.0f, hAngle, 0.0f);
    }
}

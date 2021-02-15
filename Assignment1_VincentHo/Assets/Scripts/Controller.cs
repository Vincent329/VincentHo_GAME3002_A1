using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Controller : MonoBehaviour
{
    private Cannon m_Cannon;

    [SerializeField]
    private float fHorizontalRotation;
    [SerializeField]
    private float fTheta;

    [SerializeField]
    public float fRotationSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
       //m_Cannon = GetComponent<Cannon>();
       //Assert.IsNotNull(m_Cannon, "No Cannon Component");
    }

    private void HandleUserInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            fHorizontalRotation -= fRotationSpeed;
            Output();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            fHorizontalRotation += fRotationSpeed;
            Output();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            fTheta -= fRotationSpeed;
        
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            fTheta += fRotationSpeed;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.x + fTheta, transform.rotation.y + fHorizontalRotation, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();


    }

    void Output()
    {
        Debug.Log(fHorizontalRotation);
    }
}

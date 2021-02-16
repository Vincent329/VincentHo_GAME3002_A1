using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Controller : MonoBehaviour
{
    // Purpose: To control the Cannon's properties such as aiming and fire power
    // To do: implement firepower

    private Cannon m_Cannon;
    [SerializeField]
    private float fYaw = 0; // the angle for the horizontal rotation of the cannon  
    [SerializeField]
    private float fPitch = 0; // the angle for the vertical aim of the cannon

    [SerializeField]
    public float fRotationSpeed = 0.5f; // how fast we want the cannon to rotate

    private Pivot basePoint;



    // Start is called before the first frame update
    void Start()
    {
       m_Cannon = GetComponent<Cannon>();
       Assert.IsNotNull(m_Cannon, "No Cannon Component");
       basePoint = FindObjectOfType<Pivot>();
    }

    private void HandleUserInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            fYaw -= fRotationSpeed;
            fYaw = Mathf.Clamp(fYaw, -90, 90);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            fYaw += fRotationSpeed;
            fYaw = Mathf.Clamp(fYaw, -90, 90);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            fPitch -= fRotationSpeed;
            fPitch = Mathf.Clamp(fPitch, -90, 0);

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            fPitch += fRotationSpeed;
            fPitch = Mathf.Clamp(fPitch, -90, 0);

        }

        fYaw += Input.GetAxis("Mouse X");
        fPitch -= Input.GetAxis("Mouse Y");

        //transform.rotation = Quaternion.Euler(transform.rotation.x + fPitch, transform.rotation.y + fYaw, 0.0f);
        m_Cannon.ChangeCannonAim(fYaw, fPitch);
        basePoint.RotateBase(fYaw);
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();
        Cursor.visible = false;

    }

    void Output()
    {
        Debug.Log(fYaw);
    }
}
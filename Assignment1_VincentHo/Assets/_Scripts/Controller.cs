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
        // get the components of the cannon and pivot point
       m_Cannon = GetComponent<Cannon>();
       Assert.IsNotNull(m_Cannon, "No Cannon Component");
       basePoint = FindObjectOfType<Pivot>();
       Assert.IsNotNull(basePoint, "No Pivot Component");

    }

    private void HandleUserInput()
    {
        fYaw += Input.GetAxis("Mouse X");
        fPitch -= Input.GetAxis("Mouse Y");
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            fYaw -= fRotationSpeed;
           
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            fYaw += fRotationSpeed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            fPitch -= fRotationSpeed;

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            fPitch += fRotationSpeed;
        }
        fYaw = Mathf.Clamp(fYaw, -90, 90);
        fPitch = Mathf.Clamp(fPitch, -90, 0);

        // With the updated values, change the rotation values of the cannon and the base pivot
        m_Cannon.ChangeCannonAim(fYaw, fPitch);
        basePoint.RotateBase(fYaw);
    }

    // Update is called once per frame
    void Update()
    {
        // on update, handle the user input
        HandleUserInput();
        Cursor.visible = false;

    }
}

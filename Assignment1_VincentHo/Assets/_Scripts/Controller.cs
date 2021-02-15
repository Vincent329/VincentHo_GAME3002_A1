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
    private float fHorizontalRotation = 0; // the angle for the horizontal rotation of the cannon  
    [SerializeField]
    private float fVerticalAim = 0; // the angle for the vertical aim of the cannon

    [SerializeField]
    public float fRotationSpeed = 0.5f; // how fast we want the cannon to rotate



    // Start is called before the first frame update
    void Start()
    {
       m_Cannon = GetComponent<Cannon>();
       Assert.IsNotNull(m_Cannon, "No Cannon Component");
    }

    private void HandleUserInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            fHorizontalRotation -= fRotationSpeed;
            fHorizontalRotation = Mathf.Clamp(fHorizontalRotation, -90, 90);
            Output();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            fHorizontalRotation += fRotationSpeed;
            fHorizontalRotation = Mathf.Clamp(fHorizontalRotation, -90, 90);

            Output();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            fVerticalAim -= fRotationSpeed;
            fVerticalAim = Mathf.Clamp(fVerticalAim, -90, 0);

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            fVerticalAim += fRotationSpeed;
            fVerticalAim = Mathf.Clamp(fVerticalAim, -90, 0);

        }
        //transform.rotation = Quaternion.Euler(transform.rotation.x + fVerticalAim, transform.rotation.y + fHorizontalRotation, 0.0f);
        m_Cannon.ChangeCannonAim(fVerticalAim, fHorizontalRotation);
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

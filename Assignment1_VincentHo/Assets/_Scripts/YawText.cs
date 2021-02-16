using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YawText : MonoBehaviour
{
    public Cannon cannon;
    private TextMeshProUGUI yawText;
    // Start is called before the first frame update
    void Start()
    {
        cannon = FindObjectOfType<Cannon>();
        yawText = GetComponent<TextMeshProUGUI>();

    }

     void Update()
    {
        UpdateYaw();
    }
    // Update is called once per frame
    void UpdateYaw()
    {
        yawText.text = "Yaw: " + cannon.GetYaw();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PitchText : MonoBehaviour
{
    public Cannon cannon;
    private TextMeshProUGUI pitchText;
    // Start is called before the first frame update
    void Start()
    {
        cannon = FindObjectOfType<Cannon>();
        pitchText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdatePitch();
    }
    // Update is called once per frame
    void UpdatePitch()
    {
        pitchText.text = "Pitch: " + cannon.GetPitch();
    }
}

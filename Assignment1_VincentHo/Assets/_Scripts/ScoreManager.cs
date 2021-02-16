using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int currentScore;
    [SerializeField]
    private int winScore;

    [SerializeField]
    private TextMeshProUGUI winText = null;

    [SerializeField]
    private bool m_bWin = false;

    void Start()
    {
        // set current parameters for the text
        currentScore = 0;
        winScore = 9;
        winText = GetComponent<TextMeshProUGUI>();
        Assert.IsNotNull(winText, "Win Text Not Found");
        winText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        currentScore++;
        if (currentScore >= winScore)
        {
            m_bWin = true;
            winText.enabled = m_bWin;
        }
    }
}

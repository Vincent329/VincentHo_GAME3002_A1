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
        currentScore = 0;
        winScore = 9;
        winText = GetComponent<TextMeshProUGUI>();
        Assert.IsNotNull(winText, "Win Text Not Found");
        winText.enabled = false;
        Debug.Log(winText.IsActive());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        currentScore++;
        Debug.Log(currentScore);

        if (currentScore >= winScore)
        {
            m_bWin = true;
            winText.enabled = m_bWin;
        }
    }
}

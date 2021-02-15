using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField]
    private float power = 2.5f;

    public Vector3 offset;
    [SerializeField]
    private float lifeThreshold;
    [SerializeField]
    private float lifeSpan;

    private Rigidbody m_rb;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rb, "No Rigid Body Applied");
        lifeSpan = 0.0f;
        lifeThreshold = 5.0f;
    }

    private void Start()
    {
        //m_rb.velocity = transform.forward * power;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan += 1/60.0f;
        if (lifeSpan >= lifeThreshold)
        {
            Despawn();
        }
    }

    void Despawn()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BallBehaviour : MonoBehaviour
{
    // TODO: just subject this ball to the physics and information of power from the cannon, and also have some collision detection on there.
    // functionality: on detect target, add score

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
        lifeThreshold = 10.0f;
    }

    private void Start()
    {
        //m_rb.velocity = transform.forward * power;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan += Time.deltaTime;
        if (lifeSpan >= lifeThreshold)
        {
            Despawn();
        }
    }

    void Despawn()
    {
        gameObject.SetActive(false);
    }

    //private void OnCollisionEnter(Collision collision, GameObject other)
    //{
    //}
    
    
}

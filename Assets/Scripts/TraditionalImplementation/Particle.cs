using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 forceOnThisParticle = ForcesCalculator.ElectricField[this.gameObject.GetInstanceID()];
        rigidbody.AddForce(forceOnThisParticle);
    }
}

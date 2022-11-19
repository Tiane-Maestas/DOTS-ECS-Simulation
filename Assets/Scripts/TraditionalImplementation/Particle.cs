using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _id;
    void Start()
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this._id = this._rigidbody.gameObject.GetInstanceID();

    }

    private void FixedUpdate()
    {
        Vector3 forceOnThisParticle = ForcesCalculator.vectorField[this._id];
        // this._rigidbody.AddForce(forceOnThisParticle);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ForcesCalculator : MonoBehaviour
{
    public static Dictionary<int, Vector3> vectorField = new Dictionary<int, Vector3>();
    public static float maxDistanceToCalculate = 2f;

    [SerializeField] private float lowestAllowedForce = -5f;

    private List<GameObject> _allParticles;

    private void Start()
    {
        this._allParticles = GameObject.Find("SimulationManager").GetComponent<ParticleSpawner>().particles;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < this._allParticles.Count; i++)
        {
            
            int currentParticleId = this._allParticles[i].GetInstanceID();
            Vector3 forceOnCurrentParticle = new Vector3();
            for (int j = 0; j < this._allParticles.Count; j++)
            {
                if (this._allParticles[j].GetInstanceID() == currentParticleId)
                    continue;

                forceOnCurrentParticle += ForceBetweenTwoParticles(this._allParticles[i], this._allParticles[j]);
            }

            ForcesCalculator.vectorField[currentParticleId] = forceOnCurrentParticle;
            
        }
    }

    // This calculates the force on the first gameobject by the second.
    private Vector3 ForceBetweenTwoParticles(GameObject first, GameObject second)
    {
        Vector3 direction = second.transform.position - first.transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance >= ForcesCalculator.maxDistanceToCalculate)
            return Vector3.zero;

        // Calculated from the Lennard-Jones potential.
        float sigma = 0.5f; //diameter of particle
        float eta = 5f; //U_0
        float magnitude = eta * (6 * Mathf.Pow(sigma, 6) / Mathf.Pow(distance, 7) - (12 * Mathf.Pow(sigma, 12) / Mathf.Pow(distance, 13)));

        // Limiter so the negative values don't get too large.
        if (magnitude < lowestAllowedForce)
            magnitude = lowestAllowedForce;

        return new Vector3(direction.x * magnitude, direction.y * magnitude, direction.z * magnitude);
    }
}

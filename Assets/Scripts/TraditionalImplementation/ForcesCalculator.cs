using System.Collections.Generic;
using UnityEngine;

public class ForcesCalculator : MonoBehaviour
{
    public static Dictionary<int, Vector3> vectorField = new Dictionary<int, Vector3>();
    public static float maxDistanceToCalculate = 2f;
    public static float minDistanceToCalculate = 0.54f;
    [SerializeField] private float totalEnergy = 0;
    [SerializeField] private float kineticEnergy = 0;
    [SerializeField] private float potential = 0;

    // Calculated from the Lennard-Jones potential.
    public static float sigma = 0.5f; //diameter of particle
    public static float eta = 5f; //U_0

    private List<GameObject> _allParticles;

    private void Start()
    {
        this._allParticles = GameObject.Find("SimulationManager").GetComponent<ParticleSpawner>().particles;
    }

    private void FixedUpdate()
    {
        potential = 0;
        kineticEnergy = 0;
        totalEnergy = 0;
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

            kineticEnergy += 0.5f * this._allParticles[i].GetComponent<Rigidbody>().mass * Mathf.Pow(this._allParticles[i].GetComponent<Rigidbody>().velocity.magnitude, 2);
        }
        totalEnergy = kineticEnergy + potential;
    }

    // This calculates the force on the first gameobject by the second.
    private Vector3 ForceBetweenTwoParticles(GameObject first, GameObject second)
    {
        Vector3 direction = second.transform.position - first.transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        // Limiter so the negative values don't get too large.
        if (distance < minDistanceToCalculate)
            distance = minDistanceToCalculate;

        potential += eta * (Mathf.Pow((sigma / distance), 12) - Mathf.Pow((sigma / distance), 6));

        float magnitude = eta * (6 * Mathf.Pow(sigma, 6) / Mathf.Pow(distance, 7) - (12 * Mathf.Pow(sigma, 12) / Mathf.Pow(distance, 13)));

        return new Vector3(direction.x * magnitude, direction.y * magnitude, direction.z * magnitude);
    }
}

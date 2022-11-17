using System.Collections.Generic;
using UnityEngine;

public class ForcesCalculator : MonoBehaviour
{
    public static Dictionary<int, Vector3> vectorField = new Dictionary<int, Vector3>();

    [SerializeField] private float lowestAllowedForce = -5f;
    [SerializeField] private float maxDistance = 2f;

    private void FixedUpdate()
    {
        GameObject[] allParticles = GameObject.FindGameObjectsWithTag("Particle");

        for (int i = 0; i < allParticles.Length; i++)
        {
            int currentParticleId = allParticles[i].GetInstanceID();
            Vector3 forceOnCurrentParticle = new Vector3();
            foreach (GameObject particle in allParticles)
            {
                if (particle.GetInstanceID() == currentParticleId)
                    continue;

                forceOnCurrentParticle += ForceBetweenTwoParticles(allParticles[i], particle);
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

        if (distance > maxDistance)
            return Vector3.zero;

        // Calculated from the Lennard-Jones potential.
        float sigma = 0.5f;
        float eta = 5f;
        float magnitude = eta * (6 * Mathf.Pow(sigma, 6) / Mathf.Pow(distance, 7) - (12 * Mathf.Pow(sigma, 12) / Mathf.Pow(distance, 13)));

        // Limiter so the negative values don't get too large.
        if (magnitude < lowestAllowedForce)
            magnitude = lowestAllowedForce;

        return new Vector3(direction.x * magnitude, direction.y * magnitude, 0);
    }
}

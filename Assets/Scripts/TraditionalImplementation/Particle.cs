using UnityEngine;

public class Particle : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Start()
    {
        this._rigidbody = this.transform.parent.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Environment"))
            return;

        ApplyForceFromOtherParticles(other.gameObject);
    }

    // The force on the this by other.
    private void ApplyForceFromOtherParticles(GameObject other)
    {
        Vector3 direction = other.transform.position - this.transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        // Limiter so the negative values don't get too large.
        if (distance < ParticleInfo.minDistanceToCalculate)
            distance = ParticleInfo.minDistanceToCalculate;

        ParticleInfo.potentialEnergy += ParticleInfo.eta * (Mathf.Pow((ParticleInfo.sigma / distance), 12) - Mathf.Pow((ParticleInfo.sigma / distance), 6)) / 2f;

        float magnitude = ParticleInfo.eta * (6 * Mathf.Pow(ParticleInfo.sigma, 6) / Mathf.Pow(distance, 7) - (12 * Mathf.Pow(ParticleInfo.sigma, 12) / Mathf.Pow(distance, 13)));

        this._rigidbody.AddForce(new Vector3(direction.x * magnitude, direction.y * magnitude, direction.z * magnitude));
    }

    private void FixedUpdate()
    {
        ParticleInfo.kineticEnergy += 0.5f * this._rigidbody.mass * Mathf.Pow(this._rigidbody.velocity.magnitude, 2);
    }
}

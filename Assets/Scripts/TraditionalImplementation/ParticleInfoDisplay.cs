using UnityEngine;

public class ParticleInfoDisplay : MonoBehaviour
{
    public float kinetic = 0;
    public float potential = 0;
    public float totalEnergy = 0;
    private void Start()
    {
        ResetParticleInfo();
    }

    private void Update()
    {
        kinetic = ParticleInfo.kineticEnergy;
        potential = ParticleInfo.potentialEnergy;
        ParticleInfo.totalEnergy = ParticleInfo.kineticEnergy + ParticleInfo.potentialEnergy;
        totalEnergy = ParticleInfo.totalEnergy;
    }

    private void FixedUpdate()
    {
        ResetParticleInfo();
    }

    private void ResetParticleInfo()
    {
        ParticleInfo.kineticEnergy = 0;
        ParticleInfo.potentialEnergy = 0;
        ParticleInfo.totalEnergy = 0;
    }
}

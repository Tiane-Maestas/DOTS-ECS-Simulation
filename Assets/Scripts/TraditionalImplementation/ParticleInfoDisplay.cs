using System.Threading;
using UnityEngine;

public class ParticleInfoDisplay : MonoBehaviour
{
    public float kinetic = 0;
    public float potential = 0;
    public float totalEnergy = 0;
    public bool computeAverage = false;
    private int count = 0;
    private float aggKinetic = 0;
    private float aggPotential = 0;
    private float aggTotalEnergy = 0;
    public int averageCount = 200;

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
        if (computeAverage)
        {
            aggKinetic += kinetic;
            aggPotential += potential;
            aggTotalEnergy += totalEnergy;
            count++;
        }
        if(count > averageCount)
        {
            computeAverage = false;
            WriteToCsv(aggKinetic/count, aggPotential/count, aggTotalEnergy/count);
            aggKinetic = 0;
            aggPotential = 0;
            aggTotalEnergy = 0;
            count = 0;
        }
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

    private void WriteToCsv(float kin, float pot, float tot)
    {
        print("Averages" + kin + "pot" + pot + "tot" + tot);
    }


}

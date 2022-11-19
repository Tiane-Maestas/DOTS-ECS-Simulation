using UnityEngine;
using System.Collections.Generic;

public class ParticleSpawner : MonoBehaviour
{
    public static float U_Total = 400; // Total inital energy randomly given to each particle in terms of kinetic energy.
    public static List<GameObject> particles = new List<GameObject>();

    [SerializeField] private GameObject _container;

    [SerializeField] private GameObject _particlePrefab;

    [SerializeField] private int _N = 10; // The total number of particles in the simulation.

    [SerializeField] private bool _mouseSpawnAllowed = false;

    #region Variables for  the container bounds.
    private Vector2 _insideEdgesXRange;
    private Vector2 _insideEdgesYRange;
    private Vector2 _insideEdgesZRange;
    #endregion

    private void Start()
    {
        // Ignore collisions between particles eventually!!

        // Forcing container position to be at the origin so all following calculations are correct.
        _container.transform.position = Vector3.zero;

        CalculateContainerBounds();
        SpawnParticles();
    }

    // Note: This assumes the thickness of each wall is 1 i.e. the scale is 1 for the position that's changed.
    private void CalculateContainerBounds()
    {
        float wallThicknessCorrection = 0.5f;

        GameObject rightWall = _container.transform.Find("WallRight").gameObject;
        GameObject leftWall = _container.transform.Find("WallLeft").gameObject;
        _insideEdgesXRange = new Vector2(leftWall.transform.position.x + wallThicknessCorrection,
                                         rightWall.transform.position.x - wallThicknessCorrection);

        GameObject upWall = _container.transform.Find("WallUp").gameObject;
        GameObject downWall = _container.transform.Find("WallDown").gameObject;
        _insideEdgesYRange = new Vector2(downWall.transform.position.y + wallThicknessCorrection,
                                         upWall.transform.position.y - wallThicknessCorrection);

        GameObject backWall = _container.transform.Find("WallBack").gameObject;
        GameObject frontWall = _container.transform.Find("WallFront").gameObject;
        _insideEdgesZRange = new Vector2(backWall.transform.position.z + wallThicknessCorrection,
                                         frontWall.transform.position.z - wallThicknessCorrection);
    }

    private void SpawnParticles()
    {
        // This radius is assuming the scale is the same in x, y, z of the transform.
        float particleVisualRadius = _particlePrefab.transform.localScale.x;
        Vector3 initialSpawnPosition = new Vector3(_insideEdgesXRange.x + 2 * particleVisualRadius,
                                                   _insideEdgesYRange.x + 2 * particleVisualRadius,
                                                   _insideEdgesZRange.x + 2 * particleVisualRadius);

        Vector3 currentSpawnPosition = initialSpawnPosition;
        for (int i = 0; i < _N; i++)
        {
            GameObject newParticle = Instantiate(_particlePrefab, currentSpawnPosition, this.transform.rotation);
            ParticleSpawner.particles.Add(newParticle);
            currentSpawnPosition = IncrementCurrentSpawnPosition(currentSpawnPosition, particleVisualRadius);
            if (currentSpawnPosition == Vector3.zero) // N is too large.
                break;
        }
    }

    private Vector3 IncrementCurrentSpawnPosition(Vector3 position, float particleVisualRadius)
    {
        float positionOffset = ForcesCalculator.maxDistanceToCalculate;

        //First try x
        if ((position.x + positionOffset) < _insideEdgesXRange.y)
        {
            return new Vector3(position.x + positionOffset, position.y, position.z);
        }
        position.x = _insideEdgesXRange.x + 2 * particleVisualRadius; // We exceed x bounds reset x.

        // Then try y
        if ((position.y + positionOffset) < _insideEdgesYRange.y)
        {
            return new Vector3(position.x, position.y + positionOffset, position.z);

        }
        position.y = _insideEdgesYRange.x + 2 * particleVisualRadius; // We exceed y bounds reset y.

        // Lastly try z
        if ((position.z + positionOffset) < _insideEdgesZRange.y)
        {
            return new Vector3(position.x, position.y, position.z + positionOffset);

        }
        return Vector3.zero; // N is too large for container.
    }

    void Update()
    {
        // Spawn a single particle at the mouse location on a click.
        if (Input.GetMouseButton(0) && _mouseSpawnAllowed)
        {
            Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnLocation.z = 0;
            GameObject newParticle = Instantiate(_particlePrefab, spawnLocation, this.transform.rotation);
        }
    }
}

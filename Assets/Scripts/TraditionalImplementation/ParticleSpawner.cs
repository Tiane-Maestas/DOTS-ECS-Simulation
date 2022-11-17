using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab;

    void Update()
    {
        // Spawn a single particle at the mouse location on a click.
        if (Input.GetMouseButton(0))
        {
            Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnLocation.z = 0;
            GameObject newStone = Instantiate(particlePrefab, spawnLocation, this.transform.rotation);
        }
    }
}

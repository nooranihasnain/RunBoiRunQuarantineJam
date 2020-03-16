using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] TrafficVehicles;
    public float SpawnDelay = 0.5f;
    private BoxCollider BCollider;
    // Start is called before the first frame update
    void Start()
    {
        BCollider = GetComponent<BoxCollider>();
        InvokeRepeating("SpawnCar", 0f, SpawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCar()
    {
        int RandomNum = Random.Range(0, TrafficVehicles.Length - 1);
        Vector3 SpawnLocation = new Vector3(
            Random.Range(BCollider.bounds.min.x, BCollider.bounds.max.x),
            transform.position.y,
            Random.Range(BCollider.bounds.min.z, BCollider.bounds.max.z)
        );
        GameObject SpawnedVehicle = Instantiate(TrafficVehicles[RandomNum], SpawnLocation, transform.rotation);
    }

}

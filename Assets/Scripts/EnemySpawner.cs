using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].SetActive(true);
            }
        }
    }
}

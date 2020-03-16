using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScript : MonoBehaviour
{
    private Rigidbody CarRb;
    public float CarSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        CarRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CarRb.velocity = transform.right * CarSpeed;
        Vector3 Movement = transform.right * CarSpeed * Time.deltaTime;
        CarRb.MovePosition(CarRb.position + Movement);
    }

    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.CompareTag("Player"))
        {
            PlayerController PC = Col.gameObject.GetComponent<PlayerController>();
            PC.KillPlayer();
        }
        else if(Col.gameObject.CompareTag("Police"))
        {
            PoliceScript PS = Col.gameObject.GetComponent<PoliceScript>();
            PS.KillEnemy();
        }
        else if (Col.gameObject.CompareTag("Civilian"))
        {
            CivilianScript CS = Col.gameObject.GetComponent<CivilianScript>();
            CS.KillCivilian();
        }
    }
}

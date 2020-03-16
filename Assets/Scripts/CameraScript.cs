using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject Player;
    private Vector3 Offset;

    private Transform Obstruction;
    private float ZoomSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        Offset = transform.position - Player.transform.position;
        Obstruction = Player.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Player)
        {
            transform.position = Player.transform.position + Offset;
        }
    }

    void Update()
    {
        if(Player)
        {
            ViewObstructed();
        }
    }

    void ViewObstructed()
    {
        RaycastHit RHit;
        if(Physics.Raycast(transform.position, Player.transform.position - transform.position, out RHit, 500f))
        {
            if(RHit.collider.tag != "Player")
            {
                if (RHit.collider.gameObject.tag == "Building")
                {
                    Obstruction = RHit.transform;
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
                else
                {
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
            }
        }
    }
}

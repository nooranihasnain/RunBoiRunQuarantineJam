using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceScript : MonoBehaviour
{
    private NavMeshAgent NAgent;
    private Animator EnemyAnimator;
    private GameObject Player;
    public GameObject RagdollPrefab;
    private bool IsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        NAgent = GetComponent<NavMeshAgent>();
        EnemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsDead)
        {
            Player = GameObject.FindWithTag("Player");
            EnemyAnimator.SetFloat("Speed", NAgent.velocity.magnitude);
            if (Player != null || !Player.GetComponent<PlayerController>().IsDead)
            {
                NAgent.SetDestination(Player.transform.position);
            }
        }
    }

    public void KillEnemy()
    {
        if(!IsDead)
        {
            IsDead = true;
            Instantiate(RagdollPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianScript : MonoBehaviour
{
    private Animator CivAnimator;
    private NavMeshAgent NAgent;

    public float WaitDelay = 3f;
    private float CurrTime = 0f;

    private bool IsDead = false;
    public GameObject RagdollPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CivAnimator = GetComponent<Animator>();
        NAgent = GetComponent<NavMeshAgent>();
        CurrTime = WaitDelay;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CivAnimator.SetFloat("Speed", NAgent.velocity.magnitude);
    }

    void Movement()
    {
        CurrTime += Time.deltaTime;
        if(CurrTime >= WaitDelay)
        {
            CurrTime = 0f;
            float WalkRadius = Random.Range(5f, 7f);
            Vector3 RandomDirection = Random.insideUnitSphere * WalkRadius;
            RandomDirection += transform.position;
            NavMeshHit CurrHit;
            NavMesh.SamplePosition(RandomDirection, out CurrHit, WalkRadius, 1);
            Vector3 FinalPos = CurrHit.position;
            NAgent.SetDestination(FinalPos);
        }
    }

    public void KillCivilian()
    {
        if(!IsDead)
        {
            IsDead = true;
            Instantiate(RagdollPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

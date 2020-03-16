using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 10f;
    private Animator PlayerAnimator;
    private CharacterController PlayerCharController;
    public bool IsDead = false;
    public GameObject PlayerRagdoll;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerCharController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsDead && PlayerAnimator.GetBool("GameStart"))
        {
            Movement();
        }
    }

    void Movement()
    {
        float HozAxis = Input.GetAxis("Horizontal");
        float VerAxis = Input.GetAxis("Vertical");
        Vector3 VerMovement = new Vector3(-1f, 0f, 1f) * VerAxis;
        Vector3 HozMovement = new Vector3(1f, 0f, 1f) * HozAxis;
        Vector3 Direction = (VerMovement + HozMovement);
        Direction = Vector3.ClampMagnitude(Direction, 1f);
        PlayerCharController.SimpleMove(Direction * MovementSpeed);
        Vector3 RotateDir = Vector3.RotateTowards(transform.forward, Direction, MovementSpeed * Time.deltaTime * 5f, 0.0f);
        transform.rotation = Quaternion.LookRotation(RotateDir);
        PlayerAnimator.SetFloat("Speed", PlayerCharController.velocity.magnitude);
    }

    public void KillPlayer()
    {
        if(!IsDead)
        {
            IsDead = true;
            Instantiate(PlayerRagdoll, transform.position, transform.rotation);
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowLoseScreen();
            Destroy(this.gameObject);
        }
    }

    public void GetCaught()
    {
        if (!IsDead)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowLoseScreen();
            PlayerAnimator.Play("Throw");
            GetComponent<CharacterController>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.CompareTag("Police") && !IsDead)
        {
            transform.position = Col.transform.position + (Col.transform.forward * 1f);
            GetCaught();
            Col.gameObject.GetComponent<Animator>().Play("Slam");
            IsDead = true;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public float health = 50;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip BulletHit;
    public Rigidbody rb;

    [SerializeField] private GameObject Mag;
    [SerializeField] private Transform MagSpawn;

    //States
    public float sightRange, attackRange;
    public bool playerInAttackRange;
    public PlayerManager playerManager;

    public List<Collider> RagdollParts = new List<Collider>();

    bool alive;

    [SerializeField]private float yMin ,yMax;

    private void Awake()
    {
        SetRagdollParts();
        player = GameObject.Find("Vr Rig").transform;
        agent = GetComponent<NavMeshAgent>();
        playerManager = GameObject.Find("Vr Rig").GetComponent<PlayerManager>();
        alive = true;
    }

    private void SetRagdollParts()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                RagdollParts.Add(c);
            }

        }
    }

    private void TurnOnRagdoll()
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        anim.enabled = false;
        anim.avatar = null;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    void start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {

        transform.position = new Vector3(transform.position.x,
        Mathf.Clamp(transform.position.y,yMin,yMax),transform.position.z);

        //If the zombie is alive ...
        if (alive)
        {
            ChasePlayer();

            //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            float distance = Vector3.Distance(player.transform.position, this.transform.position);

            if (distance <= 1)
            {
                Debug.Log("ATTACKING PLAYER");
                anim.SetBool("attack", true);
                AttackPlayer();//Within range attack the player
            }
            else
            {
                anim.SetBool("attack", false);
            }
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        float dist = Vector3.Distance(gameObject.transform.position, player.position);

        if (dist < 1f)
        {
            //transform.LookAt(player);

            if (!alreadyAttacked)
            {
                playerManager.TakeDamage(10);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            TakeDamage(35);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(" ZOMBIE HEALTH =" + health);
        source.PlayOneShot(BulletHit);

        if (health <= 0)
        {
            playerManager.killCounter();
            rb.AddForce(Vector3.zero);
            rb.AddForce(100f * Vector3.up);
            alive = false;
            TurnOnRagdoll();
            Invoke(nameof(DestroyEnemy), 5f);
        }
    }
    private void DestroyEnemy()
    {
        Instantiate(Mag, MagSpawn.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
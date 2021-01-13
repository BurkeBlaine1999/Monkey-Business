
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    [SerializeField] private Animator anim;

    public Rigidbody rb;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public PlayerManager playerManager;

    public List<Collider> RagdollParts = new List<Collider>();

    bool alive;

    public AudioSource audioSource;
    public AudioClip rightStep;
    public AudioClip leftStep;

    private void Awake()
    {
        SetRagdollParts();
        player = GameObject.Find("Vr Rig").transform;
        agent = GetComponent<NavMeshAgent>();
        alive = true;
    }

    private void SetRagdollParts(){
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach(Collider c in colliders){

            if(c.gameObject != this.gameObject){
                c.isTrigger = true;
                RagdollParts.Add(c);
            }
            
        }
    }

    private void TurnOnRagdoll(){
        
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        anim.enabled = false;
        anim.avatar = null;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        
        foreach(Collider c in RagdollParts){
    
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
            
        }
    }

    void start(){
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(RunningSFX());
    }

    IEnumerator RunningSFX()
    {
        Debug.Log("Sound Playing");
        yield return new WaitForSeconds(0.4f); 
        audioSource.PlayOneShot(rightStep);             
        yield return new WaitForSeconds(0.4f);             
        audioSource.PlayOneShot(leftStep);  
        
    }
    private void Update()
    {   
 

        //If the zombie is alive ...
        if(alive){
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange){
                anim.SetBool("playerFound", false);
                anim.SetBool("attack", false); 
                
                float distance = Vector3.Distance(player.transform.position, this.transform.position);

                if(distance > 200){
                    Debug.Log("PLAYER OUT OF RANGE : Deleting...");
                    Destroy(gameObject);
                }

                Patroling();//Patrols and searches for player
            }else if (playerInSightRange && !playerInAttackRange){
                anim.SetBool("attack", false); 
                anim.SetBool("playerFound", true);
                ChasePlayer();//Chases the player
            }else if (playerInAttackRange && playerInSightRange){        
                anim.SetBool("attack", true);    
                AttackPlayer();//Within range attack the player
            }else{
                anim.SetBool("attack", false); 
                anim.SetBool("playerFound", false);
            }
        }

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            playerManager.TakeDamage(100);          
            ///Attack code here
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet"){
            TakeDamage(35);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0){
            rb.AddForce(100f * Vector3.up);
            alive = false;
            TurnOnRagdoll();
            Invoke(nameof(DestroyEnemy), 5f);
        }
    }
    private void DestroyEnemy()
    {
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

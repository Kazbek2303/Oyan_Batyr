
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float detectionRadius = 10f;  
    public float flyDistance = 2f;       
    public float stopDistance = 1.5f;    

    public Transform player;
    private Animator animator;
    private NavMeshAgent agent;

    public AudioSource flySound;
    public AudioSource walkSound;

    private BoxCollider boxCollider;
    private Vector3 originalColliderCenter;
    public float goUpDistance = 10f;

    public GameObject attackPrefab; // The attack prefab with a box collider
    public Transform attackSpawnPoint; // The point where the attack prefab will be instantiated
    public float attackCooldown = 5f; // Cooldown between attacks
    private bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            originalColliderCenter = boxCollider.center;
        }
        //player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log(distanceToPlayer);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > stopDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isWalking", true);
                //walkSound.Play();
                animator.SetBool("isFlying", false);
                ResetColliderPosition();
            }
            else
            {
                agent.SetDestination(player.position);
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isFlying", true);
                FlyUp();
                //flySound.Play();
                MoveColliderUp();

                if (canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isFlying", false);
            ResetColliderPosition();
        }
    }

    private void FlyUp()
    {
        Vector3 flyPosition = new Vector3(transform.position.x, transform.position.y + flyDistance, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, flyPosition, Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    private void MoveColliderUp()
    {
        if (boxCollider != null)
        {
            Vector3 newCenter = originalColliderCenter + new Vector3(0, goUpDistance, 0);
            boxCollider.center = newCenter;
        }
    }

    private void ResetColliderPosition()
    {
        if (boxCollider != null)
        {
            boxCollider.center = originalColliderCenter;
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        Instantiate(attackPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}

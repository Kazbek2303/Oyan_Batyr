
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

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > stopDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isWalking", true);
                //walkSound.Play();
                animator.SetBool("isFlying", false);
            }
            else
            {
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isFlying", true);
                FlyUp();
                //flySound.Play();
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isFlying", false);
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

}

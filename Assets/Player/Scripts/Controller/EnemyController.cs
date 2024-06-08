using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

//[RequireComponent(typeof(CharacterCombat))]
public class EnemyController : MonoBehaviour
{

	public float lookRadius = 10f;
	public float lookSpeed = 20f; 

	public GameObject Player;
	Transform target;
	NavMeshAgent agent;
	Animator anim;
	//CharacterCombat combatManager;

	void Start()
	{
		target = Player.transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		//combatManager = GetComponent<CharacterCombat>();
	}

	void Update()
	{
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the radius
		if (distance <= lookRadius)
		{
			// Move towards the player
			agent.SetDestination(target.position);
			anim.SetBool("IsWalking", true);
			if (distance <= agent.stoppingDistance)
			{
				// Attack
				anim.SetTrigger("Attack");
				anim.SetBool("IsWalking", false);
				//combatManager.Attack(Player.instance.playerStats);
				FaceTarget();
			}
		} else
        {
			anim.SetBool("IsWalking", false);
		}
	}

	// Point towards the player
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
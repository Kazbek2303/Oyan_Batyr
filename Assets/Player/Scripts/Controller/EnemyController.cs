using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

//[RequireComponent(typeof(CharacterCombat))]
public class EnemyController : MonoBehaviour
{

	public float lookRadius = 10f;
	public float lookSpeed = 50f;
	bool isAttacking = false;
	public float waitForMoving = 2f;
	public bool isCelebrate = true;

	[SerializeField]
	private GameObject Player;
	Transform target;
	NavMeshAgent agent;
	Animator anim;
	//CharacterCombat combatManager;
	[SerializeField]
	private GameObject enemySword;
	EnemyHealth enemyHealth;
	[SerializeField] private PlayerHealth playerHealth;

	public AudioClip[] FootstepAudioClips;
	public AudioClip[] SlashAudioClips;

	[Range(0, 1)] public float FootstepAudioVolume = 0.5f;

	void Start()
	{
		target = Player.transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		enemyHealth = GetComponent<EnemyHealth>();
		enemySword.GetComponent<Collider>().enabled = false;       
		//combatManager = GetComponent<CharacterCombat>();
	}

	void Update()
	{
		//if (isAttacking) return;


		float distance = Vector3.Distance(target.position, transform.position);

		if (distance <= lookRadius)
		{
			// Move towards the player
			if (!isAttacking) agent.SetDestination(target.position);
			anim.SetBool("IsWalking", true);

			if (distance <= agent.stoppingDistance)
			{
				FaceTarget();
				StartCoroutine(Attack());
			}
		}
		else
		{
			// If the agent still has a path or is still moving, keep the walking animation
			if (agent.remainingDistance > agent.stoppingDistance && !agent.pathPending)
			{
				anim.SetBool("IsWalking", true);
			}
			else
			{
				anim.SetBool("IsWalking", false);
			}
		}

		/*// If inside the stopping distance, trigger attack
		if (distance <= agent.stoppingDistance)
		{
			anim.SetTrigger("Attack");
			anim.SetBool("IsWalking", false);
			//combatManager.Attack(Player.instance.playerStats);
			FaceTarget();
		}*/
	}
	IEnumerator Attack()
	{
		isAttacking = true;
		agent.isStopped = true;
		anim.SetBool("IsWalking", false);
		anim.SetTrigger("Attack");

		// Wait for the attack animation to finish (you can adjust this duration)
		yield return new WaitForSeconds(waitForMoving);
		if (playerHealth.health < 1 && isCelebrate)
		{
			isCelebrate = false;
			//isAttacking = false;
			//anim.SetBool("IsWalking", false);
			//anim.ResetTrigger("Attack");
			anim.SetTrigger("killed");
		}

		if (!enemyHealth.isDead)
        {
			anim.ResetTrigger("Attack");
			agent.isStopped = false;
			isAttacking = false;
		}
	}

	public void ResetSkeletonAttack()
    {
		anim.ResetTrigger("Attack");
    }
	public void AttackStart()
	{
		enemySword.GetComponent<Collider>().enabled = true;
	}
	public void AttackEnd()
	{
		enemySword.GetComponent<Collider>().enabled = false;
	}

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

	private void OnFootstep(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			if (FootstepAudioClips.Length > 0)
			{
				var index = Random.Range(0, FootstepAudioClips.Length);
				AudioSource.PlayClipAtPoint(FootstepAudioClips[index], agent.transform.position, FootstepAudioVolume);
			}
		}
	}

	private void SwordSlash(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			if (SlashAudioClips.Length > 0)
			{
				var index = Random.Range(0, SlashAudioClips.Length);
				AudioSource.PlayClipAtPoint(SlashAudioClips[index], transform.position, FootstepAudioVolume);
			}
		}
	}
}
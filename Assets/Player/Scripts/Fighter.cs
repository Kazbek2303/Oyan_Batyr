using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1;

    public bool isAttacking;

    public PauseMenu pauseMenu;
    [SerializeField]
    private GameObject swordPrefab;
    public AudioClip[] SlashAudioClips;
    [Range(0, 1)] public float SlashAudioVolume = 0.5f;

    private void Start()
    {
        swordPrefab.GetComponent<Collider>().enabled = false;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
       if(!PauseMenu.isPaused)
        {
            ExitAttack();
            //Attack();

            if (Time.time - lastClickedTime > maxComboDelay)
            {
                noOfClicks = 0;
            }

            //cooldown time
            if (Time.time > nextFireTime)
            {
                // Check for mouse input
                if (Input.GetMouseButtonDown(0) && anim.GetBool("Grounded"))
                {
                    OnClick();
                }
            }
        }
        
    }
    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            isAttacking = false;
            anim.SetBool("hit1", false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            isAttacking = false;
            noOfClicks = 0;
        }
        /*if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }*/
    }

    void OnClick()
    {
        noOfClicks++;
        lastClickedTime = Time.time;
        Attack();
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        Debug.Log(noOfClicks);
        
        /*if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }*/
    }

    public void Attack()
    {
        if (noOfClicks > 1)
        {
            Debug.Log("attack 2");
            isAttacking = true;
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
        }

        if (noOfClicks == 1)
        {
            Debug.Log("attack 1");
            isAttacking = true;
            anim.SetBool("hit1", true);
            anim.SetBool("hit2", false);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 1); // 3
    }

    public void AttackStart()
    {
        swordPrefab.GetComponent<Collider>().enabled = true;
    }
    public void AttackEnd()
    {
        swordPrefab.GetComponent<Collider>().enabled = false;
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }

    private void SwordSlash(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (SlashAudioClips.Length > 0)
            {
                var index = Random.Range(0, SlashAudioClips.Length);
                AudioSource.PlayClipAtPoint(SlashAudioClips[index], transform.position, SlashAudioVolume);
            }
        }
    }
}

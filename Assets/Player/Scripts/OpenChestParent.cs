using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestParent : MonoBehaviour
{
    public Animator chestAnim;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Child trigger enter: " + other.name);
            chestAnim.SetBool("isOpened", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Child trigger exit: " + other.name);
            chestAnim.SetBool("isOpened", false);
        }
    }
}

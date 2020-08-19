using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManMouthTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator EatAnim;

    [SerializeField]
    public GameObject FeedCanV;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SpoonController>().IfHaveMat())
        {
            EatAnim.SetTrigger("EatState");
            other.gameObject.GetComponent<SpoonController>().HaveMat(false);
            FeedCanV.SetActive(true);
        }
    }
}

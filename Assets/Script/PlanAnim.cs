using System.Collections;
using UnityEngine;

public class PlanAnim : MonoBehaviour
{
    public Animator PlanAni;
    public bool checker;
    public float AnimTime,AnimTime2;

    public GameObject Plan1, Plan2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && checker)
        {
            StartCoroutine(PlanAnimation());
        }

        if (other.transform.CompareTag("Player") && !checker)
        {
            StartCoroutine(Plan2Animation());
        }
    }
    IEnumerator Plan2Animation()
    {
        PlanAni.SetBool("Fly", true);
        yield return new WaitForSeconds(AnimTime2);
        Plan2.SetActive(false);
    }

    IEnumerator PlanAnimation()
    {
        PlanAni.SetBool("Fly", true);
        yield return new WaitForSeconds(AnimTime);
        Plan1.SetActive(false);
    }
}

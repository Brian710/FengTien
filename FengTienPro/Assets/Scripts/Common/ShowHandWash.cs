using UnityEngine;

public class ShowHandWash : MonoBehaviour
{
    public  Animator HandWashing;

    private void Start()
    {
        //HandWashing.SetInteger("AnyState", -2);
        HandWashing.gameObject.SetActive(false);
    }

    public void ShowAnimation(bool value)
    {
        if (GameController.Instance.mode == MainMode.Exam)
        {
            return;
        }

        if (value)
        {
            HandWashing.gameObject.SetActive(value);
            HandWashing.SetBool("AutoShow", value);
            HandWashing.SetInteger("AnyState", -2);
        }
        else
        {
            HandWashing.SetBool("AutoShow", value);
            HandWashing.SetInteger("AnyState", -1);
            HandWashing.gameObject.SetActive(value);
        }

    }

}

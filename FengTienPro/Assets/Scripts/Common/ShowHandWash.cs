using UnityEngine;

public class ShowHandWash : MonoBehaviour
{
    public  Animator HandWashing;

    private void Start()
    {
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
        }
        else
        {
            HandWashing.SetBool("AutoShow", value);
            HandWashing.gameObject.SetActive(value);
        }
        
    }

}

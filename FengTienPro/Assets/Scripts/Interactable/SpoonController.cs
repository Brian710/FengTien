using UnityEngine;

public class SpoonController : IObjControllerBase
{
    [SerializeField]
    private GameObject On;
    public bool IfHaveMat()
    {
        return On.activeSelf;
    }

    public void HaveMat(bool value)
    {
        On.SetActive(value);
    }

}

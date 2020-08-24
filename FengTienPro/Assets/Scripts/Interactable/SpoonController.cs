using UnityEngine;

public class SpoonController : InteracObjBase
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

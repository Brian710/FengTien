using UnityEngine;

public class SpoonController : InteractableObjBase
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

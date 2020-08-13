using UnityEngine;

[CreateAssetMenu(fileName = "New GrabObj", menuName = "InteractObjs/Grab")]
public class GrabObject : ScriptableObject 
{
    public Mesh mesh;
    public Material material;

    public bool isAlign;
    public Vector3 alignPos;
    public Vector3 alignRot;

    public HandAnim handAnim;

    public bool isGravity;

    public string takeSound;
    public string dropSound;
    public string interactSound;
}

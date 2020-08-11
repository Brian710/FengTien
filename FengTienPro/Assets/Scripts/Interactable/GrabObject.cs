using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GrabObj", menuName = "InteractObjs/Grab")]
public class GrabObject : ScriptableObject 
{
    public Mesh mesh;
    public Material material;

    public Vector3 alignPos;
    public Vector3 alignRot;

    public string soundName;
}

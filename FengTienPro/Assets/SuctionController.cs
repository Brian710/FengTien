using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionController : MonoBehaviour
{
    public Rigidbody SuctionObj;
    public int CoreSpeed;
    public bool Suction_Up, Suction_Down;
    private void Start()
    {
        Suction_Up = false;
        Suction_Down = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<VivePoseTracker>())
        {
            if (SuctionObj.transform.position.z < 0.07f) //抽
            {
                SuctionObj.AddForce(0, 0, Mathf.Clamp(SuctionObj.transform.position.z * CoreSpeed, 0.035f, 0.07f), ForceMode.Acceleration);
                if (SuctionObj.transform.position.z >= 0.07f)
                {
                    Suction_Up = true;
                    Debug.LogError("抽取胃液完成");
                }
            }                
            if(SuctionObj.transform.position.z >= 0.07f)//壓
            {
                SuctionObj.AddForce(0, 0, Mathf.Clamp(SuctionObj.transform.position.z / CoreSpeed, 0.035f, 0.07f), ForceMode.Acceleration);
                if (SuctionObj.transform.position.z <= 0.035f)
                { 
                    Suction_Down = true;
                    Debug.LogError("壓回胃液完成");
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<VivePoseTracker>())
        {
            SuctionObj.AddForce(0, 0, Mathf.Clamp(SuctionObj.transform.position.z / CoreSpeed, 0.035f, 0.07f), ForceMode.Acceleration);
        }
    }
}

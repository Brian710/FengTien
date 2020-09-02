using UnityEngine;
using System.Collections;
using MinYanGame.Core;

public class LookAt : MonoBehaviour
{
	[SerializeField]	private Transform maincam;

	// Use this for initialization
	void Start()
	{
		if (!maincam)
			maincam = PlayerController.Instance.Cam;
	}

	// Update is called once per frame
	void Update()
	{
		if(maincam != null)
			transform.LookAt(maincam.position);
	}
}

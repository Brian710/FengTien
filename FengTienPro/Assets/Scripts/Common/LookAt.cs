using UnityEngine;
using System.Collections;
using MinYanGame.Core;

public class LookAt : MonoBehaviour
{
	[SerializeField]
	private Transform maincam;

	// Use this for initialization
	void Start()
	{
		if (!maincam)
			maincam = GameController.Instance.currentPlayer.Cam;
	}

	// Update is called once per frame
	void Update()
	{
		//Vector3 mousePos = Input.mousePosition;
		//mousePos.z = 10f;
		//Vector3 target = maincam.ScreenToWorldPoint (mousePos);
		//target.x += 30f;
		//target.y -= 10f;
		//target.z = 60f;
		//transform.LookAt(target);
		if(maincam != null)
			transform.LookAt(maincam.position);
	}
}

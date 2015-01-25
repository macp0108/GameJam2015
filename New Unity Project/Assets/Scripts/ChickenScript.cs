using UnityEngine;
using System.Collections;

public class ChickenScript : MonoBehaviour {

	private float bounceTimer; 
	public float bouncePower = 160; 
	public float bouncetimereset; 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(bounceTimer > 0.0f)
		{
			bounceTimer -= Time.deltaTime; 
		}

		if(bounceTimer < 0.0f)
		{
			bounceTimer = 0.0f; 
		}

		if (bounceTimer == 0.0f)
		{
			Jump(); 
		}

		bouncetimereset = UnityEngine.Random.Range (0.3f, 3.0f); 
	
	}

	void Jump ()
	{
		rigidbody.AddForce(new Vector3(0, bouncePower, 0)); 
		bounceTimer = bouncetimereset; 
	}
}

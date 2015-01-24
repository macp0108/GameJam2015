using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Player : MonoBehaviour 
{
	public float Speed = 10.0f;
	public bool CanJump = true;
	public bool Grounded;
	float Gravity = 10.0f;
	public float JumpSpeed = 2;
	public float MaxVelocity = 10;
	public float Sensitivity = 2;
	// Use this for initialization
	void Start () 
	{
		rigidbody.freezeRotation = true;
		//rigidbody.useGravity = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Grounded)
		{
			Vector3 TargetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			TargetVelocity = transform.TransformDirection (TargetVelocity);
			TargetVelocity *= Speed;

			Vector3 CurrentVelocity = rigidbody.velocity;
			Vector3 ChangeVelocity = (TargetVelocity - CurrentVelocity);
			ChangeVelocity.x = Mathf.Clamp (ChangeVelocity.x, -MaxVelocity, MaxVelocity);
			ChangeVelocity.z = Mathf.Clamp (ChangeVelocity.z, -MaxVelocity, MaxVelocity);
			ChangeVelocity.y = 0;

			rigidbody.AddForce (ChangeVelocity, ForceMode.VelocityChange);

			if(CanJump && Input.GetButtonDown("Jump"))
			{
				rigidbody.velocity = new Vector3(CurrentVelocity.x,CalcJumpVelocity(),CurrentVelocity.z);
			}
		}

		Grounded = false;
	}

	void FixedUpdate()
	{
		Plane playerplane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		float hitdist = 0.0f;

		if(playerplane.Raycast (ray, out hitdist))
		{
			Vector3 tagetPoint = ray.GetPoint(hitdist);

			Quaternion targetRotation = Quaternion.LookRotation(tagetPoint - transform.position);

			transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, Sensitivity * Time.deltaTime);
		}
	}

	void OnCollisionStay(Collision other)
	{
		Grounded = true;
	}

	float CalcJumpVelocity()
	{
		return Mathf.Sqrt (JumpSpeed * 2 * -Physics.gravity.y);
	}
}

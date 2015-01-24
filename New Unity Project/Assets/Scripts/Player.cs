using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Player : MonoBehaviour 
{
	public float Speed = 5.0f;
	public bool CanJump = true;
	public bool Grounded;
	public bool Interact;
	public float JumpSpeed = 2;
	public float MaxVelocity = 10;
	public float Sensitivity = 2;
	public bool Sprint;
	float SprintBoost = 1;
	public Camera _Camera;
	public CameraMovement _Movement;
	// Use this for initialization
	void Start () 
	{
		rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 TargetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		TargetVelocity = transform.TransformDirection (TargetVelocity);
		TargetVelocity *= (Speed * SprintBoost);
		
		Vector3 CurrentVelocity = rigidbody.velocity;
		Vector3 ChangeVelocity = (TargetVelocity - CurrentVelocity);
		ChangeVelocity.x = Mathf.Clamp (ChangeVelocity.x, -MaxVelocity, MaxVelocity);
		ChangeVelocity.z = Mathf.Clamp (ChangeVelocity.z, -MaxVelocity, MaxVelocity);
		ChangeVelocity.y = 0;
		
		rigidbody.AddForce (ChangeVelocity, ForceMode.VelocityChange);

		if(Grounded)
		{
			if(CanJump && Input.GetButtonDown("Jump"))
			{
				rigidbody.velocity = new Vector3(CurrentVelocity.x,CalcJumpVelocity(),CurrentVelocity.z);
			}
		}

		if(Input.GetKeyDown(KeyCode.LeftShift) && !Sprint)
		{
			Sprint = true;
		}
		else if(Input.GetKeyDown(KeyCode.LeftShift) && Sprint)
		{
			Sprint = false;
		}

		SprintBoost = Sprint ? 1.5f : 1.0f;

		if(Sprint)
		{
			if(rigidbody.velocity.magnitude < 0.2f)
			{
				Sprint = false;
			}
		}

		Grounded = false;
	}

	void OnCollisionStay(Collision other)
	{
		Grounded = true;
	}

	float CalcJumpVelocity()
	{
		return Mathf.Sqrt (JumpSpeed * 2 * -Physics.gravity.y);
	}

	void OnTriggerStay(Collider other)
	{
		Debug.Log (other.GetComponent<AI> ().Source.isPlaying);
		if(other.tag == "AI")
		{

			if(Input.GetKeyDown(KeyCode.E))
			{
				if(!other.GetComponent<AI>().Source.isPlaying)
				{
					other.GetComponent<AI>().PlayRandomSpeech();
				}
			}
			
		}
	}
}

using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
{
	CharacterController _Control;
	public float _Speed;
	public bool _Interact;
	Vector3 _Move = Vector3.zero;
	public bool _CanJump;
	public float _Gravity;
	public float _Jump;

	// Use this for initialization
	void Start () 
	{
		_Control = GetComponent<CharacterController> ();
		_Gravity = Physics.gravity.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 currentMove = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
		if(_Control.isGrounded)
		{
			if(Input.GetButtonDown("Jump"))
			{
				Debug.Log("CanJump");
				_CanJump = true;
			}
		}
		else
		{
			_Gravity = Physics.gravity.y * Time.deltaTime;
		}

		if(_CanJump)
		{
			currentMove.y = _Jump;
			_CanJump = false;
		}

		currentMove.y += _Gravity;


		_Move = currentMove;
		_Control.Move (_Move * _Speed * Time.deltaTime);
			
	}
}


using UnityEngine;

public class MinerMovement : MonoBehaviour
{

	#region EditorFields

	/// <summary>
	/// Time between inputs.
	/// </summary>
	[SerializeField]
	private float _inputDelay;
	
	/// <summary>
	/// The distance the player moves with each step.
	/// </summary>
	[SerializeField]
	private float _stepSize = 1f;
	
	/// <summary>
	/// Rigidbody.
	/// </summary>
	[SerializeField]
	private Rigidbody2D _rigidbody;

	/// <summary>
	/// What stops movement.
	/// </summary>
	[SerializeField]
	private LayerMask _movementBlockingLayers;
	
	#endregion

	#region Fields

	/// <summary>
	/// Internal timer.
	/// </summary>
	private float _timer;

	/// <summary>
	/// Can the player currently move or do we need to wait for input delay?
	/// </summary>
	private bool _canMove = true;

	#endregion
	
	#region Methods

	/// <summary>
	/// Called every frame.
	/// </summary>
	private void Update()
	{
		Vector3 input = new Vector3();

		if (_canMove)
		{
			if (Input.GetKey(KeyCode.W))
			{
				input.y += _stepSize;
				_canMove = false;
			}
			else if (Input.GetKey(KeyCode.A))
			{
				input.x -= _stepSize;
				_canMove = false;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				input.y -= _stepSize;
				_canMove = false;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				input.x += _stepSize;
				_canMove = false;
			}
		}
		else
		{
			_timer += Time.deltaTime;
			if (_timer > _inputDelay || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
			{
				CanMoveAgain();
			}
		}

		float radius = 0.10f;
		Debug.DrawLine(new Vector3(transform.position.x + input.x, transform.position.y + input.y - radius, transform.position.z), new Vector3(transform.position.x + input.x, transform.position.y + input.y + radius, transform.position.z), Color.red);
		Debug.DrawLine(new Vector3(transform.position.x + input.x - radius, transform.position.y + input.y, transform.position.z), new Vector3(transform.position.x + input.x + radius, transform.position.y + input.y, transform.position.z),Color.red);
		//Move only when you are not overlapping a movementBlocker
		if (Physics2D.OverlapCircle(transform.position + input, radius, _movementBlockingLayers) == false)
		{
			transform.position += input;
		}
		////Move only when there is a collider and it is not a movementBlocker
		//if (Physics2D.OverlapCircle(transform.position + input, radius, ~_movementBlockingLayers))
		//{
		//	transform.position += input;
		//}
	}

	/// <summary>
	/// Resets all flags so the player can move again.
	/// </summary>
	private void CanMoveAgain()
	{
		_canMove = true;
		_timer = 0.0f;
	}
	
	#endregion

}

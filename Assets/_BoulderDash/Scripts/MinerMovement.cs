using UnityEngine;

namespace BoulderDash
{
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
			//Get input.
			Vector2 input = Vector2.zero;
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

			//Process inputs
			if (input != Vector2.zero)
			{
				Collider2D collision = Physics2D.OverlapCircle((Vector2)transform.position + input, GlobalVariables.SPHERECAST_RADIUS);
				if (collision)
				{
					BaseBlock baseBlock = collision.GetComponent<BaseBlock>();
					if (baseBlock != null)
					{
						if(baseBlock.AttemptOverlap((collision.transform.position - transform.position).normalized))
							transform.position += (Vector3)input;
					}
				}
				else
				{
					transform.position += (Vector3)input;
				}
			}

			/*//Move only when you are not overlapping a movementBlocker
		if (Physics2D.OverlapCircle(transform.position + input, radius, _movementBlockingLayers) == false)
		{
		}*/
		
		
		
		
		
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
}

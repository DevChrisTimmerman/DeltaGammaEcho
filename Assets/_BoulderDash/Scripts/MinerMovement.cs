using UnityEngine;

namespace BoulderDash
{
	public class MinerMovement : MonoBehaviour
	{
		#region Events

		#region Turned

		/// <summary>
		/// Turned delegate.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		public delegate void TurnedDelegate(MinerMovement sender);

		/// <summary>
		/// Turned event.
		/// </summary>
		public event TurnedDelegate TurnedEvent;

		/// <summary>
		/// Turned event invocator.
		/// </summary>
		private void OnTurned()
		{
			TurnedDelegate handler = TurnedEvent;
			if (handler != null) handler(this);
		}

		#endregion

		#endregion

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

		/// <summary>
		/// Is the miner facing left.
		/// </summary>
		private bool _isFacingLeft;

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
					if (_isFacingLeft == false)
					{
						Flip();
					}
				}
				else if (Input.GetKey(KeyCode.S))
				{
					input.y -= _stepSize;
					_canMove = false;
				}
				else if (Input.GetKey(KeyCode.D))
				{
					_canMove = false;
					input.x += _stepSize;
					
					if (_isFacingLeft)
					{
						Flip();
					}
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
		}
		
		/// <summary>
		/// Flip the player.
		/// </summary>
		private void Flip()
		{
			// Switch the way the player is facing.
			_isFacingLeft = !_isFacingLeft;
			// Multiply the player's x local scale by -1.
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
			
			OnTurned();
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

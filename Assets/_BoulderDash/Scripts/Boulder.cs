﻿using System.Collections;
using UnityEngine;

namespace BoulderDash
{
	public class Boulder : BaseBlock
	{

		#region EditorFields

		/// <summary>
		/// Push duration.
		/// </summary>
		[SerializeField]
		private float _duration;
		
		/// <summary>
		/// Pivot used for rotation.
		/// </summary>
		[SerializeField]
		private Transform _pivot;
		
		/// <summary>
		/// Audio source.
		/// </summary>
		[SerializeField]
		private AudioSource _audioSource;

		#endregion

		#region Fields
		
		/// <summary>
		/// Current movement time.
		/// </summary>
		private float _currentMovementTime;
	
		/// <summary>
		/// Start position before movement lerp.
		/// </summary>
		private Vector2 _startPos;

		/// <summary>
		/// Desired position after movement lerp.
		/// </summary>
		private Vector2 _desiredPos;

		/// <summary>
		/// Start rotation before rotation lerp.
		/// </summary>
		private Quaternion _startRot;

		/// <summary>
		/// Desired rotation after lerp.
		/// </summary>
		private Quaternion _desiredRot;

		/// <summary>
		/// Is there currently a movement coroutine running ?
		/// </summary>
		private bool _isMoving;

		#endregion
	
		#region Methods

		/// <summary>
		/// Called every frame.
		/// </summary>
		private void Update()
		{
			//DEBUG CODE
			if (Input.GetKeyUp(KeyCode.Space))
			{
				Move(Vector2.left);
			}
		}

		/// <summary>
		/// Move boulder according to given delta.
		/// </summary>
		/// <param name="delta">Vector2</param>
		/// /// <returns>Can move?</returns>
		private bool Move(Vector2 delta)
		{
			//Makes sure we can't push the boulder up or down.
			delta.y = 0;
		
			if (_isMoving == false)
			{
				_startPos = transform.position;
				_desiredPos = _startPos + (delta * GlobalVariables.TILE_SIZE);
				_startRot = _pivot.rotation;
				_desiredRot = Quaternion.Euler(0,0,_pivot.rotation.eulerAngles.z + (-90 * delta.x));
				Utils.DebugDrawScanner(_desiredPos, GlobalVariables.SPHERECAST_RADIUS);
				if (Physics2D.OverlapCircle(_desiredPos, GlobalVariables.SPHERECAST_RADIUS) == false)
				{
					_audioSource.Play();
					StartCoroutine(MovementCoroutine(_duration));
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Movement coroutine.
		/// </summary>
		/// <param name="delta">Vector2</param>
		/// <param name="duration">float</param>
		private IEnumerator MovementCoroutine(float duration)
		{
			_isMoving = true;
			while (_currentMovementTime <= duration)
			{
				_pivot.localRotation = Quaternion.Lerp(_startRot, _desiredRot, _currentMovementTime / _duration);
				transform.localPosition = Vector2.Lerp(_startPos, _desiredPos, _currentMovementTime / duration);
				//Debug.Log($"Lerp A: {_startPos} // Lerp B: {_desiredPos} // {_currentMovementTime / duration}");
			
				_currentMovementTime += Time.deltaTime;
				yield return null;
			}

			//Finishing up.
			transform.position = _desiredPos;
			_pivot.rotation = _desiredRot;
			_isMoving = false;
			_currentMovementTime = 0.0f;
		}


		/// <inheritdoc />
		public override bool AttemptOverlap(Vector2 dir)
		{
			return Move(dir);
		}

		#endregion

	}
}

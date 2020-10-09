using UnityEngine;

namespace BoulderDash
{
	public class MinerAnimationController : MonoBehaviour
	{

		#region EditorFields

		/// <summary>
		/// Animator.
		/// </summary>
		[SerializeField]
		private Animator _animator;
		
		/// <summary>
		/// Miner input handler.
		/// </summary>
		[SerializeField]
		private MinerMovement _minerMovement;

		private static readonly int LookAround = Animator.StringToHash("LookAround");

		#endregion

		#region Methods

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.L))
			{
				_animator.SetTrigger(LookAround);
			}
		}

		#endregion
		
	}
}

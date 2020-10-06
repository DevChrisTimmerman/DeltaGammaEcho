using System;
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

		#endregion

		#region Fields

		/// <summary>
		/// Has turned trigger hash.
		/// </summary>
		private static readonly int HasTurned = Animator.StringToHash("HasTurned");

		#endregion
		
		#region Initialization

		/// <summary>
		/// Called on start.
		/// </summary>
		private void Start()
		{
			_minerMovement.TurnedEvent += OnMinerTurned;
		}
		
		#endregion

		#region Methods
		
		/// <summary>
		/// On miner turned.
		/// </summary>
		/// <param name="sender">MinerMovement</param>
		private void OnMinerTurned(MinerMovement sender)
		{
			_animator.SetTrigger(HasTurned);
		}

		#endregion
		
	}
}

using UnityEngine;

namespace BoulderDash
{
	public class Diamond : BaseBlock
	{

		#region Methods

		/// <summary>
		/// Clears current diamond block.
		/// </summary>
		private void Clear()
		{
			Destroy(gameObject);
		}

		/// <inheritdoc />
		public override bool AttemptOverlap(Vector2 dir)
		{
			GameManager.Instance.DiamondCollected();
			Clear();
			return true;
		}

		#endregion
	}
}

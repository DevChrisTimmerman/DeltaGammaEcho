using UnityEngine;

namespace BoulderDash
{
	public class Diamond : BaseBlock
	{

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
			//TODO: add Points
			Clear();
			return true;
		}
	}
}

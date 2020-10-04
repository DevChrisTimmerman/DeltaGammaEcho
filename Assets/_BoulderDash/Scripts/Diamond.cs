using UnityEngine;

namespace BoulderDash
{
	public class Diamond : BaseBlock
	{
		/// <inheritdoc />
		public override bool AttemptOverlap(Vector2 pos)
		{
			return false;
		}
	}
}

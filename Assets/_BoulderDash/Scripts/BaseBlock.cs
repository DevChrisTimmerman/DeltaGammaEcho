using UnityEngine;

namespace BoulderDash
{
	public abstract class BaseBlock : MonoBehaviour
	{

		#region Methods

		/// <summary>
		/// Attempt to overlap block.
		/// </summary>
		/// <param name="dir">normalized vector of overlapping direction.</param>
		/// <returns>Whether or not the overlap attempt was successful</returns>
		public abstract bool AttemptOverlap(Vector2 dir);

		#endregion

	}
}

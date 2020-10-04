namespace BoulderDash
{
	public static class GlobalVariables
	{

		#region Properties

		/// <summary>
		/// Tile size.
		/// </summary>
		public const float TILE_SIZE = 0.25f;
		
		/// <summary>
		/// Tile radius.
		/// </summary>
		public static float TILE_RADIUS => TILE_SIZE / 2.0f;

		/// <summary>
		/// Radius used for physics checks with sphere.
		/// We subtract a small value to give some margin for error.
		/// </summary>
		public static float SPHERECAST_RADIUS => TILE_RADIUS - 0.005f;

		#endregion

	}
}

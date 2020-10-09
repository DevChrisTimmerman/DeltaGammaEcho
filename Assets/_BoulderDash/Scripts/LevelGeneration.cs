using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BoulderDash
{
	public class LevelGeneration : MonoBehaviour
	{

		#region Types

		[Flags]
		public enum Direction
		{
			Left = 0,
			Up = 1,
			Right = 2,
			Down = 4,
		}

		public struct LevelTile
		{
			public GameObject TilePrefab;
			public Direction TileDirections;
		}

		#endregion

		#region Fields

		/// <summary>
		/// Level container.
		/// </summary>
		private GameObject _levelContainer;

		#endregion

		#region EditorFields

		/// <summary>
		/// Number of tiles to generate;
		/// </summary>
		[SerializeField]
		private int _nrOfTiles;
		
		/// <summary>
		/// prefab tiles used to generate the level.
		/// </summary>
		[SerializeField]
		private GameObject[] _prefabTiles;

		#endregion

		#region Methods

		private void Start()
		{
			_levelContainer = new GameObject("[Level Container]");
			int randomIndex = Random.Range(0, _prefabTiles.Length);
			//GameObject spawnedTile = Instantiate(_prefabTiles[randomIndex], _levelContainer.transform);
			
			for (int i = 0; i < _nrOfTiles; i++)
			{
				
			}


			UnitTestInverseDirection();
		}

		private void UnitTestInverseDirection()
		{
			foreach (Direction dir in Enum.GetValues(typeof(Direction)))
			{
				Debug.Log($"Direction: {dir}, Inverse direction: {InvertDirection(dir)}");
			}
		}
		
		public Direction InvertDirection(Direction dir)
		{
			switch (dir)
			{
				case Direction.Left:
					dir = Direction.Right;
					break;
				case Direction.Up:
					dir = Direction.Down;
					break;
				case Direction.Right:
					dir = Direction.Left;
					break;
				case Direction.Down:
					dir = Direction.Up;
					break;
			}

			return dir;
		}

		private Direction GetRoomConnections(GameObject go)
		{
			string dir = go.name;
			dir = dir.Substring(0,dir.IndexOf('-'));
			Direction retVal = Direction.Left;
			foreach (char t in dir)
			{
				switch (t)
				{
					case 'L':
						break;
					case 'U':
						break;
					case 'R':
						break;
					case 'D':
						break;
				}
			}

			return retVal;
		}
		

		#endregion
		
	}
}

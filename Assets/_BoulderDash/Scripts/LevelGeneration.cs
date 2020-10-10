using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			Left = 2,
			Up = 4,
			Right = 8,
			Down = 16,
		}

		#endregion

		#region Fields

		/// <summary>
		/// Level container.
		/// </summary>
		private GameObject _levelContainer;

		/// <summary>
		/// Loaded in prefab tiles ready to be used for level generation.
		/// </summary>
		private readonly Dictionary<Direction, GameObject> _loadedPrefabTiles = new Dictionary<Direction, GameObject>();

		/// <summary>
		/// Previous spawned tile.
		/// </summary>
		private GameObject _previousSpawnedTile;

		/// <summary>
		/// Previous direction we came from.
		/// </summary>
		private Direction _previousInverseDir;

		private List<Vector2> _generatedTilePositions;

		#endregion

		#region EditorFields

		/// <summary>
		/// Number of tiles to generate;
		/// </summary>
		[SerializeField]
		private int _nrOfTiles;
		
		/// <summary>
		/// Size of a prefab tile.
		/// </summary>
		[SerializeField]
		private int _prefabSize = 11;
		
		/// <summary>
		/// prefab tiles used to generate the level.
		/// </summary>
		[SerializeField]
		private GameObject[] _prefabTiles;
		
		/// <summary>
		/// Variable used for testing.
		/// </summary>
		[SerializeField]
		private Direction _test;
		
		/// <summary>
		/// Time between each tile spawn.
		/// </summary>
		[SerializeField]
		private float _timeBetweenTiles = 0.5f;

		#endregion

		#region Methods

		/// <summary>
		/// Called at the start.
		/// </summary>kevin de paepe
		private void Start()
		{
			foreach (GameObject tile in _prefabTiles)
			{
				Direction connections = GetRoomConnections(tile);
				_loadedPrefabTiles.Add(connections, tile);
			}

			StartCoroutine(SpawnTiles());
			
			//UnitTest();
		}

		/// <summary>
		/// Start level generation.
		/// </summary>
		private IEnumerator SpawnTiles()
		{
			bool hasOverlapped = false;
			do
			{
				_levelContainer = new GameObject("[Level Container]");
				_generatedTilePositions = new List<Vector2>();
				//Starting tile
				int randomIndex = Random.Range(0, _loadedPrefabTiles.Count);
				_previousSpawnedTile = Instantiate(_loadedPrefabTiles.ElementAt(randomIndex).Value, _levelContainer.transform);
				_generatedTilePositions.Add(_previousSpawnedTile.transform.position);
				
				//Generate amount of tiles - starting tile
				for (int i = 0; i < _nrOfTiles - 1; i++)
				{
					Direction roomConnectionTag = GetRoomConnections(_previousSpawnedTile);
					//Remove previous direction so we dont go back
					roomConnectionTag = roomConnectionTag & ~_previousInverseDir;
					Direction newDir;
					Vector2 offset;
					GameObject newRoom;
					do
					{
						//Get random next tile but it must have the right connection to the previous tile
						newDir = GetRandomDirection(roomConnectionTag);
						offset = GetOffset(newDir);
						newDir = InvertDirection(newDir);
						newRoom = _loadedPrefabTiles.ElementAt(Random.Range(0, _loadedPrefabTiles.Count)).Value;
					} while (GetRoomConnections(newRoom).HasFlag(newDir) == false);

					//Spawn newly decided tile
					Vector2 previousTilePos = _previousSpawnedTile.transform.localPosition;
					_previousSpawnedTile = Instantiate(newRoom, _levelContainer.transform);
					_previousSpawnedTile.transform.localPosition = previousTilePos + offset;
					_previousInverseDir = newDir;

					//Does new tile overlap with previous tiles?
					if (_generatedTilePositions.Contains(_previousSpawnedTile.transform.position))
					{
						hasOverlapped = true;
						Debug.Log($"Overlapped at tile Nr: {i + 2}");
						Destroy(_levelContainer);
						break;
					}
					else
					{
						hasOverlapped = false;
						_generatedTilePositions.Add(_previousSpawnedTile.transform.position);
					}

					yield return new WaitForSeconds(_timeBetweenTiles);
				}
			} while (hasOverlapped);
		}

		/// <summary>
		/// Unit test.
		/// </summary>
		private void UnitTest()
		{
			foreach (Direction dir in Enum.GetValues(typeof(Direction)))
			{
				Debug.Log($"Direction: {dir}, Inverse direction: {InvertDirection(dir)}");
			}

			Debug.Log($"Direction: {_test}, Random: {GetRandomDirection(_test)}");
			Debug.Log($"Manual has flag check with !=0:{(_test & Direction.Right) != 0}");
		}
		
		/// <summary>
		/// Invert given direction.
		/// </summary>
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

		/// <summary>
		/// Get the rooms connections.
		/// </summary>
		/// <param name="go">Input room.</param>
		/// <returns>Direction flag</returns>
		private Direction GetRoomConnections(GameObject go)
		{
			string dir = go.name;
			dir = dir.Substring(0,dir.IndexOf('-'));
			Direction retVal = 0;
			foreach (char t in dir)
			{
				switch (t)
				{
					case 'L':
						retVal |= Direction.Left;
						break;
					case 'U':
						retVal |= Direction.Up;
						break;
					case 'R':
						retVal |= Direction.Right;
						break;
					case 'D':
						retVal |= Direction.Down;
						break;
				}
			}

			return retVal;
		}

		/// <summary>
		/// Get random direction from direction flag enum.
		/// </summary>
		/// <param name="dir">Collection possibly containing multiple values</param>
		/// <returns>One random direction</returns>
		private Direction GetRandomDirection(Direction dir)
		{
			Direction[] possibleDir = Enum.GetValues(typeof(Direction)).Cast<Direction>().Where(c => (dir & c) == c).ToArray();
			return  possibleDir[Random.Range(0 , possibleDir.Length)];
		}

		private Vector2 GetOffset(Direction dir)
		{
			Vector2 retVal = Vector2.zero;
			switch (dir)
			{
				case Direction.Left:
					retVal.x = -GlobalVariables.TILE_SIZE * _prefabSize;
					break;
				case Direction.Up:
					retVal.y = GlobalVariables.TILE_SIZE * _prefabSize;
					break;
				case Direction.Right:
					retVal.x = GlobalVariables.TILE_SIZE * _prefabSize;
					break;
				case Direction.Down:
					retVal.y = -GlobalVariables.TILE_SIZE * _prefabSize;
					break;
			}
			return retVal;
		}
		

		#endregion
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRally.Utils {
	public enum TileType {
		Normal,
		Rotator,
		Wall,
		PrioCore,
		Conveyor,
		TrapDoor,
		Stomper,
		Radioactive,
		RepairSite,
		Button,
		OneWayWall,
		Puddle,
		Pit,
		Ramp,
	}

	public enum Direction {
		Up,
		Right,
		Down,
		Left,
	}

	public class Tile {

		public Tile() {

		}

		public Tile(TileType type, bool isEmpty, Direction tileDirection, Direction rotaterDirection, int level) {
			Type = type;
			IsEmpty = isEmpty;
			TileDirection = tileDirection;
			RotatorDirection = rotaterDirection;
			Level = level;
		}

		public TileType Type = TileType.Normal;
		public bool IsEmpty = true;
		public Direction TileDirection = Direction.Up;
		public Direction RotatorDirection = Direction.Left;
		public int Level = 1;

	}
}

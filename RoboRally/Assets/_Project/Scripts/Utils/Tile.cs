using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Ramp
}

public enum Direction {
	Up,
	Down,
	Left,
	Right
}

public class Tile {

	public TileType Type = TileType.Normal;
	public bool IsEmpty = true;
	public Direction TileDirection = Direction.Up;
	public Direction RotatorDirection = Direction.Left;


}

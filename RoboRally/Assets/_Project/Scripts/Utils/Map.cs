using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map {

	[JsonProperty]
	private Tile[,] Tiles; // Columns | Rows => Tiles[0][2] = 0 | 2
	[JsonProperty]
	private int ColumnCount = 10;
	[JsonProperty]
	private int RowCount = 10;

	public Map(int columnCount = 10, int rowCount = 10) {
		this.ColumnCount = columnCount;
		this.RowCount = rowCount;
		Tiles = EmptyMap();
	}

	public Tile[,] EmptyMap(int columnCount, int rowCount) {
		this.ColumnCount = columnCount;
		this.RowCount = rowCount;
		return EmptyMap();
	}

	public Tile[,] EmptyMap() {
		Tile[,] tiles = new Tile[ColumnCount, RowCount];

		for(int c = 0; c < ColumnCount; c++) {
			for(int r = 0; r < RowCount; r++) {
				tiles[c, r] = new Tile();
			}
		}

		return tiles;
	}

	public Tile GetTile(int x, int y) {
		if(ColumnCount <= x || RowCount <= y)
			return null;

		return Tiles[x, y];
	}

	public bool SetTile(int x, int y, Tile tile) {
		if(ColumnCount <= x || RowCount <= y)
			return false;
		if(tile.Type == TileType.PrioCore && PrioCoreCount() > 0)
			return false;

		Tiles[x, y] = tile;
		return true;
	}

	public bool AddColumn(int index) {
		if(index < 0 || index > ColumnCount)
			return false;

		ColumnCount++;
		Tile[,] tiles = new Tile[ColumnCount, RowCount];
		for(int c = 0; c < ColumnCount; c++) {
			for(int r = 0; r < RowCount; r++) {
				if(c < index) {
					tiles[c, r] = Tiles[c, r];
				} else if(c > index) {
					tiles[c, r] = Tiles[c - 1, r];
				} else {
					tiles[c, r] = new Tile();
				}
			}
		}
		Tiles = tiles;

		return true;
	}

	public bool RemoveColumn(int index) {
		if(index < 0 || index >= ColumnCount)
			return false;

		ColumnCount--;
		Tile[,] tiles = new Tile[ColumnCount, RowCount];
		for(int c = 0; c < ColumnCount; c++) {
			for(int r = 0; r < RowCount; r++) {
				if(c < index) {
					tiles[c, r] = Tiles[c, r];
				} else if(c < index) {
					tiles[c, r] = Tiles[c + 1, r];
				} else {
					tiles[c, r] = new Tile();
				}
			}
		}
		Tiles = tiles;

		return true;
	}

	public bool AddRow(int index) {
		if(index < 0 || index > RowCount)
			return false;

		RowCount++;
		Tile[,] tiles = new Tile[ColumnCount, RowCount];
		for(int c = 0; c < ColumnCount; c++) {
			for(int r = 0; r < RowCount; r++) {
				if(r < index) {
					tiles[c, r] = Tiles[c, r];
				} else if(r > index) {
					tiles[c, r] = Tiles[c, r - 1];
				} else {
					tiles[c, r] = new Tile();
				}
			}
		}
		Tiles = tiles;

		return true;
	}

	public bool RemoveRow(int index) {
		if(index < 0 || index >= RowCount)
			return false;

		RowCount--;
		Tile[,] tiles = new Tile[ColumnCount, RowCount];
		for(int c = 0; c < ColumnCount; c++) {
			for(int r = 0; r < RowCount; r++) {
				if(r < index) {
					tiles[c, r] = Tiles[c, r];
				} else if(r > index) {
					tiles[c, r] = Tiles[c, r + 1];
				} else {
					tiles[c, r] = new Tile();
				}
			}
		}
		Tiles = tiles;

		return true;
	}

	public bool SwitchTiles(int x1, int y1, int x2, int y2) {
		if(ColumnCount <= x1 || RowCount <= y1 || ColumnCount <= x2 || RowCount <= y2)
			return false;

		Tile tile1 = Tiles[x1, y1];
		Tile tile2 = Tiles[x2, y2];

		Tiles[x1, y1] = tile2;
		Tiles[x2, y2] = tile1;

		return true;
	}

	public bool SwitchColumns(int column1, int column2) {
		if(ColumnCount <= column1 || ColumnCount <= column2)
			return false;

		for(int r = 0; r < RowCount; r++) {
			SwitchTiles(column1, r, column2, r);
		}

		return true;
	}

	public bool SwitchRows(int row1, int row2) {
		if(RowCount <= row1 || RowCount <= row2)
			return false;

		for(int c = 0; c < ColumnCount; c++) {
			SwitchTiles(c, row1, c, row2);
		}

		return true;
	}

	public int PrioCoreCount() {
		int count = 0;
		for(int c = 0; c < ColumnCount; c++) {
			for(int r = 0; r < RowCount; r++) {
				if(Tiles[c, r].Type == TileType.PrioCore)
					count++;
			}
		}
		return count;
	}

	public bool IsValid() {
		// Checks if all Tiles are set
		for(int c = 0; c < ColumnCount; c++) {
			for(int r = 0; r < RowCount; r++) {
				if(Tiles[c, r] == null)
					return false;
			}
		}

		// Checks if there is only one priority Beacon
		if(PrioCoreCount() != 1)
			return false;

		return true;
	}

	public string ToMapString() {
		string mapString = "";
		for(int r = 0; r < RowCount + 1; r++) {
			for(int c = 0; c < ColumnCount + 1; c++) {
				if(r == RowCount) {
					if(c == 0) {
						mapString += "╚═══";
					} else if(c == ColumnCount) {
						mapString += "╝\n";
					} else {
						mapString += "╧═══";
					}
				} else {
					if(r == 0 && c == 0) {
						mapString += "╔═══";
					} else if(r == 0 && c == ColumnCount) {
						mapString += "╗\n";
					} else if(c == 0) {
						mapString += "╟═══";
					} else if(c == ColumnCount) {
						mapString += "╢\n";
					} else if(r == 0) {
						mapString += "╤═══";
					} else {
						mapString += "╬═══";
					}
				}
			}
			for(int c = 0; c < ColumnCount; c++) {
				if(r == RowCount) {
				} else {
					string value = ((int) Tiles[c, r].Type).ToString();
					mapString += "║";
					mapString += " ";
					mapString += value;
					mapString += new string(' ', 2 - value.Length);
					if(c == ColumnCount - 1)
						mapString += "║\n";
				}
			}
		}
		return mapString;
	}
}

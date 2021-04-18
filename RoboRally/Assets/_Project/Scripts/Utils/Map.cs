using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tgm.Roborally.Api.Model;

namespace Gameclient.Utils {
	[DataContract]
	public class Map {

		[DataMember] private Tile[] _tiles;

		public Map(int columnCount = 10, int rowCount = 10) {
			Height = columnCount;
			Width = rowCount;
			_tiles = InitEmpty(columnCount, rowCount);
		}

		[DataMember] public int Height { get; private set; }

		[DataMember] public int Width { get; private set; }

		public MapInfo Info => new MapInfo(this);
		
		public Position PrioCorePos {
			get {
				for(int y = 0; y < Width; y++) {
					for(int x = 0; x < Height; x++) {
						if(this[x, y].Type == TileType.PrioCore)
							return new Position(x, y);
					}
				}
				return null;
			}
		}


		/// <summary>
		/// Set/Get the Tile at the specific position defined by X and Y cordinates
		/// </summary>
		/// <param name="x">the x cordinate (starting at 0)</param>
		/// <param name="y">the y cordinate (starting at 0)</param>
		/// <exception cref="ArgumentException">When trying to set an illegal tile (eg. 2 prio cores on one map)</exception>
		public Tile this[int x, int y] {
			get => _tiles[x + y * Width];
			set {
				if(value.Type == TileType.PrioCore && PrioCoreCount > 0)
					throw new ArgumentException("Only one Prio Core per Map allowed");
				_tiles[x + y * Width] = value;
			}
		}

		public int PrioCoreCount {
			get {
				int count = 0;
				for(int c = 0; c < Height; c++) {
					for(int r = 0; r < Width; r++) {
						if(this[c, r].Type == TileType.PrioCore)
							count++;
					}
				}

				return count;
			}
		}

		private static Tile[] InitEmpty(int columnCount, int rowCount) {
			Tile[] tiles = new Tile[columnCount * rowCount];
			for(int c = 0; c < columnCount; c++) {
				for(int r = 0; r < rowCount; r++)
					tiles[r * columnCount + c] = new Tile();
			}
			return tiles;
		}

		public bool AddColumn(int index) {
			if(index < 0 || index > Height)
				return false;

			Height++;
			Tile[,] tiles = new Tile[Height, Width];
			for(int c = 0; c < Height; c++) {
				for(int r = 0; r < Width; r++) {
					if(c < index)
						tiles[c, r] = this[c, r];
					else if(c > index)
						tiles[c, r] = this[c - 1, r];
					else
						tiles[c, r] = new Tile();
				}
			}
			SetDeepTiles(tiles);

			return true;
		}

		public bool RemoveColumn(int index) {
			if(index < 0 || index >= Height)
				return false;

			Height--;
			Tile[,] tiles = new Tile[Height, Width];
			for(int c = 0; c < Height; c++) {
				for(int r = 0; r < Width; r++) {
					if(c < index)
						tiles[c, r] = this[c, r];
					else if(c > index)
						tiles[c, r] = this[c + 1, r];
					else
						tiles[c, r] = new Tile();
				}
			}

			SetDeepTiles(tiles);

			return true;
		}

		private void SetDeepTiles(Tile[,] tiles) {
			Height = tiles.GetLength(1);
			Width = tiles.GetLength(0);
			_tiles = new Tile[Height * Width];
			for(int y = 0; y < Height; y++) {
				for(int x = 0; x < Width; x++) {
					this[x, y] = tiles[x, y];
				}
			}
		}

		public bool AddRow(int index) {
			if(index < 0 || index > Width)
				return false;

			Width++;
			Tile[,] tiles = new Tile[Height, Width];
			for(int c = 0; c < Height; c++) {
				for(int r = 0; r < Width; r++) {
					if(r < index)
						tiles[c, r] = this[c, r];
					else if(r > index)
						tiles[c, r] = this[c, r - 1];
					else
						tiles[c, r] = new Tile();
				}
			}

			SetDeepTiles(tiles);

			return true;
		}

		public bool RemoveRow(int index) {
			if(index < 0 || index >= Width)
				return false;

			Width--;
			Tile[,] tiles = new Tile[Height, Width];
			for(int c = 0; c < Height; c++) {
				for(int r = 0; r < Width; r++) {
					if(r < index)
						tiles[c, r] = this[c, r];
					else if(r > index)
						tiles[c, r] = this[c, r + 1];
				}
			}

			SetDeepTiles(tiles);

			return true;
		}

		public bool SwitchTiles(int x1, int y1, int x2, int y2) {
			if(Height <= x1 || Width <= y1 || Height <= x2 || Width <= y2)
				return false;

			Tile tile1 = this[x1, y1];
			Tile tile2 = this[x2, y2];

			this[x1, y1] = tile2;
			this[x2, y2] = tile1;

			return true;
		}

		public bool SwitchColumns(int column1, int column2) {
			if(Height <= column1 || Height <= column2)
				return false;

			for(int r = 0; r < Width; r++)
				SwitchTiles(column1, r, column2, r);

			return true;
		}

		public bool SwitchRows(int row1, int row2) {
			if(Width <= row1 || Width <= row2)
				return false;

			for(int c = 0; c < Height; c++)
				SwitchTiles(c, row1, c, row2);

			return true;
		}


		public bool IsValid() {
			// Checks if all Tiles are set
			for(int c = 0; c < Height; c++) {
				for(int r = 0; r < Width; r++) {
					if(this[c, r] == null)
						return false;
				}
			}

			// Checks if there is only one priority Beacon
			if(PrioCoreCount != 1)
				return false;

			return true;
		}

		public string ToMapString() {
			string mapString = "";
			for(int r = 0; r < Width + 1; r++) {
				for(int c = 0; c < Height + 1; c++) {
					if(r == Width) {
						if(c == 0)
							mapString += "╚═══";
						else if(c == Height)
							mapString += "╝\n";
						else
							mapString += "╧═══";
					} else {
						if(r == 0 && c == 0)
							mapString += "╔═══";
						else if(r == 0 && c == Height)
							mapString += "╗\n";
						else if(c == 0)
							mapString += "╟═══";
						else if(c == Height)
							mapString += "╢\n";
						else if(r == 0)
							mapString += "╤═══";
						else
							mapString += "╬═══";
					}
				}

				for(int c = 0; c < Height; c++) {
					if(r == Width) {
					} else {
						string value = ((int) this[c, r].Type).ToString();
						mapString += "║";
						mapString += " ";
						mapString += value;
						mapString += new string(' ', 2 - value.Length);
						if(c == Height - 1)
							mapString += "║\n";
					}
				}
			}

			return mapString;
		}

		public bool IsWithin(Position newPos) => newPos.X < Width && newPos.Y < Height;
	}
}

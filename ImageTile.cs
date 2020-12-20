using System;

namespace Advent_of_Code_2020 {
	internal class ImageTile {
		public readonly char[,] grid;
		public readonly int id;
		public Rotation rotation;
		public bool flipped = false;

		public enum Rotation {
			NORTH,EAST,SOUTH,WEST
		}

		public ImageTile(char[,] grid, int id) {
			this.grid = grid;
			this.id = id;
		}

		internal bool MatchEdge(char[] edge2, int ox, int oy) {
			char[] edge1 = GetEdge(ox, oy, rotation, flipped);
			for(int i=0; i<edge1.Length;i++) {
				if(edge1[i] != edge2[i])
					return false;
			}
			return true;
		}

		internal char[] GetEdge(int ox, int oy, Rotation rot, bool v3) {
			oy = -oy;
			int l = grid.GetLength(0);
			char[] edge = new char[l];
			Rotation r = SideFrom(ox, oy);
			rot = (Rotation)(((int)rot + (v3 ? -1 : 1) * (int)r + 4) % 4);
			for(int i=0;i<l;i++) {
				edge[i] = GetChar(i, rot);
			}
			if(r == Rotation.SOUTH || r == Rotation.WEST) {
				Array.Reverse(edge);
			}
			if(v3) {
				Array.Reverse(edge);
			}
			return edge;
		}

		private Rotation SideFrom(int ox, int oy) {
			if(ox > 0) return Rotation.EAST;
			if(ox < 0) return Rotation.WEST;
			if(oy < 0) return Rotation.SOUTH;
			return Rotation.NORTH;
		}

		private char GetChar(int i, Rotation rot) {
			int l = grid.GetLength(0)-1;
			if(rot == Rotation.NORTH) {
				return grid[i, 0];
			}
			if(rot == Rotation.SOUTH) {
				return grid[l - i, l];
			}
			if(rot == Rotation.EAST) {
				return grid[l, i];
			}
			if(rot == Rotation.WEST) {
				return grid[0, l-i];
			}
			return '!';
		}

		public override string ToString() {
			return id.ToString();
		}

		internal char GetCharAt(int x, int y) {
			int l = grid.GetLength(0) - 1;
			Rotation rot = rotation;
			if(flipped) {
				x = l - x;
			}
			if(rot == Rotation.NORTH) {
				return grid[x, y];
			}
			if(rot == Rotation.SOUTH) {
				return grid[l - x, l-y];
			}
			if(rot == Rotation.EAST) {
				return grid[l-y, x];
			}
			if(rot == Rotation.WEST) {
				return grid[y, l - x];
			}
			return '!';
		}
	}
}
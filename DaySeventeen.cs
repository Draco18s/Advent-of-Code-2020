using System;

namespace Advent_of_Code_2020 {
	internal class DaySeventeen {
		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			int mid = lines.Length / 2;
			char[,,] grid = new char[lines.Length, lines.Length, lines.Length];
			for(int z = 0; z < lines.Length; z++) {
				for(int x=0; x < lines.Length; x++) {
					for(int y = 0; y < lines.Length; y++) {
						if(z == mid)
							grid[x, y, mid] = lines[x][y];
						else
							grid[x, y, z] = '.';
					}
				}
			}
			for(int t=0;t<6;t++) {
				char[,,] newgrid = new char[grid.GetLength(0) + 2, grid.GetLength(1) + 2, grid.GetLength(2) + 2];
				for(int x = 0; x < newgrid.GetLength(0); x++) {
					for(int y = 0; y < newgrid.GetLength(1); y++) {
						for(int z = 0; z < newgrid.GetLength(2); z++) {
							newgrid[x,y,z] = GetNew(grid, x-1, y-1, z-1);
						}
					}
				}
				grid = newgrid;
			}
			int count = 0;
			for(int z = 0; z < grid.GetLength(2); z++) {
				//Console.Write("Layer " + z + "\n");
				for(int x = 0; x < grid.GetLength(0); x++) {
					for(int y = 0; y < grid.GetLength(1); y++) {
						if(grid[x, y, z] == '#') count++;
						//Console.Write(grid[x, y, z]);
					}
					//Console.Write("\n");
				}
			}
			return count;
		}

		private static char GetNew(char[,,] grid, int x, int y, int z) {
			int count = 0;
			char th = '.';
			if(!(x < 0 || y < 0 || z < 0 || x >= grid.GetLength(0) || y >= grid.GetLength(1) || z >= grid.GetLength(2))) {
				th = grid[x, y, z];
			}
			for(int ox = -1; ox <= 1; ox++) {
				for(int oy = -1; oy <= 1; oy++) {
					for(int oz = -1; oz <= 1; oz++) {
						if(ox + x < 0 || ox + x >= grid.GetLength(0)) continue;
						if(oy + y < 0 || oy + y >= grid.GetLength(1)) continue;
						if(oz + z < 0 || oz + z >= grid.GetLength(2)) continue;
						if(ox == 0 && oy == 0 && oz == 0) continue;
						if(grid[ox + x, oy + y, oz + z] == '#')
							count++;
					}
				}
			}
			if(th == '.' && count == 3) return '#';
			if(th == '#' && (count == 2 || count == 3)) return '#';
			return '.';
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			int mid = lines.Length / 2;
			char[,,,] grid = new char[lines.Length, lines.Length, lines.Length, lines.Length];
			for(int w = 0; w < lines.Length; w++) {
				for(int z = 0; z < lines.Length; z++) {
					for(int x = 0; x < lines.Length; x++) {
						for(int y = 0; y < lines.Length; y++) {
							if(z == mid && w == mid)
								grid[mid, x, y, mid] = lines[x][y];
							else
								grid[w, x, y, z] = '.';
						}
					}
				}
			}
			for(int t = 0; t < 6; t++) {
				char[,,,] newgrid = new char[grid.GetLength(0) + 2, grid.GetLength(1) + 2, grid.GetLength(2) + 2, grid.GetLength(3) + 2];
				for(int w = 0; w < newgrid.GetLength(0); w++) {
					for(int x = 0; x < newgrid.GetLength(1); x++) {
						for(int y = 0; y < newgrid.GetLength(2); y++) {
							for(int z = 0; z < newgrid.GetLength(3); z++) {
								newgrid[w, x, y, z] = GetNew2(grid, w - 1, x - 1, y - 1, z - 1);
							}
						}
					}
				}
				grid = newgrid;
			}
			int count = 0;
			for(int w = 0; w < grid.GetLength(0); w++) {
				for(int z = 0; z < grid.GetLength(3); z++) {
					for(int x = 0; x < grid.GetLength(1); x++) {
						for(int y = 0; y < grid.GetLength(2); y++) {
							if(grid[w, x, y, z] == '#') count++;
						}
					}
				}
			}
			return count;
		}

		private static char GetNew2(char[,,,] grid, int w, int x, int y, int z) {
			int count = 0;
			char th = '.';
			if(!(w < 0 || x < 0 || y < 0 || z < 0 || w >= grid.GetLength(0) || x >= grid.GetLength(1) || y >= grid.GetLength(2) || z >= grid.GetLength(3))) {
				th = grid[w, x, y, z];
			}
			for(int ow = -1; ow <= 1; ow++) {
				for(int ox = -1; ox <= 1; ox++) {
					for(int oy = -1; oy <= 1; oy++) {
						for(int oz = -1; oz <= 1; oz++) {
							if(ow + w < 0 || ow + w >= grid.GetLength(0)) continue;
							if(ox + x < 0 || ox + x >= grid.GetLength(1)) continue;
							if(oy + y < 0 || oy + y >= grid.GetLength(2)) continue;
							if(oz + z < 0 || oz + z >= grid.GetLength(3)) continue;
							if(ow == 0 && ox == 0 && oy == 0 && oz == 0) continue;
							if(grid[ow + w, ox + x, oy + y, oz + z] == '#') count++;
						}
					}
				}
			}
			if(th == '.' && count == 3) return '#';
			if(th == '#' && (count == 2 || count == 3)) return '#';
			return '.';
		}
	}
}
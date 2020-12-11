using System;
using System.Linq;

namespace Advent_of_Code_2020 {
	internal class DayEleven {
		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			char[,] grid = new char[lines[0].Length, lines.Length];
			for(int y=0; y<lines.Length;y++) {
				string line = lines[y];
				for(int x = 0; x < line.Length; x++) {
					grid[x, y] = line[x];
				}
			}
			int loops = 0;
			do {
				loops++; // just in case
				char[,] ngrid = new char[lines[0].Length,lines.Length];
				bool didChange = false;
				for(int y = 0; y < grid.GetLength(1); y++) {
					for(int x = 0; x < grid.GetLength(0); x++) {
						ngrid[x, y] = grid[x, y];
						if(grid[x, y] == '.') continue; // floor spaces don't update
						int num = GetAround(grid, x, y);
						if(num == 0 && grid[x,y] == 'L') {
							ngrid[x, y] = '#';
							didChange |= true;
						}
						// more than 4 neighbors: move
						if(num >= 4 && grid[x, y] == '#') {
							ngrid[x, y] = 'L';
							didChange |= true;
						}
					}
				}
				grid = ngrid;
				if(!didChange || loops > 5000) {
					int seatsFilled = 0;
					for(int y = 0; y < grid.GetLength(1); y++) {
						for(int x = 0; x < grid.GetLength(0); x++) {
							if(grid[x, y] == '#') seatsFilled++;
							//Console.Write(grid[x, y]);
						}
						//Console.Write('\n');
					}
					return seatsFilled;
				}
			} while(true);
		}

		internal static int Part2(string input) {
			string[] lines = input.Split('\n');
			char[,] grid = new char[lines[0].Length, lines.Length];
			for(int y = 0; y < lines.Length; y++) {
				string line = lines[y];
				for(int x = 0; x < line.Length; x++) {
					grid[x, y] = line[x];
				}
			}
			int loops = 0;
			do {
				loops++; // just in case
				char[,] ngrid = new char[lines[0].Length, lines.Length];
				bool didChange = false;
				for(int y = 0; y < grid.GetLength(1); y++) {
					for(int x = 0; x < grid.GetLength(0); x++) {
						ngrid[x, y] = grid[x, y];
						if(grid[x, y] == '.') continue;
						int num = GetAroundLOS(grid, x, y);
						if(num == 0 && grid[x, y] == 'L') {
							ngrid[x, y] = '#';
							didChange |= true;
						}
						// rule update. now 5 'neighbors'
						if(num >= 5 && grid[x, y] == '#') {
							ngrid[x, y] = 'L';
							didChange |= true;
						}
					}
				}
				grid = ngrid;
				if(!didChange || loops > 5000) {
					int seatsFilled = 0;
					for(int y = 0; y < grid.GetLength(1); y++) {
						for(int x = 0; x < grid.GetLength(0); x++) {
							if(grid[x, y] == '#') seatsFilled++;
							//Console.Write(grid[x, y]);
						}
						//Console.Write('\n');
					}
					return seatsFilled;
				}
			} while(true);
		}

		private static int GetAround(char[,] grid, int x, int y) {
			int c = 0;
			for(int xo = -1; xo <= 1; xo++) {
				for(int yo = -1; yo <= 1; yo++) {
					if(xo == 0 && yo == 0) continue;
					if(xo + x < 0 || xo + x >= grid.GetLength(0)) continue;
					if(yo + y < 0 || yo + y >= grid.GetLength(1)) continue;
					if(grid[x + xo, y + yo] == '#') { // check all 8 directions for filled seats
						c++;
					}
				}
			}
			return c;
		}

		private static int GetAroundLOS(char[,] grid, int x, int y) {
			int c = 0;
			for(int xo = -1; xo <= 1; xo++) {
				for(int yo = -1; yo <= 1; yo++) {
					if(xo == 0 && yo == 0) continue;
					int m = 1; //line of sight distance
					int xoo = xo;
					int yoo = yo;
					while(!(xoo + x < 0 || xoo + x >= grid.GetLength(0)) && !(yoo + y < 0 || yoo + y >= grid.GetLength(1)) && grid[x + xoo, y + yoo] == '.') {
						m++;
						xoo = xo * m;
						yoo = yo * m;
					}
					if(xoo + x < 0 || xoo + x >= grid.GetLength(0)) continue;
					if(yoo + y < 0 || yoo + y >= grid.GetLength(1)) continue;
					if(grid[x + xoo, y + yoo] == '#') { // check all 8 directions for LOS filled seats
						c++;
					}
				}
			}
			return c;
		}
	}
}
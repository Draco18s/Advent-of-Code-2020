using System;

namespace Advent_of_Code_2020 {
	internal class DayTwentyFour {
		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			bool[,] hexGrid = new bool[100, 100];
			
			int refx = 50;
			int refy = 50;
			foreach(string line in lines) {
				int x = refx;
				int y = refy;
				for(int c =0; c<line.Length;c++) {
					HexDir dir = HexDir.E;
					string next = "";
					if(line[c] == 's' || line[c] == 'n') {
						next = line[c].ToString()+line[c+1];
						c++;
					}
					else {
						next = line[c].ToString();
					}
					dir = (HexDir)Enum.Parse(typeof(HexDir), next.ToUpper());
					GetHexPos(ref x, ref y, dir);
				}
				hexGrid[x, y] = !hexGrid[x, y];
			}
			int count = 0;
			bool offset = false;
			for(int py = 0; py < 100; py++) {
				for(int px = 0; px < 100; px++) {
					if(hexGrid[px, py]) {
						count++;
					}
					//Console.Write((hexGrid[px, py] ? "##" : "  "));
				}
				offset = !offset;
				//Console.Write("\n" + (offset?" ":""));
			}
			return count;
		}

		private static void GetHexPos(ref int x, ref int y, HexDir dir) {
			switch(dir) {
				case HexDir.E:
					x++;
					return;
				case HexDir.W:
					x--;
					return;
				case HexDir.NW:
					y--;
					return;
				case HexDir.SE:
					y++;
					return;
				case HexDir.NE:
					x++;
					y--;
					return;
				case HexDir.SW:
					x--;
					y++;
					return;
			}
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			bool[,] hexGrid = new bool[200, 200];

			int refx = 100;
			int refy = 100;
			foreach(string line in lines) {
				int x = refx;
				int y = refy;
				for(int c = 0; c < line.Length; c++) {
					HexDir dir = HexDir.E;
					string next = "";
					if(line[c] == 's' || line[c] == 'n') {
						next = line[c].ToString() + line[c + 1];
						c++;
					}
					else {
						next = line[c].ToString();
					}
					dir = (HexDir)Enum.Parse(typeof(HexDir), next.ToUpper());
					GetHexPos(ref x, ref y, dir);
				}
				hexGrid[x, y] = !hexGrid[x, y];
			}
			for(int i =0; i< 100; i++)
				MutateGrid(ref hexGrid);
			int count = 0;
			bool offset = false;
			for(int py = 0; py < 200; py++) {
				for(int px = 0; px < 200; px++) {
					if(hexGrid[px, py]) {
						count++;
					}
					//Console.Write((hexGrid[px, py] ? "##" : "  "));
				}
				offset = !offset;
				//Console.Write("\n" + (offset ? " " : ""));
			}
			return count;
		}

		private static HexDir[] dirs = { HexDir.E, HexDir.W, HexDir.SE, HexDir.SW, HexDir.NE, HexDir.NW };

		private static void MutateGrid(ref bool[,] hexGrid) {
			bool[,] ngrid = new bool[200, 200];
			for(int x=0;x<200;x++) {
				for(int y = 0; y < 200; y++) {
					int count = 0;
					foreach(HexDir d in dirs) {
						int ox = x;
						int oy = y;
						GetHexPos(ref ox, ref oy, d);
						if(ox < 0 || oy < 0) continue;
						if(ox >= 200 || oy >= 200) continue;
						if(hexGrid[ox, oy]) { // count black
							count++;
						}
					}
					ngrid[x, y] = hexGrid[x, y];
					if(hexGrid[x,y] == false && count == 2) { // white
						ngrid[x, y] = true;
					}
					else if(hexGrid[x, y] == true && (count == 0 || count > 2)) {
						ngrid[x, y] = false;
					}
				}
			}
			hexGrid = ngrid;
		}

		private enum HexDir {
			E,W,SE,NE,SW,NW
		}
	}
}
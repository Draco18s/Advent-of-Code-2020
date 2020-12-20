using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020 {
	// Heck if I remember how any of this shiz works any more.
	internal class DayTwenty {
		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			List<ImageTile> tileList = new List<ImageTile>();
			for(int l = 0; l < lines.Length; l++){
				string line = lines[l];
				if(line.Contains("Tile")) {
					int i = Array.IndexOf(lines, line);
					int j = FindNextEmpty(i, lines);
					char[,] grid = new char[lines[l+1].Length, j - i - 1];
					for(int q = 0; q < grid.GetLength(0); q++) { 
						for(int w = 0; w < grid.GetLength(1); w++) {
							grid[w, q] = lines[i+q+1][w];
						}
					}
					string nn = line.Substring(5, line.Length - 6);
					int id = int.Parse(nn);
					ImageTile tile = new ImageTile(grid, id);
					tileList.Add(tile);
				}
			}
			List<Tuple<ImageTile,int>> possibilities = FindPossibleCorners(tileList);
			long a = possibilities[0].Item1.id;
			long b = possibilities[1].Item1.id;
			long c = possibilities[2].Item1.id;
			long d = possibilities[3].Item1.id;
			return a*b*c*d;
		}

		private static List<Tuple<ImageTile, int>> FindPossibleCorners(List<ImageTile> list) {
			List<Tuple<ImageTile, int>> possibilities = new List<Tuple<ImageTile, int>>();
			Vector2Int cell = new Vector2Int(0, 0);
			foreach(ImageTile img in list) {
				List<ImageTile> neighbors = new List<ImageTile>();
				int nd = 0;
				foreach(Vector2Int dir in dirs) {
					Vector2Int off = new Vector2Int(cell.x + dir.x, cell.y + dir.y);
					bool added = false;
					foreach(ImageTile t2 in list) {
						if(t2 == img) continue;
						if(CheckPlacement(img, t2, new Vector2Int(-dir.x, -dir.y))) {
							neighbors.Add(t2);
							added = true;
						}
					}
					if(added) {
						nd++;
					}
				}
				int c = neighbors.Select(m => m.id).Distinct().Count();
				if(c <= 3 && nd == 2) {
					possibilities.Add(new Tuple<ImageTile,int>(img,c));
				}
			}
			return possibilities;
		}

		private static ImageTile FindSingleValid(Dictionary<Vector2Int, ImageTile> image, Vector2Int cell, List<ImageTile> list) {
			List<ImageTile> neighbors = new List<ImageTile>();
			Vector2Int check = new Vector2Int(0, 0);
			foreach(Vector2Int dir in dirs) {
				Vector2Int off = new Vector2Int(cell.x + dir.x, cell.y + dir.y);
				if(image.ContainsKey(off)) {
					check = dir;
					foreach(ImageTile t2 in list) {
						if(CheckPlacement(image[off], t2, new Vector2Int(-dir.x, -dir.y))) {
							neighbors.Add(t2);
						}
					}
					break;
				}
			}
			List<ImageTile> neighbors2 = new List<ImageTile>();
			foreach(Vector2Int dir in dirs) {
				if(dir.x == check.x && dir.y == check.y) continue;
				Vector2Int off = new Vector2Int(cell.x + dir.x, cell.y + dir.y);
				if(image.ContainsKey(off)) {
					foreach(ImageTile t2 in neighbors) {
						if(CheckPlacement(image[off], t2, new Vector2Int(-dir.x, -dir.y))) {
							neighbors2.Add(t2);
						}
					}
					break;
				}
			}
			if(neighbors2.Count == 1) {
				return neighbors2[0];
			}
			return null;
		}

		private static ImageTile GetSingleOption(ImageTile start, List<ImageTile> list) {
			foreach(Vector2Int dir in dirs) {
				List<ImageTile> neighbors = new List<ImageTile>();
				foreach(ImageTile t2 in list) {
					if(start == t2) continue;
					if(CheckPlacement(start, t2, dir)) {
						neighbors.Add(t2);
					}
				}
				if(neighbors.Count == 1) {
					return neighbors[0];
				}
			}
			return null;
		}

		private static bool[] flips = { true, false };
		private static ImageTile.Rotation[] rots = { ImageTile.Rotation.NORTH, ImageTile.Rotation.EAST, ImageTile.Rotation.SOUTH, ImageTile.Rotation.WEST };
		private static Vector2Int[] dirs = { new Vector2Int(-1,0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1)  };
		
		private static bool CheckPlacement(ImageTile existing, ImageTile newTile, Vector2Int dir) {
			foreach(ImageTile.Rotation rot in rots) {
				foreach(Vector2Int dir2 in dirs) {
					foreach(bool fl in flips) {
						char[] edge1 = newTile.GetEdge(dir2.x, dir2.y, rot, fl);
						if(existing.MatchEdge(edge1, dir.x, dir.y)) {
							return true;
						}
					}
				}
			}
			return false;
		}

		private static bool CheckPlacementWithOrrientation(ImageTile existing, ImageTile newTile, Vector2Int dir) {
			char[] edge1 = newTile.GetEdge(dir.x, dir.y, newTile.rotation, newTile.flipped);
			char[] edge2 = existing.GetEdge(-dir.x, -dir.y, existing.rotation, existing.flipped);
			for(int i = 0; i < edge1.Length; i++) {
				if(edge1[i] != edge2[i])
					return false;
			}
			return true;
		}

		private static int FindNextEmpty(int v, string[] lines) {
			for(; v < lines.Length; v++) {
				if(lines[v] == "") {
					return v;
				}
			}
			return v;
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			List<ImageTile> tileList = new List<ImageTile>();
			for(int l = 0; l < lines.Length; l++) {
				string line = lines[l];
				if(line.Contains("Tile")) {
					int i = Array.IndexOf(lines, line);
					int j = FindNextEmpty(i, lines);
					char[,] grid = new char[lines[l + 1].Length, j - i - 1];
					for(int q = 0; q < grid.GetLength(0); q++) {
						for(int w = 0; w < grid.GetLength(1); w++) {
							grid[w, q] = lines[i + q + 1][w];
						}
					}
					string nn = line.Substring(5, line.Length - 6);
					int id = int.Parse(nn);
					ImageTile tile = new ImageTile(grid, id);
					tileList.Add(tile);
				}
			}
			ImageTile starting = null;
			List<Tuple<ImageTile, int>> possibilities = FindPossibleCorners(tileList);
			starting = possibilities[0].Item1;

			int size = (int)Math.Sqrt(tileList.Count);
			ImageTile[,] fullImage = new ImageTile[size, size];

			List<Vector2Int> possibleDirs = new List<Vector2Int>();
			Vector2Int cell = new Vector2Int(0, 0);
			foreach(Vector2Int dir in dirs) {
				Vector2Int off = new Vector2Int(cell.x + dir.x, cell.y + dir.y);
				foreach(ImageTile t2 in tileList) {
					if(t2 == starting) continue;
					if(CheckPlacement(starting, t2, new Vector2Int(-dir.x, -dir.y))) {
						possibleDirs.Add(dir);
					}
				}
			}
			int ax = possibleDirs[0].x + possibleDirs[1].x;
			int ay = possibleDirs[0].y + possibleDirs[1].y;
			Vector2Int startPos = new Vector2Int((ax == 1 ? size - 1 : 0), (ay == 1 ? size - 1 : 0));
			fullImage[startPos.x, startPos.y] = starting;
			tileList.Remove(starting);
			while(tileList.Count > 0) {
				List<Tuple<Vector2Int, int>> expansions = FindOpenNeighbors(fullImage);
				bool changed = false;
				do {
					Tuple<Vector2Int, int> npos = expansions.OrderByDescending(a => a.Item2*1000-a.Item1.y-a.Item1.x).First();
					expansions.Remove(npos);
					if(TryToFill(fullImage, npos.Item1, tileList)) {
						changed = true;
						tileList.Remove(fullImage[npos.Item1.x, npos.Item1.y]);
						break;
					}
				} while(expansions.Count > 0);
				if(!changed) {
					return -1;
				}
			}
			// make the pattern look nice in code
			string pattern = @"
                  # 
#    ##    ##    ###
 #  #  #  #  #  #   ";
			//strip off the \r\n at the front...
			pattern = pattern.Substring(2,pattern.Length-2);
			int s = fullImage[0, 0].grid.GetLength(0);
			char[,] renderedImage = new char[fullImage.GetLength(0) * (s-2), fullImage.GetLength(1) * (s - 2)];
			for(int y= renderedImage.GetLength(0)-1; y>=0;y--) {
				for(int x = 0; x < renderedImage.GetLength(0); x++) {
					renderedImage[x, y] = GetCharFromFull(fullImage, x, y, s-2);
				}
			}
			string[] patterns = GeneratePatterns(pattern);
			int answer = SearchForSeaMonster(patterns, renderedImage);
			return answer;
		}

		private static int SearchForSeaMonster(string[] patterns, char[,] renderedImage) {
			int wavecount = 0;
			int monsters = 0;
			for(int x=0;x<renderedImage.GetLength(0);x++) {
				for(int y = 0; y < renderedImage.GetLength(0); y++) {
					if(renderedImage[x, y] == '#') wavecount++;
					foreach(string p in patterns) {
						if(CheckForSeaMonster(renderedImage, p, x, y)) {
							monsters++;
						}
					}
				}
			}
			// Because curious
			Console.WriteLine("There are " + monsters + " sea monsters!");
			// Original pattern has 15 # marks (counted by hand)
			return wavecount - (15 * monsters);
		}

		private static bool CheckForSeaMonster(char[,] renderedImage, string p, int x, int y) {
			string[] lines = p.Split('\n');
			int matchCount = 0;
			int matchTotal = 0;
			if(lines.Length + y > renderedImage.GetLength(1)) return false;
			if(lines[0].Length + x > renderedImage.GetLength(0)) return false;
			bool b = false;
			for(int oy=0;oy<lines.Length; oy++) {
				for(int ox = 0; ox < lines[0].Length; ox++) {
					if(lines[oy][ox] == ' ') {
						if(b) Console.Write(' ');
						continue;
					}
					else matchTotal++;
					if(lines[oy][ox] == renderedImage[x + ox, y + oy]) {
						matchCount++;
						if(b) Console.Write('O');
					}
					else {
						if(b) Console.Write('-');
					}
				}
				if(b) Console.Write('\n');
			}
			return matchCount == matchTotal;
		}

		private static string[] GeneratePatterns(string pattern) {
			pattern = pattern.Replace("\r", "");
			string[] pats = new string[8];
			StringBuilder sb = new StringBuilder();
			sb.Append(pattern);
			pats[0] = sb.ToString();
			string[] lines = pattern.Split('\n');
			Array.Reverse(lines);
			sb = new StringBuilder();
			foreach(string l in lines) {
				sb.Append(l);
				sb.Append("\n");
			}
			sb.Remove(sb.Length - 1, 1);
			pats[1] = sb.ToString();
			pats[2] = RotateString(pats[0], 1);
			pats[3] = RotateString(pats[1], 1);

			pats[4] = RotateString(pats[0], 2);
			pats[5] = RotateString(pats[1], 2);

			pats[6] = RotateString(pats[0], 3);
			pats[7] = RotateString(pats[1], 3);
			return pats;
		}

		private static string RotateString(string str, int times) {
			if(times == 0) return str;
			string[] lines = str.Split('\n');
			Array.Reverse(lines);
			StringBuilder newstring = new StringBuilder();
			for(int x = 0; x < lines[0].Length; x++) {
				for(int y=0; y< lines.Length; y++) {
					newstring.Append(lines[y][x]);
				}
				newstring.Append("\n");
			}
			newstring.Remove(newstring.Length - 1, 1);
			times--;
			return RotateString(newstring.ToString(), times);
		}

		private static char GetCharFromFull(ImageTile[,] fullImage, int x, int y, int size) {
			int partx = x / size;
			int party = y / size;
			int px = x % size;
			int py = y % size;
			return fullImage[partx, party].GetCharAt(px+1, py+1);
		}

		private static void PrintEdge(char[] v) {
			foreach(char c in v) {
				Console.Write(c);
			}
			Console.Write("\n");
		}

		private class Orrientation {
			public ImageTile.Rotation rot;
			public bool fl;
		}

		private static bool TryToFill(ImageTile[,] fullImage, Vector2Int pos, List<ImageTile> list) {
			int size = fullImage.GetLength(0);
			List<Tuple<ImageTile,Orrientation>> allowed = new List<Tuple<ImageTile, Orrientation>>();
			foreach(ImageTile img in list) {
				img.rotation = ImageTile.Rotation.NORTH;
				img.flipped = false;
				foreach(bool fl in flips) {
					foreach(ImageTile.Rotation rot in rots) {
						bool doesFit = true;
						img.rotation = rot;
						img.flipped = fl;
						Orrientation o = new Orrientation {
							rot = img.rotation,
							fl = img.flipped
						};
						foreach(Vector2Int offset in dirs) {
							if(pos.x + offset.x < 0 || pos.y + offset.y < 0) continue;
							if(pos.x + offset.x >= size || pos.y + offset.y >= size) continue;
							if(fullImage[pos.x + offset.x, pos.y + offset.y] == null) continue;
							ImageTile e = fullImage[pos.x + offset.x, pos.y + offset.y];
							doesFit &= CheckPlacementWithOrrientation(e, img, offset);
						}
						if(doesFit) {
							allowed.Add(new Tuple<ImageTile, Orrientation>(img,o));
						}
					}
				}
			}
			if(allowed.Count == 1) {
				Tuple<ImageTile, Orrientation> tt = allowed[0];
				tt.Item1.rotation = tt.Item2.rot;
				tt.Item1.flipped = tt.Item2.fl;
				fullImage[pos.x, pos.y] = tt.Item1;
				return true;
			}
			return false;
		}

		private static List<Tuple<Vector2Int, int>> FindOpenNeighbors(ImageTile[,] fullImage) {
			List<Tuple<Vector2Int, int>> options = new List<Tuple<Vector2Int, int>>();
			int size = fullImage.GetLength(0);
			for(int x=0; x < size; x++) {
				for(int y = 0; y < size; y++) {
					if(fullImage[x,y] == null) {
						int v = CountFilledNeighbors(fullImage, x, y);
						if(v > 0 && v < 4) {
							options.Add(new Tuple<Vector2Int, int>(new Vector2Int(x,y), v));
						}
					}
				}
			}
			return options;
		}

		private static int CountFilledNeighbors(ImageTile[,] fullImage, int x, int y) {
			int size = fullImage.GetLength(0);
			int count = 0;
			for(int ox=-1; ox<=1; ox++) {
				for(int oy = -1; oy <= 1; oy++) {
					if(ox == 0 && oy == 0) continue;
					if(ox != 0 && oy != 0) continue;
					if(x+ox < 0 || y+oy < 0) continue;
					if(x + ox >= size || y + oy >= size) continue;
					if(fullImage[x+ox,y+oy] != null) {
						count++;
					}
				}
			}
			return count;
		}
	}
}
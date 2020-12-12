using System;

namespace Advent_of_Code_2020 {
	internal class DayTwelve {
		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			Ship s = new Ship();
			foreach(string lin in lines) {
				char op = lin[0];
				int q = int.Parse(lin.Substring(1, lin.Length - 1));

				switch(op) {
					case 'N':
						s.posY += q;
						break;
					case 'S':
						s.posY -= q;
						break;
					case 'E':
						s.posX += q;
						break;
					case 'W':
						s.posX -= q;
						break;
					case 'L':
						s.facing += q;
						break;
					case 'R':
						s.facing -= q;
						break;
					case 'F':
						s.Forward(q);
						break;
					default:
						Console.WriteLine("Invalid " + op);
						break;
				}
			}

			return Math.Abs(s.posX) + Math.Abs(s.posY);
		}

		internal static int Part2(string input) {
			string[] lines = input.Split('\n');
			ShipAndWaypoint s = new ShipAndWaypoint();
			foreach(string lin in lines) {
				char op = lin[0];
				int q = int.Parse(lin.Substring(1, lin.Length - 1));

				switch(op) {
					case 'N':
						s.North(q);
						break;
					case 'S':
						s.South(q);
						break;
					case 'E':
						s.East(q);
						break;
					case 'W':
						s.West(q);
						break;
					case 'L':
						s.RotL(q);
						break;
					case 'R':
						s.RotR(q);
						break;
					case 'F':
						s.Forward(q);
						break;
					default:
						Console.WriteLine("Invalid " + op);
						break;
				}
			}

			return Math.Abs(s.posX) + Math.Abs(s.posY);
		}

		public class Ship {
			public int facing = 0; // due east
			public int posX = 0;
			public int posY = 0;

			public void Forward(int v) {
				int dy = (int)Math.Sin(Angle(facing));
				int dx = (int)Math.Cos(Angle(facing));
				posX += dx * v;
				posY += dy * v;
			}

			private double Angle(int f) {
				return f * Math.PI / 180;
			}
		}


		public class ShipAndWaypoint {
			public int posX = 0;
			public int posY = 0;

			public int wposX = 10;
			public int wposY = 1;

			public void Forward(int v) {
				posX += wposX * v;
				posY += wposY * v;
			}

			public void North(int dist) {
				wposY += dist;
			}

			public void South(int dist) {
				wposY -= dist;
			}

			public void East(int dist) {
				wposX += dist;
			}

			public void West(int dist) {
				wposX -= dist;
			}

			// the trig here is overkill, but whatever
			public void RotL(int dist) {
				int sn = (int)Math.Sin(Angle(dist));
				int cs = (int)Math.Cos(Angle(dist));
				int wx = wposX;
				int wy = wposY;
				wposX = wx * cs - wy * sn;
				wposY = wy * cs + wx * sn;
			}

			public void RotR(int dist) {
				int sn = (int)Math.Sin(Angle(-dist));
				int cs = (int)Math.Cos(Angle(-dist));
				int wx = wposX;
				int wy = wposY;
				wposX = wx * cs - wy * sn;
				wposY = wy * cs + wx * sn;
			}

			private double Angle(int f) {
				return f * Math.PI / 180;
			}
		}
	}
}
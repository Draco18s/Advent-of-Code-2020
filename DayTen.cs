using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DayTen {
		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			List<int> lineInts = new List<int>();
			lineInts.Add(0);
			foreach(string line in lines) {
				int.TryParse(line, out int v);
				lineInts.Add(v);
			}
			lineInts.Sort();
			lineInts.Add(lineInts[lineInts.Count - 1] + 3);
			int diff = 0;
			int last = -1;
			int diffOne = 0;
			int diffThree = 0;
			foreach(int j in lineInts) {
				if(last < 0) {
					last = j;
				}
				else {
					diff = j - last;
					last = j;
					if(diff == 1) diffOne++;
					if(diff == 3) diffThree++;
				}
			}
			return diffOne * diffThree;
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			List<int> lineInts = new List<int>();
			foreach(string line in lines) {
				int.TryParse(line, out int v);
				lineInts.Add(v);
			}
			lineInts.Sort();
			int maxJ = lineInts[lineInts.Count - 1] + 3;
			Connector c = new Connector(0);
			foreach(int j in lineInts) {
				Connector cc = null;
				// kind of a hack, but this lets us skip to *somewhere* towards the end
				if(j > 10) {
					allConnectors.TryGetValue(j - 4, out cc);
					if(cc == null) {
						allConnectors.TryGetValue(j - 5, out cc);
					}
					if(cc == null) {
						allConnectors.TryGetValue(j - 6, out cc);
					}
					if(cc == null) {
						allConnectors.TryGetValue(j - 7, out cc);
					}
				}
				if(cc == null) {
					cc = c; // if all else fails, start at the beginning.
				}
				cc.TryToAdd(j);
			}
			return c.GetCount(maxJ);
		}
		protected static Dictionary<int, Connector> allConnectors = new Dictionary<int, Connector>();
		protected class Connector {
			readonly int val;
			long beenHere = 0;
			List<Connector> validNext = new List<Connector>();

			public Connector(int c) {
				val = c;
			}

			public void TryToAdd(int v) {
				if(v > val && v <= val + 3) {
					Connector c;
					if(allConnectors.ContainsKey(v)) {
						c = allConnectors[v];
					}
					else {
						c = new Connector(v);
						allConnectors.Add(v, c);
					}
					if(!validNext.Contains(c))
						validNext.Add(c);
				}
				foreach(Connector cc in validNext) {
					cc.TryToAdd(v);
				}
			}

			public long GetCount(int maxJolts) {
				long r = 0;
				if(val + 3 >= maxJolts) {
					return 1;
				}
				// we've been to this node, we already know the sum of all further nodes
				// this optimization took an embarrassingly long time to work out
				if(beenHere > 0) return beenHere;

				foreach(Connector cc in validNext) {
					r += (cc.GetCount(maxJolts));
				}
				beenHere = r; // store this value!!!
				return r;
			}
		}
	}
}
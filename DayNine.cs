using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DayNine {
		internal static int Part1(string input) {
			Queue<int> numQueue = new Queue<int>();
			string[] lines = input.Split('\n');
			foreach(string line in lines) {
				int v;
				if(numQueue.Count < 25) {
					int.TryParse(line, out v);
					numQueue.Enqueue(v);
				}
				else {
					int.TryParse(line, out v);
					for(int x=0; x < 25; x++) {
						for(int y = x; y < 25; y++) {
							int xx = numQueue.ElementAt(x);
							int yy = numQueue.ElementAt(y);
							if(xx + yy == v) {
								numQueue.Dequeue();
								numQueue.Enqueue(v);
								goto pairFound; // hey look! a goto!
							}
						}
					}
					return v;
				}
			pairFound:
				;
			}
			return -1;
		}

		internal static int Part2(string input) {
			int key = Part1(input);
			string[] lines = input.Split('\n');
			// no, this isn't the smartest way to do it
			// dropping the whole sum rather than accordianing
			// from the front is going to be slower.
			// but on ~500 values, who cares?
			for(int x=0; x < lines.Length; x++) {
				int total = 0;
				int v;
				for(int y = x; y < lines.Length; y++) {
					int.TryParse(lines[y], out v);
					total += v;
					if(total == key) {
						int min = key;
						int max = 0;
						for(int z = x; z < y; z++) {
							int.TryParse(lines[z], out v);
							if(v < min) min = v;
							if(v > max) max = v;
						}
						return min + max;
					}
					else if(total > key) {
						break;
					}
				}
			}
			return -1;
		}
	}
}
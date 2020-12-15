using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DayFifteen {
		internal static long Part1(string input) {
			string[] lines = input.Split(',');
			List<int> turns = new List<int>();
			foreach(string l in lines) {
				int v = int.Parse(l);
				turns.Add(v);
			}
			while(true) {
				int last = turns[turns.Count - 1]; // shove into list
				int next = GetDelay(turns, last); // get how long it has been
				turns.Add(next);
				if(turns.Count == 2020) {
					return next;
				}
			}
		}

		private static int GetDelay(List<int> turns, int last) {
			// loop from end towards beginning (ignoring last index or we will always get 1)
			for(int i = 1; i < turns.Count; i++) {
				int pp = turns[turns.Count - 1 - i];
				if(pp == last) {
					return i; // return how many values we had to look at
				}
			}
			return 0; // value is unique!
		}

		internal static long Part2(string input) {
			// we don't need the whole fekking list. We just need to know what turn
			// the value was last seen
			Dictionary<int, int> dict = new Dictionary<int, int>();
			string[] lines = input.Split(',');
			int turn = 0;
			foreach(string l in lines) {
				int v = int.Parse(l);
				dict.Add(v, turn);
				turn++;
			}
			// input values are guaranteed unique, so the most recent number is always
			// the first time it was said, so the first turn after input is always 0.
			int next = 0;
			while(true) {
				int last = next; // the "next" number becomes the "last" number

				// compute the new "next" value
				if(dict.ContainsKey(last)) {
					next = turn - dict[last];
				}
				else {
					next = 0;
				}

				// update the turn index for the "last" value
				if(dict.ContainsKey(last)) {
					dict[last] = turn;
				}
				else {
					dict.Add(last, turn);
				}
				turn++; // incrememnt turn
				if(turn == 30000000) { // 30 million
					return last; // we need the "last" number spoken
				}
			}
		}
	}
}
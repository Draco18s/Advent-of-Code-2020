using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020 {
	internal class DayTwentyThree {
		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			char[] nums = input.ToCharArray();
			List<int> cups = new List<int>();
			foreach(char c in nums) {
				cups.Add(int.Parse(c.ToString()));
			}

			int currentIndex = 0;
			int moves = 100;
			do {
				int cupVal = cups[currentIndex];
				List<int> ext = ExtractThreeCups(ref cups, currentIndex);
				int dest = GetDestination(cups, cupVal);
				InsertThreeCups(ref cups, ext, dest);
				currentIndex = cups.IndexOf(cupVal);
				currentIndex = (currentIndex + 1) % cups.Count;
				moves--;
			} while(moves>0) ;
			string output = "";
			int start = cups.IndexOf(1);
			for(int x=1;x<cups.Count;x++) {
				output += cups[(x + start) % cups.Count];
			}
			Console.WriteLine(output);
			return -1;
		}

		private static void InsertThreeCups(ref List<int> cups, List<int> ext, int dest) {
			int i = cups.IndexOf(dest);
			cups.InsertRange(i+1, ext);
		}

		private static int GetDestination(List<int> cups, int cupVal) {
			do {
				cupVal--;
				if(cupVal < 1) {
					cupVal = cups.Max();
				}
			} while(!cups.Contains(cupVal));
			return cupVal;
		}

		private static List<int> ExtractThreeCups(ref List<int> cups, int currentIndex) {
			List<int> ret = new List<int>();
			for(int x = 0; x<3; x++) {
				int i = (currentIndex + 1 + x) % cups.Count;
				ret.Add(cups[i]);
			}
			foreach(int i in ret) {
				cups.Remove(i);
			}
			return ret;
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			char[] nums = input.ToCharArray();

			// Linked list of cups
			Cup prev = null;
			Cup veryFirst = null;

			// Quick lookup table lable->Cup
			Dictionary<int, Cup> indexor = new Dictionary<int, Cup>();

			foreach(char c in nums) {
				Cup cup = new Cup(int.Parse(c.ToString()));
				if(veryFirst == null) {
					prev = cup;
					veryFirst = cup;
				}
				else {
					prev.next = cup;
					prev = cup;
				}
				indexor.Add(cup.label, cup);
			}
			int maxCup = 1000000;
			for(int x = 10; x <= maxCup; x++) {
				Cup cup = new Cup(x);
				prev.next = cup;
				prev = cup;
				indexor.Add(cup.label, cup);
			}
			prev.next = veryFirst;
			Cup pointer = veryFirst;
			int moves = 10000000;
			do {
				int cupVal = pointer.label;
				Cup rcup = ExtractThreeCups(pointer);
				InsertThreeCups(indexor[GetDestination(cupVal-1, rcup, maxCup)], rcup);
				pointer = pointer.next;
				moves--;
			} while(moves > 0);

			Cup scup = indexor[1].next;
			return (long)scup.label * scup.next.label;
		}

		private static int GetDestination(int desired, Cup extracted, int maxVal) {
			if(desired < 1)
				desired = maxVal;
			while(desired == extracted.label || desired == extracted.next.label || desired == extracted.next.next.label) {
				desired--;
				if(desired < 1)
					desired = maxVal;
			}
			return desired;
		}

		private static void InsertThreeCups(Cup after, Cup rcup) {
			Cup temp = after.next;
			after.next = rcup;
			rcup.next.next.next = temp;
		}

		private static Cup ExtractThreeCups(Cup pointer) {
			Cup rcup = pointer.next;
			pointer.next = rcup.next.next.next;
			rcup.next.next.next = null;
			return rcup;
		}

		public class Cup {
			public Cup next;
			public int label;

			public Cup(int v) {
				label = v;
			}

			public override string ToString() {
				return label + " > " + (next == null ? -1 : next.label) + " >> " + (next.next == null ? -1 : next.next.label);
			}
		}
	}
}
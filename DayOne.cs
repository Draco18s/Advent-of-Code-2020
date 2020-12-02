using System;

namespace Advent_of_Code_2020 {
	internal class DayOne {
		internal static int Part1(string input) {
			string[] p = input.Split(' ');
			int[] ints = new int[p.Length];
			for(int i = 0; i < p.Length; i++) {
				ints[i] = int.Parse(p[i]);
			}
			for(int i = 0; i < p.Length; i++) {
				for(int j = 0; j < p.Length; j++) {
					if(ints[i] + ints[j] == 2020) {
						return ints[i] * ints[j];
					}
				}
			}
			return -1;
		}

		internal static int Part2(string input) {
			string[] p = input.Split(' ');
			int[] ints = new int[p.Length];
			for(int i = 0; i < p.Length; i++) {
				ints[i] = int.Parse(p[i]);
			}
			for(int i = 0; i < p.Length; i++) {
				for(int j = 0; j < p.Length; j++) {
					for(int k = 0; k < p.Length; k++) {
						if(ints[i] + ints[j] + ints[k] == 2020) {
							return ints[i] * ints[j] * ints[k];
						}
					}
				}
			}
			return -1;
		}
	}
}
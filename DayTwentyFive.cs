using System;

namespace Advent_of_Code_2020 {
	internal class DayTwentyFive {
		public static long Transform(long subjectNum, int loopSize) {
			long value = 1;
			for(; loopSize > 0; loopSize--) {
				value *= subjectNum;
				value = value % 20201227;
			}
			return value;
		}

		public static long Transform2(long value, long subjectNum) {
			value *= subjectNum;
			value = value % 20201227;
			return value;
		}

		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			long cardInput = long.Parse(lines[0]);
			long doorInput = long.Parse(lines[1]);

			int testLoop = 102710;
			int cardSecret = -1;
			int doorSecret = -1;
			long value = Transform(7, testLoop);
			do {
				testLoop++;
				long cardPub = Transform2(value, 7);
				value = cardPub;
				if(cardPub == cardInput) {
					cardSecret = testLoop;
				}
				if(cardPub == doorInput) {
					doorSecret = testLoop;
				}
			} while(cardSecret < 0 || doorSecret < 0);
			return Transform(cardInput, doorSecret);
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');

			return -1;
		}
	}
}
using System;

namespace Advent_of_Code_2020 {
	internal class DayFive {
		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			int maxRow = 0;
			foreach(string line in lines) {
				int row = getRow(line);
				int collumn = getCollumn(line);
				int rowID = row * 8 + collumn;
				if(rowID > maxRow) {
					maxRow = rowID;
				}
			}
			return maxRow;
		}

		private static int getRow(string line) {
			int minRange = 0;
			int maxRange = 127;
			int changeAmt = 64;
			for(int x = 0; x < 6; x++) {
				if(line[x] == 'F') {
					maxRange -= changeAmt;
				}
				else {
					minRange += changeAmt;
				}
				changeAmt = changeAmt >> 1;
			}
			if(line[6] == 'F') { //this is stupid and I hate it
				return minRange;
			}
			else {
				return maxRange;
			}
		}

		private static int getCollumn(string line) {
			int minRange = 0;
			int maxRange = 7;
			int changeAmt = 4;
			for(int x = 7; x < 10; x++) {
				if(line[x] == 'L') {
					maxRange -= changeAmt;
				}
				else {
					minRange += changeAmt;
				}
				changeAmt = changeAmt >> 1;
			}
			if(line[9] == 'L') {
				return minRange;
			}
			else {
				return maxRange;
			}
		}

		internal static int Part2(string input) {
			string[] lines = input.Split('\n');
			bool[] takenSeats = new bool[1024];
			for(int i=0;i<1024;i++) {
				takenSeats[i] = false;
			}
			int minID = 1024;
			foreach(string line in lines) {
				int row = getRow(line);
				int collumn = getCollumn(line);
				int rowID = row * 8 + collumn;
				takenSeats[rowID] = true;
				if(rowID < minID) {
					minID = rowID;
				}
			}
			for(int i = minID; i < 1024; i++) {
				if(takenSeats[i] == false) {
					return i;
				}
			}
			return -1;
		}
	}
}
using System.Collections.Generic;

namespace Advent_of_Code_2020 {
	internal class DayEight {
		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			ExecutionContext e = new ExecutionContext(lines); // make an execution context to handle the VM
			int ac;
			List<int> visted = new List<int>();
			do {
				int p = e.Run(out ac); // while true: tick VM
				if(visted.Contains(p)) { // track pointer location
					return ac; // if we infinite loop, print accumulator
				}
				visted.Add(p);
			} while(true);
		}

		internal static int Part2(string input) {
			string[] lines;
			int lastInstChanged = 0;
			do {
				lines = input.Split('\n');
				int ind = lastInstChanged + 1; // pick up where the last change left off
				while(true) {
					string[] inst2 = lines[ind].Split(' ');
					if(inst2[0] != "acc") {
						if(inst2[0] == "nop") { // swap jmp/nop
							inst2[0] = "jmp";
						}
						else {
							inst2[0] = "nop";
						}
						lines[ind] = inst2[0] + " " + inst2[1]; // recombine
						lastInstChanged = ind; // save where we were
						break;
					}
					ind++;
				}
				ExecutionContext e = new ExecutionContext(lines);
				int ac;
				List<int> visited = new List<int>();
				do {
					int p = e.Run(out ac);
					if(p >= lines.Length) { // if we reach the end of the program: winner winner chicken dinner
						return ac;
					}
					if(visited.Contains(p)) { // if we infinite loop, break out
						break;
					}
					visited.Add(p);
				} while(true);
				// change failed

			} while(lastInstChanged < lines.Length); // if we somehow try every line, something went wrong
			return -1;
		}
	}
}
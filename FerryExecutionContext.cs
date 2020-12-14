using System;
using System.Collections.Generic;

namespace Advent_of_Code_2020 {
	internal class FerryExecutionContext {
		private string[] lines;
		private Dictionary<long, long> memory;
		private string mask;
		private int index;

		public FerryExecutionContext(string[] lines) {
			memory = new Dictionary<long, long>();
			this.lines = lines;
			index = 0;
		}

		public bool TickPart1() {
			string com = lines[index];
			string[] parts = com.Split('=');
			if(parts[0] == "mask ") {
				mask = parts[1].Substring(1, parts[1].Length - 1);
			}
			else {
				if(!long.TryParse(parts[0].Substring(4, parts[0].Length - 6), out long addr)) {
					Console.WriteLine("Error! " + com);
					return false;
				}
				if(!long.TryParse(parts[1].Substring(1, parts[1].Length - 1), out long val)) {
					Console.WriteLine("Error! " + com);
					return false;
				}
				val = ApplyMaskPart1(val, mask);
				if(memory.ContainsKey(addr)) {
					memory[addr] = val;
				}
				else {
					memory.Add(addr, val);
				}
			}

			index++;
			return index < lines.Length;
		}

		private static long ApplyMaskPart1(long val, string mask) {
			// This doesn't work and I don't know why
			/*for(int i=0;i<mask.Length;i++) {
				int j = 1 << i;
				int m = (mask.Length - i)-1;
				char ms = mask[m];
				switch(ms) {
					case '0':
						if((val & j) > 0) {
							val -= j;
						}
						break;
					case '1':
						if((val & j) == 0) {
							val += j;
						}
						break;
					default:
						break;
				}
			}*/
			string v = Convert.ToString(val, 2);
			v = v.PadLeft(mask.Length, '0');
			char[] vv = v.ToCharArray();
			for(int i = 0; i < mask.Length; i++) {
				if(mask[i] != 'X') {
					vv[i] = mask[i];
				}
			}
			v = new string(vv);
			val = Convert.ToInt64(v, 2);
			return val;
		}

		public long GetMemorySum() {
			long total = 0;
			foreach(long val in memory.Values) {
				total += val;
			}
			return total;
		}

		public bool TickPart2() {
			string com = lines[index];

			string[] parts = com.Split('=');
			if(parts[0] == "mask ") {
				mask = parts[1].Substring(1, parts[1].Length - 1);
			}
			else {
				if(!long.TryParse(parts[0].Substring(4, parts[0].Length - 6), out long addr)) {
					Console.WriteLine("Error! " + com);
					return false;
				}
				if(!long.TryParse(parts[1].Substring(1, parts[1].Length - 1), out long val)) {
					Console.WriteLine("Error! " + com);
					return false;
				}
				ApplyMaskPart2(memory, addr, val, mask);
			}

			index++;
			return index < lines.Length;
		}

		private static void ApplyMaskPart2(Dictionary<long,long> mem, long addr, long val, string mask) {
			Queue<string> toParse = new Queue<string>();
			List<string> memAddr = new List<string>();
			string adr = Convert.ToString(addr, 2);
			adr = adr.PadLeft(mask.Length, '0');
			char[] vv = adr.ToCharArray();
			for(int i = 0; i < mask.Length; i++) {
				if(mask[i] == '0') {
					//nothing
				}
				if(mask[i] == '1') {
					vv[i] = '1';
				}
				if(mask[i] == 'X') {
					vv[i] = 'X';
				}
			}
			adr = new string(vv);
			toParse.Enqueue(adr); // Queue 'em up
			while(toParse.Count > 0) {
				string a = toParse.Dequeue();
				char[] aa = a.ToCharArray();
				for(int i = 0; i < aa.Length; i++) {
					if(aa[i] == 'X') {
						aa[i] = '1';
						string one = new string(aa);
						aa[i] = '0';
						string two = new string(aa);
						if(Array.IndexOf(aa,'X') >= 0) {
							toParse.Enqueue(one);
							toParse.Enqueue(two);
							break;
						}
						else {
							memAddr.Add(one);
							memAddr.Add(two);
							break;
						}
					}
				}
			}
			foreach(string a in memAddr) {
				long ada = Convert.ToInt64(a, 2);
					if(mem.ContainsKey(ada)) {
					mem[ada] = val;
				}
				else {
					mem.Add(ada, val);
				}
			}
		}
	}
}
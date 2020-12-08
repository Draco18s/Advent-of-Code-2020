namespace Advent_of_Code_2020 {
	internal class ExecutionContext {
		private string[] lines;
		private int pointer;
		private int accumulator;

		public ExecutionContext(string[] lines) {
			this.lines = lines;
			accumulator = 0;
			pointer = 0;
		}

		public int Run(out int acc) {
			string inst = lines[pointer]; // get instruction at pointer
			string[] inst2 = inst.Split(' ');
			if(inst2[0] == "nop") {
				pointer++; // next
			}
			if(inst2[0] == "acc") {
				int v;
				int.TryParse(inst2[1], out v);
				accumulator += v; // increment
				pointer++; // next
			}
			if(inst2[0] == "jmp") {
				int v;
				int.TryParse(inst2[1], out v);
				pointer += v; // jump to new instruction
			}
			acc = accumulator; // return accumulator
			return pointer; // return pointer
		}
	}
}
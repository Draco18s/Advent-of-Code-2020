namespace Advent_of_Code_2020 {
	internal class DayFourteen {
		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			FerryExecutionContext context = new FerryExecutionContext(lines);
			bool r;
			do {
				r = context.TickPart1();
			} while(r);
			return context.GetMemorySum();
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			FerryExecutionContext context = new FerryExecutionContext(lines);
			bool r;
			do {
				r = context.TickPart2();
			} while(r);
			return context.GetMemorySum();
		}
	}
}
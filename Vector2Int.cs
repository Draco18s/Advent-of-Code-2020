namespace Advent_of_Code_2020 {
	internal struct Vector2Int {
		public int x;
		public int y;

		public Vector2Int(int _x, int _y) {
			x = _x;
			y = _y;
		}

		public override string ToString() {
			return "(" + x + "," + y + ")";
		}
	}
}
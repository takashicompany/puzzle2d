namespace TakashiCompany.Game.Sample.Threes
{

	using p2 = TakashiCompany.Game.Puzzle2d;

	public class Threes : p2.Logic<Cell, CellContent>
	{
		public bool isGameOver { get; private set; }

		public Threes() : base(4, 4, true)
		{

		}

		protected override void Move(p2.Direction dir)
		{
			if (isGameOver)
			{
				return;
			}

			base.Move(dir);

			if (IsAllCellFilled())
			{
				GameOver();
				return;
			}
			
			int x = -1;
			int y = -1;

			switch (dir)
			{
				case p2.Direction.Down:
					y = height - 1;
				break;

				case p2.Direction.Up:
					y = 0;
				break;

				case p2.Direction.Left:
					x = width - 1;
				break;

				case p2.Direction.Right:
					x = 0;
				break;
			}
			
			var random = new System.Random();

			bool findEmpty = false;

			if (x == -1)
			{
				for (int myX = 0; myX < width; myX++)
				{
					var cell = _cells[myX, y];

					if (cell.IsEmpty())
					{
						findEmpty = true;
						break;
					}
				}
			}

			if (y == -1)
			{
				for (int myY = 0; myY < height; myY++)
				{
					var cell = _cells[x, myY];

					if (cell.IsEmpty())
					{
						findEmpty = true;
						break;
					}
				}
			}

			if (!findEmpty)
			{
				GameOver();
				return;
			}

			while (x == -1)
			{
				var myX = random.Next(0, width);
				var cell = _cells[myX, y];
				if (cell.IsEmpty())
				{
					x = myX;
				}
			}

			while (y == -1)
			{
				var myY = random.Next(0, height);
				var cell = _cells[x, myY];
				if (cell.IsEmpty())
				{
					y = myY;
				}
			}

			var rate = random.Next(0, 4);

			int number = 1;

			for (int i = 0; i < rate; i++)
			{
				number = number + number;
			}

			var content = new CellContent(number);

			_cells[x, y].SetContent(content);
		}

		private void GameOver()
		{
			UnityEngine.Debug.Log("GameOver");
			isGameOver = true;
		}
	}

	public class Cell : p2.Cell<CellContent>
	{

	}

	public class CellContent : p2.CellContent
	{
		public int number { get; private set; }

		public CellContent(int number) : base()
		{
			SetNumber(number);
		}

		public void SetNumber(int number)
		{
			this.number = number;
		}

		public override p2.CollisionResult OnCollision(p2.CellContent target)
		{
			return OnCollision((CellContent)target);
		}

		protected virtual p2.CollisionResult OnCollision(CellContent target)
		{
			if (this.number == target.number)
			{
				target.SetNumber(this.number + target.number);
				return p2.CollisionResult.Broken;
			}

			return p2.CollisionResult.Stoped;
		}

		public override string ToString()
		{
			return number.ToString();
		}
	}
}

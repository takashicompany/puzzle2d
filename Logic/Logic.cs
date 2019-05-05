namespace TakashiCompany.Game.Puzzle2d
{

	using System.Collections.Generic;

	public enum Direction
	{
		Down,
		Up,
		Left,
		Right
	}

	public enum CollisionResult
	{
		Stoped,
		Through,
		Broken
	}

	public interface ILogic
	{
		int width { get; }
		int height { get; }
		ICell GetCell(int x, int y);
	}

	public class Logic<T_Cell, T_CellContent> : ILogic where T_Cell : Cell<T_CellContent>, new() where T_CellContent : CellContent
	{
		protected T_Cell[,] _cells;

		public T_Cell[,] cells { get { return _cells; } } 

		private List<CellContent> _contents;

		public int width { get; private set; }

		public int height { get; private set; }

		public bool moveOne { get; private set; }

		public delegate void EachCellsDelegete(T_Cell cell, int x, int y);

		public Logic(int width, int height, bool moveOne)
		{
			this.width = width;
			this.height = height;
			this.moveOne = moveOne;

			_cells = new T_Cell[width, height];

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					var cell = new T_Cell();
					cell.Init(x, y, this);
					_cells[x, y] = cell;
				}
			}
		}

		public void AddContent(T_CellContent content, int x, int y)
		{
			_cells[x, y].SetContent(content);
		}

		public ICell GetCell(int x, int y)
		{
			return _cells[x, y];
		}

		public void EachCells(EachCellsDelegete function)
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					function(cells[x, y], x, y);
				}
			}
		}

		public void MoveDown()
		{
			Move(Direction.Down);
		}

		public void MoveUp()
		{
			Move(Direction.Up);
		}

		public void MoveLeft()
		{
			Move(Direction.Left);
		}

		public void MoveRight()
		{
			Move(Direction.Right);
		}

		public virtual void Move(Direction dir)
		{
			switch (dir)
			{
				case Direction.Down:
				case Direction.Up:

					for (int x = 0; x < width; x++)
					{
						var line = GetLine(x, dir);
						ProcessLine(line);
					}

				break;

				case Direction.Left:
				case Direction.Right:

					for (int y = 0; y < height; y++)
					{
						var line = GetLine(y, dir);
						ProcessLine(line);
					}

				break;
			}
		}

		private void ProcessLine(List<T_Cell> line)
		{
			for (int i = 0; i < line.Count; i++)
			{
				if (line[i].IsEmpty())
				{
					continue;
				}
				var target = line[i].content;
				target.OnStartTurn();

				if (!line[i].content.canMove)
				{
					target.OnCompleteTurn();
					continue;
				}
				
				for (int j = i; 0 <= j - 1; j--)
				{
					if (moveOne && j != i)
					{
						break;
					}

					var current = line[j];
					var next = line[j - 1];

					if (current.IsEmpty())
					{
						break;
					}

					if (next.IsEmpty())
					{
						next.SetContent(current.content);
						current.RemoveContent();
					}
					else
					{
						var result = current.content.OnCollision(next.content);

						if (result == CollisionResult.Stoped)
						{
							break;
						}
						else if (result == CollisionResult.Through)
						{
							next.SetContent(current.content);
							current.RemoveContent();
						}
						else if (result == CollisionResult.Broken)
						{
							current.RemoveContent();
							break;
						}
					}
				}

				target.OnCompleteTurn();
			}
		}

		private List<T_Cell> GetLine(int line, Direction dir)
		{
			var list = new List<T_Cell>();

			switch (dir)
			{
				case Direction.Down:
					
					for (int y = 0; y < height; y++)
					{
						list.Add(_cells[line, y]);
					}

				break;

				case Direction.Up:

					for (int y = height - 1; 0 <= y; y--)
					{
						list.Add(_cells[line, y]);
					}

				break;

				case Direction.Left:

					for (int x = 0; x < width; x++)
					{
						list.Add(_cells[x, line]);
					}

				break;

				case Direction.Right:

					for (int x = width - 1; 0 <= x; x--)
					{
						list.Add(_cells[x, line]);
					}

				break;
			}

			return list;
		}

		protected bool IsAllCellFilled()
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (_cells[x, y].IsEmpty())
					{
						return false;
					}
				}
			}

			return true;
		}
 	}
}

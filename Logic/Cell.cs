namespace TakashiCompany.Game.Puzzle2d
{
	public interface ICell
	{
		int x { get; }
		int y { get; }
		bool IsEmpty();
	}

	public class Cell<T> : ICell where T : CellContent
	{
		private ILogic _logic;

		public int x { get; private set; }

		public int y { get; private set; }

		public T content { get; private set; }

		public Cell()
		{

		}

		public void Init(int x, int y, ILogic logic)
		{
			_logic = logic;
			this.x = x;
			this.y = y;
		}

		public bool IsEmpty()
		{
			return content == null;
		}

		public void SetContent(T content)
		{
			this.content = content;
			this.content.OnCell(this);
		}

		public void RemoveContent()
		{
			content = null;
		}

		public override string ToString()
		{
			return content == null ? "" : content.ToString();
		}

		public Cell<T> GetAdjoin(Direction direction)
		{
			var myX = x;
			var myY = y;

			switch (direction)
			{
				case Direction.Up:	 	myY += 1; break;
				case Direction.Down:	myY -= 1; break;
				case Direction.Left:	myX -= 1; break;
				case Direction.Right:	myX += 1; break;
			}

			if (myX < 0 || _logic.width <= myX || myY < 0 || _logic.height <= myY)
			{
				return null;
			}

			return _logic.GetCell(myX, myY) as Cell<T>;
		}
	}
}
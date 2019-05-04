namespace TakashiCompany.Game.Puzzle2d
{

	public abstract class CellContent
	{
		public virtual bool canMove { get { return true; } }

		public virtual void OnStartTurn()
		{
			
		}

		public virtual void OnCell(ICell cell)
		{
			
		}
		
		public virtual CollisionResult OnCollision(CellContent target)
		{
			return CollisionResult.Stoped;
		}

		public virtual void OnCompleteTurn()
		{

		}

		public override string ToString()
		{
			return string.Format("[CellContent: canMove={0}]", canMove);
		}
	}
}

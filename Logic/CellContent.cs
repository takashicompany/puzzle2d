namespace TakashiCompany.Game.Puzzle2d
{

	public abstract class CellContent
	{
		public virtual bool isMove { get { return true; } }

		public virtual void OnCell(ICell cell)
		{
			
		}
		
		public virtual CollisionResult OnCollision(CellContent target)
		{
			return CollisionResult.Stoped;
		}

		public override string ToString()
		{
			return string.Format("[CellContent: isMove={0}]", isMove);
		}
	}
}

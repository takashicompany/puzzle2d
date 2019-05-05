namespace TakashiCompany.Game.Puzzle2d.Unity
{
	using UnityEngine;
	using UnityEngine.UI;

	[RequireComponent(typeof(GridLayoutGroup))]
	public class LogicView : MonoBehaviour
	{
		[SerializeField]
		private CellView _cellViewPrefab;

		private GridLayoutGroup _grid;

		private ILogic _logic;

		void Awake()
		{
		}

		public void SetLogic(ILogic logic)
		{
			_logic = logic;
			_grid = GetComponent<GridLayoutGroup>();
			_grid.constraintCount = _logic.width;

			for (int y = _logic.height - 1; y >= 0; y--)
			{
				for (int x = 0; x < _logic.width; x++)
				{
					var cell = _logic.GetCell(x, y);
					var cellView = Instantiate<CellView>(_cellViewPrefab, this.transform);
					cellView.Setup(cell);
				}
			}
		}
	}
}

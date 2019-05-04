namespace TakashiCompany.Game.Puzzle2d.Unity
{
	using UnityEngine;
	using UnityEngine.UI;

	[RequireComponent(typeof(CanvasGroup))]
	public class CellView : MonoBehaviour
	{
		public delegate void UpdateViewDelegate(CellView viwe, ICell cell);

		public static UpdateViewDelegate onUpdateView;

		[SerializeField]
		private Image _outLine;

		public Image outLine { get { return _outLine; } }

		[SerializeField]
		private Image _background;

		public Image background { get { return _background; } }

		[SerializeField]
		private Text _text;
	
		private ICell _cell;

		private CanvasGroup _canvasGroup;


		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		public void Setup(ICell cell)
		{
			_cell = cell;
		}

		void Update()
		{
			if (_cell == null)
			{
				_canvasGroup.alpha = 0f;
			}
			else
			{
				_background.enabled = !_cell.IsEmpty();
				_text.enabled = !_cell.IsEmpty();

				if (!_cell.IsEmpty())
				{
					_text.text = _cell.ToString();
				}

				if (onUpdateView != null)
				{
					onUpdateView(this, _cell);
				}
			}
		}
	}
}

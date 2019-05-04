namespace TakashiCompany.Game.Sample.Threes.Unity
{
	using UnityEngine;
	using TakashiCompany.Game.Puzzle2d.Unity;

	public class ThreesGame : MonoBehaviour
	{
		[SerializeField]
		private LogicView _logicView;

		private Threes _threes;

		void Start()
		{
			_threes = new Threes();
			_logicView.SetLogic(_threes);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				_threes.MoveDown();
			}

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				_threes.MoveUp();
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				_threes.MoveLeft();
			}

			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				_threes.MoveRight();
			}
		}
	}
}
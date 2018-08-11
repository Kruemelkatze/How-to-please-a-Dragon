using UnityEngine;

namespace Assets.Scripts.Shelf
{
	[CreateAssetMenu(fileName = "new shelf", menuName = "Shelf")]
	public class Shelf : ScriptableObject
	{

		public string ShelfName;

		public int TotalAmount;

		public int CurrentAmount;

		public Sprite artwork;
	
	}
}

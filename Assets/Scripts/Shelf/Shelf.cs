using UnityEngine;

namespace Assets.Scripts.Shelf
{
	[CreateAssetMenu(fileName = "new shelf", menuName = "Shelf")]
	public class Shelf : ScriptableObject
	{

		public string ShelfName;
		public int TotalAmount = 1;

		public Sprite Frame;
		public Sprite Filling;
		public Sprite Background;
		
		public void PrintInfo()
		{
			Debug.Log($"Shelfname: {ShelfName}, TotalAmount: ${TotalAmount}");
		}
		
	}
}

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Shelf;
using UnityEngine;
using UnityEngine.U2D;

public class ShelfDisplay : MonoBehaviour
{

	public Shelf Shelf;

	public SpriteRenderer Frame;
	public SpriteRenderer Filling;
	public SpriteRenderer Background;
	
	// Use this for initialization
	void Start () {
//		Shelf.PrintInfo();
		
		Frame.sprite = Shelf.Frame;
		Filling.sprite = Shelf.Filling;
		Background.sprite = Shelf.Background;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

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
	
	public int CurrentAmount;
		
	public float FillingPercentage => (float) CurrentAmount / Shelf.TotalAmount;
	
	public float MaxHeightOffset = 7.2F;
		
	public float SmoothTime = 0.3f;
	private Vector3 _moveTargetDefault;
	private Vector3 _velocity = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		Shelf.PrintInfo();
		
		Frame.sprite = Shelf.Frame;
		Filling.sprite = Shelf.Filling;
		Background.sprite = Shelf.Background;
		
		// set init pos
		var initPos = new Vector3(
			x: Filling.gameObject.transform.position.x,
			y: Filling.gameObject.transform.position.y - MaxHeightOffset,
			z: Filling.gameObject.transform.position.z
		);
		Filling.gameObject.transform.position = initPos;
		_moveTargetDefault = initPos;
	}
	
	private void UpdateFillingPoisition(bool instantly = false)
	{
		var newMoveTargetPos = new Vector3(
			x: Filling.gameObject.transform.position.x,
			y: _moveTargetDefault.y + FillingPercentage * MaxHeightOffset,
			z: Filling.gameObject.transform.position.z
		);

		if (instantly)
		{
			Filling.gameObject.transform.position = newMoveTargetPos;
		}
		else
		{
			Filling.gameObject.transform.position =
				Vector3.SmoothDamp(Filling.gameObject.transform.position, newMoveTargetPos, ref _velocity, SmoothTime);
		}
	}
	
	// Update is called once per frame
	void Update () {
//		Shelf.PrintInfo();
		UpdateFillingPoisition();
	}
}

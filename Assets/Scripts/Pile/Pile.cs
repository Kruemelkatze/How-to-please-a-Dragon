using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : SceneSingleton<Pile>
{

	public float Level { get; set; } = 0;
	public Transform MoveTarget { get; set; }

	public float MaxHeightOffset { get; set; } = 4;
	public float MinHeightOffset { get; set; } = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

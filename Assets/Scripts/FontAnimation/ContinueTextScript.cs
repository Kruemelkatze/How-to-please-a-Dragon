using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueTextScript : MonoBehaviour
{

	private TextMeshProUGUI _continueTextMeshPro;
	private bool _grow;
	
	// Use this for initialization
	void Start ()
	{
		_grow = true;
		_continueTextMeshPro = gameObject.GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_grow)
		{
			_continueTextMeshPro.fontSize += 0.25F;
			if (_continueTextMeshPro.fontSize > 70)
			{
				_grow = false;
			}
		}
		else
		{
			_continueTextMeshPro.fontSize -= 0.25F;
			if (_continueTextMeshPro.fontSize < 50)
			{
				_grow = true;
			}
		}
	}
}

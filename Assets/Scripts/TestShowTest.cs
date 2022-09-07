using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShowTest : MonoBehaviour
	
{

	public Canvas TextCanvas;
	// Start is called before the first frame update
	void Start()
    {
        
    }

void Awake()
{
	TextCanvas.enabled = false;

}
	public void TextOn()
	{
		TextCanvas.enabled = true;
	}

	// Update is called once per frame
	void Update()
	{
	

	}

}

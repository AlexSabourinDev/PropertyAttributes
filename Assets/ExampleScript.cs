using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TestEnum
{
	One,
	Two,
	Three
}

public class ExampleScript : MonoBehaviour
{
	[StringToEnum(typeof(TestEnum))]
	public string TestEnum;

	[ReadOnly]
	public float TestReadOnly;

	[HistoryGraph]
	public float TestHistory = 0.0f;

	private void Update()
	{
		TestHistory = Mathf.Sin(Time.time * 2.0f);
	}
}

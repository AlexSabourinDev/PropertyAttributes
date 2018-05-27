using System;
using UnityEngine;

public class StringToEnumAttribute : PropertyAttribute
{
	public Type EnumType;

	public StringToEnumAttribute(Type enumType)
	{
		EnumType = enumType;
	}
}

public class ReadOnlyAttribute : PropertyAttribute
{
}

public class HistoryGraphAttribute : PropertyAttribute
{
	public const int PointCount = 10;

	public Keyframe[] History = new Keyframe[PointCount];
}

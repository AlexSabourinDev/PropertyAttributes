using System;
using System.Linq;

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringToEnumAttribute))]
public class StringToEnumDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		StringToEnumAttribute stringToEnumAttrib = (StringToEnumAttribute)attribute;
		Debug.Assert(property.propertyType == SerializedPropertyType.String);

		string[] enumNames = Enum.GetNames(stringToEnumAttrib.EnumType);
		Array enumValues = Enum.GetValues(stringToEnumAttrib.EnumType);
		Debug.Assert(enumNames.Any());

		string currentPropertyValue = property.stringValue;
		Enum currentEnumValue = (Enum)enumValues.GetValue(0);

		int enumIndex = -1;
		for(int i = 0; i < enumNames.Length; i++)
		{
			if(currentPropertyValue == enumNames[i])
			{
				enumIndex = i;
				currentEnumValue = (Enum)enumValues.GetValue(i);
			}
		}

		if (enumIndex == -1)
		{
			currentEnumValue = (Enum)enumValues.GetValue(0);
		}

		property.stringValue = EditorGUI.EnumPopup(position, label, currentEnumValue).ToString();
	}
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}

[CustomPropertyDrawer(typeof(HistoryGraphAttribute))]
public class HistoryDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		const int pointCount = HistoryGraphAttribute.PointCount;
		HistoryGraphAttribute historyGraph = (HistoryGraphAttribute)attribute;

		Debug.Assert(property.propertyType == SerializedPropertyType.Float);

		for(int i = 0; i < pointCount - 1; i++)
		{
			historyGraph.History[i] = historyGraph.History[i + 1];
		}
		historyGraph.History[pointCount - 1] = new Keyframe(Time.time, property.floatValue, 1.0f, 1.0f);

		EditorGUI.CurveField(position, label, new AnimationCurve(historyGraph.History));
	}
}

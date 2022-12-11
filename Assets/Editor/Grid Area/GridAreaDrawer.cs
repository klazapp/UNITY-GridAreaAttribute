using System.Collections.Generic;
using editorAdditional;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

//Use [GridArea] to display grid area selection
[CustomPropertyDrawer(typeof(GridAreaAttribute))]
public class GridAreaDrawer : PropertyDrawer
{
	private Texture unitTex;
	private Texture enabledTex;
	private Texture disabledTex;
	private bool checkIfBoxIsEnabled;

	private const float PROPERTY_HEIGHT_EXPANDED = 1200F;
	private const float PROPERTY_HEIGHT_NONEXPANDED = 20F;
	private const float GRID_AREA_TABLE_BOX_FULL_SIZE = 30F;
	private const float GRID_AREA_TABLE_BOX_HALF_SIZE = 15F;
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		var showWindow = property.FindPropertyRelative("showWindow");
		return showWindow.boolValue ? PROPERTY_HEIGHT_EXPANDED : PROPERTY_HEIGHT_NONEXPANDED;
	}
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		//Load required textures if they are null
		unitTex ??= AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Grid Area/unit.png");
		enabledTex ??= AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Grid Area/enabledV2.png");
		disabledTex ??= AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Grid Area/disabledV2.png");
		
		//Retrieve required properties
		var showWindow = property.FindPropertyRelative("showWindow");
		var noOfColumns = property.FindPropertyRelative("noOfColumns");
		var noOfRows = property.FindPropertyRelative("noOfRows");
		var rowColumnStructList = property.FindPropertyRelative("rowColumnStructList");
		var area = property.FindPropertyRelative("area");
		
		var foldOutPosition = position;
		const float EXTRA_HEIGHT_PADDING = 10f;
		foldOutPosition.y = position.y - position.height / 2f + EXTRA_HEIGHT_PADDING;
		showWindow.boolValue = EditorGUI.Foldout(foldOutPosition, showWindow.boolValue, property.name, false);

		if (!showWindow.boolValue)
			return;
		
		//Draw title
		var titlePosition = position;
		titlePosition.y += 40f;
		EditorGUI.LabelField(titlePosition, "Size", CustomEditorStyling.titleStyle);
		
		//Draw rows
		var rowPosition = titlePosition;
		rowPosition.y += 40f;
		EditorGUI.LabelField(rowPosition, "Rows", CustomEditorStyling.labelStyle);

		var noOfRowsPosition = rowPosition;
		noOfRowsPosition.y += 30f;
		noOfRowsPosition.height = 20f;
		noOfRows.intValue = EditorGUI.IntSlider(noOfRowsPosition, noOfRows.intValue, 3, 21);
		if (noOfRows.intValue % 2 == 0)
		{
			noOfRows.intValue += 1;
		}
		
		//Draw columns
		var columnPosition = noOfRowsPosition;
		columnPosition.y += 40f;
		EditorGUI.LabelField(columnPosition, "Columns", CustomEditorStyling.labelStyle);
			
		var noOfColumnsPosition = columnPosition;
		noOfColumnsPosition.y += 30f;
		noOfColumnsPosition.height = 20f;
		noOfColumns.intValue = EditorGUI.IntSlider(noOfColumnsPosition, noOfColumns.intValue, 3, 21);
		if (noOfColumns.intValue % 2 == 0)
		{
			noOfColumns.intValue += 1;
		}
			
		//Change row values according to slider value
		var getNoOfRows = rowColumnStructList.arraySize;

		if (getNoOfRows < noOfRows.intValue)
		{
			rowColumnStructList.InsertArrayElementAtIndex(rowColumnStructList.arraySize);
		}
		else if (getNoOfRows > noOfRows.intValue)
		{
			rowColumnStructList.DeleteArrayElementAtIndex(rowColumnStructList.arraySize - 1);
		}
		
		//Change column values according to slider value
		if (rowColumnStructList.arraySize == noOfRows.intValue)
		{
			for (var i = 0; i < noOfRows.intValue; i++)
			{
				var columnList = rowColumnStructList.GetArrayElementAtIndex(i).FindPropertyRelative("columnList");
				var getNoOfColumns = columnList.arraySize;
			
				if (getNoOfColumns < noOfColumns.intValue)
				{
					columnList.InsertArrayElementAtIndex(columnList.arraySize);
				}
				else if (getNoOfColumns > noOfColumns.intValue)
				{
					columnList.DeleteArrayElementAtIndex(columnList.arraySize - 1);
				}
			}
		}
		
		//Draw grid are table
		var gridAreaRect = noOfColumnsPosition;
		gridAreaRect.y += 60f;
		gridAreaRect.height = 600f;
		var getUnitRowIndex = noOfRows.intValue / 2;
		var getUnitColumnIndex = noOfColumns.intValue / 2;

		const float OFFSET = 20f;
		const float PX = 20f;
			
		GUI.BeginGroup(new Rect(Screen.width * 0.5f - Screen.width / 2f, Screen.height * 0.5f - Screen.height / 6f, PROPERTY_HEIGHT_EXPANDED, PROPERTY_HEIGHT_EXPANDED));
		
		for (var i = 0; i < noOfRows.intValue; i++)
		{
			for (var j = 0; j < noOfColumns.intValue; j++)
			{
				if (i == getUnitRowIndex && j == getUnitColumnIndex)
				{
					GUI.Button(
						new Rect(j * GRID_AREA_TABLE_BOX_HALF_SIZE + PX + j * OFFSET,
							i * GRID_AREA_TABLE_BOX_HALF_SIZE + PX + i * OFFSET, 
							GRID_AREA_TABLE_BOX_FULL_SIZE,
							GRID_AREA_TABLE_BOX_FULL_SIZE), 
						unitTex);
				}
				else
				{
					if (rowColumnStructList is not null && i < rowColumnStructList.arraySize)
					{
						var columnList = rowColumnStructList.GetArrayElementAtIndex(i).FindPropertyRelative("columnList");
						if (columnList is not null && j < columnList.arraySize)
						{
							checkIfBoxIsEnabled = columnList.GetArrayElementAtIndex(j).boolValue;
						}
					}

					if (GUI.Button(new Rect(j * GRID_AREA_TABLE_BOX_HALF_SIZE + PX + j * OFFSET, i * GRID_AREA_TABLE_BOX_HALF_SIZE + PX + i * OFFSET, GRID_AREA_TABLE_BOX_FULL_SIZE, GRID_AREA_TABLE_BOX_FULL_SIZE), checkIfBoxIsEnabled ? enabledTex : disabledTex))
					{
						var columnList = rowColumnStructList.GetArrayElementAtIndex(i).FindPropertyRelative("columnList");

						if (columnList is null) 
							continue;
						
						checkIfBoxIsEnabled = columnList.GetArrayElementAtIndex(j).boolValue;
					
						checkIfBoxIsEnabled = !checkIfBoxIsEnabled;
					
						columnList.GetArrayElementAtIndex(j).boolValue = checkIfBoxIsEnabled;
					}
				}
			}
		}
		GUI.EndGroup();
			
		#region Convert rowColumnStructList to internal int2 List
		var tempList = new List<int2>();
		            
		for (var i = 0; i < noOfRows.intValue; i++)
		{
			for (var j = 0; j < noOfColumns.intValue; j++)
			{
				if (i == getUnitRowIndex && j == getUnitColumnIndex)
				{
					//Unit in middle
				}
				else
				{
					if (rowColumnStructList is not null && i < rowColumnStructList.arraySize)
					{
						var columnList = rowColumnStructList.GetArrayElementAtIndex(i).FindPropertyRelative("columnList");
						if (columnList is null || j >= columnList.arraySize) 
							continue;
						
						checkIfBoxIsEnabled = columnList.GetArrayElementAtIndex(j).boolValue;

						if (!checkIfBoxIsEnabled) 
							continue;
							
						var finalPatternVal = new int2(0)
						{
							y = getUnitRowIndex - i,
							x = j - getUnitColumnIndex
						};
			
						tempList.Add(finalPatternVal);
					}
				}
			}
		}

		if (area.arraySize < tempList.Count)
		{
			area.InsertArrayElementAtIndex(area.arraySize);
		}
		else if (area.arraySize > tempList.Count)
		{
			area.DeleteArrayElementAtIndex(area.arraySize - 1);
		}

		if (area.arraySize == tempList.Count)
		{
			for (var i = 0; i < area.arraySize; i++)
			{
				SerializedPropertyExtensions.SetInt2FromVector(area.GetArrayElementAtIndex(i), tempList[i]);
			}
		}
		#endregion
	}
}

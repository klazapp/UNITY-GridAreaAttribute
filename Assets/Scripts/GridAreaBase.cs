using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct GridAreaBase
{
    public List<int2> area;
    
#if UNITY_EDITOR
    #region _EDITOR
    public bool showWindow;
    [Range(3, 21)]
    public int noOfColumns;
    [Range(3, 21)]
    public int noOfRows;

    [Serializable]
    public struct RowColumnStruct
    {
        public List<bool> columnList;

        public RowColumnStruct(List<bool> boolList)
        {
            columnList = boolList;
        }
    }

    public List<RowColumnStruct> rowColumnStructList;
    #endregion
#endif
    
}

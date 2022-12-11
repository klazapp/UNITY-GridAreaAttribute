using Unity.Mathematics;
using UnityEditor;

namespace editorAdditional
{
    public static class SerializedPropertyExtensions 
    {
        public static int2 GetInt2AsVector(SerializedProperty property) 
        {
            int2 output;
            var p = property.Copy();
            p.Next(true);
            output.x = p.intValue;
            p.Next(true);
            output.y = p.intValue;
            return output;
        }

        public static void SetInt2FromVector(SerializedProperty property, int2 value)
        {        
            var p = property.Copy();
            p.Next(true);
            p.intValue = value.x;
            p.Next(true);
            p.intValue = value.y;        
        }
    }
}

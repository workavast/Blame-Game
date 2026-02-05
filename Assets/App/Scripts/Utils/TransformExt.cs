using UnityEngine;

namespace App.Utils
{
    public static class TransformExt
    {
        public static void DestroyChildren(this Transform transform)
        {
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++) 
                Object.Destroy(transform.GetChild(i).gameObject);
        }
        
        public static void DestroyChildren(this RectTransform transform)
        {
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
                Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
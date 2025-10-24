using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Avastrad.Extensions
{
    public static class ListExt
    {
        /// <returns>return true if new value added, else false</returns>
        public static bool TryAdd<T>(this List<T> list, T newValue)
        {
            if (list.Contains(newValue))
                return false;
            
            list.Add(newValue);
            return true;
        }
        
        /// <summary> remove value if list contains it </summary>
        /// <returns> return true if value removed, else false </returns>
        public static bool TryRemove<T>(this List<T> list, T value)
        {
            if (list.Contains(value))
                return false;

            list.Remove(value);
            return true;
        }
        
        public static int RandomIndex<T>(this List<T> list)
        {
            if (list == null)
                throw new NullReferenceException();
            if (list.Count <= 0)
                throw new IndexOutOfRangeException();
            
            var randomIndex = Random.Range(0, list.Count);
            return randomIndex;
        }
        
        public static T RandomValue<T>(this List<T> list)
        {
            var randomIndex = list.RandomIndex();
            return list[randomIndex];
        }

        public static bool IsEndIndex<T>(this List<T> list, int index) 
            => index >= list.Count - 1;

        public static bool Contains<T>(this IReadOnlyList<T> list, T value) 
            where T: class 
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == value)
                    return true;
            
            return false;
        }
    }
}
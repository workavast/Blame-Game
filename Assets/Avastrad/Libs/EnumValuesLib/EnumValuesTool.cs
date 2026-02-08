using System;
using System.Collections.Generic;
using System.Linq;

namespace Avastrad.Libs.EnumValuesLib
{
    public static class EnumValuesTool
    {
        /// <returns>
        /// return array of enum values
        /// </returns>
        public static IEnumerable<T> GetValues<T>() where T : Enum 
            => Enum.GetValues(typeof(T)).Cast<T>();
    }
}
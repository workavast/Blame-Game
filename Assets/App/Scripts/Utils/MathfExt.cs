using System;
using UnityEngine;

namespace App.Utils
{
    public static class MathfExt
    {
        public static int Repeat(int value, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than zero.");

            return (value % length + length) % length;
        }
    }
}
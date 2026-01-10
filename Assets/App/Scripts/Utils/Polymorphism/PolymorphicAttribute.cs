using System;
using UnityEngine;

namespace App.Utils.Polymorphism
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class PolymorphicAttribute : PropertyAttribute { }
}
using UnityEngine;

namespace Avastrad.ConfigsRepositories.HideIfOverride
{
    internal class HideIfOverrideAttribute : PropertyAttribute
    {
        internal string MethodName { get; }

        internal HideIfOverrideAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
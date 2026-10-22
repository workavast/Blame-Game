using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Avastrad.Editor.Toolbar
{
    public static class ElementStyler
    {
        public static void StyleElement<T>(string elementName, System.Action<T> styleAction) where T : VisualElement
        {
            EditorApplication.delayCall += () =>
            {
                ApplyStyle(elementName, (element) =>
                {
                    T targetElement = null;

                    if (element is T typedElement)
                        targetElement = typedElement;
                    else
                        targetElement = element.Query<T>().First();

                    if (targetElement != null)
                        styleAction(targetElement);
                });
            };
        }

        private static void ApplyStyle(string elementName, System.Action<VisualElement> styleCallback)
        {
            var element = FindElementByName(elementName);
            if (element != null)
                styleCallback(element);
        }

        private static VisualElement FindElementByName(string name)
        {
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach (var window in windows)
            {
                var root = window.rootVisualElement;
                if (root == null)
                    continue;

                VisualElement element;
                if ((element = root.FindElementByName(name)) != null)
                    return element;
                if ((element = root.FindElementByTooltip(name)) != null)
                    return element;
            }

            return null;
        }
        
        private static VisualElement FindElementByName(this VisualElement element, string name) 
            => element.FindElement(e => e.name == name);

        private static VisualElement FindElementByTooltip(this VisualElement element, string tooltip) 
            => element.FindElement(e => e.tooltip == tooltip);
        
        private static VisualElement FindElement(this VisualElement element, System.Func<VisualElement, bool> predicate) {
            if (predicate(element))
                return element;

            return element.Query<VisualElement>().Where(predicate).First();
        }
    }
}
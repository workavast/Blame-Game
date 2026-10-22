using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Avastrad.Editor.Toolbar
{
    public class ProjectSettingsBtn
    {
        [MainToolbarElement("Custom/Open Project Settings", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement ProjectSettingsButton()
        {
            var icon = EditorGUIUtility.IconContent("SettingsIcon").image as Texture2D;
            var content = new MainToolbarContent(icon);
            return new MainToolbarButton(content, () => { SettingsService.OpenProjectSettings(); });
        }
    }
}
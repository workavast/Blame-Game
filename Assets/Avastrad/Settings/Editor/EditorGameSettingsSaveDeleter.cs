using UnityEditor;

namespace Avastrad.Settings.Editor
{
    public static class EditorGameSettingsSaveDeleter
    {
        [MenuItem(Consts.AppName +"/Delete settings Save")]
        public static void Delete() => SettingsSaver.Delete();
    }
}
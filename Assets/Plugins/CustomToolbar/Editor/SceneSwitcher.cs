using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Avastrad.Editor.Toolbar
{
    public static class SceneSwitcher
    {
        private const string ToolbarElementName = "Custom/Scene Switcher";

        static SceneSwitcher()
        {
            EditorApplication.projectChanged += RefreshToolbar;
            EditorApplication.playModeStateChanged += _ => RefreshToolbar();
            EditorSceneManager.activeSceneChangedInEditMode += (_, __) => RefreshToolbar();
        }
        
        [MainToolbarElement(ToolbarElementName, defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement SceneSwitcherDropdown()
        {
            var icon = EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D;
            var activeSceneName = EditorSceneManager.GetActiveScene().name;
            if (string.IsNullOrEmpty(activeSceneName))
                activeSceneName = "Scene Switcher";

            var content = new MainToolbarContent(activeSceneName, icon, "Switch active scene");
            var dropdown = new MainToolbarDropdown(content, ShowSceneMenu);
            dropdown.enabled = !EditorApplication.isPlayingOrWillChangePlaymode;

            return dropdown;
        }

        private static void ShowSceneMenu(Rect dropDownRect)
        {
            var menu = new GenericMenu();
            var buildScenes = GetBuildScenes();
            var otherScenes = GetOtherScenes(buildScenes.Select(item => item.ScenePath).ToHashSet()).ToList();
            var sceneMenuLabels = GetSceneMenuLabels(
                buildScenes.Select(scene => scene.ScenePath).Concat(otherScenes));

            if (buildScenes.Count > 0)
            {
                foreach (var scene in buildScenes)
                    menu.AddItem(new GUIContent(sceneMenuLabels[scene.ScenePath]), false, () => OpenScene(scene.ScenePath));
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("No scenes in build settings"));
            }

            if (otherScenes.Count > 0)
            {
                menu.AddSeparator(string.Empty);
                foreach (var scenePath in otherScenes)
                    menu.AddItem(new GUIContent(sceneMenuLabels[scenePath]), false, () => OpenScene(scenePath));
            }

            menu.DropDown(dropDownRect);
        }

        private static IReadOnlyList<BuildSceneInfo> GetBuildScenes()
        {
            return EditorBuildSettings.scenes
                .Where(scene => scene != null && !string.IsNullOrEmpty(scene.path) && !IsSceneInReadOnlyPackage(scene.path))
                .Select((scene, index) => new BuildSceneInfo(index, scene.path))
                .OrderBy(scene => scene.BuildIndex)
                .ToArray();
        }

        private static IEnumerable<string> GetOtherScenes(HashSet<string> buildScenePaths)
        {
            var sceneGuids = AssetDatabase.FindAssets("t:Scene");
            var scenePaths = sceneGuids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => !string.IsNullOrEmpty(path)
                    && !buildScenePaths.Contains(path)
                    && !IsSceneInReadOnlyPackage(path))
                .OrderBy(path => path)
                .ToList();

            return scenePaths;
        }

        private static bool IsSceneInReadOnlyPackage(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
                return false;

            if (!scenePath.StartsWith("Packages/") && !scenePath.StartsWith("Library/PackageCache/"))
                return false;

            var packageInfo = PackageInfo.FindForAssetPath(scenePath);
            if (packageInfo == null)
                return true;

            switch (packageInfo.source)
            {
                case UnityEditor.PackageManager.PackageSource.Local:
                case UnityEditor.PackageManager.PackageSource.Embedded:
                    return false;
                case UnityEditor.PackageManager.PackageSource.BuiltIn:
                case UnityEditor.PackageManager.PackageSource.Registry:
                case UnityEditor.PackageManager.PackageSource.Git:
                case UnityEditor.PackageManager.PackageSource.Unknown:
                default:
                    return true;
            }
        }

        private static Dictionary<string, string> GetSceneMenuLabels(IEnumerable<string> scenePaths)
        {
            var paths = scenePaths.ToList();
            var labels = new Dictionary<string, string>();

            foreach (var group in paths.GroupBy(System.IO.Path.GetFileNameWithoutExtension))
            {
                var pathsWithSameName = group.ToList();
                if (pathsWithSameName.Count == 1)
                {
                    labels[pathsWithSameName[0]] = GetSceneName(pathsWithSameName[0]);
                    continue;
                }

                foreach (var scenePath in pathsWithSameName)
                    labels[scenePath] = GetShortestUniqueSceneLabel(scenePath, pathsWithSameName);
            }

            return labels;
        }

        private static string GetShortestUniqueSceneLabel(string scenePath, IReadOnlyList<string> pathsWithSameName)
        {
            var parentFoldersByPath = pathsWithSameName
                .Distinct()
                .ToDictionary(path => path, GetParentFolders);
            var allParentFolders = parentFoldersByPath.Values.ToList();
            var commonPrefixCount = GetCommonPrefixCount(allParentFolders);
            var commonSuffixCount = GetCommonSuffixCount(allParentFolders, commonPrefixCount);
            var distinctiveFoldersByPath = parentFoldersByPath.ToDictionary(
                item => item.Key,
                item => item.Value
                    .Skip(commonPrefixCount)
                    .Take(item.Value.Count - commonPrefixCount - commonSuffixCount)
                    .ToArray());

            var distinctiveFolders = distinctiveFoldersByPath[scenePath];
            for (var folderCount = 1; folderCount <= distinctiveFolders.Count(); folderCount++)
            {
                var folderSuffix = GetFolderSuffix(distinctiveFolders, folderCount);
                var isUnique = pathsWithSameName
                    .Where(path => path != scenePath)
                    .All(path => GetFolderSuffix(distinctiveFoldersByPath[path], folderCount) != folderSuffix);

                if (isUnique)
                    return $"{folderSuffix} -> {GetSceneName(scenePath)}";
            }

            return scenePath.Replace("/", " -> ");
        }

        private static IReadOnlyList<string> GetParentFolders(string scenePath)
        {
            var pathParts = scenePath.Split('/');
            return pathParts.Take(pathParts.Length - 1).ToArray();
        }

        private static int GetCommonPrefixCount(IReadOnlyList<IReadOnlyList<string>> folderPaths)
        {
            var maxPrefixCount = folderPaths.Min(path => path.Count);
            var prefixCount = 0;

            while (prefixCount < maxPrefixCount
                && folderPaths.All(path => path[prefixCount] == folderPaths[0][prefixCount]))
            {
                prefixCount++;
            }

            return prefixCount;
        }

        private static int GetCommonSuffixCount(
            IReadOnlyList<IReadOnlyList<string>> folderPaths,
            int commonPrefixCount)
        {
            var maxSuffixCount = folderPaths.Min(path => path.Count) - commonPrefixCount;
            var suffixCount = 0;

            while (suffixCount < maxSuffixCount
                && folderPaths.All(path => path[path.Count - suffixCount - 1]
                    == folderPaths[0][folderPaths[0].Count - suffixCount - 1]))
            {
                suffixCount++;
            }

            return suffixCount;
        }

        private static string GetFolderSuffix(IReadOnlyList<string> folders, int folderCount)
        {
            var firstFolderIndex = System.Math.Max(0, folders.Count - folderCount);
            return string.Join(" -> ", folders.Skip(firstFolderIndex));
        }

        private static string GetSceneName(string scenePath)
        {
            var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            return string.IsNullOrEmpty(sceneName) ? scenePath : sceneName;
        }

        private static void OpenScene(string scenePath)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Debug.LogError($"Cant open scene during play mode");
                return;
            }
            
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }

        private static void RefreshToolbar()
        {
            MainToolbar.Refresh(ToolbarElementName);
        }

        private readonly struct BuildSceneInfo
        {
            public int BuildIndex { get; }
            public string ScenePath { get; }

            public BuildSceneInfo(int buildIndex, string scenePath)
            {
                BuildIndex = buildIndex;
                ScenePath = scenePath;
            }
        }
    }
}

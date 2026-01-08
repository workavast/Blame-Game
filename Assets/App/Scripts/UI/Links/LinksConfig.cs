using System;
using UnityEngine;

namespace App.UI.Links
{
    [CreateAssetMenu(fileName = nameof(LinksConfig), menuName = AppConsts.ConfigsPath + nameof(LinksConfig))]
    public class LinksConfig : ScriptableObject
    {
        [SerializeField] private string[] links = Array.Empty<string>();

        public string[] Links => links;

        public int Count => links?.Length ?? 0;

        public string GetLinkByIndex(int index)
        {
            if (links == null) 
                return null;
            
            if (index >= 0 && index < links.Length) 
                return links[index];

            Debug.LogWarning($"LinksConfig: index {index} is out of range (0..{(links.Length - 1)})");
            return null;
        }
    }
}

using System.Collections.Generic;
using System.Text.RegularExpressions;
using Avastrad.ConfigsRepositories.HideIfOverride;
using UnityEngine;

namespace Avastrad.ConfigsRepositories
{
    public abstract partial class ConfigsRepository<TConfig> : ConfigsRepositoryBase 
        where TConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Example: Mission {x} (also can be empty)"), HideIfOverride(nameof(Comparison))] private string comparePattern;
        [SerializeField] private ConfigsRepositoryCell<TConfig> configsRepositoryCell = new(true);

        protected List<TConfig> Configs => configsRepositoryCell.Configs;
        public IReadOnlyList<TConfig> ConfigsRO => configsRepositoryCell.Configs;

        protected virtual int Comparison(TConfig a, TConfig b)
        {
            if (string.IsNullOrEmpty(comparePattern))
                return 0;

            if (a == null)
            {
                if (b == null)
                    return 0;
                else
                    return -1;
            }
            else if (b == null)
                return 1;

            var (x, y) = CompareByPattern(comparePattern, a.name, b.name);

            if (x == y)
                return 0;

            if (x > y)
                return 1;
            else
                return -1;
        }

        private static (int,int) CompareByPattern(string pattern, string a, string b)
        {
            // Convert pattern in regular expression. Example: "Mission {X}" -> "Mission (\d+)"
            var regexPattern = Regex.Replace(pattern, @"\{.*?\}", @"(\d+)");
            var regex = new Regex(regexPattern);

            var matchA = regex.Match(a);
            var matchB = regex.Match(b);

            if (!matchA.Success || !matchB.Success)
            {
                Debug.LogWarning($"String doesnt contains pattern: [{pattern}] [{a}] [{b}]");
                return (0, 0);
            }

            var valueA = int.Parse(matchA.Groups[1].Value);
            var valueB = int.Parse(matchB.Groups[1].Value);

            return (valueA, valueB);
        }
    }
}
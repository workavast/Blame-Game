using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Localization.Tables;

#if UNITY_EDITOR
using UnityEditor.Localization;
#endif

namespace App.Localization
{
    [CreateAssetMenu(fileName = nameof(StringTablesConfig), menuName = Consts.ConfigsPath + nameof(StringTablesConfig))]
    public class StringTablesConfig : ScriptableObject
    {
        [field: SerializeField, Tooltip("ReadOnly"), ReadOnly] private List<TableReference> tableReferences = new();

        public IReadOnlyList<TableReference> TableReferences => tableReferences;
        
#if UNITY_EDITOR
        [SerializeField] private StringTableCollection[] tables;

        private void OnValidate() 
            => UpdateReferences();

        private void OnEnable() 
            => UpdateReferences();

        private void UpdateReferences()
        {
            if (tables != null)
            {
                tableReferences.Clear();
                tableReferences.Capacity = tables.Length;
                foreach (var table in tables)
                {
                    if (table != null)
                        tableReferences.Add(table.TableCollectionName);
                }
            }
        }
#endif
    }
}
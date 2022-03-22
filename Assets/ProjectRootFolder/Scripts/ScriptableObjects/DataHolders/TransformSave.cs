using System.Collections.Generic;
using Helpers.MetaClasses;
using MyBox;
using ScriptableObjects;
using UnityEngine;

namespace MetaClasses.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New TransformSave", menuName = "ScriptableObjects/TransformSave")]
    public class TransformSave : ScriptableObject, ISave<TransformValues>
    {
        [field: SerializeField, ReadOnly] public List<TransformValues> Data { get; set; } = new List<TransformValues>();

        public void Save(List<TransformValues> data)
        {
            Data.Clear();
            Data.AddRange(data);
        }

        public List<TransformValues> Load() => Data;
    }
}
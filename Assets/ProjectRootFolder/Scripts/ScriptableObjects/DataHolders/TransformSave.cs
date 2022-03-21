using System.Collections.Generic;
using Helpers.MetaClasses;
using MyBox;
using ScriptableObjects;
using UnityEngine;

namespace MetaClasses.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New TransformSaver", menuName = "ScriptableObjects/TransformSaver")]
    public class TransformSave : ScriptableObject, ISave<TransformValues>
    {
        [field: SerializeField, ReadOnly] public List<TransformValues> Data { get; set; }

        public void Save(List<TransformValues> data)
        {
            Data.Clear();
            Data.AddRange(data);
        }

        public List<TransformValues> Load() => Data;
    }
}
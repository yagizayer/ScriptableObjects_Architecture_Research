using System;
using System.Collections.Generic;
using Helpers.MetaClasses;
using MyBox;
using ScriptableObjects;
using UnityEngine;

namespace Helpers
{
    public class TransformSaver : MonoBehaviour
    {
        [SerializeField] private ScriptableObject saveFile;
        [SerializeField] private GameObject prefab;

        private ISave<TransformValues> _saveFile;

        private void OnValidate()
        {
            if (!saveFile.ValidateInterface(typeof(ISave<TransformValues>))) saveFile = null;
            else _saveFile = saveFile as ISave<TransformValues>;
        }

        [ButtonMethod]
        private void Save()
        {
            var result = new List<TransformValues>();
            transform.GetAllChildren().ForEach(t => result.Add(new TransformValues(t)));
            _saveFile.Save(result);

            Debug.Log($"Saved {result.Count} transforms.");
        }

        [ButtonMethod]
        private void Load()
        {
            var data = _saveFile.Load();

            if (data.Count != transform.GetAllChildren().Count)
            {
                if (prefab == null)
                {
                    Debug.LogError("Data size is not matching with saved file and Prefab is null.");
                    return;
                }

                transform.Clear();
                foreach (var dataPart in data)
                {
                    var newGo = Instantiate(prefab, transform);
                    newGo.transform.ToLocalTransform(dataPart);
                }
            }
            else
            {
                var counter = 0;
                transform.GetAllChildren().ForEach(t =>
                {
                    if (counter < data.Count)
                    {
                        t.localPosition = data[counter].LocalPosition;
                        t.localRotation = data[counter].LocalRotation;
                        t.localScale = data[counter].Scale;
                    }

                    counter++;
                });
            }

            Debug.Log($"Loaded {data.Count} transforms.");
        }

        [ButtonMethod]
        private void Clear() => transform.Clear();
    }
}
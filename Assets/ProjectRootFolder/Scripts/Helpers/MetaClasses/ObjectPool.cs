using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using static Helpers.Functions;

namespace Helpers
{
    [Serializable]
    public class ObjectPool
    {
        public GameObject prefab;
        public int size;
        public Transform parent;
        public Component cachedComponent;

        private Queue<GameObject> _pool;
        private List<GameObject> _dequeued;
        private int _count;
        public Dictionary<GameObject, Component> CachedComponents { get; } = new Dictionary<GameObject, Component>();

        public void Initialize(bool prewarm = true)
        {
            _pool = new Queue<GameObject>();
            _dequeued = new List<GameObject>();
            _count = 0;
            if (prewarm) PreWarm();
        }

        public GameObject Get(Action<GameObject> beforeActivate = null)
        {
            var obj = _pool.Count > 0 ? _pool.Dequeue() : Create();
            beforeActivate?.Invoke(obj);
            _dequeued.Add(obj);
            obj.SetActive(true);
            return obj;
        }

        public void Return(GameObject obj, Action<GameObject> action = null)
        {
            obj.SetActive(false);
            action?.Invoke(obj);
            obj.transform.SetParent(parent);
            _pool.Enqueue(obj);
            _dequeued.Remove(obj);
        }

        private GameObject Create(bool preWarming = false)
        {
            var obj = Object.Instantiate(prefab, parent, false);
            obj.name = prefab.name + "_" + (++_count).ToString("00");
            if (preWarming)
                _pool.Enqueue(obj);
            obj.SetActive(false);
            CachedComponents.Add(obj, obj.GetComponent(cachedComponent.GetType()));
            return obj;
        }

        public void Clear()
        {
            foreach (var obj in _pool) Object.Destroy(obj);
            _pool.Clear();
        }

        public void ResetDequeued(Action<GameObject> action = null)
        {
            var list = _dequeued.ToArray();
            foreach (var obj in list)
                Return(obj, action);

            _dequeued.Clear();
        }

        public void PreWarm() => Range(0, size, () => Create(true));
    }
}
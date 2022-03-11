using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Helpers
{
    [Serializable]
    public class MovementKeys
    {
        public KeyCode Forward;
        public KeyCode Backward;
        public KeyCode Left;
        public KeyCode Right;
    }
    
    [Serializable]
    public class GradientVariables
    {
        [HideInInspector] public Gradient GradientMaster;
        public List<Color> Colors = new List<Color>();
        public float Duration = 3;

        public void SetGradientMaster()
        {
            GradientMaster = new Gradient();

            var colorKeys = new GradientColorKey[Colors.Count];
            var alphaKeys = new GradientAlphaKey[Colors.Count];

            for (var i = 0; i < Colors.Count; i++)
            {
                colorKeys[i].color = Colors[i];
                colorKeys[i].time = ((float) i).Remap(0, Colors.Count, 0, Duration);
                alphaKeys[i].alpha = Colors[i].a;
                alphaKeys[i].time = ((float) i).Remap(0, Colors.Count, 0, Duration);
            }

            GradientMaster.SetKeys(colorKeys, alphaKeys);
        }
    }

    [Serializable]
    public class TransformValues
    {
        public Vector3 Position = Vector3.zero;
        public Vector3 LocalPosition = Vector3.zero;
        public Quaternion Rotation = Quaternion.identity;
        public Quaternion LocalRotation = Quaternion.identity;
        public Vector3 Scale = Vector3.one;

        public TransformValues()
        {
        }

        public TransformValues(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public TransformValues(Transform transform)
        {
            Position = transform.position;
            LocalPosition = transform.localPosition;
            Rotation = transform.rotation;
            LocalRotation = transform.localRotation;
            Scale = transform.localScale;
        }

        public TransformValues(TransformValues values)
        {
            Position = values.Position;
            LocalPosition = values.LocalPosition;
            Rotation = values.Rotation;
            LocalRotation = values.LocalRotation;
            Scale = values.Scale;
        }

        public static TransformValues Lerp(TransformValues t1, TransformValues t2, float t) =>
            new TransformValues(
                Vector3.Lerp(t1.LocalPosition, t2.LocalPosition, t),
                Quaternion.Lerp(t1.LocalRotation, t2.LocalRotation, t),
                Vector3.Lerp(t1.Scale, t2.Scale, t));

        public static TransformValues Lerp(Transform t1, Transform t2, float t) =>
            new TransformValues(
                Vector3.Lerp(t1.localPosition, t2.localPosition, t),
                Quaternion.Lerp(t1.localRotation, t2.localRotation, t),
                Vector3.Lerp(t1.localScale, t2.localScale, t));

        public static float Distance(TransformValues t1, TransformValues t2) =>
            Vector3.Distance(t1.LocalPosition, t2.LocalPosition);
    }
    [Serializable]
    public class NavmeshMovementConfig
    {
        [SerializeField] private Camera _currentCamera;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _sprintingSpeed;
        private float _horizontalInput;
        private bool _isSprinting;
        private float _verticalInput;

        public NavmeshMovementConfig(float vertical, float horizontal, bool isSprinting, Camera currentCamera,
            float moveSpeed = 5, float rotationSpeed = 120, float sprintingSpeed = 10, bool isMoving = false)
        {
            _currentCamera = currentCamera;
            _moveSpeed = moveSpeed;
            _rotationSpeed = rotationSpeed;
            _verticalInput = vertical;
            _horizontalInput = horizontal;
            _isSprinting = isSprinting;
            _sprintingSpeed = sprintingSpeed;
            IsMoving = isMoving;
        }

        public Transform CurrentCamera => _currentCamera.transform;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float SprintingSpeed => _sprintingSpeed;

        public float VerticalInput
        {
            get => _verticalInput;
            set
            {
                _verticalInput = value;
                if (_verticalInput == 0 && _horizontalInput == 0)
                    IsMoving = false;
                else
                    IsMoving = true;
            }
        }

        public float HorizontalInput
        {
            get => _horizontalInput;
            set
            {
                _horizontalInput = value;
                if (_verticalInput == 0 && _horizontalInput == 0)
                    IsMoving = false;
                else
                    IsMoving = true;
            }
        }

        public bool IsSprinting
        {
            get => _isSprinting;
            set => _isSprinting = value;
        }

        public bool IsMoving { get; private set; }
    }

    public class ObjectPool
    {
        public ObjectPool(string objectPathToPool, int amountToPool, Transform parent)
        {
            ObjectToPool = (GameObject) Resources.Load(objectPathToPool);
            PoolParent = parent;
            for (var i = 0; i < amountToPool; ++i) CreateObject();
        }

        public ObjectPool(GameObject objectToPool, int amountToPool)
        {
            ObjectToPool = objectToPool;
            for (var i = 0; i < amountToPool; ++i) CreateObject();
        }

        public ObjectPool(GameObject objectToPool, int amountToPool, Transform parent)
        {
            ObjectToPool = objectToPool;
            PoolParent = parent;
            for (var i = 0; i < amountToPool; ++i) CreateObject();
        }

        public Dictionary<GameObject, int> PooledObjectsDict { get; } = new Dictionary<GameObject, int>();
        public List<GameObject> ActiveObjects { get; } = new List<GameObject>();
        public List<GameObject> PooledObjects { get; } = new List<GameObject>();
        public GameObject ObjectToPool { get; }
        public Transform PoolParent { get; }
        public int ObjectCount { get; private set; }

        public GameObject GetObject(bool activate)
        {
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                GameObject t = PooledObjects[i];
                if (t.activeInHierarchy) continue;

                // We may want to do some setup before actually activating the object
                if (activate)
                    t.SetActive(true);

                if (!ActiveObjects.Contains(t))
                    ActiveObjects.Add(t);
                return t;
            }

            return CreateObject(true);
        }

        public GameObject GetObject()
        {
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                GameObject t = PooledObjects[i];
                if (t.activeInHierarchy) continue;
                return t;
            }

            return CreateObject();
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj == null) return;
            if (ActiveObjects.Contains(obj)) ActiveObjects.Remove(obj);
            obj.transform.SetParent(PoolParent);
            obj.SetActive(false);
        }

        private GameObject CreateObject(bool isActive = false)
        {
            var obj = Object.Instantiate(ObjectToPool, PoolParent, false);
            obj.SetActive(isActive);
            PooledObjects.Add(obj);
            PooledObjectsDict[obj] = ObjectCount++;
            if (isActive) ActiveObjects.Add(obj);
            return obj;
        }
    }

    public class AnimationCurveKey
    {
        public float time;
        public float value;

        public AnimationCurveKey(float time, float value)
        {
            this.time = time;
            this.value = value;
        }
    }
}
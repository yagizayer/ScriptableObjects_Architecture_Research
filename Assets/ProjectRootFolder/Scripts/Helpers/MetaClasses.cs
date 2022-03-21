using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers.MetaClasses
{
    public static class LocalExtensions
    {
        public static Transform ToLocalTransform(this Transform me, TransformValues values)
        {
            me.localPosition = values.LocalPosition;
            me.localRotation = values.LocalRotation;
            me.localScale = values.Scale;
            return me;
        }

        public static Transform ToTransform(this Transform me, TransformValues values)
        {
            me.position = values.Position;
            me.localPosition = values.LocalPosition;
            me.rotation = values.Rotation;
            me.localRotation = values.LocalRotation;
            me.localScale = values.Scale;
            return me;
        }

        public static Transform ToWorldTransform(this Transform me, TransformValues values)
        {
            me.position = values.Position;
            me.rotation = values.Rotation;
            me.localScale = values.Scale;
            return me;
        }

        public static TransformValues ToValues(this Transform me) =>
            new TransformValues(me);
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

        public static void Set(ref Transform t, TransformValues values) => t.ToLocalTransform(values);
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
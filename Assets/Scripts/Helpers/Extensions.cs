using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using Random = System.Random;
using URandom = UnityEngine.Random;

namespace Helpers
{
    public static class Extensions
    {
        private static readonly Random R = new Random();

        #region Vector3 and Quaternion Functions

        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Modifies the vector's given axis values based on old values
        /// </summary>
        /// <param name="oldValues">caller vector</param>
        /// <param name="axis">axis to rewrite</param>
        /// <param name="newValue">new values</param>
        /// <returns>Modified vector</returns>
        public static Vector3 ModifyAdd(this Vector3 oldValues, Vector3Values axis, float newValue)
        {
            return axis switch
            {
                Vector3Values.X => new Vector3(oldValues.x + newValue, oldValues.y, oldValues.z),
                Vector3Values.Y => new Vector3(oldValues.x, oldValues.y + newValue, oldValues.z),
                Vector3Values.Z => new Vector3(oldValues.x, oldValues.y, oldValues.z + newValue),
                _ => oldValues
            };
        }

        /// <summary>
        /// overrides the vector's given axis values
        /// </summary>
        /// <param name="oldValues">caller vector</param>
        /// <param name="axis">axis to rewrite</param>
        /// <param name="newValues">new values</param>
        /// <returns>Modified vector</returns>
        public static Vector3 Modify(this Vector3 oldValues, Vector3Values axis, Vector3 newValues)
        {
            return axis switch
            {
                Vector3Values.X => new Vector3(newValues.x, oldValues.y, oldValues.z),
                Vector3Values.Y => new Vector3(oldValues.x, newValues.y, oldValues.z),
                Vector3Values.Z => new Vector3(oldValues.x, oldValues.y, newValues.z),
                Vector3Values.XY => new Vector3(newValues.x, newValues.y, oldValues.z),
                Vector3Values.XZ => new Vector3(newValues.x, oldValues.y, newValues.z),
                Vector3Values.YZ => new Vector3(oldValues.x, newValues.y, newValues.z),
                Vector3Values.XYZ => newValues,
                _ => oldValues
            };
        }

        /// <summary>
        /// overrides the vector's given axis values
        /// </summary>
        /// <param name="oldValues">caller vector</param>
        /// <param name="axis">axis to rewrite</param>
        /// <param name="newValue">new values</param>
        /// <returns>Modified vector</returns>
        public static Vector3 Modify(this Vector3 oldValues, Vector3Values axis, float newValue)
        {
            return axis switch
            {
                Vector3Values.X => new Vector3(newValue, oldValues.y, oldValues.z),
                Vector3Values.Y => new Vector3(oldValues.x, newValue, oldValues.z),
                Vector3Values.Z => new Vector3(oldValues.x, oldValues.y, newValue),
                _ => oldValues
            };
        }

        /// <summary>
        /// Converts the vector 2 to a vector 3
        /// </summary>
        /// <param name="me">caller vector</param>
        /// <param name="axis">axis to rewrite as vector3</param>
        /// <returns>vector 3 with given axis value</returns>
        public static Vector3 ToVector3(this Vector2 me, Vector3Values axis)
        {
            return axis switch
            {
                Vector3Values.X => new Vector3(me.x, 0, 0),
                Vector3Values.Y => new Vector3(0, me.x, 0),
                Vector3Values.Z => new Vector3(0, 0, me.x),
                Vector3Values.XY => new Vector3(me.x, me.y, 0),
                Vector3Values.XZ => new Vector3(me.x, 0, me.y),
                Vector3Values.YZ => new Vector3(0, me.x, me.y),
                Vector3Values.XYZ => new Vector3(me.x, me.y, me.x),
                _ => new Vector3(me.x, me.y, 0),
            };
        }

        /// <summary>
        /// overrides the quaternion's given axis values based on given vector as euler angles
        /// </summary>
        /// <param name="oldValues">caller quaternion</param>
        /// <param name="axis">axis to rewrite</param>
        /// <param name="newValues">new values</param>
        /// <returns>Modified quaternion</returns>
        public static Quaternion Modify(this Quaternion oldValues, Vector3Values axis, Vector3 newValues)
        {
            return axis switch
            {
                Vector3Values.X => Quaternion.Euler(newValues.x, oldValues.y, oldValues.z),
                Vector3Values.Y => Quaternion.Euler(oldValues.x, newValues.y, oldValues.z),
                Vector3Values.Z => Quaternion.Euler(oldValues.x, oldValues.y, newValues.z),
                Vector3Values.XY => Quaternion.Euler(newValues.x, newValues.y, oldValues.z),
                Vector3Values.XZ => Quaternion.Euler(newValues.x, oldValues.y, newValues.z),
                Vector3Values.YZ => Quaternion.Euler(oldValues.x, newValues.y, newValues.z),
                Vector3Values.XYZ => Quaternion.Euler(newValues),
                _ => oldValues
            };
        }

        /// <summary>
        /// overrides the quaternion's given axis values based on given quaternion as euler angles
        /// </summary>
        /// <param name="oldValues">caller quaternion</param>
        /// <param name="axis">axis to rewrite</param>
        /// <param name="newValues">new values</param>
        /// <returns>Modified quaternion</returns>
        public static Quaternion Modify(this Quaternion oldValues, Vector3Values axis, Quaternion newValues)
        {
            return axis switch
            {
                Vector3Values.X => Quaternion.Euler(newValues.eulerAngles.x, oldValues.y, oldValues.z),
                Vector3Values.Y => Quaternion.Euler(oldValues.x, newValues.eulerAngles.y, oldValues.z),
                Vector3Values.Z => Quaternion.Euler(oldValues.x, oldValues.y, newValues.eulerAngles.z),
                Vector3Values.XY => Quaternion.Euler(newValues.eulerAngles.x, newValues.eulerAngles.y, oldValues.z),
                Vector3Values.XZ => Quaternion.Euler(newValues.eulerAngles.x, oldValues.y, newValues.eulerAngles.z),
                Vector3Values.YZ => Quaternion.Euler(oldValues.x, newValues.eulerAngles.y, newValues.eulerAngles.z),
                Vector3Values.XYZ => Quaternion.Euler(newValues.eulerAngles),
                _ => oldValues
            };
        }

        /// <summary>
        /// overrides the Quaternion's given axis values
        /// </summary>
        /// <param name="oldValues">caller Quaternion</param>
        /// <param name="axis">axis to rewrite</param>
        /// <param name="newValue">new values</param>
        /// <returns>Modified Quaternion</returns>
        public static Quaternion Modify(this Quaternion oldValues, Vector3Values axis, float newValue)
        {
            return axis switch
            {
                Vector3Values.X => Quaternion.Euler(newValue, oldValues.y, oldValues.z),
                Vector3Values.Y => Quaternion.Euler(oldValues.x, newValue, oldValues.z),
                Vector3Values.Z => Quaternion.Euler(oldValues.x, oldValues.y, newValue),
                Vector3Values.XY => Quaternion.Euler(newValue, newValue, oldValues.z),
                Vector3Values.XZ => Quaternion.Euler(newValue, oldValues.y, newValue),
                Vector3Values.YZ => Quaternion.Euler(oldValues.x, newValue, newValue),
                Vector3Values.XYZ => Quaternion.Euler(newValue, newValue, newValue),
                _ => oldValues
            };
        }

        /// <summary>
        /// Gives relative direction of caller vector based on cameras forward vector
        /// </summary>
        /// <param name="me">caller vector(usually movement direction)</param>
        /// <param name="currentCamera">camera to relate operation</param>
        /// <returns>relative direction based on given camera</returns>
        public static Vector3 RelativeToCamera(this Vector3 me, Transform currentCamera)
        {
            var cameraForwardNormalized = Vector3.ProjectOnPlane(currentCamera.forward, Vector3.up);
            var rotationToCamNormal = Quaternion.LookRotation(cameraForwardNormalized, Vector3.up);

            var finalMoveDir = rotationToCamNormal * me;
            return finalMoveDir;
        }

        public static Vector3 RelativeToCamera(this Vector3 me, Camera currentCamera) =>
            RelativeToCamera(me, currentCamera.transform);

        /// <summary>
        /// Returns Distance between given vectors
        /// </summary>
        /// <param name="me">vector 1</param>
        /// <param name="other">vector 2</param>
        /// <returns>float distance value </returns>
        public static float Distance(this Vector3 me, Vector3 other) => Vector3.Distance(me, other);

        /// <summary>
        /// Returns true if Distance between given vectors are less then given value
        /// </summary>
        /// <param name="me">Caller position</param>
        /// <param name="other">target object position</param>
        /// <param name="range">decision maker threshold</param>
        /// <exception cref="ArgumentOutOfRangeException">When range value is negative</exception>
        /// <returns>true if target object is closer than given threshold</returns>
        public static bool InRange(this Vector3 me, Vector3 other, float range)
        {
            if (range < 0) throw new ArgumentOutOfRangeException(nameof(range));
            return (me - other).sqrMagnitude < range * range;
        }

        /// <summary>
        /// Neutrallizes given vector3's given axis values
        /// </summary>
        /// <param name="me">caller vector</param>
        /// <param name="axis">axis to neutralize</param>
        /// <returns>neutralized vector</returns>
        public static Vector3 Neutralize(this Vector3 me, Vector3Values axis)
        {
            if (axis.HasFlag(Vector3Values.X)) me.x = 0;
            if (axis.HasFlag(Vector3Values.Y)) me.y = 0;
            if (axis.HasFlag(Vector3Values.Z)) me.z = 0;
            return me;
        }

        /// <summary>
        /// Gives the pivot offset of the box in world position
        /// </summary>
        /// <param name="me">caller vector</param>
        /// <param name="pivot">pivot to give world point based on caller vector</param>
        /// <returns>world position of the pivot point</returns>
        /// <examples>
        /// var vector = new Vector3(1,2,3).GetPivot(Pivot3D.BottomBackLeft);
        /// // vector is now (.5f, 1, 1.5f)
        /// 
        /// vector = new Vector3(1,2,3).GetPivot(Pivot3D.CenterCenterCenter);
        /// // vector is now (0, 0, 0)
        ///
        /// vector = new Vector3(1,2,3).GetPivot(Pivot3D.TopFrontRight);
        /// // vector is now (-.5f, -1, -1.5f)
        /// </examples>
        public static Vector3 GetPivotOffset(this Vector3 me, Pivot3D pivot = Pivot3D.CenterCenterCenter)
        {
            me /= -2;
            return pivot switch
            {
                Pivot3D.BottomBackLeft => new Vector3(-me.x, -me.y, -me.z),
                Pivot3D.BottomBackCenter => new Vector3(0, -me.y, -me.z),
                Pivot3D.BottomBackRight => new Vector3(me.x, -me.y, -me.z),
                Pivot3D.BottomCenterLeft => new Vector3(-me.x, 0, -me.z),
                Pivot3D.BottomCenterCenter => new Vector3(0, 0, -me.z),
                Pivot3D.BottomCenterRight => new Vector3(me.x, 0, -me.z),
                Pivot3D.BottomFrontLeft => new Vector3(-me.x, me.y, -me.z),
                Pivot3D.BottomFrontCenter => new Vector3(0, me.y, -me.z),
                Pivot3D.BottomFrontRight => new Vector3(me.x, me.y, -me.z),
                Pivot3D.CenterBackLeft => new Vector3(-me.x, -me.y, 0),
                Pivot3D.CenterBackCenter => new Vector3(0, -me.y, 0),
                Pivot3D.CenterBackRight => new Vector3(me.x, -me.y, 0),
                Pivot3D.CenterCenterLeft => new Vector3(-me.x, 0, 0),
                Pivot3D.CenterCenterCenter => new Vector3(0, 0, 0),
                Pivot3D.CenterCenterRight => new Vector3(me.x, 0, 0),
                Pivot3D.CenterFrontLeft => new Vector3(-me.x, me.y, 0),
                Pivot3D.CenterFrontCenter => new Vector3(0, me.y, 0),
                Pivot3D.CenterFrontRight => new Vector3(me.x, me.y, 0),
                Pivot3D.TopBackLeft => new Vector3(-me.x, -me.y, me.z),
                Pivot3D.TopBackCenter => new Vector3(0, -me.y, me.z),
                Pivot3D.TopBackRight => new Vector3(me.x, -me.y, me.z),
                Pivot3D.TopCenterLeft => new Vector3(-me.x, 0, me.z),
                Pivot3D.TopCenterCenter => new Vector3(0, 0, me.z),
                Pivot3D.TopCenterRight => new Vector3(me.x, 0, me.z),
                Pivot3D.TopFrontLeft => new Vector3(-me.x, me.y, me.z),
                Pivot3D.TopFrontCenter => new Vector3(0, me.y, me.z),
                Pivot3D.TopFrontRight => new Vector3(me.x, me.y, me.z),
                _ => me
            };
        }

        /// <summary>
        /// returns true if given vector is lesser than given other vector
        /// </summary>
        /// <param name="me">caller vector</param>
        /// <param name="offset">offset of measurement for given vector</param>
        /// <param name="boxBoundaries">threshold box crossection</param>
        /// <returns>true if caller is Inside the box</returns>
        public static bool IsInsideBox(this Vector3 me, Vector3 offset = default, Vector3 boxBoundaries = default)
        {
            if (boxBoundaries == default)
                boxBoundaries = Vector3.one;
            if (offset == default)
                offset = Vector3.zero;

            boxBoundaries += offset;
            Debug.Log(boxBoundaries);
            Debug.Log(me);

            var minX = me.x < 0 ? -boxBoundaries.x : 0;
            var minY = me.y < 0 ? -boxBoundaries.y : 0;
            var minZ = me.z < 0 ? -boxBoundaries.z : 0;

            return me.x >= minX && me.x <= boxBoundaries.x &&
                   me.y >= minY && me.y <= boxBoundaries.y &&
                   me.z >= minZ && me.z <= boxBoundaries.z;
        }


        /// <summary>
        ///     Returns Vector3 parsed from given string
        /// </summary>
        /// <param name="me">given string</param>
        /// <returns>givens strings vector3 representation</returns>
        public static Vector3 ToVector3(this string me) =>
            new Vector3(
                float.Parse(me.Split(',')[0]),
                float.Parse(me.Split(',')[1]),
                float.Parse(me.Split(',')[2])
            );

        /// <summary>
        ///     returns squared distance between two vectors (faster than Distance)
        /// </summary>
        /// <param name="me">vector one</param>
        /// <param name="other">vector two</param>
        /// <returns>squared distance</returns>
        public static float DistanceSqr(this Vector3 me, Vector3 other) => (me - other).sqrMagnitude;

        /// <summary>
        ///     returns true if distance between given vectors is less then given value
        ///     faster than Distance function
        /// </summary>
        /// <param name="me">vector a </param>
        /// <param name="other">vector b</param>
        /// <param name="range">desired distance</param>
        /// <returns>true if distance between given vectors is less then given value</returns>
        public static bool IsInRange(this Vector3 me, Vector3 other, float range) =>
            me.DistanceSqr(other) <= range * range;


        /// <summary>
        /// returns Quaternion value of given vector
        /// </summary>
        /// <param name="me">given vector</param>
        /// <returns>Quaternion value of given vector</returns>
        public static Quaternion ToQuaternion(this Vector3 me) => Quaternion.Euler(me);


        /// <summary>
        /// returns caller vector3 and given vector3's middle point added to caller vector3
        /// </summary>
        /// <param name="me">caller vector3</param>
        /// <param name="other">given vector3</param>
        /// <returns>Average point of given vectors</returns>
        /// <example>
        /// <code>var vector1 = new Vector3(1,2,4);
        /// var vector2 = new Vector3(3,4,5);
        /// var average = vector1.Average(vector2);
        /// // average is now (2,3,4.5f);</code>
        /// </example>
        public static Vector3 Average(this Vector3 me, Vector3 other) => Vector3.Lerp(me, other, 0.5f);

        /// <summary>
        /// returns caller vector3 with same signed values on given direction
        /// </summary>
        /// <param name="me">caller vector3</param>
        /// <param name="direction">given direction</param>
        /// <returns>Absolute value of given vector</returns>
        /// <example>
        /// <code>
        /// new vector3(-1,2,-3).Absolute(Vector3.right); // new vector3(1,2,-3)
        /// new vector3(1,2,-3).Absolute(Vector3.down); //new vector3(1,-2,-3)
        /// </code>
        /// </example>
        public static Vector3 Absolute(this Vector3 me, Vector3 direction) =>
            new Vector3(me.x * direction.normalized.x, me.y * direction.normalized.y, me.z * direction.normalized.z);


        /// <summary>
        /// Returns lerped value between two given vector with given another value
        /// </summary>
        /// <param name="me">caller vector 3</param>
        /// <param name="other">other vector3</param>
        /// <param name="t">multiplier variable</param>
        /// <returns>lerped value between two given vector with given another value</returns>
        /// <example>
        /// <code>
        /// new vector3(0,1,2).Lerp(new vector3(2,3,4),0.5); //new vector3(1,2,3)
        /// </code>
        /// </example>
        public static Vector3 Lerp(this Vector3 me, Vector3 other, float t) => Vector3.Lerp(me, other, t);

        /// <summary>
        /// Remaps a vector3 with given axis and percentage value
        /// </summary>
        /// <param name="me">caller vector 3</param>
        /// <param name="remappedValues">axis to rewrite</param>
        /// <param name="percentage">percentage value (Between 0 and 1)</param>
        /// <returns>new vector3 with overriden on given axis</returns>
        /// <example>
        /// <code>
        /// new vector3(4,1,2).Remap(Vector3Values.X,0.5); //new vector3(2,1,2)
        /// new vector3(4,1,2).Remap(Vector3Values.XZ,1.5); //new vector3(6,1,3)
        /// </code>
        /// </example>
        public static Vector3 Remap(this Vector3 me, Vector3Values remappedValues, float percentage)
        {
            var percentX = me.x * percentage;
            var percentY = me.y * percentage;
            var percentZ = me.z * percentage;
            return remappedValues switch
            {
                Vector3Values.X => new Vector3(percentX, me.y, me.z),
                Vector3Values.Y => new Vector3(me.x, percentY, me.z),
                Vector3Values.Z => new Vector3(me.x, me.y, percentZ),
                Vector3Values.XY => new Vector3(percentX, percentY, me.z),
                Vector3Values.YZ => new Vector3(me.x, percentY, percentZ),
                Vector3Values.XZ => new Vector3(percentX, me.y, percentZ),
                Vector3Values.XYZ => new Vector3(percentX, percentY, percentZ),
                _ => me
            };
        }

        /// <summary>
        /// Swaps Two axis values of given vector3 
        /// </summary>
        /// <param name="me">caller vector</param>
        /// <param name="axis">axis to swap</param>
        /// <returns>new vector3 with swapped axis</returns>
        /// <exception cref="ArgumentException">When called with other than 2 swap axis</exception>
        public static Vector3 SwapAxis(this Vector3 me, Vector3Values axis)
        {
            var x = me.x;
            var y = me.y;
            var z = me.z;
            return axis switch
            {
                Vector3Values.X => throw new ArgumentException("Can't swap Single axis"),
                Vector3Values.Y => throw new ArgumentException("Can't swap Single axis"),
                Vector3Values.Z => throw new ArgumentException("Can't swap Single axis"),
                Vector3Values.XY => new Vector3(y, x, z),
                Vector3Values.YZ => new Vector3(x, z, y),
                Vector3Values.XZ => new Vector3(z, y, x),
                Vector3Values.XYZ => throw new ArgumentException("Can't swap Three axis"),
                _ => me
            };
        }

        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region Transform and GameObject Functions

        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Clears all children of the transform
        /// </summary>
        /// <param name="transform">caller transform</param>
        public static void Clear(this Transform transform)
        {
            var objectsToDestroy = (from Transform child in transform select child.gameObject).ToList();
            if (!Application.isPlaying)
                Debug.LogWarning("Destroying objects in editor mode will delete the objects from the scene. " +
                                 "This is not recommended.");
            foreach (var obj in objectsToDestroy)
                if (Application.isPlaying)
                    Object.Destroy(obj);
                else
                    Object.DestroyImmediate(obj);

            objectsToDestroy.Clear();
        }

        /// <summary>
        /// returns true if the transform has any child 
        /// </summary>
        /// <param name="transform">caller transform</param>
        /// <returns>returns true if any child of caller transform</returns>
        public static bool HasChild(this Transform transform) => transform.Cast<Transform>().Any();

        /// <summary>
        /// resets the transform's local values to its default values
        /// </summary>
        /// <param name="transform">caller transform</param>
        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// gives first child of the transform
        /// </summary>
        /// <param name="me">caller transform</param>
        /// <returns>child transform of caller transform</returns>
        public static Transform GetFirstChild(this Transform me) => me.Cast<Transform>().FirstOrDefault();

        /// <summary>
        /// Finds all children with given tag
        /// </summary>
        /// <param name="me">Caller object</param>
        /// <param name="tagName">Desired tag</param>
        /// <returns>List of Transforms</returns>
        public static List<Transform> GetChildrenWithTag(this Transform me, string tagName) =>
            me.Cast<Transform>().Where(child => child.CompareTag(tagName)).ToList();

        /// <summary>
        /// Gets random child from given transform
        /// </summary>
        /// <param name="me">given transform</param>
        /// <returns>a child Transform</returns>
        public static Transform Choose(this Transform me) => me.GetChild(R.Next(0, me.childCount));

        /// <summary>
        /// Gives all child Transforms of a Transform
        /// </summary>
        /// <param name="me">Parent Transform</param>
        /// <returns>List of Transforms</returns>
        public static List<Transform> GetAllChildren(this Transform me) => me.Cast<Transform>().ToList();

        /// <summary>
        /// Tells whether or not given gameObject has a component of given type
        /// </summary>
        /// <param name="me">given game object</param>
        /// <typeparam name="T">component type to check</typeparam>
        /// <returns>bool answer</returns>
        public static bool HasComponent<T>(this GameObject me) where T : Component => me.GetComponent<T>() != null;

        public static bool HasComponent<T>(this Transform me) where T : Component => me.GetComponent<T>() != null;

        /// <summary>
        /// Gives gameObject's component of given type and if not found, adds it then returns it
        /// </summary>
        /// <param name="me">target game object</param>
        /// <typeparam name="T">wanted component</typeparam>
        /// <returns>component of given type from gameObject</returns>
        public static T AddGetComponent<T>(this GameObject me) where T : Component =>
            me.GetComponent<T>() ?? me.AddComponent<T>();

        /// <summary>
        /// Tells whether or not given transform is seen by given camera
        /// </summary>
        /// <param name="me">target transform</param>
        /// <param name="cam">given camera</param>
        /// <returns>is target seen by camera</returns>
        public static bool IsVisibleByCamera(this Transform me, Camera cam) =>
            cam.WorldToViewportPoint(me.position).Neutralize(Vector3Values.Z).IsInsideBox();

        /// <summary>
        /// Compares Given gameObjects layer and given layerMask
        /// </summary>
        /// <param name="me">extended object</param>
        /// <param name="layer">comparable layerMask</param>
        /// <returns>true if layerMask Includes gameObjects layer</returns>
        public static bool CompareLayer(this GameObject me, LayerMask layer) => layer == (layer | (1 << me.layer));

        public static bool CompareLayer(this Transform me, LayerMask layer) =>
            layer == (layer | (1 << me.gameObject.layer));

        public static bool CompareLayer(this GameObject me, int layer) => layer == me.layer;
        public static bool CompareLayer(this GameObject me, string layer) => layer == LayerMask.LayerToName(me.layer);


        /// <summary>
        /// Searches all children of given transform and its children and searches for given name
        /// </summary>
        /// <param name="parent">parent transform</param>
        /// <param name="childName">desired child name</param>
        /// <returns>null or desired named transform</returns>
        public static Transform FindRecursively(this Transform parent, string childName)
        {
            foreach (Transform child in parent)
                if (child.name == childName)
                    return child;
                else
                {
                    var found = FindRecursively(child, childName);
                    if (found != null) return found;
                }

            return null;
        }


        /// <summary>
        /// Closes or opens all children of given transform
        /// </summary>
        /// <param name="me">given transform</param>
        /// <param name="setActive">true if you want to close all children</param>
        /// <param name="recursive">true if you want to close all children's children</param>
        /// <exception cref="Exception">When there is no children</exception>
        public static void CloseOpenChildren(this Transform me, bool setActive, bool recursive = false)
        {
            if (me.childCount == 0) throw new Exception("No children found");
            foreach (Transform child in me)
            {
                child.gameObject.SetActive(setActive);
                if (recursive)
                    child.CloseOpenChildren(setActive, true);
            }
        }

        /// <summary>
        /// closes all enabled children and opens all disabled children of given transform
        /// </summary>
        /// <param name="me">given transform</param>
        /// <param name="recursive">true if you want to toggle all children's children</param>
        /// <exception cref="Exception">When there is no children</exception>
        public static void ToggleChildren(this Transform me, bool recursive = false)
        {
            if (me.childCount == 0) throw new Exception("No children found");
            foreach (Transform child in me)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
                if (recursive)
                    child.ToggleChildren(true);
            }
        }

        /// <summary>
        /// Copies all children of given transform to another transform
        /// </summary>
        /// <param name="me">given transform</param>
        /// <param name="target">target transform</param>
        /// <param name="recursive">true if you want to copy all children's children</param>
        /// <param name="copyPosition">true if you want to copy position</param>
        /// <param name="copyRotation">true if you want to copy rotation</param>
        /// <param name="copyScale">true if you want to copy scale</param>
        /// <returns>list of copied children</returns>
        public static List<Transform> CopyChildren(this Transform me, Transform target, bool recursive = false,
            bool copyPosition = true, bool copyRotation = true, bool copyScale = true)
        {
            var list = new List<Transform>();
            foreach (Transform child in me)
            {
                var newChild = Object.Instantiate(child.gameObject, target).transform;
                list.Add(newChild);
                if (copyPosition)
                    newChild.position = child.position;
                if (copyRotation)
                    newChild.rotation = child.rotation;
                if (copyScale)
                    newChild.localScale = child.localScale;
                if (recursive)
                    list.AddRange(child.CopyChildren(newChild, true, copyPosition, copyRotation, copyScale));
            }

            return list;
        }

        /// <summary>
        /// Activates children of given transform by given names list
        /// </summary>
        /// <param name="me">given transform</param>
        /// <param name="names">given names list</param>
        /// <returns>list of activated children</returns>
        public static List<Transform> ActivateChildrenByName(this Transform me, List<string> names)
        {
            var list = new List<Transform>();
            foreach (Transform child in me)
            {
                if (!names.Contains(child.name)) continue;

                child.gameObject.SetActive(true);
                list.Add(child);
            }

            return list;
        }

        /// <summary>
        /// sets given gameObjects layer to given layer
        /// </summary>
        /// <param name="me">given gameObject</param>
        /// <param name="layer">given layer</param>
        /// <param name="recursive">true if you want to set layer to all children</param>
        /// <returns>list of changed gameObjects</returns>
        public static List<GameObject> SetLayer(this GameObject me, int layer, bool recursive = false)
        {
            var list = new List<GameObject>();
            if (me.layer != layer)
            {
                me.layer = layer;
                list.Add(me);
            }

            if (!recursive) return list;

            foreach (Transform child in me.transform)
                list.AddRange(child.gameObject.SetLayer(layer, true));
            return list;
        }

        public static List<GameObject> SetLayer(this GameObject me, string layerName, bool recursive = false) =>
            me.SetLayer(LayerMask.NameToLayer(layerName), recursive);

        /// <summary>
        /// returns true if given transforms forward is looking from top to bottom
        /// </summary>
        /// <param name="me">given transform</param>
        /// <returns>true or false according to given transforms forward</returns>
        public static bool TopDown(this Transform me) =>
            me.forward == Vector3.down;
        
        /// <summary>
        /// returns true if given transforms forward is looking from bottom to top
        /// </summary>
        /// <param name="me">given transform</param>
        /// <returns>true or false according to given transforms forward</returns>
        public static bool DownTop(this Transform me) =>
            me.forward == Vector3.up;
        
        /// <summary>
        /// returns true if given transforms forward is looking from forward to backward
        /// </summary>
        /// <param name="me">given transform</param>
        /// <returns>true or false according to given transforms forward</returns>
        public static bool ForwardBackward(this Transform me) =>
            me.forward == Vector3.back;
        
        /// <summary>
        /// returns true if given transforms forward is looking from backward to forward
        /// </summary>
        /// <param name="me">given transform</param>
        /// <returns>true or false according to given transforms forward</returns>
        public static bool BackwardForward(this Transform me) =>
            me.forward == Vector3.forward;
        
        /// <summary>
        /// returns true if given transforms forward is looking from left to right
        /// </summary>
        /// <param name="me">given transform</param>
        /// <returns>true or false according to given transforms forward</returns>
        public static bool LeftRight(this Transform me) =>
            me.forward == Vector3.right;
        
        /// <summary>
        /// returns true if given transforms forward is looking from right to left
        /// </summary>
        /// <param name="me">given transform</param>
        /// <returns>true or false according to given transforms forward</returns>
        public static bool RightLeft(this Transform me) =>
            me.forward == Vector3.left;
        
        


        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region RectTransform Functions

        // ------------------------------------------------------------------------------------------------------------

        public static RectTransform Reset(this RectTransform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.sizeDelta = Vector2.zero;
            transform.rect.Set(0, 0, 100, 100);
            return transform;
        }


        /// <summary>
        /// Halve Given rect
        /// </summary>
        /// <param name="me">Given Rect</param>
        /// <returns>Halve sized rect of given rect</returns>
        public static Rect Half(this Rect me) => new Rect(me.x, me.y, me.width / 2, me.height);

        /// <summary>
        /// Quarter Given rect 
        /// </summary>
        /// <param name="me">Given Rect</param>
        /// <returns>Quarter sized rect of given rect</returns>
        public static Rect Quarter(this Rect me) => new Rect(me.x, me.y, me.width / 4, me.height);

        /// <summary>
        /// Modifies given rect with given modify field and returns new rect
        /// </summary>
        /// <param name="rect">rect that wanted to be modified</param>
        /// <param name="field">desired field on rect</param>
        /// <param name="value">new value</param>
        /// <returns>new rect with given rects values and new value</returns>
        public static Rect Modify(this Rect rect, RectFields field, float value)
        {
            switch (field)
            {
                case RectFields.X:
                    rect.x = value;
                    break;
                case RectFields.Y:
                    rect.y = value;
                    break;
                case RectFields.Width:
                    rect.width = value;
                    break;
                case RectFields.Height:
                    rect.height = value;
                    break;
                case RectFields.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(field), field, null);
            }

            return rect;
        }

        /// <summary>
        /// Modifies and adds new value to given rect with given modify field and returns new rect
        /// </summary>
        /// <param name="rect">rect that wanted to be modified</param>
        /// <param name="field">desired field on rect</param>
        /// <param name="value">new value</param>
        /// <returns>new rect with given rects values and new value</returns>
        public static Rect ModifyAdd(this Rect rect, RectFields field, float value)
        {
            switch (field)
            {
                case RectFields.X:
                    rect.x += value;
                    break;
                case RectFields.Y:
                    rect.y += value;
                    break;
                case RectFields.Width:
                    rect.width += value;
                    break;
                case RectFields.Height:
                    rect.height += value;
                    break;
                case RectFields.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(field), field, null);
            }

            return rect;
        }


        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region IEnumerable Functions

        // ------------------------------------------------------------------------------------------------------------


        /// <summary>
        ///     returns list form of an array
        /// </summary>
        /// <param name="me"> given array</param>
        /// <typeparam name="T">type of array</typeparam>
        /// <returns>generic list with given type</returns>
        public static List<T> ToList<T>(this T[] me) => Enumerable.ToList(me);

        /// <summary>
        /// Makes a random choice in given generic list
        /// </summary>
        /// <param name="me">List to choice</param>
        /// <typeparam name="T">Generic type of list</typeparam>
        /// <returns>An element of given list</returns>
        public static T Choose<T>(this IEnumerable<T> me)
        {
            var enumerable = me as T[] ?? me.ToArray();
            return enumerable.ElementAt(R.Next(0, enumerable.Count()));
        }


        /// <summary>
        /// Gives a slice of elements of the given list
        /// </summary>
        /// <param name="me">Given list</param>
        /// <param name="startIncluded">starting Index of slice</param>
        /// <typeparam name="T">any type</typeparam>
        /// <returns>Slice of elements of the given list</returns>
        public static List<T> Slice<T>(this IEnumerable<T> me, int startIncluded) =>
            me.Where((item, i) => i >= startIncluded).ToList();

        /// <summary>
        /// Gives a slice of elements of the given list
        /// </summary>
        /// <param name="me">Given list</param>
        /// <param name="startIncluded">starting Index of slice</param>
        /// <param name="endIncluded">ending Index of slice</param>
        /// <typeparam name="T">any type</typeparam>
        /// <returns>Slice of elements of the given list</returns>
        public static List<T> Slice<T>(this IEnumerable<T> me, int startIncluded, int endIncluded) =>
            me.Where((item, i) => i >= startIncluded && i <= endIncluded).ToList();


        /// <summary>
        /// Shuffles given list
        /// </summary>
        /// <param name="me">Given list</param>
        /// <param name="listSize">list size for returning shuffled list</param>
        /// <typeparam name="T">Any listable type</typeparam>
        /// <exception cref="Exception">Desired list size is not compatible with caller list size</exception>
        /// <returns>Shuffled list with same elements</returns>
        public static List<T> Shuffle<T>(this IEnumerable<T> me, int listSize = -1)
        {
            var enumerable = me.ToList();
            if (listSize == -1) listSize = enumerable.Count;

            var desiredListCount = listSize;
            var excludedIndexes = new List<int>();
            var result = new List<T>();
            var currentTime = Time.time;
            var timeLimit = currentTime + 1;
            while (currentTime < timeLimit)
            {
                currentTime = Time.time;
                if (result.Count == desiredListCount) return result;

                var randomIndex = R.Next(0, enumerable.Count);
                if (excludedIndexes.Contains(randomIndex)) continue;

                result.Add(enumerable[randomIndex]);
                excludedIndexes.Add(randomIndex);
            }

            throw new Exception("Shuffle failed to return desired list size");
        }

        /// <summary>
        /// Returns a list of tuple from given dict
        /// </summary>
        /// <param name="me">Caller dictionary</param>
        /// <typeparam name="T1">generic key type of dictionary</typeparam>
        /// <typeparam name="T2">generic value type of dictionary</typeparam>
        /// <returns>List of tuple which have keys and values as item1 and item2</returns>
        public static List<(T1, T2)> ToList<T1, T2>(this Dictionary<T1, T2> me) =>
            me.Select(keyValues => (keyValues.Key, keyValues.Value)).ToList();


        /// <summary>
        /// gives last element of a list
        /// </summary>
        /// <typeparam name="T">type of list</typeparam>
        /// <param name="list">given list</param>
        /// <returns>last element of a list</returns>
        /// <exception cref="ArgumentNullException">list is null</exception>
        /// <exception cref="Exception">list is empty</exception>
        public static T Last<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (list.Count == 0) throw new Exception("List is empty");
            return list[list.Count - 1];
        }

        /// <summary>
        /// gives first element of a list
        /// </summary>
        /// <typeparam name="T">type of list</typeparam>
        /// <param name="list">given list</param>
        /// <returns>first element of a list</returns>
        /// <exception cref="ArgumentNullException">list is null</exception>
        /// <exception cref="Exception">list is empty</exception>
        public static T First<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (list.Count == 0) throw new Exception("List is empty");
            return list[0];
        }

        /// <summary>
        /// Excludes given items list from caller item list
        /// </summary>
        /// <param name="me">caller item list</param>
        /// <param name="items">given items list</param>
        /// <typeparam name="T">type of caller item list</typeparam>
        /// <returns>new list with excluded items</returns>
        /// <exception cref="ArgumentNullException">me or items is null</exception>
        /// <exception cref="Exception">when items count is greater than caller list count</exception>
        /// <example>
        /// <para>var list = new List&lt;int&gt; {1, 2, 3, 4, 5};</para>
        /// var excludedList = new List&lt;int&gt; {2, 4};
        /// <para>var result = list.Exclude(excludedList);</para>
        /// // result is {1, 3, 5}
        /// </example>
        public static List<T> Exclude<T>(this IEnumerable<T> me, List<T> items)
        {
            if (me == null) throw new ArgumentNullException(nameof(me));
            if (items == null) throw new ArgumentNullException(nameof(items));
            var enumerable = me.ToList();

            if (enumerable.Count == 0) return new List<T>();
            if (items.Count == 0) return enumerable.ToList();

            if (enumerable.Count < items.Count)
                throw new Exception("Excluded items count is greater than caller list count");

            var list = new List<T>(enumerable);
            foreach (var item in items)
                list.Remove(item);
            return list;
        }


        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region float, string, Color, AnimationCurve and Vector2 Functions

        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Clamps given float to given range
        /// </summary>
        /// <param name="me">given value</param>
        /// <param name="min">given min value</param>
        /// <param name="max">given max value</param>
        /// <returns>Given value between given range</returns>
        public static float Clamp(this float me, float min, float max) => Mathf.Clamp(me, min, max);

        /// <summary>
        /// Rounds given float to given decimal places
        /// </summary>
        /// <param name="me">Given variable</param>
        /// <param name="digits">digit count after decimal point</param>
        /// <returns>given float value with rounded decimals</returns>
        public static float Round(this float me, int digits = 2) => (float) Math.Round(me, digits);


        /// <summary>
        /// Returns absolute value of given float
        /// </summary>
        /// <param name="me">caller float</param>
        /// <returns>absolute value of given float</returns>
        public static float Abs(this float me) => Mathf.Abs(me);

        /// <summary>
        /// Remaps a value from one range to another
        /// </summary>
        /// <param name="value">value to remap</param>
        /// <param name="from1">current range min (Included)</param>
        /// <param name="to1">current range max (Included)</param>
        /// <param name="from2">desired range min (Included)</param>
        /// <param name="to2">desired range max (Included)</param>
        /// <returns>remapped float with new value</returns>
        public static float Remap(this float value, float from1, float to1, float from2, float to2) =>
            (value - from1) / (to1 - from1) * (to2 - from2) + from2;

        /// <summary>
        /// Randomizes caller float with given range
        /// </summary>
        /// <param name="me">caller float</param>
        /// <param name="range">range to randomize</param>
        /// <returns>randomized float</returns>
        public static float Randomize(this float me, float range) => me + URandom.Range(-range, range);

        /// <summary>
        ///     Modify color inline
        /// </summary>
        /// <param name="oldValues">Caller object</param>
        /// <param name="variableToModify">Specifying variable for which variable to modify in color</param>
        /// <param name="newValue">New value of specified variable</param>
        /// <returns>Color</returns>
        public static Color Modify(this Color oldValues, ColorValues variableToModify, float newValue)
        {
            oldValues = variableToModify switch
            {
                ColorValues.R => new Color(newValue, oldValues.g, oldValues.b, oldValues.a),
                ColorValues.G => new Color(oldValues.r, newValue, oldValues.b, oldValues.a),
                ColorValues.B => new Color(oldValues.r, oldValues.g, newValue, oldValues.a),
                ColorValues.A => new Color(oldValues.r, oldValues.g, oldValues.b, newValue),
                _ => oldValues
            };

            return oldValues;
        }

        /// <summary>
        ///     Modify color Alpha inline
        /// </summary>
        /// <param name="oldValues">Caller object</param>
        /// <param name="newValue">New value of specified variable</param>
        /// <returns>Color</returns>
        public static Color ModifyA(this Color oldValues, float newValue) =>
            new Color(oldValues.r, oldValues.g, oldValues.b, newValue);

        /// <summary>
        ///     Modify color Red inline
        /// </summary>
        /// <param name="oldValues">Caller object</param>
        /// <param name="newValue">New value of specified variable</param>
        /// <returns>Color</returns>
        public static Color ModifyR(this Color oldValues, float newValue) =>
            new Color(newValue, oldValues.g, oldValues.b, oldValues.a);

        /// <summary>
        ///     Modify color Green inline
        /// </summary>
        /// <param name="oldValues">Caller object</param>
        /// <param name="newValue">New value of specified variable</param>
        /// <returns>Color</returns>
        public static Color ModifyG(this Color oldValues, float newValue) =>
            new Color(oldValues.r, newValue, oldValues.b, oldValues.a);

        /// <summary>
        ///     Modify color Blue inline
        /// </summary>
        /// <param name="oldValues">Caller object</param>
        /// <param name="newValue">New value of specified variable</param>
        /// <returns>Color</returns>
        public static Color ModifyB(this Color oldValues, float newValue) =>
            new Color(oldValues.r, oldValues.g, newValue, oldValues.a);

        /// <summary>
        /// Clamps given vector2 value to screen size
        /// </summary>
        /// <param name="me">given vector2</param>
        /// <param name="screenSize">screen size</param>
        /// <returns>clamped vector2</returns>
        public static Vector2 ClampToScreen(this Vector2 me, Vector2 screenSize = default)
        {
            if (screenSize == default)
                screenSize = new Vector2(Screen.width, Screen.height);
            return new Vector2(Mathf.Clamp(me.x, 0, screenSize.x), Mathf.Clamp(me.y, 0, screenSize.y));
        }

        /// <summary>
        /// Formats given Single 
        /// </summary>
        /// <param name="me">Caller object</param>
        /// <param name="numberOfDigits">Desired output length</param>
        /// <returns>Given ints stringify and formatted version</returns>
        public static string ToStringWithFormat(this int me, int numberOfDigits)
        {
            var prefix = "";
            for (var i = 0; i < numberOfDigits - 1; i++) prefix += "0";
            return prefix + me;
        }

        /// <summary>
        /// Gives Peak points of given curve
        /// </summary>
        /// <param name="curve">caller curve</param>
        /// <returns>Returns Peak keys of given curve</returns>
        /// <exception cref="ArgumentNullException">Given argument is null</exception>
        public static List<AnimationCurveKey> GetPeaks(this AnimationCurve curve)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            if (curve.keys.Length <= 2) return new List<AnimationCurveKey>();
            var peaks = new List<AnimationCurveKey>();
            for (var i = 1; i < curve.keys.Length - 1; i++)
            {
                var prevVal = curve.keys[i - 1].value;
                var currentVal = curve.keys[i].value;
                var nextVal = curve.keys[i + 1].value;
                if (
                    currentVal > prevVal &&
                    currentVal > nextVal
                )
                    peaks.Add(new AnimationCurveKey(curve.keys[i].time, curve.keys[i].value));
            }

            return peaks;
        }

        /// <summary>
        /// Gives Last AnimationCurveKey's Value of given curve
        /// </summary>
        /// <param name="me">given curve</param>
        /// <returns>value of last element in curve</returns>
        public static AnimationCurveKey LastKey(this AnimationCurve me) =>
            new AnimationCurveKey(me.keys[me.keys.Length - 1].time, me.keys[me.keys.Length - 1].value);

        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region Enum Functions

        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Next Enum option of given enum type
        /// </summary>
        /// <param name="src">source enum</param>
        /// <typeparam name="T">Generic enum type</typeparam>
        /// <returns>Next element of given enum</returns>
        /// <exception cref="ArgumentException">Given argument is not an enum</exception>
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[]) Enum.GetValues(src.GetType());
            var j = Array.IndexOf(arr, src) + 1;
            return arr.Length == j ? arr[Array.IndexOf(arr, src)] : arr[j];
        }

        /// <summary>
        /// Previous Enum option of given enum type
        /// </summary>
        /// <param name="src">source enum</param>
        /// <typeparam name="T">Generic enum type</typeparam>
        /// <returns>Previous element of given enum</returns>
        /// <exception cref="ArgumentException">Given argument is not an enum</exception>
        public static T Previous<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[]) Enum.GetValues(src.GetType());
            var j = Array.IndexOf(arr, src) - 1;
            return j <= 0 ? arr[0] : arr[j];
        }

        /// <summary>
        /// Makes a random choice in given Enum
        /// </summary>
        /// <param name="src">caller Enum</param>
        /// <param name="exclusions">Excluded Enum elements</param>
        /// <example>MySpecialEnum.AnyElement.Choose();</example>
        /// <returns>An element of given Enum</returns>
        /// <exception cref="ArgumentException">Given argument is not an enum</exception>
        public static T Choose<T>(this T src, List<T> exclusions = null) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[]) Enum.GetValues(src.GetType());
            if (exclusions == null) return arr[R.Next(0, arr.Length)];

            var temp = arr.Where(item => !exclusions.Contains(item)).ToList();
            return temp[R.Next(0, temp.Count)];
        }

        /// <summary>
        /// Makes a random choice in given Enum
        /// </summary>
        /// <param name="src">caller Enum</param>
        /// <param name="exclusion">Excluded Enum element</param>
        /// <example>MySpecialEnum.AnyElement.Choose();</example>
        /// <returns>An element of given Enum</returns>
        /// <exception cref="ArgumentException">Given argument is not an enum</exception>
        public static T Choose<T>(this T src, T exclusion) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[]) Enum.GetValues(src.GetType());
            var temp = arr.Where(item => !item.Equals(exclusion)).ToList();
            return temp[R.Next(0, temp.Count)];
        }

        /// <summary>
        /// Converts given enum to another enum by its name
        /// </summary>
        /// <param name="me">caller enum</param>
        /// <param name="targetEnum">target enum</param>
        /// <typeparam name="T1">current elements Enum type</typeparam>
        /// <typeparam name="T2">elements target Enum type</typeparam>
        /// <returns>Target Enums same named element</returns>
        /// <exception cref="ArgumentException">When T1 or T2 is not enum or doesn't match names</exception>
        /// <example>var weaponKnife = KitchenTools.Knife.ToEnum(Weapons.Knife)</example>
        public static T2 ToEnum<T1, T2>(this T1 me, T2 targetEnum)
        {
            if (!typeof(T1).IsEnum) throw new ArgumentException("T1 must be an enumerated type");
            if (!typeof(T2).IsEnum) throw new ArgumentException("T2 must be an enumerated type");
            if (targetEnum.ToString() != me.ToString())
                throw new ArgumentException($"Given {me} is not matching with given {targetEnum.ToString()}");
            return (T2) Enum.Parse(typeof(T2), me.ToString());
        }

        public static T2 ToEnum<T1, T2>(this T1 me)
        {
            if (!typeof(T1).IsEnum) throw new ArgumentException("T1 must be an enumerated type");
            if (!typeof(T2).IsEnum) throw new ArgumentException("T2 must be an enumerated type");
            if (Enum.GetName(typeof(T2), me) == null)
                throw new ArgumentException($"Given {me} is not matching with given {typeof(T2).Name}");
            return (T2) Enum.Parse(typeof(T2), me.ToString());
        }

        /// <summary>
        /// Converts string or int to enum
        /// </summary>
        /// <param name="me">caller object (can be string or int)</param>
        /// <typeparam name="T">Targeted Enum</typeparam>
        /// <returns>Converted Enum element</returns>
        /// <exception cref="ArgumentException">When T is not enum</exception>
        /// <example>var weaponKnife = "Knife".ToEnum&lt;Weapons&gt;();</example>
        public static T ToEnum<T>(this object me) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
            return (T) Enum.Parse(typeof(T), me.ToString());
        }

        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region Event Functions

        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Executes a function in all list of MonoBehaviours.
        /// </summary>
        /// <param name="me">MonoBehaviour List</param>
        /// <param name="functionName">Function name</param>
        /// <typeparam name="T">Any class which derives from MonoBehaviour</typeparam>
        public static void ExecuteAll<T>(this List<T> me, string functionName) where T : MonoBehaviour =>
            me.ForEach(item => item.Invoke(functionName, 0));

        /// <summary>
        /// Moves a nav mesh agent related to camera
        /// </summary>
        /// <param name="me">Caller Nav mesh agent</param>
        /// <param name="conf">Configuration options for movement details</param>
        /// <exception cref="ArgumentNullException">Given argument is null</exception>
        /// <returns>Next movement target</returns>
        public static Vector3 MoveRelatedToCam(this NavMeshAgent me, NavmeshMovementConfig conf)
        {
            if (conf == null) throw new ArgumentNullException(nameof(conf));

            var rawMoveDir = Vector3.forward * conf.VerticalInput + Vector3.right * conf.HorizontalInput;

            var cameraForwardNormalized = Vector3.ProjectOnPlane(conf.CurrentCamera.forward, Vector3.up);
            var rotationToCamNormal = Quaternion.LookRotation(cameraForwardNormalized, Vector3.up);

            var finalMoveDir = rotationToCamNormal * rawMoveDir;

            if (!Equals(finalMoveDir, Vector3.zero))
            {
                var rotationToMoveDir = Quaternion.LookRotation(finalMoveDir, Vector3.up);
                me.transform.rotation = Quaternion.RotateTowards(me.transform.rotation, rotationToMoveDir,
                    conf.RotationSpeed * Time.deltaTime);
            }

            var multipliedMovementSpeed = conf.IsSprinting ? conf.SprintingSpeed : conf.MoveSpeed;

            var result = finalMoveDir * multipliedMovementSpeed * Time.deltaTime;
            me.Move(result);

            return result;
        }

        /// <summary>
        /// adds explosion force with given parameters to given rigidbody
        /// </summary>
        /// <param name="me"> given rigidbody</param>
        /// <param name="force">explosion force</param>
        /// <param name="onlyUpwards">explosion vector only applies upwards directions</param>
        public static void ExplodeRandomly(this Rigidbody me, float force = 10, bool onlyUpwards = true)
        {
            Vector3 random;
            do
                random = UnityEngine.Random.onUnitSphere;
            while (onlyUpwards && random.x < .6f);

            me.AddForce(random * force, ForceMode.Impulse);
            me.AddTorque(UnityEngine.Random.onUnitSphere * 300);
        }

        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region Animation Functions

        // ------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gives length of named clip on an animator
        /// </summary>
        /// <param name="me">Given Animator</param>
        /// <param name="clipName">Clip that desired its length</param>
        /// <returns>Clip length</returns>
        public static float GetClipLength(this Animator me, string clipName)
        {
            // TODO : get current animation state name and use it here (currently using clip name)
            if (me.runtimeAnimatorController.animationClips.Length <= 0) return 0;
            var clip = Array.Find(me.runtimeAnimatorController.animationClips, clip => clip.name == clipName);
            if (clip)
                return clip.length;
            return 0;
        }

        /// <summary>
        /// Gives length of named state on an animator
        /// </summary>
        /// <param name="me">Given Animator</param>
        /// <param name="stateName">Clip that desired its length</param>
        /// <returns>State length</returns>
        public static float GetAnimationStateLength(this Animator me, string stateName)
        {
            // TODO : get current animation state name and use it here
            Debug.LogError("Not implemented yet please use GetClipLength instead.");
            return default;
        }

        /// <summary>
        /// Gives current clip's name on an animator
        /// </summary>
        /// <param name="me">Given Animator</param>
        /// <returns>Clip length</returns>
        public static string GetCurrentClipName(this Animator me) =>
            me.runtimeAnimatorController.animationClips.Length <= 0
                ? ""
                : me.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        // ------------------------------------------------------------------------------------------------------------

        #endregion

        #region Debug Functions

        // ------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// draws a red sphere at given worldPoint
        /// </summary>
        /// <param name="worldPoint">given worldPoint</param>
        /// <param name="size">size of sphere</param>
        /// <param name="color">color of sphere</param>
        /// <exception cref="Exception">When tried to build the game with this function</exception>
        public static void DrawSphere(this Vector3 worldPoint, float size = 0.5f, Color color = default)
        {
#if !UNITY_EDITOR
                throw new Exception("This function can only be used in the editor.");
#endif
            if (color == default) color = Color.red.ModifyA(.5f);
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = worldPoint;
            sphere.transform.localScale = new Vector3(size, size, size);
            sphere.GetComponent<Renderer>().material.color = color;
        }


        /// <summary>
        /// Draws a box around the given vector
        /// </summary>
        /// <param name="me">caller vector</param>
        /// <param name="pivot">Pivot Point for box to draw</param>
        /// <param name="color">color of sphere</param>
        /// <exception cref="Exception">When tried to build the game with this function</exception>
        public static void DrawBox(this Vector3 me, Pivot3D pivot = Pivot3D.BottomBackLeft, Color color = default)
        {
#if !UNITY_EDITOR
                throw new Exception("This function can only be used in the editor.");
#endif
            if (color == default) color = Color.red;
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.position = me.GetPivotOffset(pivot);
            gameObject.transform.localScale = me;
            gameObject.GetComponent<Renderer>().material.color = color;
        }

        // ------------------------------------------------------------------------------------------------------------

        #endregion
    }

#if UNITY_EDITOR
    public static class EditorExtensions
    {
        /// <summary>
        /// Saves ScriptableObject to Resources folder
        /// </summary>
        /// <typeparam name="T">type of ScriptableObject</typeparam>
        /// <param name="me">ScriptableObject</param>
        /// <param name="subFolder">path to Resources folder</param>
        /// <returns>saved ScriptableObject and count of saved ScriptableObjects</returns>
        /// <example>newLevel.SaveAsset("Resources/Levels");</example>
        public static (T, int) SaveAsset<T>(this T me, string subFolder = "Resources/") where T : ScriptableObject
        {
            var path = AssetDatabase.GetAssetPath(me);
            var count = 1;
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets/" + subFolder;
                var name = me.GetType().Name.Split('_').Last();
                while (AssetDatabase.LoadAssetAtPath<T>($"{path}/{name}_{count:00}.asset") != null)
                    count++;
                path = $"{path}/{name}_{count:00}.asset";
            }

            AssetDatabase.CreateAsset(me, path);
            AssetDatabase.SaveAssets();
            return (me, count);
        }

        /// <summary>
        /// overrides a ScriptableObject to Resources folder
        /// </summary>
        /// <typeparam name="T">type of ScriptableObject</typeparam>
        /// <param name="oldAsset">ScriptableObject</param>
        /// <param name="newAsset">ScriptableObject</param>
        /// <returns>overridden ScriptableObject</returns>
        /// <example>_levelToOperate.OverrideAsset(GetLevelData());</example>
        public static T OverrideAsset<T>(this T oldAsset, T newAsset) where T : ScriptableObject
        {
            var path = AssetDatabase.GetAssetPath(oldAsset);
            if (string.IsNullOrEmpty(path))
                Debug.Log("Asset not found");

            AssetDatabase.CreateAsset(newAsset, path);
            AssetDatabase.SaveAssets();
            return newAsset;
        }

        /// <summary>
        /// Gives last ScriptableObject on given folder
        /// </summary>
        /// <typeparam name="T">type of ScriptableObject</typeparam>
        /// <param name="me">ScriptableObject</param>
        /// <param name="subFolder">path to Resources folder</param>
        /// <returns>Last ScriptableObject on given path</returns>
        /// <example>_levelToOperate.LastAsset("Resources/Levels");</example>
        public static T LastAsset<T>(this T me, string subFolder = "Resources/") where T : ScriptableObject
        {
            var path = AssetDatabase.GetAssetPath(me);
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets/" + subFolder;
                var name = me.GetType().Name.Split('_').Last();
                var i = 1;
                while (AssetDatabase.LoadAssetAtPath<T>($"{path}/{name}_{i:00}.asset") != null)
                    i++;
                path = $"{path}/{name}_{i:00}.asset";
            }

            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        /// <summary>
        /// saves a gameObject as prefab to given path
        /// </summary>
        /// <param name="me">given gameObject</param>
        /// <param name="subFolder">path to prefab</param>
        /// <returns>saved prefab, count of saved prefabs to path, and path to prefab</returns>
        /// <example>environment.SavePrefab("Resources/Environments");</example>
        public static (GameObject, int, string) SavePrefab(this GameObject me, string subFolder = "Resources")
        {
            var path = AssetDatabase.GetAssetPath(me);
            var count = 1;
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets/" + subFolder;
                var name = me.name;
                while (AssetDatabase.LoadAssetAtPath<GameObject>($"{path}/{name}_{count:00}.prefab") !=
                       null)
                    count++;
                path = $"{path}/{name}_{count:00}.prefab";
            }

            me = PrefabUtility.SaveAsPrefabAsset(me, path);
            AssetDatabase.SaveAssets();
            return (me, count, path);
        }
    }
#endif
}
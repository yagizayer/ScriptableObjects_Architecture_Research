using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Helpers
{
    public class Functions : Manager<Functions>
    {
        [SerializeField] private AnimationCurve curveToShowData = new AnimationCurve();

        private static string _targetScene;
        // (action, did action executed) => true if action executed, false if not
        private static Dictionary<Action, bool> _onceActions = new Dictionary<Action, bool>();

        private void Start()
        {
            Singleton(this);
        }


        #region Scene Functions

        /// <summary>
        ///     Open a new tab at default browser with given url
        /// </summary>
        /// <param name="urlName">Desired web site</param>
        public void LoadURL(string urlName)
        {
            Application.OpenURL(urlName);
        }

        /// <summary>
        /// Open desired scene
        /// </summary>
        /// <param name="sceneName">Desired scene name</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Open desired scene without freezing the game
        /// Call this to lead loadingScene 
        /// </summary>
        /// <param name="sceneName">Desired scene name</param>
        public void LoadSceneAsync(string sceneName)
        {
            _targetScene = sceneName;
            LoadScene("LoadingScreen");
        }

        protected void LoadTargetSceneAsync()
        {
            SceneManager.LoadSceneAsync(_targetScene);
        }

        /// <summary>
        ///     RefreshCurrentScene
        /// </summary>
        public void RefreshScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        ///     Quit Game
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion

        #region Math Functions

        /// <summary>
        ///     Returns non-repetative random list from given object pool
        /// </summary>
        /// <param name="objectPool">The object list which contains all possible objects</param>
        /// <param name="desiredListCount">Length of list to be return.</param>
        /// <typeparam name="T">Any type which can be instantiated.</typeparam>
        /// <returns>List with given generic type</returns>
        internal List<T> GetUniqueRandomList<T>(List<T> objectPool, int desiredListCount)
        {
            desiredListCount = objectPool.Count < desiredListCount ? objectPool.Count : desiredListCount;
            var excludedIndexes = new List<int>();
            var result = new List<T>();
            var r = new Random();
            var retryCount = 0;
            while (true)
            {
                if (retryCount < 500)
                {
                    retryCount++;
                }
                else
                {
                    // Debug.LogWarning("Size error");
                    return new List<T>();
                }

                if (result.Count == desiredListCount) return result;

                var randomIndex = r.Next(0, objectPool.Count);
                if (excludedIndexes.Contains(randomIndex)) continue;

                result.Add(objectPool[randomIndex]);
                excludedIndexes.Add(randomIndex);
            }
        }

        internal List<int> GetUniqueRandomIntList(int size)
        {
            var result = new List<int>();
            for (var i = 0; i < size; i++) result.Add(i);
            return GetUniqueRandomList(result, size);
        }

        #endregion

        #region GameObject Functions

        internal bool IsLayerInLayerMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }

        internal List<Transform> FindObjectOfLayer(LayerMask layer)
        {
            var result = new List<Transform>();
            foreach (var item in FindObjectsOfType<Transform>())
                if (item.gameObject.CompareLayer(layer))
                    result.Add(item);

            return result;
        }

        #endregion

        #region ExecuteDelayed

        internal static void ExecuteDelayed(float delayInSec, Action callbackFunc) =>
            Instance.DelayStarter(delayInSec, callbackFunc);

        /// <summary>
        ///     Executes given function after given time
        /// </summary>
        /// <param name="delayInSec">delay duration</param>
        /// <param name="callbackFunc">desired function to execute</param>
        private void DelayStarter(float delayInSec, Action callbackFunc)
        {
            StartCoroutine(CO_ExecuteDelayed(delayInSec, callbackFunc));
        }

        private void DelayStarter<T>(float delayInSec, Action<T> callbackFunc, T parameter) =>
            StartCoroutine(CO_ExecuteDelayed(delayInSec, callbackFunc, parameter));

        private static IEnumerator CO_ExecuteDelayed(float delayInSec, Action callbackFunc)
        {
            yield return new WaitForSecondsRealtime(delayInSec);
            callbackFunc();
        }

        private static IEnumerator CO_ExecuteDelayed<T>(float delayInSec, Action<T> callbackFunc, T parameter)
        {
            yield return new WaitForSecondsRealtime(delayInSec);
            if (Application.isPlaying)
                callbackFunc(parameter);
        }

        internal static void ExecuteDelayedChain(float delayInSec, List<Action> callbacks) =>
            Instance.ChainStarter(delayInSec, callbacks);

        internal static void ExecuteDelayedChain(float delayInSec, Action[] callbacks) =>
            Instance.ChainStarter(delayInSec, callbacks.ToList());

        private void ChainStarter(float delayInSec, List<Action> callbacks) =>
            StartCoroutine(CO_ExecuteDelayedChain(delayInSec, callbacks));

        private static IEnumerator CO_ExecuteDelayedChain(float delayInSec, List<Action> callbacks)
        {
            foreach (var cb in callbacks)
            {
                yield return new WaitForSecondsRealtime(delayInSec);
                cb();
            }
        }

        #endregion

        #region FollowCurve

        /// <summary>
        /// Follows given object with given curve at Y axis
        /// </summary>
        /// <param name="target">Transform to be moved</param>
        /// <param name="curve01">Curve to follow</param>
        /// <param name="targetXZ">(Optional) Objects last positions X and Z value</param>
        /// <param name="targetMultiplier">multiplies curve value with this variable</param>
        /// <param name="cb">callback functions for before, during and after positioning</param>
        internal void FollowYCurve(Transform target, AnimationCurve curve01, Vector3 targetXZ,
            float targetMultiplier = 1,
            Action<Vector3>[] cb = null)
        {
            StartCoroutine(FollowYCurveCoroutine(target, curve01, targetXZ, targetMultiplier, cb));
        }

        internal void FollowYCurve(Transform target, AnimationCurve curve01, float targetMultiplier = 1,
            Action<Vector3>[] cb = null)
        {
            StartCoroutine(FollowYCurveCoroutine(target, curve01, target.position, targetMultiplier, cb));
        }

        internal IEnumerator FollowYCurveCoroutine(Transform target, AnimationCurve curve01, Vector3 targetXZ,
            float yMultiplier, Action<Vector3>[] cb = null)
        {
            var startPos = target.position;
            var endPos = new Vector3(targetXZ.x, curve01.LastKey().value, targetXZ.z);
            var time = curve01.keys[curve01.length - 1].time;
            var lerpval = 0f;

            target.position = startPos;
            if (cb != null && cb.Length > 0) cb[0](target.position);
            while (lerpval < time)
            {
                var lerpedx = Mathf.Lerp(startPos.x, endPos.x, lerpval / time);
                var lerpedy = startPos.y + curve01.Evaluate(lerpval) * yMultiplier;
                var lerpedz = Mathf.Lerp(startPos.z, endPos.z, lerpval / time);

                var lerpedPos = new Vector3(lerpedx, lerpedy, lerpedz);

                target.position = lerpedPos;
                if (cb != null && cb.Length > 1) cb[1](target.position);

                lerpval += Time.deltaTime;
                yield return null;
            }

            target.position = endPos;
            if (cb != null && cb.Length == 3) cb[2](target.position);
        }

        #endregion

        #region Animation Functions

        private static float CurrentAnimationTime(Animator animator)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length <= 0) return 0;
            return clipInfo[0].clip.length / stateInfo.speed;
        }

        internal static void PlayWithEvents(Animator animator, string animationName, Action[] cb) =>
            Instance.PlayWithEventsStarter(animator, animationName, cb);

        private void PlayWithEventsStarter(Animator animator, string animationName, Action[] cb) =>
            StartCoroutine(CO_PlayWithEvents(animator, animationName, cb));

        private IEnumerator CO_PlayWithEvents(Animator animator, string animationName, Action[] cb)
        {
            animator.Play(animationName);
            yield return null;

            var animationLength = CurrentAnimationTime(animator);
            var currentTime = 0f;
            var middleReached = false;

            if (cb.Length > 0 && cb[0] != null) cb[0]();
            while (currentTime < animationLength)
            {
                if (currentTime > animationLength / 2 &&
                    !middleReached &&
                    cb.Length > 1 &&
                    cb[1] != null)
                {
                    middleReached = true;
                    cb[1]();
                }

                currentTime += Time.deltaTime;
                yield return null;
            }

            if (cb.Length > 2 && cb[2] != null) cb[2]();
        }

        #endregion

        #region Lerp via Duration

        //--------------------------------------------------------------------------------------------------------------
        // Static Lerps

        internal static void Lerp<T>(T start, T end, float duration = 1,
            Action<T, float>[] callbacks = null) =>
            Instance.LerpStarter(start, end, duration, callbacks);

        internal static void Lerp<T>(T start, T end, float duration = 1,
            Action<T, float> callback = null) =>
            Instance.LerpStarter(start, end, duration, new[] {null, callback, null});

        private void LerpStarter<T>(T start, T end, float duration = 1,
            Action<T, float>[] callbacks = null) =>
            StartCoroutine(CO_Lerp(start, end, duration, callbacks));


        private void LerpStarter<T>(T start, T end, float duration = 1,
            Action<T, float> callback = null) =>
            StartCoroutine(
                CO_Lerp(start, end, duration,
                    new[]
                    {
                        null,
                        callback,
                        null
                    }
                )
            );

        private static IEnumerator CO_Lerp<T>(T start, T end, float duration, IReadOnlyList<Action<T, float>> callbacks)
        {
            var lerpval = 0f;
            var lerpFunc = GetLerpFunc<T>();

            if (callbacks.Count >= 1 && callbacks[0] != null)
                callbacks[0](start, 0);

            lerpFunc(start, end, 0);
            while (lerpval < duration)
            {
                var lerpRatio = lerpval / duration;
                var lerped = lerpFunc(start, end, lerpRatio);

                if (callbacks.Count >= 2 && callbacks[1] != null)
                    callbacks[1](lerped, lerpRatio);

                lerpval += Time.deltaTime;
                yield return null;
            }

            lerpFunc(start, end, 1);

            if (callbacks.Count >= 3 && callbacks[2] != null)
                callbacks[2](end, 1);
        }


        //--------------------------------------------------------------------------------------------------------------
        // Dynamic Lerps

        internal static void Lerp<T>(T start, T end, float duration = 1,
            Func<T, float, (T, T, float)>[] callbacks = null) =>
            Instance.LerpStarter(start, end, duration, callbacks);

        internal static void Lerp<T>(T start, T end, float duration = 1,
            Func<T, float, (T, T, float)> callback = null) =>
            Instance.LerpStarter(start, end, duration, new[] {null, callback, null});


        private void LerpStarter<T>(T start, T end, float duration = 1,
            Func<T, float, (T, T, float)>[] callbacks = null) =>
            StartCoroutine(CO_Lerp(start, end, duration, callbacks));

        private void LerpStarter<T>(T start, T end, float duration = 1,
            Func<T, float, (T, T, float)> callback = null) =>
            StartCoroutine(
                CO_Lerp(start, end, duration,
                    new[]
                    {
                        null,
                        callback,
                        null
                    }
                )
            );

        private static IEnumerator CO_Lerp<T>(T start, T end, float duration,
            IReadOnlyList<Func<T, float, (T, T, float)>> callbacks)
        {
            var dynamicStart = start;
            var dynamicEnd = end;
            var dynamicDuration = duration;
            var lerpval = 0f;

            if (callbacks.Count >= 1 && callbacks[0] != null)
                (dynamicStart, dynamicEnd, dynamicDuration) = callbacks[0](start, 0);

            var lerpFunc = GetLerpFunc<T>();
            lerpFunc(dynamicStart, dynamicEnd, 0);
            while (lerpval < dynamicDuration)
            {
                var lerpRatio = lerpval / dynamicDuration;
                var lerped = lerpFunc(dynamicStart, dynamicEnd, lerpRatio);

                if (callbacks.Count >= 2 && callbacks[1] != null)
                    (dynamicStart, dynamicEnd, dynamicDuration) = callbacks[1](lerped, lerpRatio);

                lerpval += Time.deltaTime;
                yield return null;
            }

            lerpFunc(dynamicStart, dynamicEnd, 1);

            if (callbacks.Count >= 3 && callbacks[2] != null)
                (dynamicStart, dynamicEnd, dynamicDuration) = callbacks[2](end, 1);
        }

        //--------------------------------------------------------------------------------------------------------------

        #endregion

        #region Lerp via Step Size

        //--------------------------------------------------------------------------------------------------------------
        // Static Lerps

        public static void LerpStep<T>(T start, T end, float stepSize,
            Action<T, float>[] callbacks = null) =>
            Instance.LerpStepStarter(start, end, stepSize, callbacks);

        public static void LerpStep<T>(T start, T end, float stepSize, Action<T, float> callback = null) =>
            Instance.LerpStepStarter(start, end, stepSize, new[] {null, callback, null});

        private void LerpStepStarter<T>(T start, T end, float stepSize,
            Action<T, float>[] callbacks = null) =>
            StartCoroutine(CO_LerpStep(start, end, stepSize, callbacks));

        private void LerpStepStarter<T>(T start, T end, float stepSize,
            Action<T, float> callback = null) =>
            StartCoroutine(
                CO_LerpStep(start, end, stepSize,
                    new[]
                    {
                        null,
                        callback,
                        null
                    }
                )
            );

        private static IEnumerator CO_LerpStep<T>(T start, T end, float stepSize,
            IReadOnlyList<Action<T, float>> callbacks)
        {
            if (callbacks.Count >= 1 && callbacks[0] != null)
                callbacks[0](start, 0);

            var lerpFunc = GetLerpFunc<T>();
            var distanceFunc = GetDistanceFunc<T>();


            var remainingDistance = distanceFunc(start, end);
            var completionRatio = 0f;

            lerpFunc(start, end, 0);
            while (completionRatio < 1)
            {
                remainingDistance -= stepSize;

                var totalDistance = distanceFunc(start, end);
                completionRatio = 1 - remainingDistance / totalDistance;

                var lerped = lerpFunc(start, end, completionRatio);

                if (callbacks.Count >= 2 && callbacks[1] != null)
                    callbacks[1](lerped, completionRatio);

                yield return null;
            }

            lerpFunc(start, end, 1);
            if (callbacks.Count >= 3 && callbacks[2] != null)
                callbacks[2](end, 1);
        }
        //--------------------------------------------------------------------------------------------------------------
        // Dynamic Lerps

        public static void LerpStep<T>(T start, T end, float stepSize,
            Func<T, float, (T, T, float)>[] callbacks = null) =>
            Instance.LerpStepStarter(start, end, stepSize, callbacks);

        public static void LerpStep<T>(T start, T end, float stepSize, Func<T, float, (T, T, float)> callback = null) =>
            Instance.LerpStepStarter(start, end, stepSize, new[] {null, callback, null});

        private void LerpStepStarter<T>(T start, T end, float stepSize,
            Func<T, float, (T, T, float)>[] callbacks = null) =>
            StartCoroutine(CO_LerpStep(start, end, stepSize, callbacks));

        private void LerpStepStarter<T>(T start, T end, float stepSize,
            Func<T, float, (T, T, float)> callback = null) =>
            StartCoroutine(
                CO_LerpStep(start, end, stepSize,
                    new[]
                    {
                        null,
                        callback,
                        null
                    }
                )
            );

        private static IEnumerator CO_LerpStep<T>(T start, T end, float stepSize,
            IReadOnlyList<Func<T, float, (T, T, float)>> callbacks)
        {
            // TODO : revise this, doesnt work with dynamic values properly
            var dynamicStart = start;
            var dynamicEnd = end;
            var dynamicStepSize = stepSize;

            if (callbacks.Count >= 1 && callbacks[0] != null)
                (dynamicStart, dynamicEnd, dynamicStepSize) = callbacks[0](dynamicStart, 0);

            var lerpFunc = GetLerpFunc<T>();
            var distanceFunc = GetDistanceFunc<T>();

            var totalDistance = distanceFunc(dynamicStart, dynamicEnd);
            var remainingDistance = distanceFunc(dynamicStart, dynamicEnd);
            var completionRatio = 0f;

            lerpFunc(dynamicStart, dynamicEnd, 0);
            while (completionRatio < 1)
            {
                remainingDistance -= dynamicStepSize;
                completionRatio = 1 - remainingDistance / totalDistance;

                var lerped = lerpFunc(dynamicStart, dynamicEnd, completionRatio);

                // remainingDistance = distanceFunc(lerped, dynamicEnd);

                if (callbacks.Count >= 2 && callbacks[1] != null)
                    (dynamicStart, dynamicEnd, dynamicStepSize) = callbacks[1](lerped, completionRatio);

                yield return null;
            }

            lerpFunc(dynamicStart, dynamicEnd, 1);
            if (callbacks.Count >= 3 && callbacks[2] != null)
                (dynamicStart, dynamicEnd, dynamicStepSize) = callbacks[2](dynamicEnd, 1);
        }

        #endregion

        #region Transform Functions

        //--------------------------------------------------------------------------------------------------------------

        private static bool CheckCollisionViaRay(Vector3 currentPos, Vector3 lastPos, out List<Collider> result)
        {
            if (currentPos == lastPos)
            {
                result = null;
                return false;
            }

            var ray = new Ray(currentPos, lastPos - currentPos);
            var results = new RaycastHit[] { };
            Physics.RaycastNonAlloc(ray, results);
            result = results.Select(x => x.collider).ToList();
            return result.Count > 0;
        }

        //--------------------------------------------------------------------------------------------------------------

        #endregion

        #region Generic Getters

        private static Func<T, T, float, T> GetLerpFunc<T>()
        {
            Delegate functionHolder;
            if (typeof(T) == typeof(float))
                functionHolder = (Func<float, float, float, float>) Mathf.Lerp;
            else if (typeof(T) == typeof(Vector2))
                functionHolder = (Func<Vector2, Vector2, float, Vector2>) Vector2.Lerp;
            else if (typeof(T) == typeof(Vector3))
                functionHolder = (Func<Vector3, Vector3, float, Vector3>) Vector3.Lerp;
            else if (typeof(T) == typeof(Vector4))
                functionHolder = (Func<Vector4, Vector4, float, Vector4>) Vector4.Lerp;
            else if (typeof(T) == typeof(Quaternion))
                functionHolder = (Func<Quaternion, Quaternion, float, Quaternion>) Quaternion.Lerp;
            else if (typeof(T) == typeof(Color))
                functionHolder = (Func<Color, Color, float, Color>) Color.Lerp;
            else if (typeof(T) == typeof(TransformValues))
                functionHolder = (Func<TransformValues, TransformValues, float, TransformValues>) TransformValues.Lerp;
            else
                throw new Exception("Unsupported type");
            return (Func<T, T, float, T>) functionHolder;
        }

        private static Func<T, T, float> GetDistanceFunc<T>()
        {
            Delegate functionHolder;
            if (typeof(T) == typeof(float))
                functionHolder = new Func<float, float, float>((a, b) => Mathf.Abs(a - b));
            else if (typeof(T) == typeof(Vector2))
                functionHolder = (Func<Vector2, Vector2, float>) Vector2.Distance;
            else if (typeof(T) == typeof(Vector3))
                functionHolder = (Func<Vector3, Vector3, float>) Vector3.Distance;
            else if (typeof(T) == typeof(Vector4))
                functionHolder = (Func<Vector4, Vector4, float>) Vector4.Distance;
            else if (typeof(T) == typeof(Quaternion))
                functionHolder = (Func<Quaternion, Quaternion, float>) Quaternion.Angle;
            else if (typeof(T) == typeof(TransformValues))
                functionHolder = (Func<TransformValues, TransformValues, float>) TransformValues.Distance;
            else
                throw new Exception("Unsupported type");
            return (Func<T, T, float>) functionHolder;
        }

        #endregion

        #region Misc

        /// <summary>
        /// Does the given action exactly once and never again.
        /// <param name="action">The action to execute exactly once.</param>
        /// </summary>
        internal static void Once(Action action)
        {
            if (action == null)
                return;
            
            // if this action is new then add it and set it to "not executed"
            if (!_onceActions.ContainsKey(action))
                _onceActions.Add(action, false);
            
            // if action is executed then return
            if(_onceActions[action])
                return;
            
            action();
            _onceActions[action] = true;
        }

        
        /// <summary>
        /// Does the given function everyFrame until stopped with bool returning callback action
        /// </summary>
        /// <param name="action">action to execute every frame</param>
        /// <param name="stopAction">callback action with bool returner to control routine</param>
        /// <returns>Coroutine of function</returns>
        internal static Coroutine ExecuteContinuously(Action action, Func<bool> stopAction)
        {
            return Instance.StartCoroutine(ExecuteContinuouslyRoutine(action, stopAction));
        }
        
        private static IEnumerator ExecuteContinuouslyRoutine(Action action, Func<bool> stopAction)
        {
            while (!stopAction())
            {
                action();
                yield return null;
            }
        }
        
        
        
        #endregion

        #region Debug

        internal void ShowInAnimationCurve(float value, float startVal = 0, float endVal = 10) =>
            curveToShowData.AddKey(new Keyframe(Time.timeSinceLevelLoad, value));
        
        public void Message() => Debug.Log("executed");
        public void Message(string message) => Debug.Log(message);
        public void Message(int message) => Debug.Log(message);
        public void Message(float message) => Debug.Log(message);
        public void Message(bool message) => Debug.Log(message);

        #endregion
    }
}
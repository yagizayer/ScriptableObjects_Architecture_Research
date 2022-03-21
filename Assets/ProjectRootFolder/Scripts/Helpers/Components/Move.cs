using System;
using Helpers;
using Helpers.MetaClasses;
using MyBox;
using UnityEngine;

namespace PirateDefence.Scripts.Helpers.Components
{
    public enum MovementType
    {
        Null = 0,
        Normal = 10,
        PingPong = 20,
        Restart = 30,
        PingPongWithIdle = 40
    }

    public enum EvaluationType
    {
        Duration = 10,
        Speed = 20
    }

    public class Move : MonoBehaviour
    {
        [SerializeField] private MovementType movementType = MovementType.Normal;
        [SerializeField] private EvaluationType evaluationType = EvaluationType.Duration;
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;


        // Evaluation type : Speed
        [
            ConditionalField(nameof(evaluationType), false, EvaluationType.Speed),
            SerializeField,
            Range(0, 300)
        ]
        private float movementSpeed = 10;


        // Evaluation type : Duration
        [
            ConditionalField(nameof(evaluationType), false, EvaluationType.Duration),
            SerializeField,
            Range(0, 15)
        ]
        private float movementDuration = 3;


        // Movement type : PingPongWithIdle
        [
            ConditionalField(nameof(movementType), false, MovementType.PingPongWithIdle),
            SerializeField,
            Range(0.01f, 10)
        ]
        private float waitDuration = 2;

        private Action _movementFunction;
        private TransformValues _startValues;
        private TransformValues _endValues;

        // ------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            _movementFunction = movementType switch
            {
                MovementType.Normal => MovementNormal,
                MovementType.PingPong => MovementPingPong,
                MovementType.Restart => MovementRestart,
                MovementType.PingPongWithIdle => MovementPingPongWithIdle,
                MovementType.Null => () => { /*Debug.LogError("You have to Specify MovementType");*/ },
                _ => throw new ArgumentOutOfRangeException()
            };
            _startValues = new TransformValues(startPosition);
            _endValues = new TransformValues(endPosition);
            _movementFunction();
        }

        // ------------------------------------------------------------------------------------------------------------
        private void MovementNormal()
        {
            switch (evaluationType)
            {
                case EvaluationType.Duration:
                    Functions.Lerp(_startValues, _endValues, movementDuration, SetValues);
                    break;
                case EvaluationType.Speed:
                    Functions.LerpStep(_startValues, _endValues, movementSpeed, SetValues);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MovementPingPong()
        {
            switch (evaluationType)
            {
                case EvaluationType.Duration:
                    PlayPingPongPath();
                    break;
                case EvaluationType.Speed:
                    PlayPingPongPathStep();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MovementRestart()
        {
            switch (evaluationType)
            {
                case EvaluationType.Duration:
                    PlayRestartPath(null, 0);
                    break;
                case EvaluationType.Speed:
                    PlayRestartPathStep(null, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MovementPingPongWithIdle()
        {
            switch (evaluationType)
            {
                case EvaluationType.Duration:
                    PlayPingPongPathWithIdle(null, 0);
                    break;
                case EvaluationType.Speed:
                    PlayPingPongPathWithIdleStep(null, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        // ------------------------------------------------------------------------------------------------------------
        private void SetValues(TransformValues values, float ratio)
        {
            transform.localPosition = values.Position;
            transform.localRotation = values.Rotation;
            transform.localScale = values.Scale;
        }

        #region PingPongPath
        private void PlayPingPongPath()
        {
            var actions = new Action<TransformValues, float>[]
            {
                (v, r) => { },
                SetValues,
                RevertPath
            };
            Functions.Lerp(_startValues, _endValues, movementDuration, actions);
        }

        private void PlayPingPongPathStep()
        {
            var actions = new Action<TransformValues, float>[]
            {
                (v, r) => { },
                SetValues,
                RevertPathStep
            };
            Functions.LerpStep(_startValues, _endValues, movementSpeed, actions);
        }


        private void RevertPath(TransformValues values, float ratio)
        {
            (startPosition, endPosition) = (endPosition, startPosition);
            _startValues = new TransformValues(startPosition);
            _endValues = new TransformValues(endPosition);
            PlayPingPongPath();
        }

        private void RevertPathStep(TransformValues values, float ratio)
        {
            (startPosition, endPosition) = (endPosition, startPosition);
            _startValues = new TransformValues(startPosition);
            _endValues = new TransformValues(endPosition);
            PlayPingPongPathStep();
        }

        #endregion

        #region Restart

        private void PlayRestartPath(TransformValues values, float ratio)
        {
            var actions = new Action<TransformValues, float>[]
            {
                (v, r) => { },
                SetValues,
                PlayRestartPath
            };
            Functions.Lerp(_startValues, _endValues, movementDuration, actions);
        }

        private void PlayRestartPathStep(TransformValues values, float ratio)
        {
            var actions = new Action<TransformValues, float>[]
            {
                (v, r) => { },
                SetValues,
                PlayRestartPathStep
            };
            Functions.LerpStep(_startValues, _endValues, movementSpeed, actions);
        }


        #endregion

        #region PingPongWithIdle
        private void PlayPingPongPathWithIdle(TransformValues values, float ratio)
        {
            var actions = new Action<TransformValues, float>[]
            {
                (v, r) => { },
                SetValues,
                RevertPathWithIdle
            };
            Functions.Lerp(_startValues, _endValues, movementDuration, actions);
        }

        private void PlayPingPongPathWithIdleStep(TransformValues values, float ratio)
        {
            var actions = new Action<TransformValues, float>[]
            {
                (v, r) => { },
                SetValues,
                RevertPathWithIdleStep
            };
            Functions.Lerp(_startValues, _endValues, movementDuration, actions);
        }

        private void RevertPathWithIdle(TransformValues values, float ratio)
        {
            (startPosition, endPosition) = (endPosition, startPosition);
            _startValues = new TransformValues(startPosition);
            _endValues = new TransformValues(endPosition);
            Functions.ExecuteDelayed(waitDuration, () => PlayPingPongPathWithIdle(values, ratio));
        }

        private void RevertPathWithIdleStep(TransformValues values, float ratio)
        {
            (startPosition, endPosition) = (endPosition, startPosition);
            _startValues = new TransformValues(startPosition);
            _endValues = new TransformValues(endPosition);
            Functions.ExecuteDelayed(waitDuration, () => PlayPingPongPathWithIdleStep(values, ratio));
        }



        #endregion
    }
}

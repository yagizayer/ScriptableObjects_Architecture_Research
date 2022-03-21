using System.Collections;
using UnityEngine;

namespace Helpers.Components
{
    public class Rotate : MonoBehaviour
    {
        // TODO : revise this
        [SerializeField] private float _speed = 1f;
        [SerializeField] private SnapAxis _targetAxis = SnapAxis.Y;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);


        private void OnEnable()
        {
            StartCoroutine(Rotating());
        }


        private IEnumerator Rotating()
        {
            var accelerationDuration = _curve.keys[_curve.length - 1].time;
            var lerpVal = 0f;
            var rotateAngles = new Vector3(0, 0, 0);

            while (lerpVal < accelerationDuration)
            {
                var nextRotationSpeed = _curve.Evaluate(lerpVal) * _speed * Time.deltaTime;
                switch (_targetAxis)
                {
                    case SnapAxis.X:
                        rotateAngles = new Vector3(nextRotationSpeed, 0, 0);
                        break;
                    case SnapAxis.Y:
                        rotateAngles = new Vector3(0, nextRotationSpeed, 0);
                        break;
                    case SnapAxis.Z:
                        rotateAngles = new Vector3(0, 0, nextRotationSpeed);
                        break;
                }

                transform.Rotate(rotateAngles);
                lerpVal += Time.deltaTime;
                yield return null;
            }

            while (true)
            {
                transform.Rotate(rotateAngles);
                yield return null;
            }
        }
    }
}

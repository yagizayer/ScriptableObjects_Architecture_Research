using UnityEngine;

namespace Helpers.Components
{
    public class Ocilate : MonoBehaviour
    {
        // TODO : revise this
        [SerializeField] private float _speed = 1f;
        [SerializeField] private SnapAxis _targetAxis = SnapAxis.Y;

        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            var offset = _startPosition;
            switch (_targetAxis)
            {
                case SnapAxis.X:
                    offset.x += Mathf.Sin(Time.time * _speed) * 0.5f;
                    break;
                case SnapAxis.Y:
                    offset.y += Mathf.Sin(Time.time * _speed) * 0.5f;
                    break;
                case SnapAxis.Z:
                    offset.z += Mathf.Sin(Time.time * _speed) * 0.5f;
                    break;
            }

            transform.position = offset;
        }
    }
}

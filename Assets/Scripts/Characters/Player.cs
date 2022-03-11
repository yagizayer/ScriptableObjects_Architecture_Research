using Helpers;
using UnityEngine;

namespace Characters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = .5f;

        public void Move(Vector2 direction)
        {
            transform.position += direction.ToVector3(Vector3Values.XY) * speed;
        }
    }
}
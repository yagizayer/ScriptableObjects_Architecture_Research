using ScriptableObjects.Events.Channels;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Vector2EventChannelSo movementChannel;

        public void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            movementChannel.Raise(new Vector2(horizontal, vertical));
        }
        
    }
}
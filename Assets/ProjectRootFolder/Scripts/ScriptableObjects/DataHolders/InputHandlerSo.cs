using ScriptableObjects.Events.Channels;
using UnityEngine;

namespace ProjectRootFolder.Scripts.ScriptableObjects.DataHolders
{
    [CreateAssetMenu(fileName = "New InputHandlerSo", menuName = "ScriptableObjects/InputHandlerSo")]
    public class InputHandlerSo : ScriptableObject
    {
        [Header("Broadcasting on")] [SerializeField]
        private Vector2EventChannelSo movementEventChannel;

        [SerializeField] private FloatEventChannelSo verticalAxisEventChannel;
        [SerializeField] private BoolEventChannelSo sprintEventChannel;
        [SerializeField] private VoidEventChannelSo detonateEventChannel;


        [Space] [SerializeField] private Vector2EventChannelSo cameraRotateEventChannel;
        public bool MovementInputEnabled { get; private set; } = false;
        public bool CameraInputEnabled { get; private set; } = false;


        public void Update()
        {
            if (MovementInputEnabled)
            {
                var xAxis = Input.GetAxis("Horizontal");
                var yAxis = Input.GetKey(KeyCode.E) ? 1 : Input.GetKey(KeyCode.Q) ? -1 : 0;
                var zAxis = Input.GetAxis("Vertical");
                var sprint = Input.GetKeyDown(KeyCode.LeftShift);
                var detonate = Input.GetKeyDown(KeyCode.Space);

                MoveXZ(new Vector2(xAxis, zAxis));
                MoveY(yAxis);
                Sprint(sprint);
                if (detonate) Detonate();
            }

            if (CameraInputEnabled)
            {
                var xAxis = Input.GetAxis("Mouse X");
                var yAxis = Input.GetAxis("Mouse Y");
                RotateCamera(new Vector2(xAxis, yAxis));
            }
        }

        private void MoveXZ(Vector2 direction)
        {
            if (direction.magnitude > 0)
                movementEventChannel.Raise(direction);
        }

        private void MoveY(float vertical)
        {
            if (vertical != 0)
                verticalAxisEventChannel.Raise(vertical);
        }

        public void Sprint(bool sprint) => sprintEventChannel.Raise(sprint);

        public void Detonate() => detonateEventChannel.Raise();

        public void RotateCamera(Vector2 rot) => cameraRotateEventChannel.Raise(rot);


        public void EnableMovementInput() => MovementInputEnabled = true;

        public void DisableMovementInput() => MovementInputEnabled = false;

        public void EnableCameraInput() => CameraInputEnabled = true;

        public void DisableCameraInput() => CameraInputEnabled = false;

        public void EnableAllInput()
        {
            MovementInputEnabled = true;
            CameraInputEnabled = true;
        }

        public void DisableAllInput()
        {
            MovementInputEnabled = false;
            CameraInputEnabled = false;
        }
    }
}
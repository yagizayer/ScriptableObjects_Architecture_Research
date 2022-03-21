using PirateDefence.Scripts.Helpers.Components;
using UnityEngine;

namespace Helpers.Components
{
    /**
     * Utility component to add to game objects whose events you want forwarded from Unity's message
     * system to standard C# events. Handles all events as of Unity 4.6.1.
     * @author Jackson Dunstan - http://jacksondunstan.com/articles/2922
     */
    public class EventForwarder : MonoBehaviour
    {
        public EventForwarderTarget target;

        public delegate void EventHandler();

        public delegate void EventHandler<T>(T param);

        public delegate void EventHandler<T1, T2>(T1 param1, T2 param2);

        public void Awake()
        {
            target.Awake_();
            AwakeEvent();
        }

        public void Reset()
        {
            target.Reset_();
            ResetEvent();
        }

        public void Start()
        {
            target.Start_();
            StartEvent();
        }

        public void Update()
        {
            target.Update_();
            UpdateEvent();
        }

        public void FixedUpdate()
        {
            target.FixedUpdate_();
            FixedUpdateEvent();
        }

        public void LateUpdate()
        {
            target.LateUpdate_();
            LateUpdateEvent();
        }

        public void OnEnable()
        {
            target.OnEnable_();
            OnEnableEvent();
        }

        public void OnDisable()
        {
            target.OnDisable_();
            OnDisableEvent();
        }

        public void OnDestroy()
        {
            target.OnDestroy_();
            OnDestroyEvent();
        }

        public void OnGUI()
        {
            target.OnGUI_();
            OnGUIEvent();
        }

        public void OnAnimatorIK(int layerIndex)
        {
            target.OnAnimatorIK_(layerIndex);
            OnAnimatorIKEvent(layerIndex);
        }

        public void OnAnimatorMove()
        {
            target.OnAnimatorMove_();
            OnAnimatorMoveEvent();
        }

        public void OnApplicationFocus(bool focusStatus)
        {
            target.OnApplicationFocus_(focusStatus);
            OnApplicationFocusEvent(focusStatus);
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            target.OnApplicationPause_(pauseStatus);
            OnApplicationPauseEvent(pauseStatus);
        }

        public void OnApplicationQuit()
        {
            target.OnApplicationQuit_();
            OnApplicationQuitEvent();
        }

        public void OnAudioFilterRead(float[] data, int channels)
        {
            target.OnAudioFilterRead_(data, channels);
            OnAudioFilterReadEvent(data, channels);
        }

        public void OnBecameInvisible()
        {
            target.OnBecameInvisible_();
            OnBecameInvisibleEvent();
        }

        public void OnBecameVisible()
        {
            target.OnBecameVisible_();
            OnBecameVisibleEvent();
        }

        public void OnCollisionEnter(Collision collision)
        {
            target.OnCollisionEnter_(collision);
            OnCollisionEnterEvent(collision);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            target.OnCollisionEnter2D_(collision);
            OnCollisionEnter2DEvent(collision);
        }

        public void OnCollisionExit(Collision collision)
        {
            target.OnCollisionExit_(collision);
            OnCollisionExitEvent(collision);
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            target.OnCollisionExit2D_(collision);
            OnCollisionExit2DEvent(collision);
        }

        public void OnCollisionStay(Collision collision)
        {
            target.OnCollisionStay_(collision);
            OnCollisionStayEvent(collision);
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            target.OnCollisionStay2D_(collision);
            OnCollisionStay2DEvent(collision);
        }

        public void OnConnectedToServer()
        {
            target.OnConnectedToServer_();
            OnConnectedToServerEvent();
        }

        public void OnControllerColliderHit(ControllerColliderHit hit)
        {
            target.OnControllerColliderHit_(hit);
            OnControllerColliderHitEvent(hit);
        }

        public void OnDrawGizmos()
        {
            target.OnDrawGizmos_();
            OnDrawGizmosEvent();
        }

        public void OnDrawGizmosSelected()
        {
            target.OnDrawGizmosSelected_();
            OnDrawGizmosSelectedEvent();
        }

        public void OnJointBreak(float breakForce)
        {
            target.OnJointBreak_(breakForce);
            OnJointBreakEvent(breakForce);
        }

        public void OnMouseDown()
        {
            target.OnMouseDown_();
            OnMouseDownEvent();
        }

        public void OnMouseDrag()
        {
            target.OnMouseDrag_();
            OnMouseDragEvent();
        }

        public void OnMouseEnter()
        {
            target.OnMouseEnter_();
            OnMouseEnterEvent();
        }

        public void OnMouseExit()
        {
            target.OnMouseExit_();
            OnMouseExitEvent();
        }

        public void OnMouseOver()
        {
            target.OnMouseOver_();
            OnMouseOverEvent();
        }

        public void OnMouseUp()
        {
            target.OnMouseUp_();
            OnMouseUpEvent();
        }

        public void OnMouseUpAsButton()
        {
            target.OnMouseUpAsButton_();
            OnMouseUpAsButtonEvent();
        }

        public void OnParticleCollision(GameObject other)
        {
            target.OnParticleCollision_(other);
            OnParticleCollisionEvent(other);
        }

        public void OnPostRender()
        {
            target.OnPostRender_();
            OnPostRenderEvent();
        }

        public void OnPreCull()
        {
            target.OnPreCull_();
            OnPreCullEvent();
        }

        public void OnPreRender()
        {
            target.OnPreRender_();
            OnPreRenderEvent();
        }

        public void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            target.OnRenderImage_(src, dest);
            OnRenderImageEvent(src, dest);
        }

        public void OnRenderObject()
        {
            target.OnRenderObject_();
            OnRenderObjectEvent();
        }

        public void OnServerInitialized()
        {
            target.OnServerInitialized_();
            OnServerInitializedEvent();
        }

        public void OnTriggerEnter(Collider other)
        {
            target.OnTriggerEnter_(other);
            OnTriggerEnterEvent(other);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            target.OnTriggerEnter2D_(other);
            OnTriggerEnter2DEvent(other);
        }

        public void OnTriggerExit(Collider other)
        {
            target.OnTriggerExit_(other);
            OnTriggerExitEvent(other);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            target.OnTriggerExit2D_(other);
            OnTriggerExit2DEvent(other);
        }

        public void OnTriggerStay(Collider other)
        {
            target.OnTriggerStay_(other);
            OnTriggerStayEvent(other);
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            target.OnTriggerStay2D_(other);
            OnTriggerStay2DEvent(other);
        }

        public void OnValidate()
        {
            target.OnValidate_();
            OnValidateEvent();
        }

        public void OnWillRenderObject()
        {
            target.OnWillRenderObject_();
            OnWillRenderObjectEvent();
        }

        public event EventHandler AwakeEvent = () => { };
        public event EventHandler FixedUpdateEvent = () => { };
        public event EventHandler LateUpdateEvent = () => { };
        public event EventHandler<int> OnAnimatorIKEvent = layerIndex => { };
        public event EventHandler OnAnimatorMoveEvent = () => { };
        public event EventHandler<bool> OnApplicationFocusEvent = focusStatus => { };
        public event EventHandler<bool> OnApplicationPauseEvent = pauseStatus => { };
        public event EventHandler OnApplicationQuitEvent = () => { };
        public event EventHandler<float[], int> OnAudioFilterReadEvent = (data, channels) => { };
        public event EventHandler OnBecameInvisibleEvent = () => { };
        public event EventHandler OnBecameVisibleEvent = () => { };
        public event EventHandler<Collision> OnCollisionEnterEvent = collision => { };
        public event EventHandler<Collision2D> OnCollisionEnter2DEvent = collision => { };
        public event EventHandler<Collision> OnCollisionExitEvent = collision => { };
        public event EventHandler<Collision2D> OnCollisionExit2DEvent = collision => { };
        public event EventHandler<Collision> OnCollisionStayEvent = collision => { };
        public event EventHandler<Collision2D> OnCollisionStay2DEvent = collision => { };
        public event EventHandler OnConnectedToServerEvent = () => { };
        public event EventHandler<ControllerColliderHit> OnControllerColliderHitEvent = hit => { };
        public event EventHandler OnDestroyEvent = () => { };
        public event EventHandler OnDisableEvent = () => { };
        public event EventHandler OnDrawGizmosEvent = () => { };
        public event EventHandler OnDrawGizmosSelectedEvent = () => { };
        public event EventHandler OnEnableEvent = () => { };
        public event EventHandler OnGUIEvent = () => { };
        public event EventHandler<float> OnJointBreakEvent = breakForce => { };
        public event EventHandler OnMouseDownEvent = () => { };
        public event EventHandler OnMouseDragEvent = () => { };
        public event EventHandler OnMouseEnterEvent = () => { };
        public event EventHandler OnMouseExitEvent = () => { };
        public event EventHandler OnMouseOverEvent = () => { };
        public event EventHandler OnMouseUpEvent = () => { };
        public event EventHandler OnMouseUpAsButtonEvent = () => { };
        public event EventHandler<GameObject> OnParticleCollisionEvent = other => { };
        public event EventHandler OnPostRenderEvent = () => { };
        public event EventHandler OnPreCullEvent = () => { };
        public event EventHandler OnPreRenderEvent = () => { };
        public event EventHandler<RenderTexture, RenderTexture> OnRenderImageEvent = (src, dest) => { };
        public event EventHandler OnRenderObjectEvent = () => { };
        public event EventHandler OnServerInitializedEvent = () => { };
        public event EventHandler<Collider> OnTriggerEnterEvent = other => { };
        public event EventHandler<Collider2D> OnTriggerEnter2DEvent = other => { };
        public event EventHandler<Collider> OnTriggerExitEvent = other => { };
        public event EventHandler<Collider2D> OnTriggerExit2DEvent = other => { };
        public event EventHandler<Collider> OnTriggerStayEvent = other => { };
        public event EventHandler<Collider2D> OnTriggerStay2DEvent = other => { };
        public event EventHandler OnValidateEvent = () => { };
        public event EventHandler OnWillRenderObjectEvent = () => { };
        public event EventHandler ResetEvent = () => { };
        public event EventHandler StartEvent = () => { };
        public event EventHandler UpdateEvent = () => { };
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PirateDefence.Scripts.Helpers.Components
{
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(RectTransform))]
    public sealed class FpsCounter : MonoBehaviour
    {
        [Tooltip("How many times per second should the FPS be updated?")] [SerializeField, Range(1, 60)]
        private int updateInterval = 1;

        private Text _text;
        private int _frames;

        private void Start() => Initialize();


        private void Initialize()
        {
            _text = GetComponent<Text>();
            StartCoroutine(UpdateFps());
        }


        private IEnumerator UpdateFps()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / updateInterval);
                _text.text = $"FPS: {_frames}";
                _frames = 0;
            }
        }

        private void Update() => _frames++;
    }
}
using System;
using System.Collections.Generic;
using ScriptableObjects.Events.Channels;

namespace ScriptableObjects
{
    public interface IInputHandler
    {
        Vector2EventChannelSo TouchStartedEventChannel { get; }
        Vector2EventChannelSo TouchMovedEventChannel { get; }
        Vector2EventChannelSo TouchStationaryEventChannel { get; }
        Vector2EventChannelSo TouchEndedEventChannel { get; }
        bool InputEnabled { get; }
        void Update();
        void EnableInput();
        void DisableInput();
    }
}
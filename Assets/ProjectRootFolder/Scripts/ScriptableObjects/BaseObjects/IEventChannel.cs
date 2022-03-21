using System;

namespace ScriptableObjects
{
    public interface IEventChannel
    {
    }
    public interface IEventChannelBase : IEventChannel
    {
        public Action OnEventRaised { get; set; }
        public void Raise();
    }

    public interface IEventChannelBase<T>  : IEventChannel
    {
        public Action<T> OnEventRaised { get; set; }
        public void Raise(T param);
    }

    public interface IEventChannelBase<T1, T2>  : IEventChannel
    {
        public Action<T1, T2> OnEventRaised { get; set; }
        public void Raise(T1 param1, T2 param2);
    }
}
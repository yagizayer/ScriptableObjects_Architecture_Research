using System;
using System.Collections.Generic;

namespace ScriptableObjects
{
    public interface ISave<T>
    {
        List<T> Data { get; set; }
        
        void Save(List<T> data);
        
        List<T> Load();
    }
}
using System.Collections.Generic;

namespace Services.SaveLoad
{
    public class SaveLoadListener
    {
        private readonly List<IReadListener> _listeners = new();

        public void AddListener(IReadListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IReadListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Read(ProgressData data)
        {
            foreach (IReadListener listener in _listeners) 
                listener.Read(data);
        }

        public void Write(ProgressData data)
        {
            foreach (IReadListener it in _listeners)
            {
                if (it is IWriteListener listener)
                    listener.Write(data);
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.SaveLoad
{
    public class SaveLoadComposite : MonoBehaviour, IWriteListener
    {
        [Inject]
        private SaveLoadListener _listener;

        [InjectLocal] 
        private List<IReadListener> _listeners;

        private void Start()
        {
            _listener.AddListener(this);
        }

        private void OnDestroy()
        {
            _listener.RemoveListener(this);
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
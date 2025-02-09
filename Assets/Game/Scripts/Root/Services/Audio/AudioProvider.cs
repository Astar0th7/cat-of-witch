using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Root.Services.Audio
{
    public class AudioProvider : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private List<AudioSource> _sounds;

        public void PlayMusic(AudioClip clip)
        {
            _music.clip = clip;
            _music.loop = true;
            _music.Play();
        }

        public void PlaySoundOneShot(AudioClip clip)
        {
            AudioSource soundSource = _sounds.FirstOrDefault(x => x.isPlaying == false);
            if (soundSource == null)
            {
                Debug.LogWarning("Нет доступных для проигрывания звуков объектов");
                return;
            }
            
            soundSource.PlayOneShot(clip);
        }
    }
}
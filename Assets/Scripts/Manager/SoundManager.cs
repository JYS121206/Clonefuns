using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCont];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public AudioSource Play(string path, float volume = 1.0f, Define.Sound type = Define.Sound.Effect)
    {
        AudioSource audioSources;

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null) { Debug.Log($"AudioClip Missing! ({path})"); return null; }
            else
            {
                audioSources = _audioSources[(int)Define.Sound.Bgm];

                if (audioSources.isPlaying)
                    audioSources.Stop();

                audioSources.volume = volume;
                audioSources.clip = audioClip;
                audioSources.Play();
            }

        }
        else
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null) { Debug.Log($"AudioClip Missing! ({path})"); return null; }
            else
            {
                audioSources = _audioSources[(int)Define.Sound.Effect];
                audioSources.volume = volume;
                audioSources.PlayOneShot(audioClip);
            }
        }
        return audioSources;
    }

    AudioClip GetOrAddAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip) == false)
        {
            audioClip = Managers.Resource.Load<AudioClip>($"Sounds/{path}");
            _audioClips.Add(path, audioClip);
        }
        return audioClip;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
}
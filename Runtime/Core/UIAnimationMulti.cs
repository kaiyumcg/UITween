using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExt;

namespace UITween
{
    internal enum UIAnimationMultiMode
    {
        Playlist = 0,
        PlaylistLoop = 1,
        Shuffle = 2,
        PlayAtOnce = 3,
        PlayAtOnceLoop = 4
    }
    [AddComponentMenu("Kaiyum/UITween/UI Animation Multiple")]
    public sealed class UIAnimationMulti : UIAnimation
    {
        [SerializeField] List<UIAnimationAsset> animationFiles;
        [SerializeField] UIAnimationMultiMode mode = UIAnimationMultiMode.Playlist;
        public override void Play(Action OnComplete = null, TagComparer tagComparer = TagComparer.And, params string[] tags)
        {
            if (handle == null) { handle = new AnimHandle(this); }
            var files = ValidateAgainstTags(animationFiles, tagComparer, tags);
            if (!files.ExIsValid()) { OnComplete?.Invoke(); return; }

            Coroutine cor = null;
            if (mode == UIAnimationMultiMode.PlayAtOnce || mode == UIAnimationMultiMode.PlayAtOnceLoop)
            {
                cor = StartCoroutine(PlayAtOnce(OnComplete));
            }
            else if (mode == UIAnimationMultiMode.Playlist || mode == UIAnimationMultiMode.PlaylistLoop)
            {
                cor = StartCoroutine(Playlist(OnComplete));
            }
            else if (mode == UIAnimationMultiMode.Shuffle)
            {
                cor = StartCoroutine(Shuffle());
            }

            if (cor != null)
            {
                handle.AddCoroutine(cor);
            }
            IEnumerator Playlist(Action OnComplete)
            {
                while (true)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];
                        var done = false;
                        file.Play(this, handle, () => { done = true; });
                        while (!done) { yield return null; }
                    }
                    if (mode == UIAnimationMultiMode.Playlist) { break; }
                    yield return null;
                }
                OnComplete?.Invoke();
            }
            IEnumerator PlayAtOnce(Action OnComplete)
            {
                while (true)
                {
                    var doneCount = 0;
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];
                        file.Play(this, handle, () => { doneCount++; });
                    }
                    while (doneCount < files.Count) { yield return null; }
                    if (mode == UIAnimationMultiMode.PlayAtOnce) { break; }
                    yield return null;
                }
                OnComplete?.Invoke();
            }
            IEnumerator Shuffle()
            {
                while (true)
                {
                    var id = UnityEngine.Random.Range(0, files.Count);
                    var file = files[id];
                    var done = false;
                    file.Play(this, handle, () => { done = true; });
                    while (!done) { yield return null; }
                }
            }
        }
        public override void Stop()
        {
            base.Stop();
            animationFiles.ExForEachSafe((i) =>
            {
                i.OnStop(this);
            });
        }
    }
}
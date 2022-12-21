using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UITween
{
    internal enum UIAnimationSingleMode
    {
        Once = 0,
        Loop = 1
    }
    [AddComponentMenu("Kaiyum/UITween/UI Animation Single")]
    public sealed class UIAnimationSingle : UIAnimation
    {
        [SerializeField] UIAnimationAsset animationFile;
        [SerializeField] UIAnimationSingleMode mode = UIAnimationSingleMode.Once;
        public override void Play(Action OnComplete = null, TagComparer tagComparer = TagComparer.And, params string[] tags)
        {
            if (handle == null) { handle = new AnimHandle(this); }
            var valid = ValidateAgainstTags(animationFile, tagComparer, tags);
            if (!valid) { OnComplete?.Invoke(); return; }

            if (mode == UIAnimationSingleMode.Once)
            {
                animationFile.Play(this, handle, OnComplete);
            }
            else
            {
                var cor = StartCoroutine(PlayCOR());
                handle.AddCoroutine(cor);
                IEnumerator PlayCOR()
                {
                    while (true)
                    {
                        var done = false;
                        animationFile.Play(this, handle, () => { done = true; });
                        while (!done) { yield return null; }
                        yield return null;
                    }
                }
            }
        }
        public override void Stop()
        {
            base.Stop();
            animationFile.OnStop(this);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityExt;

namespace UITween
{
    public class AnimHandle
    {
        List<Coroutine> coroutines;
        MonoBehaviour callerContext;
        public AnimHandle(MonoBehaviour callerContext)
        {
            this.callerContext = callerContext;
        }
        public void AddCoroutine(Coroutine coroutine)
        {
            if (coroutines == null) { coroutines = new List<Coroutine>(); }
            coroutines.Add(coroutine);
        }
        public void StopAll()
        {
            coroutines.ExForEachSafeCustomClass((cor) =>
            {
                callerContext.StopCoroutine(cor);
            });
            coroutines = new List<Coroutine>();
        }
    }

    public enum TagComparer
    {
        And = 0,
        Or = 1
    }

    public abstract class UIAnimation : MonoBehaviour
    {
        [SerializeField] bool playOnEnable = true;
        [SerializeField] protected UnityEvent OnComplete;
        protected AnimHandle handle = null;

        private void OnEnable()
        {
            if (playOnEnable)
            {
                Play();
            }
        }
        public abstract void Play(Action OnComplete = null, TagComparer tagComparer = TagComparer.And, params string[] tags);

        protected List<UIAnimationAsset> ValidateAgainstTags(List<UIAnimationAsset> inputFiles, TagComparer comparer, string[] tagsToCompare)
        {
            List<UIAnimationAsset> result = inputFiles;
            if (tagsToCompare.ExIsValid())
            {
                result = new List<UIAnimationAsset>();
                inputFiles.ExForEachSafe((i) =>
                {
                    if (comparer == TagComparer.And)
                    {
                        if (i.Tags.HasAll(tagsToCompare))
                        {
                            result.Add(i);
                        }
                    }
                    else if (comparer == TagComparer.Or)
                    {
                        if (i.Tags.HasAny(tagsToCompare))
                        {
                            result.Add(i);
                        }
                    }
                });
            }
            return result;
        }
        protected bool ValidateAgainstTags(UIAnimationAsset inputFile, TagComparer comparer, string[] tagsToCompare)
        {
            var valid = true;
            if (tagsToCompare.ExIsValid())
            {
                if (comparer == TagComparer.And && !inputFile.Tags.HasAll(tagsToCompare))
                {
                    valid = false;
                }
                else if (comparer == TagComparer.Or && !inputFile.Tags.HasAny(tagsToCompare))
                {
                    valid = false;
                }
            }
            return valid;
        }

        /// <summary>
        /// Stop all animation
        /// </summary>
        public virtual void Stop()
        {
            if (handle != null)
            {
                handle.StopAll();
            }
        }
    }
}
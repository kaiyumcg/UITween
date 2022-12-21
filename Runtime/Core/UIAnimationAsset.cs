using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UITween
{
    public abstract class UIAnimationAsset : ScriptableObject
    {
        [SerializeField] List<string> tags;
        internal List<string> Tags { get { return tags; } }
        protected internal abstract void Play(UIAnimation callerContext, AnimHandle handle, Action OnComplete);
        protected internal abstract void OnStop(UIAnimation callerContext);
    }
}
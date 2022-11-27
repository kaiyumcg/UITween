using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
//using DG.Tweening;
using UnityEngine.UI;
using UnityExt;

namespace UITween
{
    public class Tween { }

    public class AnimHandle
    {
        List<Tween> tweens;
        List<Coroutine> coroutines;
        MonoBehaviour script;
        public AnimHandle(MonoBehaviour script)
        {
            this.script = script;
        }
        public void AddTween(Tween tween)
        {
            if (tweens == null) { tweens = new List<Tween>(); }
            tweens.Add(tween);
        }
        public void AddCoroutine(Coroutine coroutine)
        {
            if (coroutines == null) { coroutines = new List<Coroutine>(); }
            coroutines.Add(coroutine);
        }
        public void StopAll()
        {
            tweens.ExForEach((tw) =>
            {
                //tw.ExResetDT();
            });
            tweens = new List<Tween>();

            coroutines.ExForEach((cor) =>
            {
                script.StopCoroutine(cor);
            });
            coroutines = new List<Coroutine>();
        }
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

        /// <summary>
        /// Play animation. Tags are not considered if it is a single asset type
        /// </summary>
        /// <param name="OnComplete">Completion callback</param>
        /// <param name="tags">Ignored if NOT a list of animation asset</param>
        public abstract void Play(Action OnComplete = null, params string[] tags);

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
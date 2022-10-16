using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UITween
{
    [CreateAssetMenu(fileName = "scaleTween", menuName = "UITween/Object/Create Scale Tween", order = 1)]
    public class ScaleTweener : UIAnimationAsset
    {
        [SerializeField] bool customScale = false;
        [SerializeField] float forceInitScaleAmount = 1.0f;

        protected internal override void Play(UIAnimation script, AnimHandle handle, System.Action OnComplete)
        {
            var cor = script.StartCoroutine(TapToStartTweenPlay(script.transform));
            handle.AddCoroutine(cor);
            IEnumerator TapToStartTweenPlay(Transform tr)
            {
                var tweenSize = 0.02f;
                var tweenDuration = 0.4f;
                while (true)
                {
                    var scaleUp = false;
                    var sc = customScale ? Vector3.one * forceInitScaleAmount : Vector3.one;
                    var tw1 = tr.DOScale(sc * (1f + tweenSize), tweenDuration).OnComplete(() =>
                    {
                        scaleUp = true;
                    }).SetEase(Ease.Linear);
                    handle.AddTween(tw1);
                    while (scaleUp == false)
                    {
                        yield return null;
                    }
                    var scaleDown = false;

                    var tw2 = tr.DOScale(sc * (1f - tweenSize), tweenDuration).OnComplete(() =>
                    {
                        scaleDown = true;
                    }).SetEase(Ease.Linear);
                    handle.AddTween(tw2);
                    while (scaleDown == false)
                    {
                        yield return null;
                    }
                    yield return null;
                }
            }
        }

        protected internal override void OnStop(UIAnimation script)
        {
            script.transform.localScale = customScale ? Vector3.one * forceInitScaleAmount : Vector3.one;
        }
    }
}
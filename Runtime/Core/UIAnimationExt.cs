using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UITween
{
    internal static class UIAnimationExt
    {
        internal static bool HasAll(this List<string> list, string[] items)
        {
            var hasIt = true;
            if (items != null && items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item)) { continue; }
                    if (list.Contains(item) == false)
                    {
                        hasIt = false;
                        break;
                    }
                }
            }
            else
            {
                hasIt = false;
            }
            return hasIt;
        }
        internal static bool HasAny(this List<string> list, string[] items)
        {
            var hasIt = false;
            if (items != null && items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item)) { continue; }
                    if (list.Contains(item))
                    {
                        hasIt = true;
                        break;
                    }
                }
            }
            return hasIt;
        }
    }
}
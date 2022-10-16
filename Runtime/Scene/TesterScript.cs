using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UITween;

public class TesterScript : MonoBehaviour
{
    [SerializeField] List<UIAnimation> anims;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var a in anims)
            {
                a.Play(() =>
                {
                    Debug.Log(a.gameObject.name + " has finished its UI Animation");
                });
            }
        }
    }
}
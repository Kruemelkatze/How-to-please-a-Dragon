using System;
using System.Collections;
using System.Collections.Generic;
using Prime31.TransitionKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class SceneTransition : MonoBehaviour
{
    public int NextSceneIndex = -1;

    public bool Transitioning;
    public float Duration = 0.7f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Transitioning && Input.GetKeyDown(KeyCode.Space))
        {
            Transitioning = true;
            TransitionToScene(NextSceneIndex, Duration);
        }
    }

    public static void TransitionToScene(int sceneIndex, float duration = 0.7f)
    {
//        if (AudioControl.Instance != null)
//        {
//            AudioControl.Instance.PlaySound("change_scene");
//            Object value;
//            if (AudioControl.Instance.SoundClips.TryGetValue("change_scene", out value))
//            {
//                var audioLength = ((AudioClip) value).length;
//                duration = Math.Max(duration, audioLength);
//            }
//        }

        if (sceneIndex < 0)
        {
            Debug.LogError("Scene unknown!");
            return;
        }

        var transition = new Prime31.TransitionKit.FadeTransition()
        {
            nextScene = sceneIndex,
            duration = duration,
            fadeToColor = Color.black,
        };

        TransitionKit.instance.transitionWithDelegate(transition);
    }
}
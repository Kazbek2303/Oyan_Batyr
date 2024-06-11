using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public string nextSceneName; // Name of the next scene to load

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component not assigned!");
            return;
        }

        // Register callback for when the video reaches the end
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Load the next scene when the video ends
        SceneManager.LoadScene(nextSceneName);
    }
}

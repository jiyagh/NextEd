using UnityEngine;
using UnityEngine.Video;

public class VideoAutoPlay : MonoBehaviour
{
    void Start()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "video.mp4");
        vp.Play();
    }
}
using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ensure player capsule ka tag "Player" hai
        {
            videoPlayer.Play();
        }
    }
}
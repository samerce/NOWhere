using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScreen : MonoBehaviour {
    
    public RenderTexture targetRenderTexture;
    private VideoPlayer videoPlayer;
    

    private void Start() {
        videoPlayer = GetComponent<VideoPlayer>();
        
        videoPlayer.playOnAwake = true;

        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = targetRenderTexture;
        //videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();

        //videoPlayer.url = Resources.Load("Videos/merman.mp4").ToString();

        videoPlayer.isLooping = true;

        videoPlayer.Play();
    }



}

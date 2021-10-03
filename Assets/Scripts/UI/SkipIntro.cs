using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SkipIntro : MonoBehaviour
{
    public LevelLoader LevelLoader;
    [SerializeField] VideoPlayer video_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            video_player.Stop();
            LevelLoader.LoadNextLevel();
        }
    }
}

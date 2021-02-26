using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Audio;

#region Requirements
[RequireComponent(typeof(AudioSource))]
#endregion

public class AudioSystemVer1 : MonoBehaviour
{
    #region Variables
    public GameObject[] songList; //Tracklist

    //Speakers
    private AudioSource source;

    //Determines the song
    private int currentTrack;

    //Length of the song
    private int fullLength;

    //Current volume
    private float currentVolume;

    //Temp for volume
    private float volTemp;

    //current playtime
    private int playTime;

    //current seconds
    private int seconds;

    //current minutes
    private int minutes;
    #endregion

    #region Text References
    [Header("Image Reference")]
    public Image albumCover;

    [Header("Text References")]
    //Animated
    public Text clipTitleText;

    //NonAnimated
    public Text clipTitleText2;

    //music clip time
    public Text clipTimeText;
    #endregion

    #region Music Player
    // Start is called before the first frame update
    void Start()
    {
        
        source = GetComponent<AudioSource>();
        currentVolume = source.volume;
        //play music
    }

    public void PlayMusic() //local
    {
        if (source.isPlaying)
        {
            return;
        }

        currentTrack--;
        if (currentTrack < 0)
        {
            currentTrack = songList.Length - 1;
        }

        StartCoroutine("WaitForMusicIsEnd");
        clipTitleText.GetComponent<Animator>().Play("MusicTitle");
    }

    IEnumerator WaitForMusicIsEnd()
    {
        while (source.isPlaying)
        {
            playTime = (int)source.time;
            ShowPlayTime();
            yield return null;
        }
        NextTitle();
    }
    #endregion

    #region Music Player UI Methods
    public void NextTitle() //add next song into queue
    {
        source.Stop();
        currentTrack++;

        if (currentTrack > songList.Length - 1)
        {
            currentTrack = 0;
        }
        source.clip = songList[currentTrack].GetComponent<Song>().song;
        source.Play();

        //shows title
        ShowCurrentTitle();

        //sets song image
        ShowCurrentAlbumCover();

        StartCoroutine("WaitForMusicIsEnd");
        clipTitleText.GetComponent<Animator>().Play("MusicTitle");
    }

    public void PreviousTitle() //add previous song into queue
    {
        source.Stop();
        currentTrack--;

        if (currentTrack < 0)
        {
            currentTrack = songList.Length - 1;
        }
        source.clip = songList[currentTrack].GetComponent<Song>().song;
        source.Play();

        //show title
        ShowCurrentTitle();

        //sets song image
        ShowCurrentAlbumCover();

        StartCoroutine("WaitForMusicIsEnd");
        clipTitleText.GetComponent<Animator>().Play("MusicTitle");
    }

    public void StopMusic() //change to local pause in server communication
    {
        StopCoroutine("WaitForMusicIsEnd");
        source.Stop();
    }

    public void MuteMusic()
    {
        source.mute = !source.mute;
    }

    public void VolumeUp()
    {
        source.volume = currentVolume + 0.1f;
        currentVolume = source.volume;
    }

    public void VolumeDown()
    {
        source.volume = currentVolume - 0.1f;
        currentVolume = source.volume;
    }

    void ShowCurrentTitle()
    {
        clipTitleText.text = "♪ " + songList[currentTrack].GetComponent<Song>().songName;
        fullLength = (int)source.clip.length;
        clipTitleText2.text = songList[currentTrack].GetComponent<Song>().songName;

    }

    void ShowCurrentAlbumCover()
    {
        albumCover.GetComponent<Image>().sprite = songList[currentTrack].GetComponent<Song>().songImage;
    }

    void ShowPlayTime()
    {
        seconds = playTime % 60;
        minutes = (playTime / 60) % 60;
        clipTimeText.text = minutes + ":" + seconds.ToString("D2") + "/" + ((fullLength / 60) % 60) + ":" + (fullLength % 60).ToString("D2");
    }
    #endregion
}



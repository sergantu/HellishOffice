using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]public class Audio
{
    #region Private_Variables

    //ссылка на источник звука для воспроизведения звуков
    private AudioSource sourceSFX;

    //ссылка на источник звука для воспроизведения музыки
    private AudioSource sourceMusic;

    //ссылка на источник звука для воспроизведения звуков со случайной частотой
    private AudioSource sourceRandomPitchSFX;

    private AudioSource sourceOkr;

    //громкость музыки
    private float musicVolume = 1f;

    //громкость звуков 
    private float sfxVolume = 1f;

    //массив звуков
    [SerializeField] private AudioClip[] sounds;

    //Звук по умолчанию, на случай, если в массиве отсутствует требуемый
    [SerializeField] private AudioClip defaultClip;

    //музыка для главного меню
    [SerializeField] private AudioClip menuMusic;

    //музыка для игры на уровнях
    [SerializeField] private AudioClip gameMusic;

    public AudioSource SourceSFX { get => sourceSFX; set => sourceSFX = value; }
    public AudioSource SourceMusic { get => sourceMusic; set => sourceMusic = value; }
    public AudioSource SourceRandomPitchSFX { get => sourceRandomPitchSFX; set => sourceRandomPitchSFX = value; }
    public AudioSource SourceOkr { get => sourceOkr; set => sourceOkr = value; }
    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = value;
            SourceMusic.volume = musicVolume;
        }
    }
    public float SfxVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = value;
            SourceSFX.volume = sfxVolume;
            SourceOkr.volume = sfxVolume;
        }
    }

    #endregion

    /// <summary>
    /// Поиск звука в массиве
    /// </summary>
    /// <param name="clipName">Имя звука</param>
    /// <returns>Звук. Если звук не найден, возвращается значение переменной defaultClip</returns>
    private AudioClip GetSound( string clipName )
    {
        for ( int i = 0; i < sounds.Length; i++ )
        {
            if ( sounds[i].name == clipName )
            {
                return sounds[ i ];
            }
        }

        return defaultClip;
    }

    ///<summary>
    ///Воспроизведение звука из массива
    /// </summary>
    /// <param name="clipName"> Имя звука </param>

    public void PlaySFX(string clipName)
    {
        SourceSFX.loop = false;
        SourceSFX.clip = GetSound(clipName);
        SourceSFX.volume = SfxVolume;
        SourceSFX.Play();

    }

    public void PlaySoundLoop(string clipName)
    {
        if (!SourceSFX.isPlaying)
        {
            SourceSFX.clip = GetSound(clipName);
            SourceSFX.volume = SfxVolume;
            SourceSFX.loop = true;
            SourceSFX.Play();
        }
            
    }

    public void StopSoundLoop(string clipName)
    {
        if (SourceSFX.clip != null && SourceSFX.clip.name == clipName)
        {
            SourceSFX.clip = null;
            SourceSFX.Stop();
        }
    }

    public void StopAllSfx()
    {
        if (SourceSFX.clip != null)
        {
            SourceSFX.Stop();
            SourceSFX.clip = null;
        }
    }

    ///<summary>
    ///Воспроизведение звука из массива со случайно частотой
    /// </summary>
    /// <param name="clipName">Имя звука</param>
    public void PlaySoundRandomPitch( string clipName )
    {
        SourceRandomPitchSFX.pitch = Random.Range( 0.7f, 1.3f );
        SourceRandomPitchSFX.PlayOneShot( GetSound(clipName), SfxVolume );
    }

    ///<summary>
    ///Воспроизведение музыки
    /// </summary>
    /// <param name="menu">Для главного меню</param>
    public void PlayMusic()
    {
        SourceMusic.clip = GetSound("RABOTAI");
        SourceMusic.volume = MusicVolume;
        SourceMusic.loop = true;
        SourceMusic.Play();
    }

    public void PlayOkr()
    {
        SourceOkr.clip = GetSound("winter");
        SourceOkr.volume = SfxVolume;
        SourceOkr.loop = true;
        SourceOkr.Play();
    }

}

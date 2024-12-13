using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 0번 : 타이틀 음악 / 1번 : 메인 음악 / 2번 : 인게임 음악
    /// </summary>
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_clip;

    /// <summary>
    /// 사운드 오디오 소스?
    /// </summary>
    [SerializeField] private GameObject m_sfxSource;

    /// <summary>
    /// UI 사운드
    /// </summary>
    [SerializeField] private AudioSource m_soundSource;

    private void Awake()    
    {
        m_audioSource = GetComponent<AudioSource>();
        m_soundSource = m_sfxSource.GetComponent<AudioSource>();
    }

    public void PlayBGM(SceneType.TYPE argType)
    {
        switch (argType)
        {
            case SceneType.TYPE.Title:
                m_audioSource.clip = m_clip[0];
                m_audioSource.Play();
                break;
            case SceneType.TYPE.Main:
                m_audioSource.Stop();
                m_audioSource.clip = m_clip[1];
                m_audioSource.Play();
                break;
            default:
                m_audioSource.Stop();
                m_audioSource.clip = m_clip[2];
                m_audioSource.Play();
                break;
        }
    }

    public void UITOuch()
    {
        m_soundSource.Play();
    }

    public void MuteSFX()
    {
        m_soundSource.mute = true;
    }

    public void MuteBgm()
    {
        m_audioSource.mute = true;
    }

    public void NoMuteBgm()
    {
        m_audioSource.mute = false;

    }

    public void NoMuteSFX()
    {
        m_soundSource.mute = false;
    }
}

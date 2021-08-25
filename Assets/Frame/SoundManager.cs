//TODO仅供参考，还需优化
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager : MonoBehaviour
{
  /// <summary>
  /// 用户储存Key 背景音乐
  /// </summary>
  private const string PLAY_BGM = "PlayBGM";
  /// <summary>
  /// 用户储存Key 
  /// </summary>
  private const string PLAY_EFFECT = "PlayEffect";

  /// <summary>
  /// 文件路径
  /// </summary>
  private static string SoundPath = "Sound/{0:s}";


  /// <summary>
  /// 背景音乐播放器
  /// </summary>
  private AudioSource bgmAS = null;
  /// <summary>
  /// 音效播放器列表
  /// </summary>
  private List<AudioSource> effectASList = null;

  /// <summary>
  /// 循环播放不打断音效播放器列表
  /// </summary>
  private Dictionary<string, AudioSource> loopASList = null;


  /// <summary>
  /// 声音片段
  /// </summary>
  private Dictionary<string, AudioClip> audioClipArray = null;


  //同步锁
  public readonly object _syncObject = new object();



  /// <summary>
  /// 是否播放背景音乐 >=1播放 <=0不播放
  /// </summary>
  private static bool _bgmState = true;
  /// <summary>
  /// 是否播放音效 >=1播放 <=0不播放
  /// </summary>
  private static bool _effectState = true;

  /// <summary>
  /// bgm全局音量
  /// </summary>
  public static float bgmVolume = 0.2f;



  private static SoundManager _manage;
  public static SoundManager Ins
  {
    get
    {
      if(_manage == null)
      {
        GameObject go = new GameObject();
        go.name = "SoundManager";
        DontDestroyOnLoad(go);

        _manage = go.AddComponent<SoundManager>();
      }

      return _manage;
    }
  }

  void Awake ()
  {
    loopASList = new Dictionary<string, AudioSource>();
    effectASList = new List<AudioSource> ();
    audioClipArray = new Dictionary<string, AudioClip> ();

    //创建背景音乐播放器
    bgmAS = createAudioSource ();
    bgmAS.playOnAwake = false;
    bgmAS.loop = true;

    //读取音乐播放状态
    BGMState = PlayerPrefs.GetInt (PLAY_BGM, 1) == 1;
    EffectState = PlayerPrefs.GetInt (PLAY_EFFECT, 1) == 1;

    //默认停止背景音乐播放
    bgmAS.Stop();

    //设置初始全局音量
    bgmAS.volume = bgmVolume;

    _manage = this;
  }

  void OnDestroy ()
  {
    _manage = null;
  }

  /// <summary>
  /// 初始化
  /// </summary>
  public void init()
  {
      if(_manage == null)
      {
        GameObject go = new GameObject();
        go.name = "SoundManager";
        DontDestroyOnLoad(go);

        _manage = go.AddComponent<SoundManager>();
      }
  }

  /// <summary>
  /// 背景音乐播放状态
  /// </summary>
  public bool BGMState
  {
    get
    {
      lock (_syncObject)
      {
        return _bgmState;
      }
    }
    set
    {
      lock (_syncObject)
      {
        _bgmState = value;

        //是否启用播放器
        bgmAS.enabled = _bgmState;

        //是否播放音乐
        if (_bgmState)
        {
          bgmAS.Play ();
        } 
        else 
        {
          bgmAS.Stop ();
        }

        //储存状态
        PlayerPrefs.GetInt (PLAY_BGM, _bgmState ? 1 : 0);
      }
    }
  }


  /// <summary>
  /// 音效播放状态
  /// </summary>
  public bool EffectState
  {
    get{ return _effectState; }
    set
    {
      _effectState = value;

      for(int i = 0; i < effectASList.Count; i++)
      {
        effectASList[i].enabled = _effectState;

        if (!_effectState)
        {
          effectASList[i].Stop ();
        }
      }
    }
  }



  #region 背景音乐
  /// <summary>
  /// 通过声音片段播放背景音乐
  /// </summary>
  /// <param name="name">Name.</param>
  public void playBGMByAudioClip (AudioClip audioClip)
  {
    bgmAS.Stop ();
    bgmAS.clip = audioClip;

    if(BGMState)
    {
      bgmAS.Play ();
    }
  }

  /// <summary>
  /// 通过id播放背景音乐
  /// </summary>
  public void playBGM (string clipName)
  {
    playBGMByAudioClip (getAudioClipByName(clipName));
  }

  /// <summary>
  /// 停止当前bgm
  /// </summary>
  public void stopBgm()
  {
    bgmAS.Stop();
  }

  /// <summary>
  /// 设置BGM播放速度
  /// </summary>
  public void setBGMPitch (int num)
  {
    bgmAS.pitch = num;
  }

  /// <summary>
  /// 设置BGm音量
  /// </summary>
  /// <param name="num"></param>
  public void SetBgmVolume(float volume)
  {
    bgmAS.volume = bgmVolume * volume;
  }
  #endregion
  

  #region 音效
  /// <summary>
  /// 通过声音片段播放音效
  /// </summary>
  public AudioSource playEffectByAudioClip (AudioClip audioClip, float volume = 1, bool noOver = false, bool isCut = false)
  {
    if (!EffectState) return null;

    Debug.AssertFormat (audioClip != null, "audioClip 为空！！！");

    // 如果音效要求不重叠播放
    if(noOver)
    {
      // 有正在播放的音效，返回
      if(effectASList.Find(t => t.clip == audioClip && t.isPlaying)) return null;
    }
    
    // 播放器
    AudioSource audioS = null;
    // 如果音效要求打断
    if(isCut)
    {
      // 有正在播放的音效，打断并重播
      audioS = effectASList.Find(t => t.clip == audioClip && t.isPlaying);
    }
    else
    {
      // 找到一个没有使用的播放器
      audioS = effectASList.Find(t => !t.isPlaying);
    }


    //如果没找到就创建一个
    if(audioS == null && effectASList.Count < 60)
    {
      audioS = createAudioSource();

      effectASList.Add (audioS);
    }

    //有可用播放器播放声音
    if(audioS != null)
    {
      audioS.Stop ();
      
      audioS.clip = audioClip;

      audioS.Play ();
      
      //设置音效音量
      audioS.volume = volume;
    }

    return audioS;
  }

  /// <summary>
  /// 通过id播放音效
  /// </summary>
  public AudioSource playEffect (string clipName, float volume = 1, bool noOver = false, bool isCut = false)
  {
    if (!EffectState) return null;

    AudioClip audioClip = getAudioClipByName(clipName);

    return playEffectByAudioClip (audioClip, volume, noOver, isCut);
  }

  /// <summary>
  /// 停止音效
  /// </summary>
  /// <param name="clipName"></param>
  public void StopEffect(string clipName)
  {
    AudioClip audioClip = getAudioClipByName(clipName);
    
    AudioSource audioS = effectASList.Find(t => t.clip == audioClip && t.isPlaying);
    if (audioS != null) audioS.Stop();
  }
  #endregion



  /// <summary>
  /// 创建一个播放器
  /// </summary>
  private AudioSource createAudioSource ()
  {
    AudioSource audioS = gameObject.AddComponent<AudioSource> ();

    return audioS;
  }

  /// <summary>
  /// 通过id获取声音片段
  /// </summary>
  public AudioClip getAudioClipByName (string clipName)
  {
    AudioClip audioClip;
    if(!audioClipArray.TryGetValue(clipName, out audioClip))
    {
      audioClip = Resources.Load<AudioClip> (string.Format(SoundPath, clipName));

      audioClipArray.Add (clipName, audioClip);
    }
      
    return audioClipArray [clipName];
  }

  /// <summary>
  /// 通过id加载声音
  /// </summary>
  /// <param name="name"></param>
  public void loadSound(string clipName)
  {
    AudioClip audioClip;
    if(!audioClipArray.TryGetValue(clipName, out audioClip))
    {
      audioClip = Resources.Load<AudioClip> (string.Format(SoundPath, clipName));

      audioClipArray.Add (clipName, audioClip);
    }
  }


  /// <summary>
  /// 加载游戏场景背景音效
  /// </summary>
  public void loadGameSceneBGM(string clipName ,AudioClip clip)
  {
    if (!audioClipArray.ContainsKey (clipName))
    {
      audioClipArray.Add (clipName, clip);
    }
  }
}



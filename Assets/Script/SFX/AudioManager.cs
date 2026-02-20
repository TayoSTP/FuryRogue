using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;


public enum AudioType
{
    None,
    Music,
    Die,
    Dash,
    Explostion,
    Punch,
    Punch2,
    Punch3,
    HitFeadback,
    HitFeadback2,
    Boss,
    Cri,
    Heal,
    Iconic,
    Combats,
    BackGround,
    Wind,
    LanceFlamme,
    LanceFlamme2,
    taunt,
    Coin,
    Coin2,
    Upgrade,
    Upgrade2,
    Menu,
    Victory,
    GameOver, 
    Explosion,
    SwitchWeapon,
    Button,
    Intro,
    Outro,
    
    
    
}

public enum AudioSourceType
{
    Game,
    Player,
}



public class AudioManager : MonoBehaviour
{
  static public AudioManager Instance;
  
  public float volume = 1.0f;
  
  public AudioSource GameSource;
  public AudioSource PlayerSource;

  
  [System.Serializable]
  public struct AudioData
  {
      public AudioClip clip;
      public AudioType type;
  }
  
  public AudioData[] audioData;

  void Awake()
  {
      Instance = this;
  }

  void Start()
  {
      GameSource.volume = volume;
      PlayerSource.volume = volume;
  }

  public void Playsound(AudioType Type, AudioSourceType sourceType)
  {
      AudioClip clip = getClip(Type);

      if (sourceType == AudioSourceType.Game)
      {
          GameSource.PlayOneShot(clip);
      }
      else if (sourceType == AudioSourceType.Player)
      {
          PlayerSource.PlayOneShot(clip);
      }
  }

  AudioClip getClip(AudioType type)
  {
      foreach (AudioData data in audioData)
      {
          if (data.type == type)
          {
              return data.clip;
          }
          
      }
     Debug.LogError("Audio clip not found");
     return null;
  }
}

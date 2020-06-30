using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAD_ChangeMusicTest : MonoBehaviour
{
    public AudioSource m_player;
    public AudioClip[] m_music;
    public int m_index;
  
    public void ChangeMusic()
    {
        m_index++;
        if (m_index > m_music.Length)
            m_index = 0;
        m_player.clip = m_music[m_index];
        m_player.Play();

    }
}

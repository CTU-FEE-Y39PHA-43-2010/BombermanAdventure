using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace BombermanAdventure.SoundManager
{
    class SoundManager
    {
        public static SoundEffect explosion;
        public static SoundEffect death;
        public static SoundEffect win;
        public static SoundEffect enemyKilled;
        public static SoundEffect bonus;

        public static void PlayExplosion()
        {
            if(explosion == null)
            {
                return;
            }
            explosion.Play(0.5f, 0f, 0f);
        }

        public static void PlayDeath()
        {
            if (explosion == null)
            {
                return;
            }
            death.Play(0.5f, 0f, 0f);
        }

        public static void PlayWin()
        {
            if (win == null)
            {
                return;
            }
            win.Play(0.5f, 0f, 0f);
        }

        public static void PlayEnemyKilled()
        {
            if (enemyKilled == null)
            {
                return;
            }
            enemyKilled.Play(0.5f, 0f, 0f);
        }

        public static void PlayBonus()
        {
            if (bonus == null)
            {
                return;
            }
            bonus.Play(0.5f, 0f, 0f);
        }
    }
}

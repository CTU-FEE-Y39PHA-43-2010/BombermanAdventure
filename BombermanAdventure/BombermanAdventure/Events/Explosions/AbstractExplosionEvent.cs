using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Events.Explosions
{
    class AbstractExplosionEvent : CommonEvent, IDestructible
    {
        /// <summary>
        /// hrac ktery bombu polozil
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="model">exploze</param>
        /// <param name="player">hrac ktery bombu polozil</param>
        public AbstractExplosionEvent(AbstractExplosion model, Player player) : base(model) 
        {
            Player = player;
        }
    }
}

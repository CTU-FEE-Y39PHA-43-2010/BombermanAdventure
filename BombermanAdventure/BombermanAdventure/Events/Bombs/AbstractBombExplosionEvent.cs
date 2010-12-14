using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Models.GameModels.Bombs;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Events.Bombs
{
    abstract class AbstractBombExplosionEvent : CommonEvent, IDestructible
    {
        /// <summary>
        /// hrac ktery bombu polozil
        /// </summary>
        Player player;
        public Player Player 
        {
            get { return player; }
        }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="model">explodujici bomba</param>
        /// <param name="player">hrac ktery bombu polozil</param>
        public AbstractBombExplosionEvent(AbstractBomb model, Player player) : base(model) 
        {
            this.player = player;
        }
    }
}

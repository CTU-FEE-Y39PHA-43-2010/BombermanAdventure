using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Models.GameModels;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Events.Collisions
{
    class CollisionEvent : CommonEvent
    {
        /// <summary>
        /// instance kolidujiciho hrace
        /// </summary>
        AbstractPlayer player;
        AbstractPlayer Player 
        {
            get { return player; }
        }

        /// <summary>
        /// konstruktor eventu pro kolize hrace s predmetem
        /// </summary>
        /// <param name="player">kolidujici hrac</param>
        /// <param name="model">kolidujici objekt</param>
        public CollisionEvent(AbstractPlayer player, AbstractGameModel model) : base(model) 
        {
            this.player = player;
        }
    }
}

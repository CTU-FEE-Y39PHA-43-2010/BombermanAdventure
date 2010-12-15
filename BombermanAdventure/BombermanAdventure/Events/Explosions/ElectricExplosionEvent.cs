using System;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Events.Explosions
{
    class ElectricExplosionEvent : AbstractExplosionEvent
    {
        public ElectricExplosionEvent(AbstractExplosion model, Player player) : base(model, player) { }
    }
}

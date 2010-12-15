using System;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Events.Explosions
{
    class WaterExplosionEvent : AbstractExplosionEvent
    {
        public WaterExplosionEvent(AbstractExplosion model, Player player) : base(model, player) { }
    }
}

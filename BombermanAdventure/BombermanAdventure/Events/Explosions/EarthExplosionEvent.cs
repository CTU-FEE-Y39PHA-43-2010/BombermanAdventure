using System;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Events.Explosions
{
    class EarthExplosionEvent : AbstractExplosionEvent
    {
        public EarthExplosionEvent(AbstractExplosion model, Player player) : base(model, player) { }
    }
}

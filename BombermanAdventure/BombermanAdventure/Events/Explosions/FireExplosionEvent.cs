using System;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Events.Explosions
{
    class FireExplosionEvent : AbstractExplosionEvent
    {
        public FireExplosionEvent(AbstractExplosion model, Player player) : base(model, player) { }
    }
}

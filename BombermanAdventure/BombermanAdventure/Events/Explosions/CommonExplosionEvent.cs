using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Events.Explosions
{
    class CommonExplosionEvent : AbstractExplosionEvent
    {
        public CommonExplosionEvent(AbstractExplosion model, AbstractPlayer player) : base(model, player) { }
    }
}

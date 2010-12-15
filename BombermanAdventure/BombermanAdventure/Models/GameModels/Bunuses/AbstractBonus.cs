using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Walls
{
    abstract class AbstractBonus : AbstractGameModel
    {
        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="game">instance hry</param>
        /// <param name="x">poloha v hernim poli na ose x</param>
        /// <param name="y">poloha v hernim poli na ose y</param>
        public AbstractBonus(Game game, AbstractWall wall)
            : base(game)
        {
            base.modelPosition = new Vector3(wall.ModelPosition.X, 0, wall.ModelPosition.Z);
            boundingBox = new BoundingBox(new Vector3(modelPosition.X - 10, modelPosition.Y, modelPosition.Z - 10),
                new Vector3(modelPosition.X + 10, modelPosition.Y + 20, modelPosition.Z + 10));
        }
    }
}

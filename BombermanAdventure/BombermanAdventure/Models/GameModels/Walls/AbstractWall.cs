using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Walls
{
    abstract class AbstractWall : AbstractGameModel
    {
        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="game">instance hry</param>
        /// <param name="x">poloha v hernim poli na ose x</param>
        /// <param name="y">poloha v hernim poli na ose y</param>
        public AbstractWall(Game game, int x, int y)
            : base(game)
        {
            base.modelPosition = new Vector3(x * 20, 0, y * 20);
            boundingBox = new BoundingBox(new Vector3(modelPosition.X - 10, modelPosition.Y, modelPosition.Z - 10),
                new Vector3(modelPosition.X + 10, modelPosition.Y + 20, modelPosition.Z + 10));
        }
    }
}

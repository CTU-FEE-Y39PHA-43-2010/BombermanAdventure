using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Labyrinths
{
    class LabyrinthFloorUnit : AbstractGameModel
    {
        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="game">instance hry</param>
        /// <param name="position">pozice podlahy</param>
        public LabyrinthFloorUnit(Game game, Vector3 position)
            : base(game)
        {
            base.modelPosition = position;
        }

        /// <summary>
        /// inicializacni metoda
        /// </summary>
        public override void Initialize()
        {
            base.modelName = "Models/GroundUnit";
            base.modelScale = 1f;
            base.modelRotation = new Vector3();
            base.Initialize();
        }

        /// <summary>
        /// metoda pro provedeni reakce na udalost
        /// </summary>
        /// <param name="ieEvent">udalost</param>
        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime) { }

        /// <summary>
        /// metoda pro vykresleni podlahy
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Matrix world;
            world = Matrix.CreateScale(modelScale);
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(modelRotation.X));
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(modelRotation.Y));
            world *= Matrix.CreateRotationZ(MathHelper.ToRadians(modelRotation.Z));
            world *= Matrix.CreateTranslation(modelPosition);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.LightingEnabled = true;
                    effect.World = world;
                    effect.View = models.Camera.viewMatrix;
                    effect.Projection = models.Camera.projectionMatrix;
                }
                mesh.Draw();
            }
        }
    }
}

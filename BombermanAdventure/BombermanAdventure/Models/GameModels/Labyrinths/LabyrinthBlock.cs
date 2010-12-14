using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Labyrinths
{
    class LabyrinthBlock : AbstractGameModel
    {
        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="game">instance tridy</param>
        /// <param name="position">pozice bloku</param>
        public LabyrinthBlock(Game game, Vector3 position)
            : base(game)
        {
            base.modelPosition = position;
            boundingBox = new BoundingBox(new Vector3(position.X - 10, position.Y, position.Z - 10),
                new Vector3(position.X + 10, position.Y + 20, position.Z + 10));
        }

        /// <summary>
        /// inicializacni metoda
        /// </summary>
        public override void Initialize()
        {
            color = new Vector3();
            base.modelName = "Models/IndestructibleBlock";
            base.modelScale = 1f;
            base.modelRotation = new Vector3();
            base.Initialize();
        }

        Vector3 color;

        /// <summary>
        /// metoda pro provedeni reakce na udalost
        /// </summary>
        /// <param name="ieEvent">udalost</param>
        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime) { }

        /// <summary>
        /// metoda pro vykresleni bloku
        /// </summary>
        /// <param name="gameTime">herni cas</param>
        public override void Draw(GameTime gameTime)
        {
            Matrix world;
            world = Matrix.CreateScale(modelScale);
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(modelRotation.X));
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(modelRotation.Y));
            world *= Matrix.CreateRotationZ(MathHelper.ToRadians(modelRotation.Z));
            world *= Matrix.CreateTranslation(modelPosition);

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.PreferPerPixelLighting = true;
                    effect.LightingEnabled = true;
                    effect.Alpha = 0.8f;
                    effect.World = transforms[mesh.ParentBone.Index] * world;
                    effect.DiffuseColor = color;
                    //effect.World = world;
                    effect.View = models.Camera.viewMatrix;
                    effect.Projection = models.Camera.projectionMatrix;
                }
                mesh.Draw();
            }
        }
        public void ChangeColor(Vector3 color)
        {
            this.color = color;
        }
    }
}

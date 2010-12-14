using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    class ExplosionItem : AbstractGameModel
    {
        protected Vector3 color;

        public ExplosionItem(Game game, Vector3 color, Vector3 position)
            : base(game)
        {
            this.modelPosition = position;
            this.color = color;
            this.boundingBox = new BoundingBox(new Vector3(modelPosition.X - 9.9f, modelPosition.Y, modelPosition.Z - 9.9f),
                new Vector3(modelPosition.X + 9.9f, modelPosition.Y + 20f, modelPosition.Z + 9.9f));
        }

        public override void Initialize()
        {
            base.modelName = "Models/IndestructibleBlock";
            base.modelScale = 1f;
            base.modelRotation = new Vector3();
            models = ModelList.GetInstance();
            LoadContent();
        }

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
                    effect.World = world;
                    effect.DiffuseColor = color;
                    //effect.World = world;
                    effect.View = models.Camera.viewMatrix;
                    effect.Projection = models.Camera.projectionMatrix;
                }
                mesh.Draw();
            }
        }


        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

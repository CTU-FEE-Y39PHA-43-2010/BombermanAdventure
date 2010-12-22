using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Events;
using BombermanAdventure.ParticleSystems;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    class ExplosionItem : AbstractGameModel
    {
        protected Vector3 color;
        protected Vector3 explosionPosition;

        ParticleSystem explosionParticles;
        ParticleEmitter emitter;

        public ExplosionItem(Game game, Vector3 color, Vector3 position)
            : base(game)
        {
            modelPosition = position;
            explosionPosition = new Vector3(modelPosition.X, 10, modelPosition.Z);
            this.color = color;
            boundingBox = new BoundingBox(new Vector3(modelPosition.X - 9.9f, modelPosition.Y, modelPosition.Z - 9.9f),
                new Vector3(modelPosition.X + 9.9f, modelPosition.Y + 20f, modelPosition.Z + 9.9f));
        }

        public override void Initialize()
        {
            modelName = "Models/IndestructibleBlock";
            modelScale = 1f;
            modelRotation = new Vector3();
            models = ModelList.GetInstance();
            explosionParticles = new ExplosionParticleSystem(game, game.Content);
            explosionParticles.DrawOrder = 400;
            explosionParticles.Initialize();
            emitter = new ParticleEmitter(explosionParticles, 20, explosionPosition);
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            emitter.Update(gameTime, explosionPosition);
            explosionParticles.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            /*
            var world = Matrix.CreateScale(modelScale);
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(modelRotation.X));
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(modelRotation.Y));
            world *= Matrix.CreateRotationZ(MathHelper.ToRadians(modelRotation.Z));
            world *= Matrix.CreateTranslation(modelPosition);

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (var mesh in model.Meshes)
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
            }*/

            explosionParticles.SetCamera(models.Camera.viewMatrix, models.Camera.projectionMatrix);
            explosionParticles.Draw(gameTime);
        }


        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels
{
    abstract class AbstractGameModel : DrawableGameComponent
    {
        /// <summary>
        /// 3D model
        /// </summary>
        protected Model model;
        public Model Model 
        {
            get { return model; }
        }

        /// <summary>
        /// instance modellistu
        /// </summary>
        protected ModelList models;

        /// <summary>
        /// meritko modelu
        /// </summary>
        protected float modelScale;

        /// <summary>
        /// nazev modelu
        /// </summary>
        protected String modelName;

        /// <summary>
        /// pozice modelu
        /// </summary>
        protected Vector3 modelPosition;
        public Vector3 ModelPosition
        {
            get { return modelPosition; }
        }

        /// <summary>
        /// rotace modelu
        /// </summary>
        protected Vector3 modelRotation;

        /// <summary>
        /// boundingBox objektu
        /// </summary>
        protected BoundingBox boundingBox;
        public BoundingBox BoundingBox
        {
            get { return boundingBox; }
        }
        protected BoundingSphere boundingSphere;
        public BoundingSphere BoundingSphere
        {
            get { return boundingSphere; }
        }

        protected Game game;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="game">instance hry</param>
        public AbstractGameModel(Game game) : base(game) 
        {
            this.game = game;
            Initialize();
        }

        /// <summary>
        /// inicializacni metoda
        /// </summary>
        public override void Initialize() 
        {
            models = ModelList.GetInstance();
            LoadContent();
            base.Initialize();
        }

        /// <summary>
        /// nahrani obsahu
        /// </summary>
        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>(modelName);
            base.LoadContent();
        }

        /// <summary>
        /// metoda pro vykresleni objektu
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

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = models.Camera.viewMatrix;
                    effect.Projection = models.Camera.projectionMatrix;
                }
                mesh.Draw();
            }
        }

        /// <summary>
        /// metoda pro provedeni reakce na udalost
        /// </summary>
        /// <param name="ieEvent">udalost</param>
        public abstract void OnEvent(CommonEvent ieEvent, GameTime gameTime);
    }
}

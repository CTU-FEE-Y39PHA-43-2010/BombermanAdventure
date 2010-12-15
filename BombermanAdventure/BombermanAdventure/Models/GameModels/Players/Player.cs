using System;
using BombermanAdventure.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BombermanAdventure.Models.GameModels.Bombs;

namespace BombermanAdventure.Models.GameModels.Players
{
    class Player : AbstractPlayer
    {
        enum Bombs { COMMON, WATER, MUD, ELECTRIC }

        Bombs selectedBombType;
        KeyboardState oldState;
        Game game;

        Vector3 min, max;

        private Vector3 prevModelPosition;

        public Player(Game game, Profile profile, int x, int y)
            : base(game, profile, x, y)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            base.modelName = "Models/Player";
            base.modelScale = 0.1f;


            modelRotation = new Vector3();

            life = 100;
            speed = 1f;

            selectedBombType = Bombs.COMMON;
            possibleBombsCount = 3;
            bombsCount = 0;
            // BombRange = 6; <- fix this................
            bombRange = 1;

            prevModelPosition = modelPosition;

            min = new Vector3();
            max = new Vector3();
            boundingSphere = new BoundingSphere();
            boundingBox = new BoundingBox();
            UpdateBoundingBox();

            base.Initialize();
        }

        void UpdateBoundingBox()
        {
            min.X = modelPosition.X - 9.9f;
            min.Y = modelPosition.Y - 9.9f;
            min.Z = modelPosition.Z - 9.9f;
            max.X = modelPosition.X + 9.9f;
            max.Y = modelPosition.Y + 9.9f;
            max.Z = modelPosition.Z + 9.9f;
            boundingBox.Min = min;
            boundingBox.Max = max;
            boundingSphere.Center = modelPosition;
            boundingSphere.Radius = 7.5f;
        }

        public override void Update(GameTime gameTime)
        {
            prevModelPosition = modelPosition;
            KeyBoardHandler(gameTime);
            UpdateBoundingBox();
            base.Update(gameTime);

        }

        public override void OnEvent(Events.CommonEvent ieEvent, GameTime gameTime)
        {
            if (ieEvent is Events.Bombs.AbstractBombExplosionEvent)
            {
                Events.Bombs.AbstractBombExplosionEvent leEvent = (Events.Bombs.AbstractBombExplosionEvent)ieEvent;
                if (leEvent.Player == this)
                {
                    bombsCount--;
                }
            }
            if (ieEvent is Events.Collisions.CollisionEvent)
            {
                modelPosition = prevModelPosition;
            }
        }

        #region Ovladani

        private void KeyBoardHandler(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            Walking(ks);

            if (ks.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    this.PutBomb(gameTime);
                }
            }
            if (ks.IsKeyDown(Keys.LeftControl))
            {
                if (!oldState.IsKeyDown(Keys.LeftControl))
                {
                    this.ChageBombType(gameTime);
                }
            }
            oldState = ks;
        }

        private void ChageBombType(GameTime gameTime)
        {
            switch (selectedBombType)
            {
                case Bombs.COMMON:
                    selectedBombType = Bombs.WATER;
                    break;
                case Bombs.WATER:
                    selectedBombType = Bombs.ELECTRIC;
                    break;
                case Bombs.ELECTRIC:
                    selectedBombType = Bombs.MUD;
                    break;
                case Bombs.MUD:
                    selectedBombType = Bombs.COMMON;
                    break;
            }
        }

        public override void PutBomb(GameTime gameTime)
        {
            if (bombsCount < possibleBombsCount)
            {
                switch (selectedBombType)
                {
                    case Bombs.COMMON:
                        Vector3 pos = new Vector3();
                        if ((Math.Abs(modelPosition.X % 20)) >= 10)
                        {
                            if (modelPosition.X % 20 < 0)
                            {
                                pos.X = modelPosition.X - 20 - modelPosition.X % 20;
                            }
                            else
                            {
                                pos.X = modelPosition.X + 20 - modelPosition.X % 20;
                            }
                            
                        }
                        else
                        {
                            pos.X = modelPosition.X - modelPosition.X % 20;
                        }
                        if ((Math.Abs(modelPosition.Z % 20)) >= 10)
                        {
                            if (modelPosition.Z % 20 < 0)
                            {
                                pos.Z = modelPosition.Z - 20 - modelPosition.Z % 20;
                            }
                            else
                            {
                                pos.Z = modelPosition.Z + 20 - modelPosition.Z % 20;
                            }
                            
                        }
                        else
                        {
                            pos.Z = modelPosition.Z - modelPosition.Z % 20;
                        }
                        pos.Y = modelPosition.Y; 
                        //Vector3 pos = new Vector3(modelPosition.X - (modelPosition.X % 20), modelPosition.Y, modelPosition.Z - (modelPosition.Z % 20));
                        base.models.AddBomb(new CommonBomb(game, pos, this, gameTime));
                        break;
                    case Bombs.WATER:
                        base.models.AddBomb(new WaterBomb(game, modelPosition, this, gameTime));
                        break;
                    case Bombs.ELECTRIC:
                        base.models.AddBomb(new ElectricBomb(game, modelPosition, this, gameTime));
                        break;
                    case Bombs.MUD:
                        base.models.AddBomb(new MudBomb(game, modelPosition, this, gameTime));
                        break;
                }
                bombsCount++;
            }
        }

        public override void Fire()
        {
            throw new NotImplementedException();
        }

        #region Ovladani Chuze

        public override void GoUp()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.X -= speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.Z += speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.X += speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.Z -= speed;
                    break;
            }
        }

        public override void GoDown()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.X += speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.Z -= speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.X -= speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.Z += speed;
                    break;
            }
        }

        public override void GoLeft()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.Z += speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.X += speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.Z -= speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.X -= speed;
                    break;
            }
        }

        public override void GoRight()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.Z -= speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.X -= speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.Z += speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.X += speed;
                    break;
            }
        }

        private void Walking(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Right))
            {
                GoRight();

            }

            if (ks.IsKeyDown(Keys.Left))
            {
                GoLeft();
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                GoUp();
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                GoDown();
            }
        }
        #endregion //Ovladani Chuze
        #endregion
    }
}

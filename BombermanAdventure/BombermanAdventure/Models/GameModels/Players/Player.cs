﻿using System;
using BombermanAdventure.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BombermanAdventure.Models.GameModels.Bombs;

namespace BombermanAdventure.Models.GameModels.Players
{
    class Player : AbstractGameModel
    {
        enum Bombs { Common, Water, Mud, Electric }

        private Bombs _selectedBombType;
        private KeyboardState _oldState;
        private Vector3 _min;
        private Vector3 _max;
        private Vector3 _prevModelPosition;
        public Profile PlayerProfile { get; set; }
        public int BombsCount { get; set; }

        public Player(Game game, Profile profile, int x, int y)
            : base(game)
        {
            PlayerProfile = profile;
            base.modelPosition = new Vector3(x * 20, 10, y * 20);
        }

        public override void Initialize()
        {
            base.modelName = "Models/Player";
            base.modelScale = 1f;

            modelRotation = new Vector3();
            _selectedBombType = Bombs.Common;
            _prevModelPosition = modelPosition;

            _min = new Vector3();
            _max = new Vector3();
            boundingSphere = new BoundingSphere();
            boundingBox = new BoundingBox();
            UpdateBoundingBox();

            base.Initialize();
        }

        void UpdateBoundingBox()
        {
            _min.X = modelPosition.X - 9.9f;
            _min.Y = modelPosition.Y - 9.9f;
            _min.Z = modelPosition.Z - 9.9f;
            _max.X = modelPosition.X + 9.9f;
            _max.Y = modelPosition.Y + 9.9f;
            _max.Z = modelPosition.Z + 9.9f;
            boundingBox.Min = _min;
            boundingBox.Max = _max;
            boundingSphere.Center = modelPosition;
            boundingSphere.Radius = 7.5f;
        }

        public override void Update(GameTime gameTime)
        {
            _prevModelPosition = modelPosition;
            KeyBoardHandler(gameTime);
            UpdateBoundingBox();
            base.Update(gameTime);

        }

        public override void OnEvent(Events.CommonEvent ieEvent, GameTime gameTime)
        {
            if (ieEvent is Events.Bombs.AbstractBombExplosionEvent)
            {
                var leEvent = (Events.Bombs.AbstractBombExplosionEvent)ieEvent;
                if (leEvent.Player == this)
                {
                    BombsCount--;
                }
            }
            if (ieEvent is Events.Collisions.CollisionEvent)
            {
                modelPosition = _prevModelPosition;
            }
        }

        #region Ovladani

        private void KeyBoardHandler(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            Walking(ks);

            if (ks.IsKeyDown(Keys.Space))
            {
                if (!_oldState.IsKeyDown(Keys.Space))
                {
                    PutBomb(gameTime);
                }
            }
            if (ks.IsKeyDown(Keys.LeftControl))
            {
                if (!_oldState.IsKeyDown(Keys.LeftControl))
                {
                    ChageBombType(gameTime);
                }
            }
            _oldState = ks;
        }

        private void ChageBombType(GameTime gameTime)
        {
            switch (_selectedBombType)
            {
                case Bombs.Common:
                    _selectedBombType = Bombs.Water;
                    break;
                case Bombs.Water:
                    _selectedBombType = Bombs.Electric;
                    break;
                case Bombs.Electric:
                    _selectedBombType = Bombs.Mud;
                    break;
                case Bombs.Mud:
                    _selectedBombType = Bombs.Common;
                    break;
            }
        }

        public void PutBomb(GameTime gameTime)
        {
            if (BombsCount < PlayerProfile.PossibleBombsCount)
            {
                switch (_selectedBombType)
                {
                    case Bombs.Common:
                        var pos = new Vector3();
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
                    case Bombs.Water:
                        base.models.AddBomb(new WaterBomb(game, modelPosition, this, gameTime));
                        break;
                    case Bombs.Electric:
                        base.models.AddBomb(new ElectricBomb(game, modelPosition, this, gameTime));
                        break;
                    case Bombs.Mud:
                        base.models.AddBomb(new MudBomb(game, modelPosition, this, gameTime));
                        break;
                }
                BombsCount++;
            }
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        #region Ovladani Chuze

        public void GoUp()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.X -= PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.Z += PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.X += PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.Z -= PlayerProfile.Speed;
                    break;
            }
        }

        public void GoDown()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.X += PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.Z -= PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.X -= PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.Z += PlayerProfile.Speed;
                    break;
            }
        }

        public void GoLeft()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.Z += PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.X += PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.Z -= PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.X -= PlayerProfile.Speed;
                    break;
            }
        }

        public void GoRight()
        {
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.Z -= PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.X -= PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.Z += PlayerProfile.Speed;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.X += PlayerProfile.Speed;
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
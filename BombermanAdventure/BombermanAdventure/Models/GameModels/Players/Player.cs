using System;
using System.Diagnostics;
using BombermanAdventure.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BombermanAdventure.Models.GameModels.Bombs;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Players
{
    class Player : AbstractGameModel
    {
        public enum Bombs { Common, Water, Mud, Electric }
        enum Orientation { Up, Down, Left, Right }

        private Bombs _selectedBombType;
        private KeyboardState _oldState;
        private Vector3 _min;
        private Vector3 _max;
        private Vector3 _prevModelPosition;
        public Profile PlayerProfile { get; set; }
        public int BombsCount { get; set; }
        public bool Dead { get; set; }
        public bool Winner { get; set; }
        private Orientation _orientation;

        public Player(Game game, Profile profile, int x, int y)
            : base(game)
        {
            PlayerProfile = profile;
            base.modelPosition = new Vector3(x * 20, 10, y * 20);
            Dead = false;
            Winner = false;
            _orientation = Orientation.Up;
        }

        public override void Initialize()
        {
            base.modelName = "Models/Player";
            base.modelScale = 1f;

            modelRotation = new Vector3(0f, 230f, 0f);
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

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
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
            if (Dead)
            {
                return;
            }

            KeyboardState ks = Keyboard.GetState();
            Walking(ks);

            if (ks.IsKeyDown(Keys.Z) || ks.IsKeyDown(Keys.Y))
            {
                if (!_oldState.IsKeyDown(Keys.Z) && !_oldState.IsKeyDown(Keys.Y))
                {
                    PutBomb(gameTime, Bombs.Common);
                }
            }
            if (ks.IsKeyDown(Keys.X))
            {
                if (!_oldState.IsKeyDown(Keys.X))
                {
                    PutBomb(gameTime, Bombs.Electric);
                }
            }
            if (ks.IsKeyDown(Keys.C))
            {
                if (!_oldState.IsKeyDown(Keys.C))
                {
                    PutBomb(gameTime, Bombs.Mud);
                }
            }
            if (ks.IsKeyDown(Keys.V))
            {
                if (!_oldState.IsKeyDown(Keys.V))
                {
                    PutBomb(gameTime, Bombs.Water);
                }
            }

            _oldState = ks;
        }

        private void PutBomb(GameTime gameTime, Bombs bType)
        {
            if (BombsCount < PlayerProfile.PossibleBombsCount)
            {
                var pos = GetBombPosition();
                switch(bType)
                {
                    case Bombs.Common:
                        models.AddBomb(new FireBomb(game, pos, this, gameTime));
                        break;
                    case Bombs.Water:
                        models.AddBomb(new WaterBomb(game, pos, this, gameTime));
                        break;
                    case Bombs.Electric:
                        models.AddBomb(new ElectricBomb(game, pos, this, gameTime));
                        break;
                    case Bombs.Mud:
                        models.AddBomb(new MudBomb(game, pos, this, gameTime));
                        break;
                }
                BombsCount++;
            }
        }

        public void Remove()
        {
            modelPosition.Y += 100;
        }

        public Vector3 GetBombPosition()
        {
            var pos = new Vector3();
            if ((Math.Abs(modelPosition.X%20)) >= 10)
            {
                if (modelPosition.X%20 < 0)
                {
                    pos.X = modelPosition.X - 20 - modelPosition.X%20;
                }
                else
                {
                    pos.X = modelPosition.X + 20 - modelPosition.X%20;
                }
            }
            else
            {
                pos.X = modelPosition.X - modelPosition.X%20;
            }
            if ((Math.Abs(modelPosition.Z%20)) >= 10)
            {
                if (modelPosition.Z%20 < 0)
                {
                    pos.Z = modelPosition.Z - 20 - modelPosition.Z%20;
                }
                else
                {
                    pos.Z = modelPosition.Z + 20 - modelPosition.Z%20;
                }
            }
            else
            {
                pos.Z = modelPosition.Z - modelPosition.Z%20;
            }
            pos.Y = modelPosition.Y;
            return pos;
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        #region Ovladani Chuze

        private void SetOrientation(Orientation orig, Orientation needed)
        {

            if (orig == needed)
            {
                return;
            }

            switch (orig)
            {
                case Orientation.Up:
                    switch (needed)
                    {
                        case Orientation.Down:
                            modelRotation.Y -= 180;
                            break;
                        case Orientation.Left:
                            modelRotation.Y -= 90;
                            break;
                        case Orientation.Right:
                            modelRotation.Y += 90;
                            break;
                    }
                    break;
                case Orientation.Down:
                    switch (needed)
                    {
                        case Orientation.Up:
                            modelRotation.Y += 180;
                            break;
                        case Orientation.Left:
                            modelRotation.Y += 90;
                            break;
                        case Orientation.Right:
                            modelRotation.Y -= 90;
                            break;
                    }
                    break;
                case Orientation.Left:
                    switch (needed)
                    {
                        case Orientation.Down:
                            modelRotation.Y -= 90;
                            break;
                        case Orientation.Up:
                            modelRotation.Y += 90;
                            break;
                        case Orientation.Right:
                            modelRotation.Y += 180;
                            break;
                    }
                    break;
                case Orientation.Right:
                    switch (needed)
                    {
                        case Orientation.Down:
                            modelRotation.Y += 90;
                            break;
                        case Orientation.Up:
                            modelRotation.Y -= 90;
                            break;
                        case Orientation.Left:
                            modelRotation.Y -= 180;
                            break;
                    }
                    break;
            }

        }

        public void GoUp()
        {
            Orientation _neededOrientation = Orientation.Up;
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.X -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Up;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.Z += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Right;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.X += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Down;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.Z -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Left;
                    break;
            }
            if (_orientation != _neededOrientation)
            {
                SetOrientation(_orientation, _neededOrientation);
                _orientation = _neededOrientation;
            }
        }

        public void GoDown()
        {
            Orientation _neededOrientation = Orientation.Down;
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.X += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Down;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.Z -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Left;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.X -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Up;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.Z += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Right;
                    break;
            }
            if (_orientation != _neededOrientation)
            {
                SetOrientation(_orientation, _neededOrientation);
                _orientation = _neededOrientation;
            }
        }

        public void GoLeft()
        {
            Orientation _neededOrientation = Orientation.Down;
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.Z += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Right;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.X += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Down;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.Z -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Left;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.X -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Up;
                    break;
            }
            if (_orientation != _neededOrientation)
            {
                SetOrientation(_orientation, _neededOrientation);
                _orientation = _neededOrientation;
            }
        }

        public void GoRight()
        {
            Orientation _neededOrientation = Orientation.Down;
            switch (models.Camera.position)
            {
                case Cameras.Camera.Position.FRONT:
                    modelPosition.Z -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Left;
                    break;
                case Cameras.Camera.Position.LEFT:
                    modelPosition.X -= PlayerProfile.Speed;
                    _neededOrientation = Orientation.Up;
                    break;
                case Cameras.Camera.Position.BACK:
                    modelPosition.Z += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Right;
                    break;
                case Cameras.Camera.Position.RIGHT:
                    modelPosition.X += PlayerProfile.Speed;
                    _neededOrientation = Orientation.Down;
                    break;
            }
            if (_orientation != _neededOrientation)
            {
                SetOrientation(_orientation, _neededOrientation);
                _orientation = _neededOrientation;
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

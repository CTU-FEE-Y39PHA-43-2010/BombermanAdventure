using System;
using System.Diagnostics;
using BombermanAdventure.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BombermanAdventure.Models.GameModels.Bombs;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Players
{
    class AbstractEnemy : AbstractGameModel
    {

        protected enum Orientation { Up, Down, Left, Right }
        private Vector3 _min;
        private Vector3 _max;
        protected Vector3 prevModelPosition;
        protected Orientation orientation;
        private float _step;
        private float _speed;
        protected int live;
        public int Live
        {
            get { return live; }
            set { live = value; }
        }

        public AbstractEnemy(Game game, float speed, int x, int y)
            : base(game)
        {
            base.modelPosition = new Vector3(x * 20, 10, y * 20);
            orientation = Orientation.Up;
            _step = 0f;
            _speed = speed;
        }

        public override void Initialize() 
        {
            modelRotation = new Vector3(0f, 230f, 0f);
            modelScale = 1f;
            prevModelPosition = modelPosition;
            _min = new Vector3();
            _max = new Vector3();
            boundingSphere = new BoundingSphere();
            boundingBox = new BoundingBox();
            UpdateBoundingBox();

            base.Initialize();
        }

        void UpdateBoundingBox()
        {
            _min.X = modelPosition.X -9.9f;
            _min.Y = modelPosition.Y -9.9f;
            _min.Z = modelPosition.Z -9.9f;
            _max.X = modelPosition.X +9.9f;
            _max.Y = modelPosition.Y + 9.9f;
            _max.Z = modelPosition.Z + 9.9f;
            boundingBox.Min = _min;
            boundingBox.Max = _max;
            boundingSphere.Center = modelPosition;
            boundingSphere.Radius = 7.5f;
        }

        public override void Update(GameTime gameTime)
        {
            prevModelPosition = modelPosition;
            _step += _speed;
            if (_step > 1)
            {
                if ((modelPosition.X%40 == 0 && (orientation == Orientation.Up || orientation == Orientation.Down)) ||
                    (modelPosition.Z%40 == 0 && (orientation == Orientation.Left || orientation == Orientation.Right)))
                {
                    var newOrientation = GetNextOrientation();
                    SetOrientation(orientation, newOrientation);
                    orientation = newOrientation;
                }

                switch (orientation)
                {
                    case Orientation.Up:
                        modelPosition = new Vector3(modelPosition.X - 1, modelPosition.Y, modelPosition.Z);
                        break;
                    case Orientation.Right:
                        modelPosition = new Vector3(modelPosition.X, modelPosition.Y, modelPosition.Z - 1);
                        break;
                    case Orientation.Down:
                        modelPosition = new Vector3(modelPosition.X + 1, modelPosition.Y, modelPosition.Z);
                        break;
                    case Orientation.Left:
                        modelPosition = new Vector3(modelPosition.X, modelPosition.Y, modelPosition.Z + 1);
                        break;
                }
                _step = 0f;
            }
            UpdateBoundingBox();
            base.Update(gameTime);

        }

        protected static Orientation GetNextOrientation()
        {
            var r = new Random();
            int random = r.Next(0, 4);
            
            switch (random)
            {
                case 0:
                    return Orientation.Right;
                case 1:
                    return Orientation.Down;
                case 2:
                    return Orientation.Left;
                default:
                    return Orientation.Up;
            }
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            if (ieEvent is Events.Collisions.CollisionEvent)
            {
                var newOrientation = GetNextOrientation();
                SetOrientation(orientation, newOrientation);
                orientation = newOrientation;
                modelPosition = prevModelPosition;
            }
        }

        #region Ovladani
        
        protected void SetOrientation(Orientation orig, Orientation needed)
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
                case Orientation.Down:
                    switch (needed)
                    {
                        case Orientation.Up:
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
                case Orientation.Left:
                    switch (needed)
                    {
                        case Orientation.Down:
                            modelRotation.Y += 90;
                            break;
                        case Orientation.Up:
                            modelRotation.Y -= 90;
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
                            modelRotation.Y -= 90;
                            break;
                        case Orientation.Up:
                            modelRotation.Y += 90;
                            break;
                        case Orientation.Left:
                            modelRotation.Y -= 180;
                            break;
                    }
                    break;
            }

        }

        #endregion
    }
}

using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Events;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Models.GameModels.Bombs
{
    abstract class AbstractBomb : AbstractGameModel
    {
        protected Player player;
        TimeSpan creationTime;
        bool scaleDown = true;
        protected int range;
        float creationModelScale;
        float deltaModelScale;

        public bool isCollidable;

        public AbstractBomb(Game game, Vector3 modelPosition, Player player, GameTime gameTime)
            : base(game)
        {
            this.modelPosition = modelPosition;
            this.player = player;
            creationTime = gameTime.TotalGameTime;
            range = player.PlayerProfile.BombRange;
            isCollidable = false;

            boundingSphere = new BoundingSphere(modelPosition, 5.0f);
        }

        public override void Initialize()
        {
            creationModelScale = modelScale;
            deltaModelScale = modelScale / 100;
            modelRotation = new Vector3();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (creationTime.Seconds + 3 < gameTime.TotalGameTime.Seconds)
            {
                RegisterEvent(gameTime);
            }
            else
            {
                if (scaleDown)
                {
                    modelScale -= deltaModelScale;
                }
                else
                {
                    modelScale += deltaModelScale;
                }
                if (modelScale < (creationModelScale / 2) || modelScale > creationModelScale)
                {
                    scaleDown = !scaleDown;
                }
            }
            if (!models.Player.BoundingSphere.Intersects(BoundingSphere))
            {
                isCollidable = true;
            }
        }

        abstract protected void RegisterEvent(GameTime gameTime);

        abstract public override void OnEvent(CommonEvent ieEvent, GameTime gameTime);
    }
}

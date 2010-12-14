using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BombermanAdventure.Events;
using BombermanAdventure.Events.Bombs;
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
            this.creationTime = gameTime.TotalGameTime;
            this.range = player.BombRange;
            this.isCollidable = false;

            boundingSphere = new BoundingSphere(modelPosition, 5.0f);
        }

        public override void Initialize()
        {
            this.creationModelScale = base.modelScale;
            this.deltaModelScale = base.modelScale / 100;
            modelRotation = new Vector3();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (creationTime.Seconds + 5 < gameTime.TotalGameTime.Seconds)
            {
                this.RegisterEvent(gameTime);
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
            if (!models.Player.BoundingSphere.Intersects(this.BoundingSphere))
            {
                isCollidable = true;

            }
        }

        abstract protected void RegisterEvent(GameTime gameTime);

        abstract public override void OnEvent(CommonEvent ieEvent, GameTime gameTime);
    }
}

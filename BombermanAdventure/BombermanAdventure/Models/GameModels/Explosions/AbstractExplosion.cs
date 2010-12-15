using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Events;
using BombermanAdventure.Events.Collisions;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    abstract class AbstractExplosion : AbstractGameModel
    {

        protected List<ExplosionItem> explosionItems;
        public List<ExplosionItem> ExplosionItems
        {
            get { return explosionItems; }
        }

        protected List<BoundingBox> boundingBoxes;
        public List<BoundingBox> BoundingBoxes
        {
            get { return boundingBoxes; }
        }

        protected Vector3 color;
        protected int range;
        protected TimeSpan creationTime;
        protected AbstractPlayer player;

        public AbstractExplosion(Game game, AbstractPlayer player, Vector3 position, GameTime gameTime)
            : base(game)
        {
            this.creationTime = gameTime.TotalGameTime;
            this.modelPosition = new Vector3(position.X, 0, position.Z);
            //this.range = player.BombRange; // I HAD TO DO IT THIS WAY FOR TESTING... FIX PROFILE NULL REFERENCE....
            range = 6;
            this.player = player;
            this.models = ModelList.GetInstance();
        }

        public override void Initialize()
        {
            this.explosionItems = new List<ExplosionItem>();
            this.boundingBoxes = new List<BoundingBox>();
            LoadContent();
        }

        protected override void LoadContent()
        {
            this.LoadExplosionItems();
            this.LoadBoundingBoxes();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ExplosionItem explosionItem in explosionItems)
            {
                explosionItem.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (creationTime.Milliseconds + 500 < gameTime.TotalGameTime.Milliseconds)
            {
                this.RegisterEvent(gameTime);
            }
        }

        abstract protected void RegisterEvent(GameTime gameTime);

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime) { }

        protected void LoadExplosionItems()
        {
            var explosionPositions = new List<Vector3>();
            explosionPositions.Add(modelPosition);

            bool botCollide = false;
            bool topCollide = false;
            bool leftCollide = false;
            bool rightCollide = false;

            for (int i = 0; i != range; i++)
            {
                var bot = new Vector3(modelPosition.X + (20 * (i + 1)), modelPosition.Y, modelPosition.Z);
                var top = new Vector3(modelPosition.X - (20 * (i + 1)), modelPosition.Y, modelPosition.Z);
                var left = new Vector3(modelPosition.X, modelPosition.Y, modelPosition.Z + (20 * (i + 1)));
                var right = new Vector3(modelPosition.X, modelPosition.Y, modelPosition.Z - (20 * (i + 1)));
                if (!IsCollidesWithLabyrynthBlock(bot) && !botCollide)
                {
                    explosionPositions.Add(bot);
                    if (IsCollidesWithDestroyableWall(bot))
                    {
                        botCollide = true;
                    }
                }
                else
                {
                    botCollide = true;
                }
                if (!IsCollidesWithLabyrynthBlock(top) && !topCollide)
                {
                    explosionPositions.Add(top);
                    if (IsCollidesWithDestroyableWall(top))
                    {
                        topCollide = true;
                    }
                }
                else
                {
                    topCollide = true;
                }
                if (!IsCollidesWithLabyrynthBlock(left) && !leftCollide)
                {
                    explosionPositions.Add(left);
                    if (IsCollidesWithDestroyableWall(left))
                    {
                        leftCollide = true;
                    }
                }
                else
                {
                    leftCollide = true;
                }
                if (!IsCollidesWithLabyrynthBlock(right) && !rightCollide)
                {
                    explosionPositions.Add(right);
                    if (IsCollidesWithDestroyableWall(right))
                    {
                        rightCollide = true;
                    }
                }
                else
                {
                    rightCollide = true;
                }
            }
            foreach (var pos in explosionPositions)
            {
                explosionItems.Add(new ExplosionItem(game, color, pos));
            }
        }

        private bool IsCollidesWithDestroyableWall(Vector3 side)
        {
            foreach (var wall in models.Walls)
            {
                if (wall.ModelPosition.X == side.X && wall.ModelPosition.Y == side.Y && wall.ModelPosition.Z == side.Z)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCollidesWithLabyrynthBlock(Vector3 side)
        {
            foreach (var block in models.Labyrinth.Blocks)
            {
                if (block.ModelPosition.X == side.X && block.ModelPosition.Y == side.Y && block.ModelPosition.Z == side.Z)
                {
                    return true;
                }
            }
            return false;
        }

        protected void LoadBoundingBoxes()
        {
            foreach (ExplosionItem item in explosionItems)
            {
                boundingBoxes.Add(item.BoundingBox);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BombermanAdventure.Events;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Walls;
using System.Diagnostics;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    abstract class AbstractExplosion : AbstractGameModel
    {
        public List<AbstractEnemy> KilligEnemies { get; set; }
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
        protected Player player;
        public bool KillingPlayer { get; set; }

        public AbstractExplosion(Game game, Player player, Vector3 position, GameTime gameTime)
            : base(game)
        {
            creationTime = gameTime.TotalGameTime;
            modelPosition = new Vector3(position.X, 0, position.Z);
            range = player.PlayerProfile.BombRange;
            player = player;
            models = ModelList.GetInstance();
            KillingPlayer = false;
        }

        public bool isEnemyKilled(AbstractEnemy enemy)
        {
            foreach (var killingEnemy in KilligEnemies)
            {
                if (killingEnemy == enemy)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Initialize()
        {
            explosionItems = new List<ExplosionItem>();
            boundingBoxes = new List<BoundingBox>();
            KilligEnemies = new List<AbstractEnemy>();
            LoadContent();
        }

        protected override void LoadContent()
        {
            LoadExplosionItems();
            LoadBoundingBoxes();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var explosionItem in explosionItems)
            {
                explosionItem.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (creationTime.Milliseconds + 500 < gameTime.TotalGameTime.Milliseconds)
            {
                RegisterEvent(gameTime);
            }
            else 
            {
                foreach (var explosionItem in explosionItems)
                {
                    explosionItem.Update(gameTime);
                }
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

        public abstract Player.Bombs BombType();

        private bool IsCollidesWithDestroyableWall(Vector3 side)
        {
            foreach (var wall in models.Walls)
            {
                if (wall.ModelPosition.X == side.X && wall.ModelPosition.Y == side.Y && wall.ModelPosition.Z == side.Z)
                {
                    if (wall is FireWall)
                    {
                    }
                    return true;
                }
            }
            return false;
        }

        private bool IsCollidesWithLabyrynthBlock(Vector3 side)
        {
            foreach (var wall in models.Walls)
            {
                if (wall.ModelPosition.X == side.X && wall.ModelPosition.Y == side.Y && wall.ModelPosition.Z == side.Z)
                {
                    if (wall is FireWall)
                    {
                        if (BombType() == Player.Bombs.Water)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    if (wall is ElectricWall)
                    {
                        if (BombType() == Player.Bombs.Mud)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    if (wall is WaterWall)
                    {
                        if (BombType() == Player.Bombs.Electric)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    if (wall is BrickWall)
                    {
                        if (BombType() == Player.Bombs.Common)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    } 
                }
            }
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
            foreach (var item in explosionItems)
            {
                boundingBoxes.Add(item.BoundingBox);
            }
        }
    }
}

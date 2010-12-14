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
            this.range = player.BombRange;
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
            explosionItems.Add(new ExplosionItem(game, color, modelPosition));
            for (int i = 0; i != range; i++)
            {
                explosionItems.Add(new ExplosionItem(game, color, new Vector3(modelPosition.X + (20 * (i + 1)), modelPosition.Y, modelPosition.Z)));
                explosionItems.Add(new ExplosionItem(game, color, new Vector3(modelPosition.X - (20 * (i + 1)), modelPosition.Y, modelPosition.Z)));
                explosionItems.Add(new ExplosionItem(game, color, new Vector3(modelPosition.X, modelPosition.Y, modelPosition.Z + (20 * (i + 1)))));
                explosionItems.Add(new ExplosionItem(game, color, new Vector3(modelPosition.X, modelPosition.Y, modelPosition.Z - (20 * (i + 1)))));
            }
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BombermanAdventure.Models.GameModels.Bunuses;
using Microsoft.Xna.Framework;
using BombermanAdventure.Cameras;
using BombermanAdventure.Models.GameModels;
using BombermanAdventure.Models.GameModels.Bombs;
using BombermanAdventure.Models.GameModels.Walls;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;
using BombermanAdventure.Models.GameModels.Labyrinths;
using BombermanAdventure.Events;
using BombermanAdventure.Events.Bombs;
using BombermanAdventure.Events.Collisions;
using BombermanAdventure.Events.Explosions;

namespace BombermanAdventure.Models
{
    class ModelList
    {
        /// <summary>
        /// instance k modelListu pro vytvoreni jedinacka
        /// </summary>
        static ModelList _instance = null;

        /// <summary>
        /// Lidský hráč
        /// </summary>
        Player player;
        public Player Player
        {
            get { return player; }
            set
            {
                if (player == null)
                {
                    player = value;
                }
            }
        }

        /// <summary>
        /// Camera sceny
        /// </summary>
        Camera camera;
        public Camera Camera
        {
            get { return camera; }
            set
            {
                if (camera == null)
                {
                    camera = value;
                }
            }
        }

        /// <summary>
        /// Head-Up Display
        /// </summary>
        HUD hud;
        public HUD Hud
        {
            get { return hud; }
            set
            {
                if (hud == null)
                {
                    hud = value;
                }
            }
        }

        /// <summary>
        /// model labyrintu
        /// </summary>
        Labyrinth labyrinth;
        public Labyrinth Labyrinth
        {
            get { return labyrinth; }
            set
            {
                if (labyrinth == null)
                {
                    labyrinth = value;
                }
            }
        }

        /// <summary>
        /// modely bomb
        /// </summary>
        List<AbstractBomb> bombs;
        public List<AbstractBomb> Bombs
        {
            get { return bombs; }
        }

        List<AbstractEnemy> enemies;
        public List<AbstractEnemy> Enemies
        {
            get { return enemies; }
        }

        List<AbstractBonus> bonuses;
        public List<AbstractBonus> Bonuses
        {
            get { return bonuses; }
        }

        /// <summary>
        /// modely zdi
        /// </summary>
        List<AbstractWall> walls;
        public List<AbstractWall> Walls
        {
            get { return walls; }
        }

        /// <summary>
        /// modely explozi
        /// </summary>
        List<AbstractExplosion> explosions;
        public List<AbstractExplosion> Explosions
        {
            get { return explosions; }
        }


        private ModelList()
        {
            bombs = new List<AbstractBomb>();
            walls = new List<AbstractWall>();
            bonuses = new List<AbstractBonus>(); 
            enemies = new List<AbstractEnemy>();
            explosions = new List<AbstractExplosion>();
        }

        public static ModelList GetInstance()
        {
            if (_instance == null)
                _instance = new ModelList();
            return _instance;
        }

        public void AddBomb(AbstractBomb bomb)
        {
            bombs.Add(bomb);
        }

        public void AddEnemy(AbstractEnemy enemy)
        {
            enemies.Add(enemy);
        }

        public void AddWall(AbstractWall wall)
        {
            walls.Add(wall);
        }

        public void AddExplosion(AbstractExplosion explosion)
        {
            explosions.Add(explosion);
        }

        public void RegisterEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            if (ieEvent is AbstractBombExplosionEvent)
            {
                AbstractBomb bomb = (AbstractBomb)ieEvent.Model;
                player.OnEvent(ieEvent, gameTime);
                bombs.Remove(bomb);
            }
            else if (ieEvent is AbstractExplosionEvent)
            {
                SoundManager.SoundManager.PlayExplosion();
                AbstractExplosion explosion = (AbstractExplosion)ieEvent.Model;
                explosions.Remove(explosion);
            }
            else
            {
                player.OnEvent(ieEvent, gameTime);
            }

        }

        void CheckForEnemyCollisions(GameTime gameTime)
        {
            foreach (LabyrinthBlock block in labyrinth.Blocks)
            {
                foreach(var enemy in enemies)
                {
                    if (enemy.BoundingBox.Intersects(block.BoundingBox))
                    {
                        //block.ChangeColor(new Vector3(0f, 1f, 0f));
                        enemy.OnEvent(new CollisionEvent(player, block), gameTime);
                    }
                }
            }
            foreach (var wall in walls)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.BoundingBox.Intersects(wall.BoundingBox))
                    {
                        enemy.OnEvent(new WallCollisionEvent(player, wall), gameTime);
                    }
                }
            }
            /*foreach (var enemy1 in enemies)
            {
                foreach (var enemy2 in enemies)
                {
                    if (enemy1 == enemy2)
                    {
                        continue;
                    }
                    if (enemy1.BoundingBox.Intersects(enemy2.BoundingBox))
                    {
                        //block.ChangeColor(new Vector3(0f, 1f, 0f));
                        enemy1.OnEvent(new CollisionEvent(player, enemy2), gameTime);
                        enemy2.OnEvent(new CollisionEvent(player, enemy1), gameTime);
                    }
                }
            }*/
        }

        /// <summary>
        /// metoda pro kontrolovani kolizi lidskeho hrace
        /// </summary>
        void CheckForHumanPlayerCollisions(GameTime gameTime)
        {
            foreach (LabyrinthBlock block in labyrinth.Blocks)
            {
                if (player.BoundingSphere.Intersects(block.BoundingBox))
                {
                    //block.ChangeColor(new Vector3(0f, 1f, 0f));
                    player.OnEvent(new CollisionEvent(player, block), gameTime);
                    return;
                }
            }

            List<AbstractBonus> bonusesToRemove = new List<AbstractBonus>(2);
            foreach (var bonus in bonuses)
            {
                if (player.BoundingSphere.Intersects(bonus.BoundingBox))
                {
                    bonusesToRemove.Add(bonus);
                }
            }
            foreach (var bonus in bonusesToRemove)
            {
                if (bonus is BombBonus)
                {
                    player.PlayerProfile.PossibleBombsCount++;
                    SoundManager.SoundManager.PlayBonus();
                }
                if (bonus is SpeedBonus)
                {
                    player.PlayerProfile.Speed++;
                    SoundManager.SoundManager.PlayBonus();
                }
                if (bonus is FlameBonus)
                {
                    player.PlayerProfile.BombRange++;
                    SoundManager.SoundManager.PlayBonus();
                }
                if (bonus is DoorBonus)
                {
                    player.Winner = true;
                    player.PlayerProfile.InGame = false;
                    player.Remove();
                    SoundManager.SoundManager.PlayWin();
                }
                bonuses.Remove(bonus);
            }

            foreach (var wall in walls)
            {
                if (player.BoundingSphere.Intersects(wall.BoundingBox))
                {
                    player.OnEvent(new CollisionEvent(player, wall), gameTime);
                    return;
                }
            }
            foreach (AbstractBomb bomb in bombs)
            {
                if (player.BoundingSphere.Intersects(bomb.BoundingSphere) && bomb.isCollidable)
                {
                    player.OnEvent(new CollisionEvent(player, bomb), gameTime);
                    return;
                }
            }
            foreach (AbstractEnemy enemy in enemies)
            {
                if (player.BoundingSphere.Intersects(enemy.BoundingSphere))
                {
                    KillPlayer();
                    return;
                }
            }
            foreach (AbstractBomb bomb in bombs)
            {
                foreach (AbstractEnemy enemy in enemies)
                {
                    if (enemy.BoundingBox.Intersects(bomb.BoundingSphere) && bomb.isCollidable)
                    {
                        enemy.OnEvent(new CollisionEvent(player, bomb), gameTime);
                        return;
                    }
                }
            }
        }

        void KillPlayer()
        {
            Player.PlayerProfile.Life = 0;
            Player.Dead = true;
            Player.PlayerProfile.InGame = false;
            Player.Remove();
            SoundManager.SoundManager.PlayDeath();
        }

        void CheckForBombExplosionCollisions(GameTime gameTime)
        {
            var destroyedWalls = new List<AbstractWall>();
            var killedEnemies = new List<AbstractEnemy>();
            foreach (AbstractExplosion explosion in explosions)
            {
                foreach (BoundingBox box in explosion.BoundingBoxes)
                {

                    foreach (var enemy in enemies)
                    {
                        if(enemy.BoundingBox.Intersects(box) && !explosion.isEnemyKilled(enemy))
                        {
                            Player.PlayerProfile.Score += 25;
                            enemy.Live -= 100;
                            if(enemy.Live <= 0)
                            {
                                killedEnemies.Add(enemy);
                            }
                            explosion.KilligEnemies.Add(enemy);
                        }
                    }
                    
                    if (Player.BoundingSphere.Intersects(box) && !explosion.KillingPlayer)
                    {
                        if (Player.PlayerProfile.Life <= 60)
                        {
                            KillPlayer();
                        }
                        else
                        {
                            Player.PlayerProfile.Life -= 60;
                        }
                        explosion.KillingPlayer = true;

                    }

                    foreach (AbstractBomb bomb in bombs)
                    {
                        if (bomb.BoundingSphere.Intersects(box))
                        {
                            //Logger.log(Log_Type.INFO, "Explosion", explosion.ModelPosition);
                            bomb.OnEvent(new CollisionEvent(player, bomb), gameTime);
                            return;
                        }
                    }
                    foreach (var wall in walls)
                    {
                        if (wall.BoundingBox.Intersects(box))
                        {
                            destroyedWalls.Add(wall);
                        }
                    }
                    foreach (LabyrinthBlock block in labyrinth.Blocks)
                    {
                        if (block.BoundingBox.Intersects(box))
                        {
                            block.ChangeColor(new Vector3(0f, 0f, 1f));
                            //return;
                        }
                    }
                }
            }
            foreach (var wall in destroyedWalls)
            {
                if (wall.Bonus != null)
                {
                    Bonuses.Add(wall.Bonus);
                }
                Player.PlayerProfile.Score += 10;
                walls.Remove(wall);
            }
            foreach (var enemy in killedEnemies)
            {
                enemies.Remove(enemy);
                SoundManager.SoundManager.PlayEnemyKilled();
            }
        }

        public void Update(GameTime gameTime)
        {
            List<AbstractGameModel> modelsList = new List<AbstractGameModel>();
            foreach (AbstractBomb bomb in bombs)
            {
                modelsList.Add(bomb);
            }

            foreach (AbstractWall wall in walls)
            {
                modelsList.Add(wall);
            }

            foreach (AbstractBonus bonus in bonuses)
            {
                modelsList.Add(bonus);
            }

            foreach (AbstractEnemy enemy in enemies)
            {
                modelsList.Add(enemy);
            }

            foreach (AbstractExplosion explosion in explosions)
            {
                modelsList.Add(explosion);
            }

            foreach (AbstractGameModel model in modelsList)
            {
                model.Update(gameTime);
            }

            labyrinth.Update(gameTime);
            player.Update(gameTime);
            hud.Update(gameTime);
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }
            
            CheckForHumanPlayerCollisions(gameTime);
            CheckForEnemyCollisions(gameTime);
            CheckForBombExplosionCollisions(gameTime);
        }

        public void DrawModels(GameTime gameTime)
        {
            /* GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
             //GraphicsDevice.BlendState = BlendState.AlphaBlend;
             GraphicsDevice.DepthStencilState = DepthStencilState.Default;

             foreach (AbstractGameModel model in models.Models)
             {
                 model.Draw(gameTime);
             }
             models.Player.Draw(gameTime);

             GraphicsDevice.DepthStencilState = DepthStencilState.None;
             models.Hud.Draw(gameTime);*/
        }

        public void DrawBackground(GameTime gameTime)
        {

        }

        public void DrawLabyrinth(GameTime gameTime)
        {
            labyrinth.Draw(gameTime);
        }

        public void DrawWalls(GameTime gameTime)
        {
            foreach (AbstractWall wall in walls)
            {
                wall.Draw(gameTime);
            }
        }

        public void DrawBonuses(GameTime gameTime)
        {
            foreach (AbstractBonus bonus in bonuses)
            {
                bonus.Draw(gameTime);
            }
        }

        public void DrawEnemies(GameTime gameTime) 
        {
            foreach (AbstractEnemy enemy in enemies)
            {
                enemy.Draw(gameTime);
            }
        }

        public void DrawBonusesAndGuns(GameTime gameTime)
        {

        }

        public void DrawBombs(GameTime gameTime)
        {
            foreach (AbstractBomb bomb in bombs)
            {
                bomb.Draw(gameTime);
            }
        }

        public void DrawExplosions(GameTime gameTime)
        {
            foreach (AbstractExplosion explosion in explosions)
            {
                explosion.Draw(gameTime);
            }
        }

        public void DrawShots(GameTime gameTime)
        {

        }

        public void DrawPlayer(GameTime gameTime)
        {
            if (!player.Dead)
            {
                player.Draw(gameTime);
            }
        }

        public void DrawHUD(GameTime gameTime)
        {
            hud.Draw(gameTime);
        }


        public static void Clean()
        {
            _instance = null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Models;
using BombermanAdventure.Models.GameModels;
using BombermanAdventure.Models.GameModels.Bunuses;
using BombermanAdventure.Models.GameModels.Walls;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Labyrinths;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Generators
{
    class LevelGenerator
    {
        ModelList models = ModelList.GetInstance();
        public ModelList GenerateLevel(Game game)
        {
            models.Labyrinth = new Labyrinth(game, 8, 8);
            
            //dvere
            AbstractWall wall = new ElectricWall(game, -6, -7);
            wall.Bonus = new DoorBonus(game, wall);
            models.AddWall(wall);

            wall = new WaterWall(game, -6, -8);
            models.AddWall(wall);

            wall = new WaterWall(game, -7, -8);
            models.AddWall(wall);

            wall = new FireWall(game, -8, -8);
            models.AddWall(wall);

            models.AddEnemy(new ClassicEnemy(game, -8, -7));

            wall = new FireWall(game, -8, -5);
            wall.Bonus = new SpeedBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, -7, -6);
            wall.Bonus = new FlameBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, 1, 2);
            wall.Bonus = new BombBonus(game, wall);
            models.AddWall(wall);

            wall = new ElectricWall(game, 1, 1);
            wall.Bonus = new SpeedBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, 2, 5);
            wall.Bonus = new FlameBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, 4, 5);
            wall.Bonus = new FlameBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, -2, -5);
            wall.Bonus = new SpeedBonus(game, wall);
            models.AddWall(wall);

            wall = new ElectricWall(game, 3, 8);
            wall.Bonus = new BombBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, 6, 8);
            models.AddWall(wall);

            wall = new FireWall(game, 8, 5);
            wall.Bonus = new BombBonus(game, wall);
            models.AddWall(wall);

            wall = new FireWall(game, 7, 6);
            models.AddWall(wall);

            wall = new FireWall(game, 0, 0);
            models.AddWall(wall);

            wall = new FireWall(game, 0, 0);
            models.AddWall(wall);

            wall = new FireWall(game, 0, 1);
            wall.Bonus = new BombBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, 0, 2);
            models.AddWall(wall);

            wall = new BrickWall(game, 0, 3);
            models.AddWall(wall);

            wall = new BrickWall(game, 0, 4);
            wall.Bonus = new BombBonus(game, wall);
            models.AddWall(wall);

            wall = new FireWall(game, 0, 5);
            wall.Bonus = new FlameBonus(game, wall);
            models.AddWall(wall);

            wall = new WaterWall(game, 6, 7);
            models.AddWall(wall);

            wall = new BrickWall(game, 4, 8);
            models.AddWall(wall);

            wall = new FireWall(game, 4, 6);
            models.AddWall(wall);

            wall = new ElectricWall(game, -4, 0);
            models.AddWall(wall);

            wall = new FireWall(game, -4, 1);
            models.AddWall(wall);

            wall = new ElectricWall(game, -4, 2);
            wall.Bonus = new FlameBonus(game, wall);
            models.AddWall(wall);

            wall = new WaterWall(game, -4, 4);
            models.AddWall(wall);

            wall = new FireWall(game, -4, -2);
            models.AddWall(wall);

            wall = new BrickWall(game, -4, -6);
            wall.Bonus = new SpeedBonus(game, wall);
            models.AddWall(wall);

            wall = new WaterWall(game, 7, -4);
            models.AddWall(wall);

            wall = new WaterWall(game, 8, 6);
            models.AddWall(wall);

            models.AddEnemy(new ClassicEnemy(game, -7, 8));
            models.AddEnemy(new ClassicEnemy(game, 2, 8));
            models.AddEnemy(new ClassicEnemy(game, -3, 2));
            models.AddEnemy(new SuperEnemy(game, 4, 6));
            models.AddEnemy(new SuperEnemy(game, -8, 0));
            models.AddEnemy(new SuperEnemy(game, 6, 3));

            models.Player = new Player(game, BombermanAdventureGame.ActivePlayer, 8, 8);

            return models;
        }
    }
}
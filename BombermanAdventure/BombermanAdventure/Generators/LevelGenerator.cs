﻿using System;
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

            var wall = new BrickWall(game, 0, 0);
            wall.Bonus = new BombBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, 1, 2);
            wall.Bonus = new BombBonus(game, wall);
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

            wall = new BrickWall(game, 3, 8);
            wall.Bonus = new BombBonus(game, wall);
            models.AddWall(wall);

            wall = new BrickWall(game, 6, 8);
            wall.Bonus = new DoorBonus(game, wall);
            models.AddWall(wall);

            models.AddEnemy(new ClassicEnemy(game, -7, 8));
            models.AddEnemy(new ClassicEnemy(game, 7, 8));
            models.AddEnemy(new SuperEnemy(game, 4, 6));
            models.AddEnemy(new SuperEnemy(game, 6, 3));



            models.Player = new Player(game, BombermanAdventureGame.ActivePlayer, 8, 8);

            return models;
        }
    }
}

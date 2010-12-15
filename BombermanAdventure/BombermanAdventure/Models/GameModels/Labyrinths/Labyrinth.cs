using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BombermanAdventure.Models.GameModels.Labyrinths
{
    class Labyrinth : AbstractGameModel
    {
        /// <summary>
        /// velikost jednoho pole
        /// </summary>
        int BLOCK_SIZE = 20;

        /// <summary>
        /// pocet neznicitelnych bloku v ose x
        /// </summary>
        int indestructibleBlocksCountOnX;

        /// <summary>
        /// pocet neznicitelnych bloku v ose y
        /// </summary>
        int indestructibleBlocksCountOnY;

        /// <summary>
        /// sirka mapy
        /// </summary>
        int mapWidth;

        /// <summary>
        /// vyska mapy
        /// </summary>
        int mapHeight;

        /// <summary>
        /// seznam bloku pro podlahu
        /// </summary>
        List<LabyrinthFloorUnit> floor;

        /// <summary>
        /// seznam neznicitelnych bloku
        /// </summary>
        List<LabyrinthBlock> blocks;
        public List<LabyrinthBlock> Blocks
        {
            get { return blocks; }
        }

        /// <summary>
        /// konstruktor labyrintu
        /// </summary>
        /// <param name="game">instance hry</param>
        /// <param name="x">velikost herniho pole v ose x</param>
        /// <param name="y">velikost herniho pole v ose y</param>
        public Labyrinth(Game game, int x, int y)
            : base(game)
        {
            this.indestructibleBlocksCountOnX = x;
            this.indestructibleBlocksCountOnY = y;
            this.Initialize();
        }

        /// <summary>
        /// metoda pro nahrani modelu
        /// </summary>
        protected override void LoadContent()
        {
            floor = this.GenerateFloor(mapWidth, mapHeight);
            blocks = this.GenerateBlocks(mapWidth, mapHeight);
        }

        /// <summary>
        /// inicializacni metoda
        /// </summary>
        public override void Initialize()
        {
            mapWidth = (2 * indestructibleBlocksCountOnX) + 1;
            mapHeight = (2 * indestructibleBlocksCountOnY) + 1;

            base.Initialize();
        }

        /// <summary>
        /// metoda pro vykresleni labyrintu
        /// </summary>
        /// <param name="gameTime">herni cas</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (LabyrinthFloorUnit unit in floor)
            {
                unit.Draw(gameTime);
            }
            foreach (LabyrinthBlock block in blocks)
            {
                block.Draw(gameTime);
            }
        }

        /// <summary>
        /// metoda pro generovani podlahy
        /// </summary>
        /// <param name="x">velikost herniho pole v ose x</param>
        /// <param name="y">velikost herniho pole v ose y</param>
        /// <returns>seznam s vygenerovanou podlahou</returns>
        private List<LabyrinthFloorUnit> GenerateFloor(int x, int y)
        {
            int xx = (x / 2 + 1) * BLOCK_SIZE;
            int yy = (y / 2 + 1) * BLOCK_SIZE;

            int xxx = xx;
            List<LabyrinthFloorUnit> floor = new List<LabyrinthFloorUnit>();

            for (int i = 0; i != (y + 2); i++)
            {
                for (int j = 0; j != (x + 2); j++)
                {
                    LabyrinthFloorUnit unit = new LabyrinthFloorUnit(Game, new Vector3(xx, 0, yy));
                    floor.Add(unit);
                    xx -= BLOCK_SIZE;
                }
                yy -= BLOCK_SIZE;
                xx = xxx;
            }

            return floor;
        }

        /// <summary>
        /// metoda pro generovani neznicitelnych bloku
        /// </summary>
        /// <param name="x">velikost herniho pole v ose x</param>
        /// <param name="y">velikost herniho pole v ose y</param>
        /// <returns>seznam s vygenerovanymi neznicitelnymi bloky</returns>
        private List<LabyrinthBlock> GenerateBlocks(int x, int y)
        {
            int xx = (x / 2 + 1) * BLOCK_SIZE;
            int yy = (y / 2 + 1) * BLOCK_SIZE;

            int xxx = xx;
            xx -= BLOCK_SIZE;
            List<LabyrinthBlock> blocks = new List<LabyrinthBlock>();

            for (int i = 0; i != x; i++)
            {
                LabyrinthBlock block = new LabyrinthBlock(Game, new Vector3(xx, 0, yy));
                blocks.Add(block);
                block = new LabyrinthBlock(Game, new Vector3(xx, 0, -yy));
                blocks.Add(block);
                xx -= BLOCK_SIZE;
            }
            xx = xxx;

            for (int i = 0; i != (y + 2); i++)
            {
                LabyrinthBlock block = new LabyrinthBlock(Game, new Vector3(xx, 0, yy));
                blocks.Add(block);
                block = new LabyrinthBlock(Game, new Vector3(-xx, 0, yy));
                blocks.Add(block);
                yy -= BLOCK_SIZE;
            }

            xx = (x / 2 - 1) * BLOCK_SIZE;
            yy = (y / 2 - 1) * BLOCK_SIZE;
            xxx = xx;

            for (int i = 0; i < y - 1; i += 2)
            {
                for (int j = 0; j < x - 1; j += 2)
                {
                    LabyrinthBlock block = new LabyrinthBlock(Game, new Vector3(xx, 0, yy));
                    blocks.Add(block);
                    xx -= 2 * BLOCK_SIZE;
                }
                yy -= 2 * BLOCK_SIZE;
                xx = xxx;
            }

            return blocks;
        }

        /// <summary>
        /// metoda pro provedeni reakce na udalost
        /// </summary>
        /// <param name="ieEvent">udalost</param>
        public override void OnEvent(Events.CommonEvent ieEvent, GameTime gameTime) { }
    }
}

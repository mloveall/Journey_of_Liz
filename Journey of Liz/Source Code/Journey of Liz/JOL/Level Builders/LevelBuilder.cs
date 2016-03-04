using JOL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using JOL.Classes.BlockClasses;
using Microsoft.Xna.Framework.Storage;
using JOL.Classes.ItemClasses;
using JOL.Classes.MiscClasses;

namespace JOL
{
    /// <summary>
    /// This class loads a .csv file to generate a level of blocks and enemies based on the combination of item codes.
    /// </summary>

    public class LevelBuilder
    {
        // Global variables
        static int modifier = 0;

        Texture2D marioTexture;
        Texture2D blockUsedTexture, blockBrickTexture, blockGrassTexture, blockSandTexture, blockHiddenTexture, blockQuestionTexture, blockStairPyramidTexture, blockBluePortalTexture, blockOrangePortalTexture, blockJumpTexture;
        Texture2D[] blockFloorTextures;
        Texture2D itemCoinTexture, itemFireFlowerTexture, itemMushroomTexture, itemOneUpMushroomTexture, itemStarTexture;
        Texture2D enemyGoombaTexture, enemyKoopaTexture;
        Texture2D bluePortalTexture, orangePortalTexture;
        Texture2D flagPoleTexture;
        Texture2D bigHillTexture, cloudTexture;
        Texture2D instructionOneTexture, instructionTwoTexture;
        SoundEffect soundKick, soundCoin, soundBreakBlock, soundAppears;
        SpriteFont hudFont;
        ContentManager content;

        // Constructor
        public LevelBuilder(ContentManager content)
        {
            // Loads textures to each of the manager
            marioTexture = content.Load<Texture2D>("Liz/liz_idle");
            blockBrickTexture = content.Load<Texture2D>("Blocks/brickBlock");
            blockGrassTexture = content.Load<Texture2D>("Blocks/grassBlock");
            blockSandTexture = content.Load<Texture2D>("Blocks/sandBlock");
            blockBluePortalTexture = content.Load<Texture2D>("Blocks/bluePortalBlock");
            blockOrangePortalTexture = content.Load<Texture2D>("Blocks/orangePortalBlock");
            blockHiddenTexture = content.Load<Texture2D>("Blocks/hiddenBlock");
            blockQuestionTexture = content.Load<Texture2D>("Blocks/questionBlock");
            blockStairPyramidTexture = content.Load<Texture2D>("Blocks/stairPyramidBlock");
            blockUsedTexture = content.Load<Texture2D>("Blocks/usedBlock");
            blockJumpTexture = content.Load<Texture2D>("Blocks/bounceBlock");
            itemCoinTexture = content.Load<Texture2D>("Items/coinSprite");
            itemFireFlowerTexture = content.Load<Texture2D>("Items/flowerSprite");
            itemMushroomTexture = content.Load<Texture2D>("Items/hamieEggSprite");
            itemOneUpMushroomTexture = content.Load<Texture2D>("Items/oneUpSprite");
            itemStarTexture = content.Load<Texture2D>("Items/coverageSprite");
            enemyGoombaTexture = content.Load<Texture2D>("Enemies/shroomySprite");
            enemyKoopaTexture = content.Load<Texture2D>("Enemies/hamieSprite");
            flagPoleTexture = content.Load<Texture2D>("Misc/flagPole");
            instructionOneTexture = content.Load<Texture2D>("Instructions/instructionMove");
            instructionTwoTexture = content.Load<Texture2D>("Instructions/instructionSwitch");
            bluePortalTexture = content.Load<Texture2D>("Misc/bluePortal");
            orangePortalTexture = content.Load<Texture2D>("Misc/orangePortal");
            bigHillTexture = content.Load<Texture2D>("Misc/bigHill");
            cloudTexture = content.Load<Texture2D>("Misc/cloud");
            hudFont = content.Load<SpriteFont>("HudFont");
            soundKick = content.Load<SoundEffect>("Sounds/kick");
            soundCoin = content.Load<SoundEffect>("Sounds/coin");
            soundBreakBlock = content.Load<SoundEffect>("Sounds/breakblock");
            soundAppears = content.Load<SoundEffect>("Sounds/powerup_appears");

            blockFloorTextures = new Texture2D[4];
            blockFloorTextures[0] = content.Load<Texture2D>("Blocks/floorBlock0");
            blockFloorTextures[1] = content.Load<Texture2D>("Blocks/floorBlock1");
            blockFloorTextures[2] = content.Load<Texture2D>("Blocks/floorBlock2");
            blockFloorTextures[3] = content.Load<Texture2D>("Blocks/floorBlock3");

            this.content = content;
        }

        public Level BuildLevel(Game1 game, int levelNum, Level previousLevel)
        {
            List<IBlock> blocks = new List<IBlock>();
            List<IItem> items = new List<IItem>();
            List<IEnemy> enemies = new List<IEnemy>();
            List<KillZone> killZones = new List<KillZone>();
            List<Portal> portals = new List<Portal>();
            FlagPole flagPole = null;
            List<BigHill> bigHills = new List<BigHill>();
            List<Cloud> clouds = new List<Cloud>();
            InstructionOne instructionOne = null;
            InstructionTwo instructionTwo = null;
            Player mario = null;
            Player luigi = null;
            ICamera camera;
            Random rand = new Random();
            string path = "Content/Levels/level"+ levelNum + ".csv";
            StreamReader readFile = new StreamReader(path);

            String line = readFile.ReadLine();
            Level level;

            int j=0;

            // Loop through the content of the .csv file and put everything in the building structure
            while (line != null)
            {
                int i = 0;
                for (int index = 0; index < line.Length; index++)
                {
                    char character = line[index];
                    switch (character)
                    {
                        case 'k': // Load a kill zone
                            {
                                killZones.Add(new KillZone(new Vector2(modifier + i * 32, j * 32)));
                                index++;

                                break;
                            }
                        case 'p': // Load a portal
                            {
                                index++;
                                Texture2D portalTexture = orangePortalTexture;
                                bool facingLeft = false;
                                int portalIndex1;
                                if (line[index] == 'b')
                                {
                                    portalTexture = bluePortalTexture;
                                }
                                index++;
                                if (line[index] == 'l')
                                {
                                    facingLeft = true;
                                }
                                index++;
                                portalIndex1 = line[index];
                                portals.Add(new Portal(portalTexture, new Vector2(modifier + i * 32, j * 32), portalIndex1, facingLeft));

                                break;
                            }
                        case 'f': // Load a flag
                            {
                                flagPole = new FlagPole(flagPoleTexture, new Vector2(modifier + i * 32 - 16, j * 32 - 304 + 32));
                                index++;

                                break;
                            }
                        case 'h': // Load a background hill image
                            {
                                bigHills.Add(new BigHill(bigHillTexture, new Vector2(modifier + i * 32 - 64, j * 32 - 224)));

                                break;
                            }
                        case 'c': // Load a background cloud image
                            {
                                clouds.Add(new Cloud(cloudTexture, new Vector2(modifier + i * 32 - 128, j * 32 - 64)));

                                break;
                            }
                        case '1': // Load instruction 1
                            {
                                instructionOne = new InstructionOne(instructionOneTexture, new Vector2(modifier + i * 32, j * 32));

                                break;
                            }
                        case '2': // Load instruction 2
                            {
                                instructionTwo = new InstructionTwo(instructionTwoTexture, new Vector2(modifier + i * 32, j * 32));

                                break;
                            }
                        case 'b': // Load a block, need further analysis to determine which block type it is
                            {
                                index++;
                                character = line[index];
                                IItemContainer container;
                                switch (character)
                                {
                                    case 'b': // Brick block
                                        {
                                            if (index + 1 >= line.Length)
                                                break;
                                            else
                                            {
                                                if (line[index + 1] == '(')
                                                {
                                                    index++;
                                                    container = ParseItemContainer(line, ref index, modifier + i * 32, j * 32, items);
                                                }
                                                else
                                                {
                                                    container = new EmptyItemContainer();
                                                }
                                                blocks.Add(new BrickBlock(blockBrickTexture, new Vector2(modifier + i * 32, j * 32), container, soundBreakBlock));
                                                break;
                                            }
                                        }
                                    case 'f': // Floor block
                                        {
                                            int option = rand.Next(4);
                                            blocks.Add(new FloorBlock(blockFloorTextures[option], new Vector2(modifier + i * 32, j * 32)));
                                            break;
                                        }
                                    case 'g': // Grass block
                                        {
                                            blocks.Add(new GrassBlock(blockGrassTexture, new Vector2(modifier + i * 32, j * 32)));
                                            break;
                                        }
                                    case 'a': // Sand block
                                        {
                                            blocks.Add(new SandBlock(blockSandTexture, new Vector2(modifier + i * 32, j * 32)));
                                            break;
                                        }
                                    case 'p': // Portal block
                                        {
                                            if (line[index + 1] == 'b')
                                            {

                                                index++;
                                                int portalIndex = line[index + 1];
                                                index++;
                                                blocks.Add(new BluePortalBlock(blockBluePortalTexture, blockUsedTexture, new Vector2(modifier + i * 32, j * 32), portalIndex));
                                            }
                                            else if (line[index + 1] == 'o')
                                            {
                                                index++;
                                                int portalIndex = line[index + 1];
                                                index++;
                                                blocks.Add(new OrangePortalBlock(blockOrangePortalTexture, blockUsedTexture, new Vector2(modifier + i * 32, j * 32), portalIndex));
                                            }
                                            break;
                                        }
                                    case 'h': // Hidden block
                                        {
                                            if (line[index + 1] == '(')
                                            {
                                                index++;
                                                container = ParseItemContainer(line, ref index, modifier + i * 32, j * 32, items);
                                            }
                                            else
                                            {
                                                container = new EmptyItemContainer();
                                            }
                                            blocks.Add(new HiddenBlock(blockHiddenTexture, blockUsedTexture, new Vector2(modifier + i * 32, j * 32), container));
                                            break;
                                        }
                                    case 'q': // Question block
                                        {
                                            if (line[index + 1] == '(')
                                            {
                                                index++;
                                                container = ParseItemContainer(line, ref index, modifier + i * 32, j * 32, items);
                                            }
                                            else
                                            {
                                                container = new EmptyItemContainer();
                                            }
                                            blocks.Add(new QuestionBlock(blockQuestionTexture, blockUsedTexture, new Vector2(modifier + i * 32, j * 32), container));
                                            break;
                                        }
                                    case 's': // Stair block
                                        {
                                            blocks.Add(new StairBlock(blockStairPyramidTexture, new Vector2(modifier + i * 32, j * 32)));
                                            break;
                                        }
                                    case 'u': // Used block
                                        {
                                            blocks.Add(new UsedBlock(blockUsedTexture, new Vector2(modifier + i * 32, j * 32)));
                                            break;
                                        }
                                    case 'j': // Bounce block
                                        {
                                            blocks.Add(new StairBlock(blockJumpTexture, new Vector2(modifier + i * 32, j * 32)));
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                                break;
                            }
                        case 'i': // Load an item
                            {
                                index++;
                                character = line[index];
                                switch (character)
                                {
                                    case 'f': // Fire flower
                                        {
                                            items.Add(new FireFlowerItem(itemFireFlowerTexture, modifier + i * 32, j * 32, true, soundAppears));
                                            break;
                                        }
                                    case 'm': // Mushroom
                                        {
                                            items.Add(new MushroomItem(itemMushroomTexture, modifier + i * 32, j * 32, true, soundAppears));
                                            break;
                                        }
                                    case 'u': // One-up mushroom
                                        {
                                            items.Add(new OneUpMushroomItem(itemOneUpMushroomTexture, modifier + i * 32, j * 32, true));
                                            break;
                                        }
                                    case 'c': // Coin
                                        {
                                            items.Add(new CoinItem(itemCoinTexture, soundCoin, modifier + i * 32 + 6, j * 32, true));
                                            break;
                                        }
                                    case 's': // Star
                                        {
                                            items.Add(new StarItem(itemStarTexture, modifier + i * 32, j * 32, true));
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                                break;
                            }
                        case 'e': // Load an enemy
                            {
                                index++;
                                character = line[index];
                                switch (character)
                                {
                                    case 'g': // Goomba
                                        {
                                            enemies.Add(new GoombaEnemy(enemyGoombaTexture, soundKick, modifier + i * 32, j * 32));
                                            break;
                                        }
                                    case 'k': // Koopa
                                        {
                                            enemies.Add(new KoopaEnemy(enemyKoopaTexture, soundKick, modifier + i * 32, j * 32));
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                                break;
                            }
                        case 'm': // Load a Mario (character 1)
                            {
                                mario = new Player(marioTexture, content, new Vector2(modifier + i * 32, j * 32), false);
                                break;
                            }
                        case 'l': // Load a Luigi (character 2)
                            {
                                luigi = new Player(marioTexture, content, new Vector2(modifier + i * 32, j * 32), true);
                                break;
                            }
                        case ',': // Load an empty space
                            {
                                i++;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                line = readFile.ReadLine();
                j++;
            }

            MultiPlayerHolder holder = new MultiPlayerHolder(mario, luigi);
            camera = new FollowCharacterCamera(holder, game.Window.ClientBounds.Height, game.Window.ClientBounds.Width);
            HeadsUpDisplay hud = new HeadsUpDisplay(hudFont);

            level = new Level(mario, luigi, items, blocks, enemies, killZones, flagPole, instructionOne, instructionTwo, bigHills, clouds, portals, game, camera, hud);
            if (previousLevel != null)
            {
                level.score = previousLevel.score;
                level.lives = previousLevel.lives;
            }
            
            hud.setLevel(level);
            mario.level = level;
            luigi.level = level;
            LinkPortalBlocks(blocks);
            LinkPortals(portals);

            return level;
        }

        // Connects two portal doors
        private void LinkPortals(List<Portal> portals)
        {
            for (int i =0; i < portals.Count(); i++)
            {
                Portal portal1 = portals[i];
                for (int j = i + 1; j < portals.Count(); j++)
                {
                    Portal portal2 = portals[j];
                    if (portal1.portalIndex == portal2.portalIndex)
                    {
                        portal1.setOutPortal(portal2);
                        portal2.setOutPortal(portal1);
                    }
                }
            }
        }

        // Connects two portal blocks
        private void LinkPortalBlocks(List<IBlock> blocks)
        {
            for(int i =0; i < blocks.Count(); i++)
            {
                if (blocks[i] is OrangePortalBlock)
                {
                    OrangePortalBlock opBlock = (OrangePortalBlock) blocks[i];
                    for (int j = i + 1; j < blocks.Count(); j++)
                    {
                        if (blocks[j] is BluePortalBlock )
                        {
                            BluePortalBlock bpBlock = (BluePortalBlock) blocks[j];
                            if (bpBlock.portalIndex == opBlock.portalIndex)
                            {
                                bpBlock.setOutPortal(opBlock);
                                opBlock.setOutPortal(bpBlock);
                            }
                        }
                    }
                }
                else if (blocks[i] is BluePortalBlock)
                {
                    BluePortalBlock bpBlock = (BluePortalBlock)blocks[i];
                    for (int j = i + 1; j < blocks.Count(); j++)
                    {
                        if (blocks[j] is OrangePortalBlock)
                        {
                            OrangePortalBlock opBlock = (OrangePortalBlock)blocks[j];
                            if (bpBlock.portalIndex == opBlock.portalIndex)
                            {
                                bpBlock.setOutPortal(opBlock);
                                opBlock.setOutPortal(bpBlock);
                            }
                        }
                    }
                }
            }
        }

        // Check to see if there's any item inside a particular block.
        private IItemContainer ParseItemContainer(string line, ref int index, int x, int y, List<IItem> items)
        {
            char c = line[index];
            List<IItem> containerItems = new List<IItem>();
            IItemContainer container;
            bool isVariable = false;

            while (c != ')')
            {
                IItem item = null;
                switch (c)
                {
                    case 'f': // Fire flower
                        {
                            item = new FireFlowerItem(itemFireFlowerTexture, x, y, false, soundAppears);
                            items.Add(item);
                            containerItems.Add(item);
                            System.Console.Out.WriteLine("fireflower added to block");
                            break;
                        }
                    case 'm': // Mushroom
                        {
                            item = new MushroomItem(itemMushroomTexture, x, y, false, soundAppears);
                            items.Add(item);
                            containerItems.Add(item);
                            break;
                        }
                    case 'u': // One-up mushroom
                        {
                            item = new OneUpMushroomItem(itemOneUpMushroomTexture, x, y, false);
                            items.Add(item);
                            containerItems.Add(item);
                            break;
                        }
                    case 'c': // Coin
                        {
                            item = new CoinItem(itemCoinTexture, x, y, false);
                            items.Add(item);
                            containerItems.Add(item);
                            break;
                        }
                    case 's': // Star
                        {
                            item = new StarItem(itemStarTexture, x, y, false);
                            items.Add(item);
                            containerItems.Add(item);
                            break;
                        }
                    case 'v': // Multiple items
                        {
                            isVariable = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                index ++;
                c = line[index];
            }

            // If a block contains more than one item.
            if (isVariable)
            {
                container = new VariableItemContainer(containerItems[0], containerItems[1]);
            }
            else
            {
                if (containerItems.Count == 0)
                {
                    container = new EmptyItemContainer();
                }
                else if (containerItems.Count == 1)
                {
                    container = new SingleItemContainer(containerItems[0]);
                }
                else
                {
                    container = new MultiItemContainer(containerItems);
                }
            }

            return container;
        }
    }
}

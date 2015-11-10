using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Classes.BlockClasses;
using JOL.Interfaces;
using JOL.Classes.MarioClasses;
using Microsoft.Xna.Framework;

namespace JOL.Commands
{
    class ResetCommand : ICommand
    {
        Mario mario;
        Mario luigi;
        List<IBlock> blocks;
        List<IItem> items;

        public ResetCommand(Mario mario, Mario luigi, List<IBlock> blocks, List<IItem> items)
        {
            this.mario = mario;
            this.luigi = luigi;
            this.blocks = blocks;
            this.items = items;
        }

        public void Execute()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Reset();
            }

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Reset();
            }

            Vector2 tempPos = mario.MarioSprite.SpritePosition;
            tempPos.X = 448;
            tempPos.Y = 352;
            mario.MarioSprite.SpritePosition = tempPos;
            mario.State = new SmallIdleMarioState(mario);
            mario.MarioSprite = new MarioSpriteSmallIdle(mario.MarioSprite);
            mario.MarioSprite.FacingRight = true;
            mario.MarioSprite.StarTimer = 0;
            mario.MyState = 1;
            mario.MarioSprite.FallSpeed = 0f;
            mario.MediaManager(1);
        }
    }
}

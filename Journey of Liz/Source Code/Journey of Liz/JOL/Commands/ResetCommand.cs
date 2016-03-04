using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using JOL.PlayerStates;
using JOL.Classes.BlockClasses;
using JOL.Classes.PlayerClasses;
using Microsoft.Xna.Framework;

namespace JOL.Commands
{
    class ResetCommand : ICommand
    {
        Player mario;
        Player luigi;
        List<IBlock> blocks;
        List<IItem> items;

        public ResetCommand(Player mario, Player luigi, List<IBlock> blocks, List<IItem> items)
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

            Vector2 tempPos = mario.PlayerSprite.spritePosition;
            tempPos.X = 448;
            tempPos.Y = 352;
            mario.PlayerSprite.spritePosition = tempPos;
            mario.State = new PlayerStateSmallIdle(mario);
            mario.PlayerSprite = new PlayerSpriteSmallIdle(mario.PlayerSprite);
            mario.PlayerSprite.isFacingRight = true;
            mario.PlayerSprite.starTimer = 0;
            mario.MyState = 1;
            mario.PlayerSprite.fallSpeed = 0f;
            mario.MediaManager(1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Classes.ItemClasses;
using JOL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace JOL.Classes.BlockClasses
{
    /// <summary>
    /// A basic brick block that can be destroyed through bumping.
    /// </summary>

    class IceBlock : Block
    {
        IItemContainer itemContainer;
        SoundEffect sound;
        SoundEffectInstance soundInstance;

        // Basic constructor
        public IceBlock(Texture2D texture, Vector2 location, IItemContainer itemContainer) : base(texture, location)
        {
            height = 32;
            width = 32;

            this.itemContainer = itemContainer;

            Initialize();
        }

        // Constructor with sound effect parameter
        public IceBlock(Texture2D texture, Vector2 location, IItemContainer itemContainer, SoundEffect sound) : base(texture, location)
        {
            height = 32;
            width = 32;

            this.itemContainer = itemContainer;
            this.sound = sound;
            soundInstance = sound.CreateInstance();

            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Nothing to update for floor block since no animation or change
        }

        public override void Reset()
        {
            if (isAlive == false)
            {
                isAlive = true;
                location.X -= 1800;
                location.Y -= 1800;
                destRectangle = new Rectangle((int)location.X, (int)location.Y, 32, 32);
            }
        }

        public override void Bump(Player player)
        {
            bool isPlayerSmall = false;
            if (player.myState == 1)
            {
                isPlayerSmall = true;
            }
            if (itemContainer.IsEmpty())
            {
                isAlive = false;
                toDelete = true;
                soundInstance.Play();
            }
            else
            {
                IItem item = itemContainer.ProduceItem(isPlayerSmall);
                item.Spawn();
                if (item is CoinItem)
                {
                    player.Collect(item);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using JOL.PlayerStates;
using JOL.Classes.PlayerClasses;
using JOL.Classes.BlockClasses;
using JOL.Classes.MiscClasses;

namespace JOL
{
    /// <summary>
    /// After a collision is detected, handle the responding activity of each collider.
    /// </summary>

    public static class CollisionHandler
    {
        // Constructor
        static public void HandleCollisions(Player mario, Player luigi, List<IBlock> blocks, List<IEnemy> enemies, List<KillZone> killZones, FlagPole flagPole, List<Portal> portals, ICamera camera, List<IItem> items)
        {
            HandleBlockCollisions(mario, blocks);
            HandleBlockCollisions(luigi, blocks);
            HandlePortalCollisions(mario, portals);
            HandlePortalCollisions(luigi, portals);
            HandleFlagPoleCollisions(mario, flagPole);
            HandleFlagPoleCollisions(luigi, flagPole);
            HandleMarioLuigiCollisions(mario, luigi);
            HandleItemCollisions(mario, items);
            HandleItemCollisions(luigi, items);
            HandleEnemyCollisions(mario, enemies);
            HandleEnemyCollisions(luigi, enemies);
            HandleEnemyBlockCollisions(enemies, blocks);
            HandleEnemyEnemyCollisions(enemies);
            HandleItemBlockCollisions(items, blocks);
            HandleItemItemCollisions(items);
            HandleMarioKillZoneCollisions(mario, killZones);
            HandleMarioKillZoneCollisions(luigi, killZones);
            HandleEnemiesOffScreen(camera, enemies);
            CollectAllGarbage(blocks, enemies, items);
        }

        // Take care of the collisions between Mario and blocks
        static private void HandleBlockCollisions(Player mario, List<IBlock> blocks)
        {
            foreach (IBlock block in blocks)
            {
                CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(mario.PlayerSprite.destRectangle, block.DestRectangle);
                switch (collision)
                {
                    case CollisionDetection.CollisionType.NoCollision:
                        // Do nothing.
                        break;
                    case CollisionDetection.CollisionType.TopCollision: // Top is colliding with something
                        if (mario.PlayerSprite.fallSpeed >= 0f)
                        {
                            if (mario.MyState == 1 && mario.PlayerSprite.isMoving == false && mario.PlayerSprite.isJumping)
                            {
                                mario.State = new PlayerStateSmallIdle(mario);
                                mario.PlayerSprite = new PlayerSpriteSmallIdle(mario.PlayerSprite);
                            }
                            else if (mario.MyState == 1 && mario.PlayerSprite.isJumping)
                            {
                                mario.State = new PlayerStateSmallRunning(mario);
                                mario.PlayerSprite = new PlayerSpriteSmallRunning(mario.PlayerSprite);
                            }
                            if (mario.MyState == 2 && mario.PlayerSprite.isMoving == false && mario.PlayerSprite.isJumping)
                            {
                                mario.State = new BigIdleMarioState(mario);
                                mario.PlayerSprite = new PlayerSpriteBigIdle(mario.PlayerSprite);
                            }
                            else if (mario.MyState == 2 && mario.PlayerSprite.isJumping)
                            {
                                mario.State = new BigRunningMarioState(mario);
                                mario.PlayerSprite = new PlayerSpriteBigRunning(mario.PlayerSprite);
                            }
                            if (mario.MyState == 3 && mario.PlayerSprite.isMoving == false && mario.PlayerSprite.isJumping)
                            {
                                mario.State = new FireIdleMarioState(mario);
                                mario.PlayerSprite = new PlayerSpriteFireIdle(mario.PlayerSprite);
                            }
                            else if (mario.MyState == 3 && mario.PlayerSprite.isJumping)
                            {
                                mario.State = new PlayerStateDemoRunning(mario);
                                mario.PlayerSprite = new PlayerSpriteFireRunning(mario.PlayerSprite);
                            }

                            mario.PlayerSprite.fallSpeed = 0f;
                            mario.MoveTo((int)mario.PlayerSprite.spritePosition.X, block.DestRectangle.Top - mario.PlayerSprite.destRectangle.Height + 1);
                        }
                        break;
                    case CollisionDetection.CollisionType.BottomCollision: // Bottom is colliding with something
                        if (mario.PlayerSprite.fallSpeed < 0f && mario.MyState != 0)
                        {
                            mario.MoveTo((int)mario.PlayerSprite.spritePosition.X, block.DestRectangle.Bottom + 1);
                            block.Bump(mario);
                            mario.PlayerSprite.fallSpeed = 0f;
                        }
                        break;
                    case CollisionDetection.CollisionType.LeftCollision: // Left is colliding with something
                        if (mario.MyState != 0)
                            mario.MoveTo(block.DestRectangle.Right + 1, (int)mario.PlayerSprite.spritePosition.Y);
                        break;
                    case CollisionDetection.CollisionType.RightCollision: // Right is colliding with something
                        if (mario.MyState != 0)
                            mario.MoveTo(block.DestRectangle.Left - mario.PlayerSprite.destRectangle.Width - 1, (int)mario.PlayerSprite.spritePosition.Y);
                        break;
                    default:
                        break;
                }

            }

        }

        // Take care of the collisions between enemies and blocks
        static private void HandleEnemyBlockCollisions(List<IEnemy> enemies, List<IBlock> blocks)
        {
            foreach (IEnemy enemy in enemies)
            {
                foreach (IBlock block in blocks)
                {
                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(enemy.DestRectangle, block.DestRectangle);
                    switch (collision)
                    {
                        case CollisionDetection.CollisionType.NoCollision:
                            if (enemy.FallSpeed.CompareTo(0.0f) == 0)
                            {
                                enemy.FallSpeed = 1.5f;
                            }
                            break;
                        case CollisionDetection.CollisionType.TopCollision:
                            enemy.FallSpeed = 0f;
                            enemy.MoveTo(enemy.DestRectangle.Left, block.DestRectangle.Top - enemy.DestRectangle.Height);
                            break;
                        case CollisionDetection.CollisionType.BottomCollision:
                            break;
                        case CollisionDetection.CollisionType.LeftCollision:
                            enemy.Flip();
                            break;
                        case CollisionDetection.CollisionType.RightCollision:
                            enemy.Flip();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        // Take care of the collisions between items and blocks
        static private void HandleItemBlockCollisions(List<IItem> items, List<IBlock> blocks)
        {
            foreach (IItem item in items)
            {
                if (item.isActive)
                {
                    foreach (IBlock block in blocks)
                    {
                        CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(item.DestRectangle, block.DestRectangle);
                        switch (collision)
                        {
                            case CollisionDetection.CollisionType.NoCollision:
                                if (item.FallSpeed.CompareTo(0.0f) == 0)
                                {
                                    item.FallSpeed = 1.5f;
                                }
                                break;
                            case CollisionDetection.CollisionType.TopCollision:
                                item.FallSpeed = 0f;
                                item.MoveTo(item.DestRectangle.Left, block.DestRectangle.Top - item.DestRectangle.Height);
                                break;
                            case CollisionDetection.CollisionType.BottomCollision:
                                break;
                            case CollisionDetection.CollisionType.LeftCollision:
                                item.Flip();
                                break;
                            case CollisionDetection.CollisionType.RightCollision:
                                item.Flip();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        // Take care of the collisions between enemies and enemies
        static private void HandleEnemyEnemyCollisions(List<IEnemy> enemies)
        {
            for (int i = 0; i < enemies.Count - 1; i++)
            {
                IEnemy enemy1 = enemies[i];

                for (int j = i + 1; j < enemies.Count; j++)
                {
                    IEnemy enemy2 = enemies[j];

                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(enemy1.DestRectangle, enemy2.DestRectangle);
                    switch (collision)
                    {
                        case CollisionDetection.CollisionType.NoCollision:
                            // Do nothing.
                            break;
                        case CollisionDetection.CollisionType.TopCollision:
                            // Do nothing.
                            break;
                        case CollisionDetection.CollisionType.BottomCollision:
                            // Do nothing.
                            break;
                        case CollisionDetection.CollisionType.LeftCollision:
                            enemy1.Flip();
                            enemy2.Flip();
                            break;
                        case CollisionDetection.CollisionType.RightCollision:
                            enemy1.Flip();
                            enemy2.Flip();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        // Take care of the collisions between items and items (such as mushrooms)
        static private void HandleItemItemCollisions(List<IItem> items)
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                IItem item1 = items[i];

                for (int j = i + 1; j < items.Count; j++)
                {
                    IItem item2 = items[j];

                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(item1.DestRectangle, item2.DestRectangle);
                    switch (collision)
                    {
                        case CollisionDetection.CollisionType.NoCollision:
                            // Do nothing.
                            break;
                        case CollisionDetection.CollisionType.TopCollision:
                            // Do nothing.
                            break;
                        case CollisionDetection.CollisionType.BottomCollision:
                            // Do nothing.
                            break;
                        case CollisionDetection.CollisionType.LeftCollision:
                            item1.Flip();
                            item2.Flip();
                            break;
                        case CollisionDetection.CollisionType.RightCollision:
                            item1.Flip();
                            item2.Flip();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        // Take care of the collisions between Mario and items
        static private void HandleItemCollisions(Player mario, List<IItem> items)
        {
            foreach (IItem item in items)
            {
                if (item.isActive)
                {
                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(mario.PlayerSprite.destRectangle, item.DestRectangle);
                    switch (collision)
                    {
                        case CollisionDetection.CollisionType.NoCollision:
                            // Do nothing.
                            break;
                        default:
                            if (mario.MyState != 0)
                            {
                                mario.Collect(item);
                                item.Collect();
                            }
                            break;
                    }
                }
            }
        }

        // Take care of the collisions between Mario and enemies
        static private void HandleEnemyCollisions(Player mario, List<IEnemy> enemies)
        {
            if (enemies != null)
            {
                foreach (IEnemy enemy in enemies)
                {
                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(mario.PlayerSprite.destRectangle, enemy.DestRectangle);
                    if (collision != CollisionDetection.CollisionType.NoCollision && mario.MyState != 0)
                    {
                        bool hitMario = enemy.Hit(collision, true);
                        if (hitMario)
                        {
                            if (!mario.isInvulnerable)
                            {
                                mario.Hit();
                            }
                        }
                        else
                        {
                            mario.PlayerSprite.fallSpeed = -7.4f;
                        }
                    }
                }
            }
        }

        // Take care of the collisions between Mario1 and Mario2
        static private void HandleMarioLuigiCollisions(Player mario, Player luigi)
        {
            Player currentMario, pausedMario;
            if (mario.isPaused)
            {
                currentMario = luigi;
                pausedMario = mario;
            }
            else
            {
                currentMario = mario;
                pausedMario = luigi;
            }
            CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(currentMario.PlayerSprite.destRectangle, pausedMario.PlayerSprite.destRectangle);
            switch (collision)
            {
                case CollisionDetection.CollisionType.NoCollision:
                    // Do nothing.
                    break;
                case CollisionDetection.CollisionType.TopCollision:
                    if (currentMario.PlayerSprite.fallSpeed >= 0f)
                    {
                        if (currentMario.MyState == 1 && currentMario.PlayerSprite.isMoving == false && currentMario.PlayerSprite.isJumping)
                        {
                            currentMario.State = new PlayerStateSmallIdle(currentMario);
                            currentMario.PlayerSprite = new PlayerSpriteSmallIdle(currentMario.PlayerSprite);
                        }
                        else if (currentMario.MyState == 1 && currentMario.PlayerSprite.isJumping)
                        {
                            currentMario.State = new PlayerStateSmallRunning(currentMario);
                            currentMario.PlayerSprite = new PlayerSpriteSmallRunning(currentMario.PlayerSprite);
                        }
                        if (currentMario.MyState == 2 && currentMario.PlayerSprite.isMoving == false && currentMario.PlayerSprite.isJumping)
                        {
                            currentMario.State = new BigIdleMarioState(currentMario);
                            currentMario.PlayerSprite = new PlayerSpriteBigIdle(currentMario.PlayerSprite);
                        }
                        else if (currentMario.MyState == 2 && currentMario.PlayerSprite.isJumping)
                        {
                            currentMario.State = new BigRunningMarioState(currentMario);
                            currentMario.PlayerSprite = new PlayerSpriteBigRunning(currentMario.PlayerSprite);
                        }
                        if (currentMario.MyState == 3 && currentMario.PlayerSprite.isMoving == false && currentMario.PlayerSprite.isJumping)
                        {
                            currentMario.State = new FireIdleMarioState(currentMario);
                            currentMario.PlayerSprite = new PlayerSpriteFireIdle(currentMario.PlayerSprite);
                        }
                        else if (currentMario.MyState == 3 && currentMario.PlayerSprite.isJumping)
                        {
                            currentMario.State = new PlayerStateDemoRunning(currentMario);
                            currentMario.PlayerSprite = new PlayerSpriteFireRunning(currentMario.PlayerSprite);
                        }
                        currentMario.PlayerSprite.fallSpeed = 0f;
                        currentMario.MoveTo((int)currentMario.PlayerSprite.spritePosition.X, pausedMario.PlayerSprite.destRectangle.Top - currentMario.PlayerSprite.destRectangle.Height + 1);
                    }
                    break;
                case CollisionDetection.CollisionType.BottomCollision:
                    if (currentMario.PlayerSprite.fallSpeed < 0f && currentMario.MyState != 0)
                    {
                        currentMario.MoveTo((int)currentMario.PlayerSprite.spritePosition.X, pausedMario.PlayerSprite.destRectangle.Bottom + 1);
                        currentMario.PlayerSprite.fallSpeed = 0f;
                    }
                    break;
                case CollisionDetection.CollisionType.LeftCollision:
                    if (currentMario.MyState != 0)
                        currentMario.MoveTo(pausedMario.PlayerSprite.destRectangle.Right + 1, (int)currentMario.PlayerSprite.spritePosition.Y);
                    break;
                case CollisionDetection.CollisionType.RightCollision:
                    if (currentMario.MyState != 0)
                        currentMario.MoveTo(pausedMario.PlayerSprite.destRectangle.Left - currentMario.PlayerSprite.destRectangle.Width - 1, (int)currentMario.PlayerSprite.spritePosition.Y);
                    break;
                default:
                    break;
            }
        }

        // Take care of enemies that are not in the screen
        private static void HandleEnemiesOffScreen(ICamera camera, List<IEnemy> enemies)
        {
            foreach (IEnemy e in enemies)
            {
                if (!e.IsAlive)
                {
                    if (camera.IsInView(e.DestRectangle))
                    {
                        e.IsAlive = true;
                    }
                }
            }
        }

        // Take care of the collisions between Mario and portals
        private static void HandlePortalCollisions(Player mario, List<Portal> portals)
        {
            foreach (Portal p in portals)
            {
                CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(mario.PlayerSprite.destRectangle, p.DestRectangle);
                switch (collision)
                {
                    case CollisionDetection.CollisionType.NoCollision:
                        break;
                    default:
                        p.Warp(mario);
                        break;
                }
            }
        }

        // Take care of the collisions between Mario and flag poles
        private static void HandleFlagPoleCollisions(Player mario, FlagPole flagPole)
        {
            CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(mario.PlayerSprite.destRectangle, flagPole.CollisionRectangle);
            switch (collision)
            {
                case CollisionDetection.CollisionType.NoCollision:
                    break;
                default:
                    mario.level.Win();
                    break;
            }
        }

        // Take care of the collisions between Mario and kill zones
        private static void HandleMarioKillZoneCollisions(Player mario, List<KillZone> killZones)
        {
            foreach (KillZone kz in killZones)
            {
                bool isDead = false;
                CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(mario.PlayerSprite.destRectangle, kz.DestRectangle);
                switch (collision)
                {
                    case CollisionDetection.CollisionType.NoCollision:
                        break;
                    default:
                        mario.Collect(new DeadMushroomItem());
                        isDead = true;
                        break;
                }
                if (isDead)
                {
                    break;
                }
            }
        }

        // Collects all garbage
        private static void CollectAllGarbage(List<IBlock> blocks, List<IEnemy> enemies, List<IItem> items)
        {
            blocks.RemoveAll(item => item.toDelete == true);
            enemies.RemoveAll(item => item.toDelete == true);
            items.RemoveAll(item => item.toDelete == true);
        }
    }
}

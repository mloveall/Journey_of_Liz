using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using JOL.PlayerStates;
using JOL.Classes.PlayerClasses;
using JOL.Classes.BlockClasses;
using JOL.Classes.ItemClasses;
using JOL.Classes.MiscClasses;

namespace JOL
{
    /// <summary>
    /// After a collision is detected, handle the responding activity of each collider.
    /// </summary>

    public static class CollisionHandler
    {
        // Constructor
        static public void HandleCollisions(Player player1, Player player2, List<IBlock> blocks, List<IEnemy> enemies, List<KillZone> killZones, HangingRope flagPole, List<Portal> portals, ICamera camera, List<IItem> items)
        {
            HandleBlockCollisions(player1, blocks);
            HandleBlockCollisions(player2, blocks);
            HandlePortalCollisions(player1, portals);
            HandlePortalCollisions(player2, portals);
            HandleFlagPoleCollisions(player1, flagPole);
            HandleFlagPoleCollisions(player2, flagPole);
            HandleMarioLuigiCollisions(player1, player2);
            HandleItemCollisions(player1, items);
            HandleItemCollisions(player2, items);
            HandleEnemyCollisions(player1, enemies);
            HandleEnemyCollisions(player2, enemies);
            HandleEnemyBlockCollisions(enemies, blocks);
            HandleEnemyEnemyCollisions(enemies);
            HandleItemBlockCollisions(items, blocks);
            HandleItemItemCollisions(items);
            HandleMarioKillZoneCollisions(player1, killZones);
            HandleMarioKillZoneCollisions(player2, killZones);
            HandleEnemiesOffScreen(camera, enemies);
            CollectAllGarbage(blocks, enemies, items);
        }

        // Take care of the collisions between Mario and blocks
        static private void HandleBlockCollisions(Player player, List<IBlock> blocks)
        {
            foreach (IBlock block in blocks)
            {
                CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(player.playerSprite.destRectangle, block.destRectangle);
                switch (collision)
                {
                    case CollisionDetection.CollisionType.NoCollision:
                        // Do nothing.
                        break;
                    case CollisionDetection.CollisionType.TopCollision: // Top is colliding with something
                        if (player.playerSprite.fallSpeed >= 0f)
                        {
                            if (player.myState == 1 && player.playerSprite.isMoving == false && player.playerSprite.isJumping)
                            {
                                player.playerState = new PlayerStateSmallIdle(player);
                                player.playerSprite = new PlayerSpriteSmallIdle(player.playerSprite);
                            }
                            else if (player.myState == 1 && player.playerSprite.isJumping)
                            {
                                player.playerState = new PlayerStateSmallRunning(player);
                                player.playerSprite = new PlayerSpriteSmallRunning(player.playerSprite);
                            }
                            if (player.myState == 2 && player.playerSprite.isMoving == false && player.playerSprite.isJumping)
                            {
                                player.playerState = new PlayerStateRidingIdle(player);
                                player.playerSprite = new PlayerSpriteRidingIdle(player.playerSprite);
                            }
                            else if (player.myState == 2 && player.playerSprite.isJumping)
                            {
                                player.playerState = new PlayerStateRidingRunning(player);
                                player.playerSprite = new PlayerSpriteRidingRunning(player.playerSprite);
                            }
                            if (player.myState == 3 && player.playerSprite.isMoving == false && player.playerSprite.isJumping)
                            {
                                player.playerState = new FireIdleMarioState(player);
                                player.playerSprite = new PlayerSpriteDemoIdle(player.playerSprite);
                            }
                            else if (player.myState == 3 && player.playerSprite.isJumping)
                            {
                                player.playerState = new PlayerStateDemoRunning(player);
                                player.playerSprite = new PlayerSpriteDemoRunning(player.playerSprite);
                            }

                            player.playerSprite.fallSpeed = 0f;
                            player.MoveTo((int)player.playerSprite.spritePosition.X, block.destRectangle.Top - player.playerSprite.destRectangle.Height + 1);
                        }
                        break;
                    case CollisionDetection.CollisionType.BottomCollision: // Bottom is colliding with something
                        if (player.playerSprite.fallSpeed < 0f && player.myState != 0)
                        {
                            player.MoveTo((int)player.playerSprite.spritePosition.X, block.destRectangle.Bottom + 1);
                            block.Bump(player);
                            player.playerSprite.fallSpeed = 0f;
                        }
                        break;
                    case CollisionDetection.CollisionType.LeftCollision: // Left is colliding with something
                        if (player.myState != 0)
                            player.MoveTo(block.destRectangle.Right + 1, (int)player.playerSprite.spritePosition.Y);
                        break;
                    case CollisionDetection.CollisionType.RightCollision: // Right is colliding with something
                        if (player.myState != 0)
                            player.MoveTo(block.destRectangle.Left - player.playerSprite.destRectangle.Width - 1, (int)player.playerSprite.spritePosition.Y);
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
                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(enemy.destRectangle, block.destRectangle);
                    switch (collision)
                    {
                        case CollisionDetection.CollisionType.NoCollision:
                            if (enemy.fallSpeed.CompareTo(0.0f) == 0)
                            {
                                enemy.fallSpeed = 1.5f;
                            }
                            break;
                        case CollisionDetection.CollisionType.TopCollision:
                            enemy.fallSpeed = 0f;
                            enemy.MoveTo(enemy.destRectangle.Left, block.destRectangle.Top - enemy.destRectangle.Height);
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
                        CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(item.destRectangle, block.destRectangle);
                        switch (collision)
                        {
                            case CollisionDetection.CollisionType.NoCollision:
                                if (item.fallSpeed.CompareTo(0.0f) == 0)
                                {
                                    item.fallSpeed = 1.5f;
                                }
                                break;
                            case CollisionDetection.CollisionType.TopCollision:
                                item.fallSpeed = 0f;
                                item.MoveTo(item.destRectangle.Left, block.destRectangle.Top - item.destRectangle.Height);
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

                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(enemy1.destRectangle, enemy2.destRectangle);
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

                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(item1.destRectangle, item2.destRectangle);
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
                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(mario.playerSprite.destRectangle, item.destRectangle);
                    switch (collision)
                    {
                        case CollisionDetection.CollisionType.NoCollision:
                            // Do nothing.
                            break;
                        default:
                            if (mario.myState != 0)
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
        static private void HandleEnemyCollisions(Player player, List<IEnemy> enemies)
        {
            if (enemies != null)
            {
                foreach (IEnemy enemy in enemies)
                {
                    CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(player.playerSprite.destRectangle, enemy.destRectangle);
                    if (collision != CollisionDetection.CollisionType.NoCollision && player.myState != 0)
                    {
                        bool hitMario = enemy.Hit(collision, true);
                        if (hitMario)
                        {
                            if (!player.isInvulnerable)
                            {
                                player.Hit();
                            }
                        }
                        else
                        {
                            player.playerSprite.fallSpeed = -7.4f;
                        }
                    }
                }
            }
        }

        // Take care of the collisions between Mario1 and Mario2
        static private void HandleMarioLuigiCollisions(Player player, Player luigi)
        {
            Player currentMario, pausedMario;
            if (player.isPaused)
            {
                currentMario = luigi;
                pausedMario = player;
            }
            else
            {
                currentMario = player;
                pausedMario = luigi;
            }
            CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(currentMario.playerSprite.destRectangle, pausedMario.playerSprite.destRectangle);
            switch (collision)
            {
                case CollisionDetection.CollisionType.NoCollision:
                    // Do nothing.
                    break;
                case CollisionDetection.CollisionType.TopCollision:
                    if (currentMario.playerSprite.fallSpeed >= 0f)
                    {
                        if (currentMario.myState == 1 && currentMario.playerSprite.isMoving == false && currentMario.playerSprite.isJumping)
                        {
                            currentMario.playerState = new PlayerStateSmallIdle(currentMario);
                            currentMario.playerSprite = new PlayerSpriteSmallIdle(currentMario.playerSprite);
                        }
                        else if (currentMario.myState == 1 && currentMario.playerSprite.isJumping)
                        {
                            currentMario.playerState = new PlayerStateSmallRunning(currentMario);
                            currentMario.playerSprite = new PlayerSpriteSmallRunning(currentMario.playerSprite);
                        }
                        if (currentMario.myState == 2 && currentMario.playerSprite.isMoving == false && currentMario.playerSprite.isJumping)
                        {
                            currentMario.playerState = new PlayerStateRidingIdle(currentMario);
                            currentMario.playerSprite = new PlayerSpriteRidingIdle(currentMario.playerSprite);
                        }
                        else if (currentMario.myState == 2 && currentMario.playerSprite.isJumping)
                        {
                            currentMario.playerState = new PlayerStateRidingRunning(currentMario);
                            currentMario.playerSprite = new PlayerSpriteRidingRunning(currentMario.playerSprite);
                        }
                        if (currentMario.myState == 3 && currentMario.playerSprite.isMoving == false && currentMario.playerSprite.isJumping)
                        {
                            currentMario.playerState = new FireIdleMarioState(currentMario);
                            currentMario.playerSprite = new PlayerSpriteDemoIdle(currentMario.playerSprite);
                        }
                        else if (currentMario.myState == 3 && currentMario.playerSprite.isJumping)
                        {
                            currentMario.playerState = new PlayerStateDemoRunning(currentMario);
                            currentMario.playerSprite = new PlayerSpriteDemoRunning(currentMario.playerSprite);
                        }
                        currentMario.playerSprite.fallSpeed = 0f;
                        currentMario.MoveTo((int)currentMario.playerSprite.spritePosition.X, pausedMario.playerSprite.destRectangle.Top - currentMario.playerSprite.destRectangle.Height + 1);
                    }
                    break;
                case CollisionDetection.CollisionType.BottomCollision:
                    if (currentMario.playerSprite.fallSpeed < 0f && currentMario.myState != 0)
                    {
                        currentMario.MoveTo((int)currentMario.playerSprite.spritePosition.X, pausedMario.playerSprite.destRectangle.Bottom + 1);
                        currentMario.playerSprite.fallSpeed = 0f;
                    }
                    break;
                case CollisionDetection.CollisionType.LeftCollision:
                    if (currentMario.myState != 0)
                        currentMario.MoveTo(pausedMario.playerSprite.destRectangle.Right + 1, (int)currentMario.playerSprite.spritePosition.Y);
                    break;
                case CollisionDetection.CollisionType.RightCollision:
                    if (currentMario.myState != 0)
                        currentMario.MoveTo(pausedMario.playerSprite.destRectangle.Left - currentMario.playerSprite.destRectangle.Width - 1, (int)currentMario.playerSprite.spritePosition.Y);
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
                if (!e.isAlive)
                {
                    if (camera.IsInView(e.destRectangle))
                    {
                        e.isAlive = true;
                    }
                }
            }
        }

        // Take care of the collisions between Mario and portals
        private static void HandlePortalCollisions(Player player, List<Portal> portals)
        {
            foreach (Portal p in portals)
            {
                CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(player.playerSprite.destRectangle, p.DestRectangle);
                switch (collision)
                {
                    case CollisionDetection.CollisionType.NoCollision:
                        break;
                    default:
                        p.Warp(player);
                        break;
                }
            }
        }

        // Take care of the collisions between Mario and flag poles
        private static void HandleFlagPoleCollisions(Player player, HangingRope flagPole)
        {
            CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(player.playerSprite.destRectangle, flagPole.CollisionRectangle);
            switch (collision)
            {
                case CollisionDetection.CollisionType.NoCollision:
                    break;
                default:
                    player.level.Win();
                    break;
            }
        }

        // Take care of the collisions between Mario and kill zones
        private static void HandleMarioKillZoneCollisions(Player player, List<KillZone> killZones)
        {
            foreach (KillZone kz in killZones)
            {
                bool isDead = false;
                CollisionDetection.CollisionType collision = CollisionDetection.DetectCollision(player.playerSprite.destRectangle, kz.DestRectangle);
                switch (collision)
                {
                    case CollisionDetection.CollisionType.NoCollision:
                        break;
                    default:
                        player.Collect(new DeathPotionItem());
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

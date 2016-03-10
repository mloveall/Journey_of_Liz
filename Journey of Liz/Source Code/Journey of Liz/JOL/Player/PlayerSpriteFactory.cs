using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.PlayerStates;
using Microsoft.Xna.Framework.Graphics;

namespace JOL
{
    /// <summary>
    /// This class manages the loading of different player sprites.
    /// </summary>

    public static class PlayerSpriteFactory
    {
        public static String producePlayerSprite(IPlayerState state)
        {
            String stateString = state.ToString();
            Console.Out.WriteLine(stateString);
            Console.Out.WriteLine("...");
            switch(stateString)
            {
                case "BigIdleMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "BigRunningMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "BigJumpingMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "BigCrouchMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "FireCrouchMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "FireJumpingMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "FireRunningMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "FireIdleMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "SmallCrouchMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "SmallJumpingMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "SmallRunningMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                case "SmallIdleMarioState":
                {
                    return "Liz/mario_moving_right";
                }
                default:
                {
                    return "Liz/mario_moving_right";
                }
            }
        }
    }
}

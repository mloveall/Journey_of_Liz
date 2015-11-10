using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.MarioStates;
using Microsoft.Xna.Framework.Graphics;

namespace JOL
{
    /// <summary>
    /// This class manages the loading of different mario sprites.
    /// </summary>

    public static class MarioSpriteFactory
    {
        public static String produceMarioSprite(IMarioState state)
        {
            String stateString = state.ToString();
            Console.Out.WriteLine(stateString);
            Console.Out.WriteLine("...");
            switch(stateString)
            {
                case "BigIdleMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "BigRunningMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "BigJumpingMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "BigCrouchMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "FireCrouchMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "FireJumpingMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "FireRunningMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "FireIdleMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "SmallCrouchMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "SmallJumpingMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "SmallRunningMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                case "SmallIdleMarioState":
                {
                    return "Marios/mario_moving_right";
                }
                default:
                {
                    return "Marios/mario_moving_right";
                }
            }
        }
    }
}

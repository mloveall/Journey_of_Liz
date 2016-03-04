using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JOL.Interfaces;
using JOL.PlayerStates;
using Microsoft.Xna.Framework;

namespace JOL
{
    public static class CollisionDetection
    {
        public enum CollisionType { NoCollision, TopCollision, BottomCollision, LeftCollision, RightCollision };

        static public CollisionType DetectCollision(Rectangle obstructeeRectangle, Rectangle obstructorRectangle)
        {
            CollisionType collision = CollisionType.NoCollision;

            if (obstructeeRectangle.Intersects(obstructorRectangle))
            {
                collision = DetectCollisionType(obstructeeRectangle, obstructorRectangle);
            }

            return collision;
        }

        static private CollisionType DetectCollisionType(Rectangle obstructeeRectangle, Rectangle obstructorRectangle)
        {
            CollisionType collisionType = CollisionType.NoCollision;

            Rectangle overlap = Rectangle.Intersect(obstructeeRectangle, obstructorRectangle);

            if (overlap.Height > overlap.Width)
            {
                if (obstructeeRectangle.X > obstructorRectangle.X)
                {
                    collisionType = CollisionType.LeftCollision;
                }
                else
                {
                    collisionType = CollisionType.RightCollision;
                }
            }
            else if (overlap.Height <= overlap.Width)
            {
                if (obstructeeRectangle.Y > obstructorRectangle.Y)
                {
                    collisionType = CollisionType.BottomCollision;
                }
                else
                {
                    collisionType = CollisionType.TopCollision;
                }
            }

            return collisionType;
        }

    }
}

   
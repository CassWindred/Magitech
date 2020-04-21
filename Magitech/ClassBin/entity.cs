using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Magitech
{
    public class Entity
    {
        //The position and the speed of the entity. null if Entity does not have a position
        public Vector2 position;
        public float speed;

        public Texture2D texture;
        public string texturename; //The name of the texture


        public Entity()
        {
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Entities;
using Magitech.Systems;
using System;

namespace Magitech.Components
{


    public class Sprite
    {
        public String textureName;
        public Texture2D texture;

        public Sprite(Texture2D texture = null, String textureName = null)
        {
            this.texture = texture;
            this.textureName = textureName;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Entities;
using Magitech.Systems;


namespace Magitech.Components
{
    class HexBase
    {
        public int q; //column
        public int r; //row
        public int s; //diagonal (https://www.redblobgames.com/grids/hexagons/#basics)
        public string id;
        public string textureName;
        public Texture2D texture;

        public HexBase(string textureName)
        {
            this.textureName = textureName;
            var textureStore = TextureManager.textureStore;
            texture = TextureManager.textureStore[textureName];
        }
    }

}

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
using Magitech.Components;

namespace Magitech
{

    //TODO: Figure out negative coordinates
    class HexMap //A single connected grid of hexes
    {
        HexBase[,] floorHexes;
        OrthographicCamera camera;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        public int hexheight = 64;
        public int hexwidth = 56;
        public int hexsize = 32;


        public HexMap(HexBase[,] floorHexes, GraphicsDevice graphicsDevice)
        {
            this.floorHexes = floorHexes;
            this.graphicsDevice = graphicsDevice;

            camera = CameraManager.mainCamera;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public Vector2 GetHexVectorFromPixelCoord(Vector2 pixelVector)
        {
            int r = (int)(pixelVector.Y / (hexheight * 3 / 4));
            int q = (int)((pixelVector.X - r * hexwidth / 2) / hexwidth);
            return new Vector2(q, r);
        }
        public Vector2 GetPixelVectorFromHex(HexBase hex)
        {
            var x = hex.q * hexwidth + hex.r * hexwidth/2;
            float y = (float)((hexheight*3/4) * hex.r);
            return new Vector2(x, y);
        }

        public void renderMap(GameTime gametime)
        {
            var transformMatrix = camera.GetViewMatrix();

            spriteBatch.Begin( transformMatrix: transformMatrix);
            foreach (HexBase floorHex in floorHexes)
            {
                spriteBatch.Draw(floorHex.texture, GetPixelVectorFromHex(floorHex), null, Color.White, 0f, new Vector2(0,0), new Vector2(1,1), SpriteEffects.None, layerDepth: 0.1f); //USERECTANGLE TO FORCE SPRITE INTO CORRECT SHAPE
            }
            spriteBatch.End();
        }





        public static HexMap GetEmptyHexMap(int width, int height, GraphicsDevice graphicsDevice) { //Returns a hex map of specified size with Grass Tiles
            HexBase[,] nFloorHexes = new HexBase[width, height];
            for (int q = 0; q < width; q++)
            {
                for (int r = 0; r < height; r++)
                {
                    nFloorHexes[q, r] = new HexBase("GreenHex");
                    nFloorHexes[q, r].q = q;
                    nFloorHexes[q, r].r = r;
                    nFloorHexes[q, r].s = -q-r;
                } 
            }
            return new HexMap(nFloorHexes, graphicsDevice);
        }


    }
}

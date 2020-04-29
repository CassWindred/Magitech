using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Magitech.Systems;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Content;
using Magitech.Components;

namespace Magitech
{
   

    class GuiManager
    {
        private UserInterface playInterface;
        private UserInterface mainMenuInterface;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static Vector2 selectedHex;

        public GuiManager(ContentManager content, GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            graphics = graphicsDeviceManager;
            UserInterface.Initialize(content, BuiltinThemes.hd);
            playInterface = new UserInterface();
            mainMenuInterface = new UserInterface();
            UserInterface.Active = playInterface;

            spriteBatch = new SpriteBatch(graphicsDevice);

            initPlayInterface();

            
        }

        public void Draw()
        {
            UserInterface.Active.Draw(spriteBatch);
        }

        public void Update(GameTime gametime)
        {
            UserInterface.Active.Update(gametime);
        }

        public void activateMainMenu()
        {
            UserInterface.Active = mainMenuInterface;
        }
        public void activatePlayMenu()
        {
            UserInterface.Active = playInterface;
        }

        public void addHexInfoWindow(Vector2 position, HexBase hexbase)
        {

        }

        private void initPlayInterface()
        {   
            UserInterface I = playInterface; ;
            Panel topbar = new Panel(size: new Vector2(MainGame.windowWidth-20, 50), anchor: Anchor.TopLeft, offset: new Vector2(10,10));
            I.AddEntity(topbar);
            Panel bottombar = new Panel(size: new Vector2(400, 50), anchor: Anchor.BottomLeft, offset: new Vector2(10,10));
            bottombar.Padding = new Vector2(18, 13);
            I.AddEntity(bottombar);
            Paragraph hexDisplay = new Paragraph("Loading...", offset: new Vector2(0,0), anchor: Anchor.AutoInline);
            hexDisplay.BeforeUpdate = (Entity e) => {
                Paragraph para = (Paragraph)(e);
                para.Text = $"Selected Hex: {MainGame.selectedHex.X}, {MainGame.selectedHex.Y}"; };
            bottombar.AddChild(hexDisplay);
        }
    }
}

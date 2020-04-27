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
using System.IO;

namespace Magitech
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        public static bool isPaused;
        public static Vector2 selectedHex;
        public static int windowHeight = 720;
        public static int windowWidth = 1024;

        bool initialisationComplete = false;

        World world;

        Entity player;
        HexMap loadedMap;

        GuiManager gui;
        Keybinds keybinds = new Keybinds();


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch guiSpriteBatch;

        OrthographicCamera camera;

        Dictionary<string, Texture2D> textureStore;

        Vector2 mouseWorldPosition;

        SpriteFont arialFont;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            

            Debug.WriteLine("Hey!!");
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

            // TODO: Add your initialization logic here
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, windowWidth, windowHeight);
            camera = new OrthographicCamera(viewportAdapter);
            CameraManager.mainCamera = camera;
            //Debug.WriteLine(camera.GetType());
            //Debug.WriteLine("Camera: " + camera.ToString());

            world = new WorldBuilder()
                .AddSystem(new DynamicRenderSystem(GraphicsDevice))
                .AddSystem(new PlayerMovementSystem(keybinds))
                .Build();

            //Initialise Player Entity
            player =  world.CreateEntity();
            FreePosition playerposition = new FreePosition();
            Velocity playerspeed = new Velocity();
            playerposition.position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            playerspeed.speed = 100f;
            player.Attach(playerposition);
            player.Attach(playerspeed);
            player.Attach(new Player());

            Content.RootDirectory = "Content";
            gui = new GuiManager(Content, graphics, GraphicsDevice);
            gui.activatePlayMenu();






            //this.IsMouseVisible = true;

            base.Initialize();
            initialisationComplete = true;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            guiSpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            String playerTextureName = "playerHat";
            Texture2D playerTexture = Content.Load<Texture2D>(playerTextureName);
            player.Attach(new Sprite(textureName: playerTextureName, texture: playerTexture));

            //string[] hexfloors = Directory.GetFiles("Content\\HexFloors");
            textureStore = new Dictionary<string, Texture2D>();
            TextureManager.textureStore = textureStore;
            textureStore.Add("GreenHex", Content.Load<Texture2D>("HexFloors/GreenHex"));

            arialFont = Content.Load<SpriteFont>("Fonts/Arial");


            //Load Initial empty map
            loadedMap = HexMap.GetEmptyHexMap(100, 100, GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {   
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(keybinds.ExitGame))
                Exit();

            // TODO: Add your update logic here
            var mstate = Mouse.GetState();
            var kstate = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;




            //Camera Movement Logic
            const float movementSpeed = 200;
            const float rotationSpeed = 0.5f;
            const float zoomSpeed = 0.5f;
            if (kstate.IsKeyDown(keybinds.CameraMoveUp))
            {
                camera.Move(new Vector2(0, -movementSpeed) * deltaTime);
            }
            if (kstate.IsKeyDown(keybinds.CameraMoveDown))
            {
                camera.Move(new Vector2(0, movementSpeed) * deltaTime);
            }
            if (kstate.IsKeyDown(keybinds.CameraMoveLeft))
            {
                camera.Move(new Vector2(-movementSpeed, 0) * deltaTime);
            }
            if (kstate.IsKeyDown(keybinds.CameraMoveRight))
            {
                camera.Move(new Vector2(movementSpeed, 0) * deltaTime);
            }

            Vector2 mouseScreenPosition = new Vector2(mstate.X, mstate.Y);
            //Debug.WriteLine(initialisationComplete.ToString());
            
            mouseWorldPosition = camera.ScreenToWorld(mouseScreenPosition);
            // TODO: Add your update logic here

            world.Update(gameTime);
            gui.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            var transformMatrix = camera.GetViewMatrix();



            loadedMap.renderMap(gameTime); //Renders the currently loaded hexmap


            //+ mouseWorldPosition.ToString()
            world.Draw(gameTime); //Runs all the draw systems attached to the World entity manager
            

            guiSpriteBatch.Begin();
            selectedHex = loadedMap.GetHexVectorFromPixelCoord(mouseWorldPosition);
            
            string mousePositionText = $"Mouse Cursor Position: {mouseWorldPosition.ToString()}";
            //guiSpriteBatch.DrawString(arialFont, "Test", new Vector2(100, 100), Color.Black);
            Vector2 textMiddlePoint = arialFont.MeasureString(mousePositionText) / 2;
            guiSpriteBatch.DrawString(arialFont, mousePositionText, new Vector2(200, 200), Color.Black, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 1.0f);
            guiSpriteBatch.End();


            gui.Draw();
            base.Draw(gameTime);
        }
    }
}

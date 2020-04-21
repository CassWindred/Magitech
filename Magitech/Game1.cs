using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Magitech
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {


        List<Entity> loadedEntities = new List<Entity>();
        Player player;

        Keybinds keybinds = new Keybinds();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            this.player = new Player();
            player.position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            player.speed = 100f;
            player.texturename = "hat";
            loadedEntities.Add(player);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.texture = Content.Load<Texture2D>(player.texturename);
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
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(keybinds.PlayerMoveUp))
                player.position.Y -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(keybinds.PlayerMoveDown))
                player.position.Y += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(keybinds.PlayerMoveLeft))
                player.position.X -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(keybinds.PlayerMoveRight))
                player.position.X += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (player.position.X > graphics.PreferredBackBufferWidth - player.texture.Width / 2)
                player.position.X = graphics.PreferredBackBufferWidth - player.texture.Width / 2;
            else if (player.position.X < player.texture.Width / 2)
                player.position.X = player.texture.Width / 2;

            if (player.position.Y > graphics.PreferredBackBufferHeight - player.texture.Height / 2)
                player.position.Y = graphics.PreferredBackBufferHeight - player.texture.Height / 2;
            else if (player.position.Y < player.texture.Height / 2)
                player.position.Y = player.texture.Height / 2;

            // TODO: Add your update logic here

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
            spriteBatch.Begin();
            foreach (Entity entity in loadedEntities)
            {
                spriteBatch.Draw(
                    entity.texture,
                    entity.position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(entity.texture.Width / 2, entity.texture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

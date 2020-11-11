using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FireworkBase
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int howManyPixels;
        bool launchedShell = false;
        Texture2D greenTexture;
        Texture2D redTexture;
        Texture2D shellTexture;
        Texture2D fireworkTexture;

        Rectangle shooter1;
        Rectangle shooter2;
        Rectangle shooter3;
        Rectangle selectedShooterRectangle;
        Rectangle shellRectangle;
        int shellX =100;
        int shellY = 410;
        int fireworkCounter = 0;
        bool fireworkGoing = false;
        int selectedShooter = 1;

        KeyboardState oldKb = Keyboard.GetState();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            shooter1 = new Rectangle(100, 410, 40, 70);
            shooter2 = new Rectangle(300, 410, 40, 70);
            shooter3 = new Rectangle(500, 410, 40, 70);
            selectedShooterRectangle = shooter1;
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
            redTexture = Content.Load<Texture2D>("red");
            greenTexture = Content.Load<Texture2D>("green");
            shellTexture = Content.Load<Texture2D>("shell");
            fireworkTexture = Content.Load<Texture2D>("firework");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Left) && oldKb.IsKeyUp(Keys.Left) && selectedShooter>1) {
                selectedShooter--;
            }
            if (kb.IsKeyDown(Keys.Right) && oldKb.IsKeyUp(Keys.Right) && selectedShooter <3) {
                selectedShooter++;
            }
            if (kb.IsKeyDown(Keys.Space) && oldKb.IsKeyUp(Keys.Space)) {
                launchedShell = true;
                Random random = new Random();
                howManyPixels = random.Next(0,310);
            }
            if (selectedShooter == 1 && !launchedShell)
            {
                shellX = 100;
                shellY = 410;
                selectedShooterRectangle = shooter1;
            }
            else if (selectedShooter == 2 && !launchedShell)
            {
                shellX = 300;
                shellY = 410;
                selectedShooterRectangle = shooter2;
            }
            else if (selectedShooter == 3 && !launchedShell)
            {
                shellX = 500;
                shellY = 410;
                selectedShooterRectangle = shooter3;
            }

            if (fireworkGoing) { fireworkCounter++;
                if (fireworkCounter==180) {
                    fireworkCounter = 0;
                    fireworkGoing = false;
                    launchedShell = false;
                }
            }
            if (launchedShell) {
                if (shellY <= howManyPixels) {
                    fireworkGoing = true;
                }
                if (shellY > howManyPixels) { shellY--; }
            }
            shellRectangle = new Rectangle(shellX,shellY,40,70);
            // TODO: Add your update logic here
            oldKb = kb;
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
            spriteBatch.Draw(redTexture,shooter1,Color.White);
            spriteBatch.Draw(redTexture,shooter2,Color.White);
            spriteBatch.Draw(redTexture, shooter3, Color.White);
            spriteBatch.Draw(greenTexture, selectedShooterRectangle, Color.White);
            if (fireworkGoing)
            {
                spriteBatch.Draw(fireworkTexture, shellRectangle, Color.White);
            }
            else if (launchedShell) {
                spriteBatch.Draw(shellTexture, shellRectangle, Color.White);
            }
           
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

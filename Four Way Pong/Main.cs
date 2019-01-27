using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Four_Way_Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Paddle> paddles;

        Ball ball;

        public Main()
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

            //Initialize the ball
            ball = new Ball(Content);

            paddles = new List<Paddle>();

            //Create all the movable and non-movable paddles

            paddles.Add(new Paddle(new Vector2(0, 200), '0', Content, 20, 80, paddles));
            paddles.Add(new Paddle(new Vector2(780, 200), '1', Content, 20, 80, paddles));

            paddles.Add(new Paddle(new Vector2(360, 0), '2', Content, 80, 20, paddles));
            paddles.Add(new Paddle(new Vector2(360, 460), '3', Content, 80, 20, paddles));

            paddles.Add(new Paddle(new Vector2(0, 0), '4', Content, 100, 100, paddles));
            paddles.Add(new Paddle(new Vector2(700, 380), '4', Content, 100, 100, paddles));
            paddles.Add(new Paddle(new Vector2(0, 380), '4', Content, 100, 100, paddles));
            paddles.Add(new Paddle(new Vector2(700, 0), '4', Content, 100, 100, paddles));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //Update every paddle
            foreach (Paddle paddle in paddles)
            {
                paddle.Update(ball);
            }

            ball.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Paddle paddle in paddles)
            {
                paddle.Draw(spriteBatch);
            }

            ball.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

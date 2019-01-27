using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Four_Way_Pong
{
    class Paddle
    {
        Rectangle rectangle;
        Vector2 position;
        char inputType;

        SpriteFont font;

        public Texture2D texture;

        int width;
        int height;

        int score;

        List<Paddle> paddles;

        public Paddle(Vector2 position, char inputType, ContentManager content, int width, int height, List<Paddle> paddles)
        {
            //Getting parameters to be class-level variables
            this.position = position;
            this.inputType = inputType;

            this.width = width;
            this.height = height;

            this.paddles = paddles;

            //Initializing the rectangle
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            //Loading content
            LoadContent(content);
        }

        public void Update(Ball ball)
        {
            KeyboardState state = Keyboard.GetState();
            switch (inputType)
            {
                //Different inputs and checking if the ball is out of bounds

                case '0':
                    if (state.IsKeyDown(Keys.R))
                    {
                        position.Y -= 10;
                    }
                    if (state.IsKeyDown(Keys.F))
                    {
                        position.Y += 10;
                    }

                    if (ball.IsOutLeft)
                    {
                        score++;
                        ball.Start();
                    }
                    break;
                case '1':
                    if (state.IsKeyDown(Keys.I))
                    {
                        position.Y -= 10;
                    }
                    if (state.IsKeyDown(Keys.K))
                    {
                        position.Y += 10;
                    }

                    if (ball.IsOutRight)
                    {
                        score++;
                        ball.Start();
                    }
                    break;
                case '2':
                    if (state.IsKeyDown(Keys.Right))
                    {
                        position.X += 10;
                    }
                    if (state.IsKeyDown(Keys.Left))
                    {
                        position.X -= 10;
                    }

                    if (ball.IsOutTop)
                    {
                        score++;
                        ball.Start();
                    }
                    break;
                case '3':
                    if (state.IsKeyDown(Keys.Z))
                    {
                        position.X -= 10;
                    }
                    if (state.IsKeyDown(Keys.X))
                    {
                        position.X += 10;
                    }

                    if (ball.IsOutBottom)
                    {
                        score++;
                        ball.Start();
                    }
                    break;
            }

            if (rectangle.Intersects(ball.rectangle))       //Check if the rectangles are even intersecting
            {
                //Check for specific collisions and then send the ball at a random angle

                if (position.X > ball.position.X && position.Y > ball.position.Y)
                {
                    ball.position.X -= 5;
                    ball.position.Y -= 5;

                    Random randomGen = new Random();
                    int random = randomGen.Next(291, 340);
                    ball.velocity = AngleToVector(random);
                }

                if (position.X > ball.position.X && position.Y < ball.position.Y)
                {
                    ball.position.X -= 5;
                    ball.position.Y += 5;

                    Random randomGen = new Random();
                    int random = randomGen.Next(21, 70);
                    ball.velocity = AngleToVector(random);
                }

                if (position.X < ball.position.X && position.Y > ball.position.Y)
                {
                    ball.position.X += 5;
                    ball.position.Y -= 5;

                    Random randomGen = new Random();
                    int random = randomGen.Next(201, 250);
                    ball.velocity = AngleToVector(random);
                }

                if (position.X < ball.position.X && position.Y < ball.position.Y)
                {
                    ball.position.X += 5;
                    ball.position.Y += 5;

                    Random randomGen = new Random();
                    int random = randomGen.Next(111, 160);
                    ball.velocity = AngleToVector(random);
                }

                else if (position.X > ball.position.X && position.Y == ball.position.Y)
                {
                    ball.velocity = AngleToVector(180);
                }

                else if (position.X < ball.position.X && position.Y == ball.position.Y)
                {
                    ball.velocity = AngleToVector(90);
                }

                else if (position.X == ball.position.X && position.Y < ball.position.Y)
                {
                    ball.velocity = AngleToVector(270);
                }

                else if (position.X == ball.position.X && position.Y > ball.position.Y)
                {
                    ball.velocity = AngleToVector(0);
                }

            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);

            //Drawing the 4 scores
            switch (inputType)
            {
                case '0':
                    spriteBatch.DrawString(font, score.ToString(), new Vector2(50, 200), Color.White);
                    break;
                case '1':
                    spriteBatch.DrawString(font, score.ToString(), new Vector2(750, 200), Color.White);
                    break;
                case '2':
                    spriteBatch.DrawString(font, score.ToString(), new Vector2(400, 50), Color.White);
                    break;
                case '3':
                    spriteBatch.DrawString(font, score.ToString(), new Vector2(400, 400), Color.White);
                    break;

            }
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Paddle");
            font = content.Load<SpriteFont>("font");
        }

        public Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle) * 3, -(float)Math.Cos(angle) * 3);
        }
    }
}

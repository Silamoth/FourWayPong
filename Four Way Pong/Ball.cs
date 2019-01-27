using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Four_Way_Pong
{
    class Ball
    {
        public Vector2 position;
        public Rectangle rectangle;
        public Vector2 velocity;

        Texture2D texture;

        private bool isOutLeft;
        private bool isOutRight;
        private bool isOutTop;
        private bool isOutBottom;

        public Ball(ContentManager content)
        {
            LoadContent(content);
            Start();
        }

        public void Update()
        {
            //Update position coordinates and rectangle coordinates

            position += velocity;

            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width / 4, texture.Height / 4);

            //Logic for out-of-bounds checking

            if (position.X < 0)
                isOutLeft = true;
            else if (position.X > 800)
                isOutRight = true;
            else if (position.Y > 600)
                isOutBottom = true;
            else if (position.Y < 0)
                isOutTop = true;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Paddle");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.Black);
        }

        public Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle) * 3, -(float)Math.Cos(angle) * 3);
        }

        public void Start()
        {
            //Reset everything

            position = new Vector2(400, 300);

            isOutLeft = false;
            isOutRight = false;
            isOutTop = false;
            isOutBottom = false;

            Random randGen = new Random();
            int random = randGen.Next(0, 361);

            velocity = AngleToVector(random);
        }

        //Properties

        public bool IsOutRight
        {
            get { return isOutRight; }
        }

        public bool IsOutLeft
        {
            get { return isOutLeft; }
        }

        public bool IsOutTop
        {
            get { return isOutTop; }
        }

        public bool IsOutBottom
        {
            get { return isOutBottom; }
        }
    }
}

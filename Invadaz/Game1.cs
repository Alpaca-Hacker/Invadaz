using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Invadaz
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle gameBounds;
        Player player;
        EnemyController enemyController;
        private int lastFiredTicks = 0;
        private Texture2D bulletTexture, ufoTexture;
        public List<Sprite> gameObjects = new List<Sprite>();
        Vector2 bulletOffset;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameBounds = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            var playerTexture = Content.Load<Texture2D>("Player");
            player = new Player(playerTexture, 1, 4, gameBounds, 3);
            player.Location = new Vector2(0, gameBounds.Height - 100);
            Texture2D[] enemyTextures = {Content.Load<Texture2D>("Enemy1"),
                                            Content.Load<Texture2D>("Enemy2"),
                                            Content.Load<Texture2D>("Enemy3"),
                                            Content.Load<Texture2D>("Enemy4"),
                                            Content.Load<Texture2D>("Enemy5")
            };
            ufoTexture = Content.Load<Texture2D>("UFO");
            enemyController = new EnemyController();
            enemyController.Startup(gameBounds, enemyTextures, gameObjects);
            bulletTexture = Content.Load<Texture2D>("bullet");
            bulletOffset = new Vector2((float)((playerTexture.Width / 8) * .75 - bulletTexture.Width / 2), (float)-bulletTexture.Height);
            

        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {

            if (lastFiredTicks>0)
            {
                lastFiredTicks--;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastFiredTicks == 0 && (gameObjects.FindAll(x => x.GetType().Name == "Bullet").Count <5))
            {
                gameObjects.Add(new Bullet(bulletTexture, (player.Location + bulletOffset), enemyController, gameBounds));
                lastFiredTicks = 30;
            }

            player.Update(gameTime);
            enemyController.Update(gameTime);
            if (gameObjects != null)
            {
                var removeMe = new List<Sprite>();
                foreach (var gameObject in gameObjects)
                {
                    int check = gameObject.Update(gameTime);
                    if (check == 1)
                    {
                        removeMe.Add(gameObject);
                    }
                }
                if (removeMe != null)
                {
                    foreach (var del in removeMe)
                    {
                        gameObjects.Remove(del);
                    }
                }
            }
            if (gameObjects.Find(x => x.GetType().Name == "Ufo") == null)
            {
                var rnd = new Random();
                if (rnd.Next(10000) < 25)
                {
                    gameObjects.Add(new Ufo(ufoTexture, 4, 1, 2, gameBounds));
                }
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player.Draw(spriteBatch, 0.75f);
            if (gameObjects != null)
            {
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Draw(spriteBatch);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}

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
        private Texture2D bulletTexture, ufoTexture, explosionTexture;
        public List<Sprite> gameObjects = new List<Sprite>();
        Vector2 bulletOffset;
        SpriteFont gameFont;

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
            explosionTexture = Content.Load<Texture2D>("Explosion");
            enemyController = new EnemyController(gameBounds, enemyTextures, gameObjects);
            enemyController.Startup();
            gameFont = Content.Load<SpriteFont>("GameFont20");
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
                gameObjects.Add(new Bullet(bulletTexture, (player.Location + bulletOffset), gameObjects, gameBounds, explosionTexture));
                lastFiredTicks = 30;
            }

            player.Update(gameTime);
            enemyController.Update(gameTime);
            if (gameObjects != null)
            {
                int length = gameObjects.Count;
                Sprite[] currentObjects = new Sprite[length];
                gameObjects.CopyTo(currentObjects);
                foreach (var gameObject in currentObjects)
                {
                    int check = gameObject.Update(gameTime);
                    if (check == 1)
                    {
                        gameObjects.Remove(gameObject);
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
            if (gameObjects.FindAll(x => x.GetType().Name == "Enemy").Count <= 0)
            {
                enemyController.Startup();
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
            
            spriteBatch.DrawString(gameFont, "Score : 999999", new Vector2((gameBounds.Width / 2) - 50, gameBounds.Height - 25), Color.Wheat);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}

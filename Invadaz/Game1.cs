using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        Bullet bullet;
        private Texture2D bulletTexture;
        public object[] gameObjects;
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
            player = new Player(playerTexture, 1,4,gameBounds, 3);
            player.Location = new Vector2(0, gameBounds.Height - 100);
            Texture2D[] enemyTextures = {Content.Load<Texture2D>("Enemy1"),
                                            Content.Load<Texture2D>("Enemy2"),
                                            Content.Load<Texture2D>("Enemy3"),
                                            Content.Load<Texture2D>("Enemy4"),
                                            Content.Load<Texture2D>("Enemy5")
            };
            enemyController = new EnemyController();
            enemyController.Startup(gameBounds, enemyTextures);
            bulletTexture = Content.Load<Texture2D>("single");
            bulletOffset = new Vector2( (float)((playerTexture.Width / 8)*.75 - bulletTexture.Width / 2),(float)-bulletTexture.Height);

        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space)&& bullet==null)
            {
               bullet = new Bullet(bulletTexture, (player.Location + bulletOffset), enemyController, gameBounds);
            }

            player.Update(gameTime);
            enemyController.Update(gameTime);
            if (bullet != null)
            {
                int check = bullet.Update(gameTime);
                if (check == 1)
                {
                    bullet = null;
                }
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
          
            spriteBatch.Begin();
            player.Draw(spriteBatch,0.75f);
            enemyController.Draw(spriteBatch);
            if (bullet != null)
            {
                bullet.Draw(spriteBatch,5);
            }
            spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}

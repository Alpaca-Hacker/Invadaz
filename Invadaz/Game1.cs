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
        GameContent content;
        GameObjects gameObjects;
        Rectangle gameBounds;
        Player player;
        EnemyController enemyController;      
        List<Sprite> entities;
        Vector2 bulletOffset;
        ScoreController score;

 private int lastFiredTicks = 0;

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

            gameObjects = new GameObjects();
            entities = gameObjects.Entities = new List<Sprite>();
            gameBounds = gameObjects.GameBounds = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            content = gameObjects.Content = new GameContent();
            content.PlayerTexture = Content.Load<Texture2D>("Player");
            content.EnemyTextures = new Texture2D[]{
                                             Content.Load<Texture2D>("Enemy1"),
                                             Content.Load<Texture2D>("Enemy2"),
                                             Content.Load<Texture2D>("Enemy3"),
                                             Content.Load<Texture2D>("Enemy4"),
                                             Content.Load<Texture2D>("Enemy5")
                                            };

            content.UfoTexture = Content.Load<Texture2D>("UFO");
            content.ExplosionTexture = Content.Load<Texture2D>("Explosion");
            content.GameFont = Content.Load<SpriteFont>("GameFont20");
            content.BulletTexture = Content.Load<Texture2D>("bullet");

            player = gameObjects.Player = new Player(content.PlayerTexture, 1, 4, gameBounds, 3);
            player.Location = new Vector2(0, gameBounds.Height - 100);
            enemyController = gameObjects.EnemyController = new EnemyController(gameBounds, content.EnemyTextures, entities);
            enemyController.Startup();
            bulletOffset = new Vector2((float)((content.PlayerTexture.Width / 8) * .75 
                - content.BulletTexture.Width / 2), (float)-content.BulletTexture.Height);

            score = gameObjects.Score = new ScoreController(content.GameFont, gameBounds);
            score.Score = 0;

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
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastFiredTicks == 0 && (entities.FindAll(x => x.GetType().Name == "Bullet").Count <5))
            {
                var bullet = new Bullet(content.BulletTexture, score, entities, gameBounds, content.ExplosionTexture);
                entities.Add(bullet);
                bullet.Location = player.Location + bulletOffset;
                lastFiredTicks = 30;
            }

            player.Update(gameTime);
            enemyController.Update(gameTime);
            if (entities != null)
            {
                int length = entities.Count;
                Sprite[] currentObjects = new Sprite[length];
                entities.CopyTo(currentObjects);
                foreach (var gameObject in currentObjects)
                {
                    int check = gameObject.Update(gameTime);
                    if (check == 1)
                    {
                        entities.Remove(gameObject);
                    }
                }
               
            }
            if (entities.Find(x => x.GetType().Name == "Ufo") == null)
            {
                var rnd = new Random();
                if (rnd.Next(10000) < 25)
                {
                    entities.Add(new Ufo(content.UfoTexture, 4, 1, 2, gameBounds));
                }
            }
            if (entities.FindAll(x => x.GetType().Name == "Enemy").Count <= 0)
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
            if (entities != null)
            {
                foreach (var gameObject in entities)
                {
                    gameObject.Draw(spriteBatch);
                }
            }
            score.Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}

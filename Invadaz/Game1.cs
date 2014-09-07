using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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
        ScoreController score;
        GameController gameController;
        public bool IsRunning;


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
            gameObjects = new GameObjects();
            gameObjects.Game = this;
            entities = gameObjects.Entities = new List<Sprite>();
            gameBounds = gameObjects.GameBounds = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);           
            enemyController = gameObjects.EnemyController = new EnemyController(gameObjects);
            gameController = gameObjects.GameController = new GameController(gameObjects);
            score = gameObjects.Score = new ScoreController(gameObjects);
            IsRunning = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            content = gameObjects.Content = new GameContent();
            content.PlayerTexture = new SpriteTexture(Content.Load<Texture2D>("Player"),1,4,3);
            content.EnemyTextures = new SpriteTexture[]{
                new SpriteTexture(Content.Load<Texture2D>("Enemy1"),6,1,3),
                new SpriteTexture(Content.Load<Texture2D>("Enemy2"),6,1,5),
                new SpriteTexture(Content.Load<Texture2D>("Enemy3"),6,1,2),
                new SpriteTexture(Content.Load<Texture2D>("Enemy4"),6,1,6),
                new SpriteTexture(Content.Load<Texture2D>("Enemy5"),6,1,5),                                          
                                            };
            content.UfoTexture = new SpriteTexture(Content.Load<Texture2D>("UFO"),4,1,2);
            content.ExplosionTexture = new SpriteTexture (Content.Load<Texture2D>("Explosion"),1,7,4);
            content.BigExplosionTexure = new SpriteTexture(Content.Load<Texture2D>("BigExplosion"), 1, 7, 5);
            content.GameFont = Content.Load<SpriteFont>("GameFont20");
            content.BulletTexture = new SpriteTexture(Content.Load<Texture2D>("bullet"),1,1,1);
            content.BombTexture = new SpriteTexture(Content.Load<Texture2D>("Bomb"), 1, 2, 5);
            content.TitleFont = Content.Load<SpriteFont>("LargeFont96");
            content.ExplosionBigSound = Content.Load<SoundEffect>("ExplosionBig");
            content.ExplosionSmallSound = Content.Load<SoundEffect>("ExplosionSmall");
            content.ShootSound = Content.Load<SoundEffect>("Shoot");


        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!IsRunning)
            {
                gameController.Update(gameTime);
                base.Update(gameTime);
                return;
            }
            

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
                    entities.Add(new Ufo(gameObjects));
                }
            }
            if (entities.FindAll(x => x.GetType().Name == "Enemy").Count <= 0)
            {
                score.Level++;
                enemyController.Startup();
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            if (IsRunning)
            {
                if (entities != null)
                {
                    foreach (var gameObject in entities)
                    {
                        gameObject.Draw(spriteBatch);
                    }
                }
            }
            else
            {
                gameController.Draw(spriteBatch);
            }
            score.Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}

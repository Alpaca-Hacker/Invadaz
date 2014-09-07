﻿using Microsoft.Xna.Framework;
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
        ScoreController score;


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
            entities = gameObjects.Entities = new List<Sprite>();
            gameBounds = gameObjects.GameBounds = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
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

            player = gameObjects.Player = new Player(gameObjects);
            entities.Add(player);
            player.Location = new Vector2(0, gameBounds.Height - 100);
            enemyController = gameObjects.EnemyController = new EnemyController(gameObjects);
            enemyController.Startup();

            score = gameObjects.Score = new ScoreController(gameObjects);
            score.Score = 0;
            score.Lives = 3;

        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
                enemyController.Startup();
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

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

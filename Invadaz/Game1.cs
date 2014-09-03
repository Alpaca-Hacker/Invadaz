using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invadaz
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D invaderTexture1, invaderTexture2;
        Vector2 location;
        int direction;
        bool anim = true;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

    
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            invaderTexture1 = Content.Load<Texture2D>("Ufo1");
            invaderTexture2 = Content.Load<Texture2D>("Ufo2");
            location = Vector2.Zero;
            direction = 5;
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if ((gameTime.TotalGameTime.Ticks % 5) == 0)
            {

                location.X += direction;

                if (location.X > (Window.ClientBounds.Width - 350) || location.X < 0)
                {
                    direction = -direction;
                }
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
          
            spriteBatch.Begin();
            if (location.X%2 == 0)
            {
                anim = !anim;
            }
            for (int i = 100; i < 250; i+=35)
            {
                for (int j = 0; j < 350; j+=35)
                {
                    var where = new Vector2(j, i);
                    spriteBatch.Draw((anim? invaderTexture1:invaderTexture2), location+where, Color.White);
                }
            }
            spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}

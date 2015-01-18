using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNATest1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ConsoleWindow consoleWindow;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1000;

            //Components.Add(rectangleOverlay = new RectangleOverlay(new Rectangle(0, 0, 300, 50), Color.LightGray, this));

            (consoleWindow = new ConsoleWindow()).Show();
            consoleWindow.OnInput += new ConsoleWindow.ConsoleInputEventHandler(OnConsoleInput);

            Content.RootDirectory = "Content";
        }

        private void OnConsoleInput(string str)
        {
            if (str == "exit" || str == "quit") Exit();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            consoleWindow.Log("Initialized");

            base.Initialize();
        }

        // This is a texture we can render.
        Texture2D myTexture;

        Texture2D dummyTexture;

        // Set the coordinates to draw the sprite at.
        Vector2 spritePosition = Vector2.Zero;

        // Store some information about the sprite's motion.
        Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);

        SpriteFont font;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            consoleWindow.TimerStart();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myTexture = Content.Load<Texture2D>("Pic01");

            font = Content.Load<SpriteFont>("SpriteFont1");

            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });

            consoleWindow.Log("LoadContent (" + consoleWindow.TimerStop().TotalSeconds + "s)");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {/*
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition +=
                spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - myTexture.Height;
            int MinY = 0;

            // Check for bounce.
            if (spritePosition.X > MaxX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MaxX;
            }

            else if (spritePosition.X < MinX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }

            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MaxY;
            }

            else if (spritePosition.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MinY;
            }
            */
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the sprite.
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(myTexture, spritePosition, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.Draw(dummyTexture, new Rectangle(0, 0, 300, 30), Color.LightCoral);
            spriteBatch.DrawString(font, "TEST STRING", new Vector2(10, 10), Color.White);
            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }

    public class ConsoleWindow
    {
        private System.Windows.Forms.Form debugForm;
        private System.Windows.Forms.TextBox outputTextBox, inputTextBox;

        public delegate void ConsoleInputEventHandler(string str);

        public event ConsoleInputEventHandler OnInput;

        public ConsoleWindow()
        {
            debugForm = new System.Windows.Forms.Form();
            debugForm.Text = "Console";
            debugForm.Width = 500;

            inputTextBox = new System.Windows.Forms.TextBox();
            inputTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            inputTextBox.Location = new System.Drawing.Point(0, debugForm.ClientSize.Height - inputTextBox.Height);
            inputTextBox.Width = debugForm.ClientSize.Width;
            inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyEventHandler);
            
            outputTextBox = new System.Windows.Forms.TextBox();
            outputTextBox.Multiline = true;
            outputTextBox.Width = debugForm.ClientSize.Width;
            outputTextBox.Height = debugForm.ClientSize.Height - inputTextBox.Height;
            outputTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
            outputTextBox.ReadOnly = true;
            outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            debugForm.Controls.Add(outputTextBox);
            debugForm.Controls.Add(inputTextBox);
        }

        private void KeyEventHandler(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (OnInput != null) OnInput(inputTextBox.Text);
                inputTextBox.Text = "";
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private DateTime Timer_Start, Timer_Stop;

        public TimeSpan Timer_Elapsed { get { return Timer_Stop - Timer_Start; } }

        public void TimerStart()
        {
            Timer_Start = DateTime.Now;
        }

        public TimeSpan TimerStop()
        {
            Timer_Stop = DateTime.Now;
            return Timer_Elapsed;
        }

        public void Log(string str)
        {
            outputTextBox.AppendText(DateTime.Now.ToLongTimeString() + " - " + str + "\r\n");
        }

        public void Show()
        {
            debugForm.Show();
        }

        public void Hide()
        {
            debugForm.Hide();
        }
    }

    public class RectangleOverlay : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D dummyTexture;
        Rectangle dummyRectangle;
        Color Colori;

        public RectangleOverlay(Rectangle rect, Color colori, Game game)
            : base(game)
        {
            // Choose a high number, so we will draw on top of other components.
            DrawOrder = 1000;
            dummyRectangle = rect;
            Colori = colori;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(dummyTexture, dummyRectangle, Colori);
            spriteBatch.End();
        }
    }
}

using GhostEngine.DesktopGL.Editor.Canvas;
using GhostEngine.DesktopGL.Editor.Config;
using GhostEngine.DesktopGL.Editor.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using System;
using System.Diagnostics;
using System.Xml.Linq;


namespace GhostEngine.DesktopGL.Editor
{
    internal class LevelEditor : Game
    {
       
        private GraphicsDeviceManager _graphics;
        private Desktop _desktop;
        private Button _button;
        private Panel _mainPanel;

        private SpriteBatch _spriteBatch;
        private Texture2D _pixel; // 1x1 white texture for lines and filled rectangles
        

        private int windowW, windowH, monitorH, monitorW;
        private bool isFullscreen = false;

        private const int MinWindowWidth = 800;
        private const int MinWindowHeight = 500;


        public LevelEditor()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

         
            monitorW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            monitorH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            windowW = monitorW;
            windowH = monitorH;

            _graphics.PreferredBackBufferWidth = windowW;
            _graphics.PreferredBackBufferHeight = windowH;
            _graphics.ApplyChanges();

         
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            Window.AllowAltF4 = true;
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            if (!isFullscreen)
            {
                if (Window.ClientBounds.Width == 0 || Window.ClientBounds.Height == 0)
                {
                    return;
                }

                windowW = Math.Max(Window.ClientBounds.Width, MinWindowWidth);
                windowH = Math.Max(Window.ClientBounds.Height, MinWindowHeight);

                _graphics.PreferredBackBufferWidth = windowW;
                _graphics.PreferredBackBufferHeight = windowH;
                _graphics.ApplyChanges();

            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            MyraEnvironment.Game = this;
            _desktop = new Desktop();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            
            SaveLoadConfig.Check();

            //LoadData
            var currentSettings = SaveLoadConfig.LoadData();
            ButtonsEventsSettings.DarkMode = currentSettings.IsDarkMode;
            ButtonsEventsSettings.UpdateGlobalStylesheet(currentSettings.IsDarkMode);

           //CreateGUI
            _mainPanel = UIManager.CreateMainUI() as Panel;
            ButtonsEvents.DesktopInstance = _desktop;
            ButtonsEventsSettings.DesktopInstance = _desktop;

            _desktop.Root = _mainPanel;
        }
        protected override void Draw(GameTime gameTime)
        {
           
            GraphicsDevice.Clear(ButtonsEventsSettings.BackgroundColor);

            // The level is drawn first, the UI goes on top
            _spriteBatch.Begin();
            GridRenderer.Draw(_spriteBatch, _pixel);
            _spriteBatch.End();

            _desktop?.Render();

            base.Draw(gameTime);
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
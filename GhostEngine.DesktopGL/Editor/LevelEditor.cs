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
        private bool isDark = false;

        private int windowW, windowH, monitorH, monitorW;
        private bool isFullscreen = false;


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
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            if (!isFullscreen)
            {
                windowW = Window.ClientBounds.Width;
                windowH = Window.ClientBounds.Height;

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

            _mainPanel = UIManager.CreateMainUI() as Panel;
            ButtonsEvents.DesktopInstance = _desktop;
            ButtonsEventsSettings.DesktopInstance = _desktop;
            ButtonsEventsSettings.UpdateGlobalStylesheet(isDark);
            _desktop.Root = _mainPanel;

            
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            _desktop?.Render();
            
            base.Draw(gameTime);
        }

    }
}
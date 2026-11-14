using GhostEngine.DesktopGL.Editor.Canvas;
using GhostEngine.DesktopGL.Editor.Config;
using GhostEngine.DesktopGL.Editor.Model;
using GhostEngine.DesktopGL.Editor.SaveLoadData;
using GhostEngine.DesktopGL.Editor.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static GhostEngine.DesktopGL.Editor.Config.SaveLoadConfig;


namespace GhostEngine.DesktopGL.Editor
{
    internal class LevelEditor : Game
    {
       
        private GraphicsDeviceManager _graphics;
        private Desktop _desktop;
        private Button _button;
        private Panel _mainPanel;
        private MouseState _previousMouse;
        private KeyboardState _previousKeyboard;

        private SpriteBatch _spriteBatch;
        private Texture2D _pixel;

        private double _autoSaveTimer;

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

            // Start smaller than the screen (title bar and taskbar take space); Initialize maximizes the window
            windowW = Math.Max((int)(monitorW * 0.8), MinWindowWidth);
            windowH = Math.Max((int)(monitorH * 0.8), MinWindowHeight);

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

        // MonoGame has no Maximize API; DesktopGL runs on SDL2, so call it directly
        [DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_MaximizeWindow(IntPtr window);

        protected override void Initialize()
        {
            base.Initialize();

            // Fits the work area (no taskbar overlap); ClientSizeChanged then resizes the back buffer
            Window.Position = new Point((monitorW - windowW) / 2, (monitorH - windowH) / 2);
            try { SDL_MaximizeWindow(Window.Handle); }
            catch (Exception ex) { Debug.WriteLine($"Maximize failed: {ex.Message}"); }

            MyraEnvironment.Game = this;
            _desktop = new Desktop();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            EditorState.CurrentLevel = new Level(EditorState.DefaultLevelWidth, EditorState.DefaultLevelHeight);

            
            SaveLoadConfig.Check();

            //LoadData
            var currentSettings = SaveLoadConfig.LoadData();
            ButtonsEventsSettings.DarkMode = currentSettings.IsDarkMode;
            ButtonsEventsSettings.UpdateGlobalStylesheet(currentSettings.IsDarkMode);
            ButtonsEvents.PathLastProject = currentSettings.PathLastProject;
            ButtonsEvents.PathLastSaveLevel = currentSettings.PathLastLevel;
            EditorState.CurrentLevel = SaveLevel.Load(ButtonsEvents.PathLastSaveLevel);

            //CreateGUI
            _mainPanel = UIManager.CreateMainUI() as Panel;
            ButtonsEvents.DesktopInstance = _desktop;
            ButtonsEventsSettings.DesktopInstance = _desktop;
            

            _desktop.Root = _mainPanel;
        }
        protected override void Draw(GameTime gameTime)
        {
           
            GraphicsDevice.Clear(ButtonsEventsSettings.BackgroundColor);

            _spriteBatch.Begin();
            LevelRenderer.Draw(_spriteBatch, _pixel, EditorState.CurrentLevel); // tiles first,
            GridRenderer.Draw(_spriteBatch, _pixel);                            // grid lines on top
            LevelRenderer.DrawObjects(_spriteBatch, _pixel, EditorState.CurrentLevel); // objects above the grid
            _spriteBatch.End();

            _desktop?.Render();

            base.Draw(gameTime);
        }
        protected override void Update(GameTime gameTime)
        {
            //Mouse
            var mouse = Mouse.GetState();
            PlacementTool.Update(mouse, _previousMouse,IsActive);
            _previousMouse = mouse;

            //Keyboard
            Hotkeys.Update(IsActive, _desktop);

            //AutoSave
            AutoSave.autoSave(ref _autoSaveTimer,gameTime);
           

            

            base.Update(gameTime);
        }

        protected override void OnExiting(object sender, ExitingEventArgs args)
        {
            SaveLoadConfig.SaveData(new EngineData { 
                IsDarkMode = ButtonsEventsSettings.DarkMode,
                PathLastProject = ButtonsEvents.PathLastProject,
                PathLastLevel = ButtonsEvents.PathLastSaveLevel

            });
            
            base.OnExiting(sender, args);
        }

    }
}
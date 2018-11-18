
using CardProto.System;
using CardProto.System.UI;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using Util;
using GameLib.Server.Services.ServiceLoader;
using GameLib.Server.Services;
using System.IO;
using GameLib.Server;
using GameLib.Client.System.GraphicsHandler;

namespace CardProto
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        GraphicsDeviceManager graphics;
        DataMapManager manager = new DataMapManager();
        SpriteBatch spriteBatch;
        NetworkInterface n;
        FileStream fs;
        StreamWriter sw;
        NetworkUpdatableString s;
        RenderCallHelper helper;
        TextureAtlas textureAtlas;
        GameState gs;


        public Game1()
        {
            ServiceLoader.CLASS_FILE = "Config"+ Path.DirectorySeparatorChar +"class_file.ini";
            ServiceLoader.MODULE_FILE = "Config"+ Path.DirectorySeparatorChar +"module_file.ini";
            graphics = new GraphicsDeviceManager(this);
            gs = new GameState();
     
            Player player = new Player();
            gs.players.Add(player);
            Content.RootDirectory = "Content";
            ServiceController.setRuntime(Runtime.HYBRID);

            ServiceController.LoadGameState(gs);
            
            n = new NetworkInterface();
            s = new NetworkUpdatableString(manager, "GameState");
            
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

            base.Initialize();
            fs = new FileStream("LOG"+DateTime.Now.ToString("hhmmddMMyyyy")+ ".txt", FileMode.OpenOrCreate);
            sw = new StreamWriter(fs);
            Console.SetOut(sw);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            gs.dataManager.CreateNewMap();
       
            Random r = new Random();
            // Create a new SpriteBatch, which can be used to draw textures.
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureAtlas = new TextureAtlas(Content);
            helper = new RenderCallHelper(spriteBatch, textureAtlas);
            gs.soundEffects = new GameLib.Client.System.SoundHandlers.SoundEffectAtlas(Content);
            gs.camera = new GameLib.Client.System.Camera(this.graphics.GraphicsDevice.Viewport);
            Renderer.LoadRenderingAliases("Config/rendering.alias", textureAtlas);
            ServiceLoader.ClassHook();
            ServiceLoader.ModuleHook();

            //  gs.dataManager.SendData(n.main);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
        {
            gs.dataManager.CreateNewMap();
            s.Reset();

            gs.dataManager.getCurrentMap().AddData("input:keyboard", Keyboard.GetState().GetPressedKeys());
            int overallSize = 0;
            int size = 0;
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.Escape))
            {
                this.Exit();
            }





            ServiceController.RunServices();
            gs.dataManager.ReciveData(gs.dataManager.getCurrentMap());
            gs.FrameCount++;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null, gs.camera.TransformMatrix);
            Renderer.data = Renderer.data.OrderBy(a=>a.layer).ToList();
            foreach (Renderable r in Renderer.data.Where(a=>a.draw&&a.layer != 50))
            {
                r.Render(textureAtlas,spriteBatch,helper);
            }
            spriteBatch.End();
            spriteBatch.Begin();
            foreach (Renderable r in Renderer.data.Where(a => a.draw && a.layer == 50))
            {
                r.Render(textureAtlas, spriteBatch, helper);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

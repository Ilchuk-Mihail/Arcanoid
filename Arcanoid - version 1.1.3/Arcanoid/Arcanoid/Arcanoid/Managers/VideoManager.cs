using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid.Managers
{
    class VideoManager
    {
        Video video;
        VideoPlayer videoPlayer;
        Texture2D VideoTexture;
        Rectangle VideoRecangle;
        KeyboardState state;

       public int parametr = 0;

        public VideoManager(GraphicsDevice graphicsDevice)
        {
            videoPlayer = new VideoPlayer();
            VideoRecangle = new Rectangle(graphicsDevice.Viewport.X, graphicsDevice.Viewport.Y, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height + 5);
        }

        public void Update()
        {
            state = Keyboard.GetState();

            if (parametr == 0)
            {
                if (videoPlayer.State == MediaState.Stopped)
                    parametr = 1;
            }

            if (state.IsKeyDown(Keys.Enter) || state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Escape))
            {
                parametr = 1;
                videoPlayer.Stop();
            }
        }

        public void Play()
        {
 
            if (videoPlayer.State == MediaState.Stopped)
            {
                videoPlayer.IsLooped = false;

              if(parametr == 0)
                videoPlayer.Play(video);
            }
    
        }

        public void DrawVideo(SpriteBatch spriteBatch)
        {
            if (videoPlayer.State != MediaState.Stopped)
            {
                VideoTexture = videoPlayer.GetTexture();
            }

            spriteBatch.Begin();

           if (VideoTexture != null && videoPlayer.State == MediaState.Playing)
           {
                { 
                    if(parametr == 0 )
                        spriteBatch.Draw(VideoTexture, VideoRecangle, Color.White);
                      
                }    
            }

            spriteBatch.End();

        }

        public void LoadContent(ContentManager Content)
        {
           video =  Content.Load<Video>("Video/Clip");
        }

    }
}

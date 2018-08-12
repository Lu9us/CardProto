
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLib.Client.System
{
    public class Camera
    {
        public Vector2 pos { get; set; }
        public float zoom { get; set; }
        public float rot { get; set; }
        private Rectangle bounds { get; set; }

        public Matrix TransformMatrix => Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                                          Matrix.CreateRotationZ(rot) *
                                          Matrix.CreateScale(zoom) *
                                          Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));

        public Camera(Viewport view)
        {
            bounds = view.Bounds;
            zoom = 1;
           pos= new Vector2(0,0);
          

        }

        public Vector2 mouseToWorld(Vector2 mouse)
        {
            return Vector2.Transform(mouse,Matrix.Invert( this.TransformMatrix));
           

        }
        public Vector2 WorldToScreen(Vector2 loc)
        {
            return Vector2.Transform(loc, this.TransformMatrix);


        }
    }


}

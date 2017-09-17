using System.Web;

namespace RThomaz.Web.Helpers
{
    public class ResizeParam
    {
        private readonly int _height = 100;
        private readonly int _width = 100;
        private readonly bool _isResize = false;

        public ResizeParam(HttpContextBase context)
        {
            if (context.Request["width"] != null)
            {
                _width = int.Parse(context.Request["width"]);
                _isResize = true;
            }
            if (context.Request["height"] != null)
            {
                _height = int.Parse(context.Request["height"]);
                _isResize = true;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }
        }

        public bool IsResize
        {
            get
            {
                return _isResize;
            }
        }
    }
}
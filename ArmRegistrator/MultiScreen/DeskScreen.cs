namespace ArmRegistrator.MultiScreen
{
    public class DeskScreen
    {
        public DeskScreen(ScreenRectangle prect)
        {
            _metrics = prect;
        }
        public ScreenRectangle Metrics
        {
            get { return _metrics; }
        }

        private readonly ScreenRectangle _metrics;
    }
}

using System;

namespace ArmRegistrator.Automation
{
    public class ObjectChangeStateEventArgs:EventArgs
    {
        public ObjectChangeStateEventArgs(int objectId)
        {
            ObjectId = objectId;
        }
        public int ObjectId { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ToolFunction
{
    public interface IMessage
    {
        void ShowMess(string p_strMess);
    }
    public class ConsoleMessage : MarshalByRefObject, IMessage
    {
        #region IMessage ≥…‘±

        public void ShowMess(string p_strMess)
        {
            Console.WriteLine(p_strMess);
        }

        #endregion
    }
}

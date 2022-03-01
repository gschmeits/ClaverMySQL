using System;
using System.Threading;
using System.Windows.Forms;
using PetrusClaver;

namespace ClaverMySQL
{
    public class PleaseWait : IDisposable
    {
        private Form mSplash;

        public int intPOSY;

        public PleaseWait()
        {
            Thread t = new Thread(new ThreadStart(workerThread));
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        public void Dispose()
        {
            mSplash.Invoke(new MethodInvoker(stopThread));
        }

        private void stopThread()
        {
            mSplash.Close();
        }

        private void workerThread()
        {
            mSplash = new WaitForm(intPOSY); // Substitute this with your own
            Application.Run(mSplash);
        }
    }
}

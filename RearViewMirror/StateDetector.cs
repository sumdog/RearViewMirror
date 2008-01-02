/**
 * RearViewMirror - Sumit Khanna 
 * http://penguindreams.org
 * 
 * License: GNU GPLv3 - Free to Distribute so long as any 
 *   modifications are released for free as well
 * 
 * This class taken from http://www.thescripts.com/forum/thread536508.html on 2007-12-28
 *   from the comment left by Willy Denoyette
 * 
 */

/***************** OBSOLETE ********************
 *   This class was for a workaround for the 
 *   ctrl+alt+delete bug which was fixed in
 *   AForge 1.5.1
 ***********************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RearViewMirror
{

    /// <summary>
    /// Provides an interface that can be implemented by classes that
    /// wish to detect state change.
    /// </summary>
    public interface StateInterface
    {
        void OnSessionLock();
        void OnSessionUnlock();
    }


    /// <summary>
    /// This form is designed to detect when the computer goes and comes
    /// from a "locked state." The Locked state causes the camera device to 
    /// stop working, so we need to detect it in order to restart the camera.
    /// </summary>
    public class StateDetector : Form
    {

        private StateInterface stateInt;

        //DLL Imports

        [DllImport("wtsapi32.dll")]
        private static extern bool WTSRegisterSessionNotification(IntPtr hWnd, int dwFlags);

        [DllImport("wtsapi32.dll")]
        private static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);


        //and here how to register...
        private const int NotifyForThisSession = 0; // This session only

        //and here the messages you can check..
        private const int SessionChangeMessage = 0x02B1;
        private const int SessionLockParam = 0x7;
        private const int SessionUnlockParam = 0x8;


        public StateDetector(StateInterface s)
        {
            WTSRegisterSessionNotification(this.Handle, NotifyForThisSession);
            stateInt = s;
        }



        //in your overriden WndProc.

        protected override void WndProc(ref Message m)
        {
            Console.WriteLine(m);
            // check for session change notifications
            if(m.Msg == SessionChangeMessage)
            {
                if(m.WParam.ToInt32() == SessionLockParam)
                    stateInt.OnSessionLock(); // Do something when locked
                else if(m.WParam.ToInt32() == SessionUnlockParam)
                    stateInt.OnSessionUnlock(); // Do something when unlocked
            }

            base.WndProc(ref m);
            return;
        }

    }
}*/
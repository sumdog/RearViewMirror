/*
 * RearViewMirror - Sumit Khanna 
 * http://penguindreams.org
 * 
 * License: GNU GPLv3 - Free to Distribute so long as any 
 *   modifications are released for free as well
 * 
 * Based on work by Andrew Kirillov found at the following address:
 * http://www.codeproject.com/KB/audio-video/Motion_Detection.aspx
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
//using WIA;

using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

//this class contains an old WIA based capture. It's slow and sucks and was commented out
// because of conflicts with the Camera class in the other namespace. 

namespace RearViewMirror
{
    /*
    public class Camera
    {


        private Image frame;

        private Device dev;

        public void chooseDevice()
        {
            CommonDialogClass diagclass = new CommonDialogClass();
            dev = diagclass.ShowSelectDevice(WiaDeviceType.UnspecifiedDeviceType, true, false);
            if (dev != null)
            {
                //settings.DeviceID = d.DeviceID;
                //settings.Save();
            }
        }

        public Camera()
        {
            dev = null;
            frame = null;
        }

        public bool Initalized
        {
            get
            {
                return (dev == null) ? false : true;
            }
        }

        private Image getImageFromByteArray(Byte[] b)
        {
            Image newImage;
            using (MemoryStream ms = new MemoryStream(b, 0, b.Length))
            {
                ms.Write(b, 0, b.Length);
                newImage = Image.FromStream(ms, true);
            }
            return newImage;
        }

        public Image takePhoto()
        {
            Item item = dev.ExecuteCommand(CommandID.wiaCommandTakePicture);
            
            foreach (string format in item.Formats)
            {
                if (format == FormatID.wiaFormatJPEG)
                {
                    WIA.ImageFile imagefile = item.Transfer(format) as WIA.ImageFile;

                    String filename = Path.GetTempFileName();
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    if (string.IsNullOrEmpty(filename) == false)
                    {
                        Byte[] c = (Byte[]) imagefile.FileData.get_BinaryData();
                        frame = getImageFromByteArray(c);                        
                    }
                    return frame;
                }
            }
            return null;
        }

    }*/
}

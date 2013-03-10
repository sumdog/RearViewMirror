// Motion Detector
//
// Copyright © Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
namespace motion
{
	using System;
	using System.Drawing;
	using System.Threading;
	//using VideoSource;
    using AForge.Video;
    using AForge.Video.DirectShow;

	/// <summary>
	/// Camera class
	/// </summary>
	public class Camera
	{
		private IVideoSource	videoSource = null;
		private IMotionDetector	motionDetecotor = null;
		private Bitmap			lastFrame = null;
        private Bitmap          lastRawFrame = null;


		// image width and height
		private int		width = -1, height = -1;

		// alarm level
		private double	alarmLevel = 0.005;

		//
		public event EventHandler	NewFrame;
		public event EventHandler	Alarm;

		// LastFrame property
		public Bitmap LastFrame
		{
			get { return lastFrame; }
		}

        public Bitmap LastRawFrame
        {
            get { return lastRawFrame; }
        }

		// Width property
		public int Width
		{
			get { return width; }
		}
		// Height property
		public int Height
		{
			get { return height; }
		}
		// FramesReceived property
		public int FramesReceived
		{
			get { return ( videoSource == null ) ? 0 : videoSource.FramesReceived; }
		}
		// BytesReceived property
		public long BytesReceived
		{
			get { return ( videoSource == null ) ? 0 : videoSource.BytesReceived; }
		}
		// Running property
		public bool Running
		{
			get { return ( videoSource == null ) ? false : videoSource.IsRunning; }
		}
		// MotionDetector property
		public IMotionDetector MotionDetector
		{
			get { return motionDetecotor; }
			set { motionDetecotor = value; }
		}

		// Constructor
		public Camera( IVideoSource source ) : this( source, null )
		{ }
		public Camera( IVideoSource source, IMotionDetector detector )
		{
			this.videoSource = source;
			this.motionDetecotor = detector;
			videoSource.NewFrame += new NewFrameEventHandler( video_NewFrame );
		}

		// Start video source
		public void Start( )
		{
			if ( videoSource != null )
			{
				videoSource.Start( );
			}
		}

		// Siganl video source to stop
		public void SignalToStop( )
		{
			if ( videoSource != null )
			{
				videoSource.SignalToStop( );
			}
		}

		// Wait video source for stop
		public void WaitForStop( )
		{
			// lock
			Monitor.Enter( this );

			if ( videoSource != null )
			{
				videoSource.WaitForStop( );
			}
			// unlock
			Monitor.Exit( this );
		}

		// Abort camera
		public void Stop( )
		{
			// lock
			Monitor.Enter( this );

			if ( videoSource != null )
			{
				videoSource.Stop( );
			}
			// unlock
			Monitor.Exit( this );
		}

		// Lock it
		public void Lock( )
		{
			Monitor.Enter( this );
		}

		// Unlock it
		public void Unlock( )
		{
			Monitor.Exit( this );
		}

		// On new frame
		private void video_NewFrame( object sender, NewFrameEventArgs e )
		{
			try
			{
				// lock
				Monitor.Enter( this );

				// dispose old frame
				if ( lastFrame != null )
				{
					lastFrame.Dispose( );
				}
                if (lastRawFrame != null)
                {
                    lastRawFrame.Dispose();
                }

				lastFrame = (Bitmap) e.Frame.Clone( );

                //Clone() doesn't actaully create a seperate copy
                // We need this for a raw frame, before motion 
                // detection to be transmitted via MJPEGStreams
                lastRawFrame = new Bitmap(e.Frame);

				// apply motion detector
				if ( motionDetecotor != null )
				{
					motionDetecotor.ProcessFrame( ref lastFrame );

					// check motion level
					if (
						( motionDetecotor.MotionLevel >= alarmLevel ) &&
						( Alarm != null )
						)
					{
						Alarm( this, new EventArgs( ) );
					}
				}

				// image dimension
				width = lastFrame.Width;
				height = lastFrame.Height;
			}
			catch ( Exception )
			{
			}
			finally
			{
				// unlock
				Monitor.Exit( this );
			}

			// notify client
			if ( NewFrame != null )
				NewFrame( this, new EventArgs( ) );
		}
	}
}

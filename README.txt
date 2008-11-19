Rear View Mirror
0.8.6-beta

Copyright 2007 Sumit Khanna
-GNU GPL v3
http://penguindreams.org
------------------------------

This is a simple C# Application that connects to a web camera device or MJPEG enabled webcam and displays a window when ever motion is detected. It was intended to be a "Rear View Mirror" in my cubical so people wouldn't sneak up on me anymore. 

The program also contains a simple HTTP based MJPEG server so that other clients can connect to your web camera. 

I simply developed the front end. The original motion detection was designed by Andrew Kirillov <andrew.kirillov@gmail.com> and uses Image filters from the open source AForge.NET library. His project can be found at the following:

http://www.codeproject.com/KB/audio-video/Motion_Detection.aspx


Known Issues
------------------------------

No security on MJPEG server. Anybody can connect.

User will not get a 404 error if a camera doesn't actually exist.

If you remove a camera, people connected to you at that camera will stay connected and should you recreate a camera with that name, it will start sending the new feed to people connected to previous camera.


Todo
------------------------------

Show which camera a user is connected to in server window

Automatic Update Checks

More options per Camera (alert sound, enable camera on startup, etc.)


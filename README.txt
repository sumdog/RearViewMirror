Rear View Mirror
0.8.5-beta

Copyright 2007 Sumit Khanna
-GNU GPL v3
http://penguindreams.org
------------------------------

This is a simple C# Application that connects to a web camera device or MJPEG enabled webcam and displays a window when ever motion is detected. It was intended to be a "Rear View Mirror" in my cubical so people wouldn't sneak up on me anymore. 

The program also contains a simple HTTP based MJPEG server so that other clients can connect to your web camera. The web address for the camera doesn't matter. Any URL will default to the camera (e.g. "http://yourHostname/t.jpeg").

I simply developed the front end. The original motion detection was designed by Andrew Kirillov <andrew.kirillov@gmail.com> and uses Image filters from the open source AForge.NET library. His project can be found at the following:

http://www.codeproject.com/KB/audio-video/Motion_Detection.aspx




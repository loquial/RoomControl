RoomControl
===========

Arduino, webserver, and kinect for room control using the web, voice, or gesture control. The mtoivation was that me and my roommate had lofted beds and we sometimes forgot keys, so this would allow control of that through voice or phone.

Arduino communication through usb port, didn't have a wireless capable arduino. Arduino controlled a lamp through a relay. C# webserver listened to commands sent through a simple web control (so it could be loaded on either my phone or my roommates since I had an android and he had an iphone)

Kinect also allowed voice control of the light and I was in the process of integrating gesture control, all in the same C# app. Kinect also had voice direction detection, so it was possible to limit commands to a certain angle, and filter out voice from other directions.

Voice/mobile interface could also control music playing/shuffling, though it just accessed the music directory since hooking into windows music player with C# was annoying
Could easily be scaled to other room tasks, like AC/door lock, just didn't have the hardware at the time and now I no longer live in a small dorm room so I stopped working on this.

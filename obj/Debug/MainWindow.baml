   M S B A M L   `   `   `  ����I  EKinectExplorer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:Microsoft.Samples.Kinect.KinectExplorer'Microsoft.Samples.Kinect.KinectExplorer  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   8    2Microsoft.Samples.Kinect.KinectExplorer.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Cl5clr-namespace:Microsoft.Samples.Kinect.KinectExplorer     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          � Title$  Kinect Explorer - Console��5   <   
 � Top$ 0��5   	    � Left$ 0��6   $	��936��5   	   $	��788��6    � 	Resources 5	         NormalBrandBrush �� Color  HoverBrandBrush  SecondaryBrandBrush  LightNeutralBrush  MediumNeutralBrush  DarkNeutralBrush  
KinectFont  HeaderFontSize  LabelFontSize 	 ItemFontSize �� DataType>   8Microsoft.Samples.Kinect.KinectExplorer.KinectSensorItem>   8Microsoft.Samples.Kinect.KinectExplorer.KinectStatusItem � ColumnDefinitions �� Path �� StringFormatA   ;Microsoft.Samples.Kinect.KinectExplorer.VisibilityConverter 
 VisibilityConverter��%Q  &	        &	 !     &	 B     &	 c     &	 �     &	 �     &	 �     &	 �     &	      &		     (�� 8    5      *, +)7  7  7  7  7 	 7  (�� �    5,      *, +)7  7 	 7  7  7 	 7  7  7 	 7  &	
 b    �� 5
   
   $ #52318F��63   �� 5   
   $ #322350��62   �� 5   
   $ #00BCF2��66   �� 5   
   $ #D2D2D2��64   �� 5   
   $ #969696��65   ��5   
   #505050��6<   *�5   
   Segoe UI)�62   X�5   
   24.0Q�62   X�5   
   18.0Q�61   X�5   
   12.0Q�60   �� 5   
   "  6   .R�5      �� -   5        
������ $��	0 10 0 10q�5   Y   ��5      �� 5      .R�5      ]� #��z��6$   .+�5      �� $��Right=�6$   $��Images\KinectSensor.png��6;   $	��100��6\   �� 5      .+�5   "   �� 8��  8�� 8�� ��5!   ,   �� *Status+�� 5"   "   8�� 8�� 8�� ��5%   ,   �� *Id+5&      5'      5(      5*      5+      �� 5,   
   "  6   .R�5-      �  5.      �� 5/      $��* �6'   �� 50      $	��140 �6'   �� 51      $	��100 �6'   .+�53      �� $��0��6   8��  8�� 8�� ��57      �� *Id+�� 58      $��1��6   8�� 8�� $
��Bold"�5;      8�� ��5=      �� *Status+�� 5>      $��2��6   8�� 8�� 8�� ��5B      �� $ DateTime�$ {0:t}��5C      5D        5E   
   6    5F      .��5G      � .+�5H   
   � 
������ $��20q�6(   	 � RowDefinitions	 5I      �� 5J      $
��Auto �6    �� 5K      $
��Auto �6    �� 5L      $
��Auto �6    �� 5M      $
��Auto �6    �� 5N      $��* �6    .+�5P      �� 
���   �$��4��6,   $��92,210,378,217q�69   $��2��6Q   .+�5Q      r� -   5        $��myPlayer��5Q   #   $	��276��63   $	��115��6?   
 r� LoadedBehavior$
 Manual=�6L    r� UnloadedBehavior$
 Stop=�6d    r� Stretch$
 Fill=�6|   $
��Left=�6�   $��10q�6�   �� 5T      $
��Left=�6!   $	��279��6<    �� Orientation$ 
Horizontal=�6H   $��50��6a   $��10q�6m   .+�5U      �� -   5        $��prevBtn��5U   j   $
��Prev��6    �� 	ClickMode$ Release=�6-   $��25��6^   $��50��6z   $��20,0,0,0q�6�   �� 5V      -   5        $��pauseBtn��5V   o   $��Pause��6   $ Release=�6-   $��25��6c   $��50��6   $��50,0,0,0q�6�   �� 5W      -   5        $��nextBtn��5W   i   $
��Next��6   $ Release=�6,   $��25��6]   $��50��6x   $��50,0,0,0q�6�   5f      �� 5g      -   5        $��currentSong��5g   4   $��Label��6   $��28��6(   ���	#FFFCFCFC6G   5h      �� 5i      
���   �$��4��6,   $��92,530,378,20q�69   .+�5j      �� -   5        $��consoleLabel��5j   4   $��Label��6   $��28��6(   ���	#FFFCFCFC6H   �� 5k      -	   5        $��
consoleBox��5k   '   $	��112��6   $	��379��69   5l      �� 5m      -
   5        $��button1��5m   o   $��Button��6   $��4��6&   $��23��63   $
��Left=�6?   $��723,446,0,0q�6Z   $	��Top=�6~   $��75��6�   6�   5n      5o      5p      
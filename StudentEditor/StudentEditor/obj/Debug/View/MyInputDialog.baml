   M S B A M L   `   `   `  ����  StudentEditor&     StudentEditor.View.MyInputDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  MyInputDialog��5   	   $	��300��6   $	��400��6,    � 
ResizeMode$ NoResize=�6N    � AllowsTransparency .  � WindowStyle$
 None=�5   #   ��5   8   Z�  � 	Resources 5         RoundButton �� 
TargetType �� CornerRadius �� TextAlignment��%.  &	        �� 5   
   " ��6.   .R�5      �� 
���   �$��1q�6)   $ 5,5,5,5��6=   ._�5	      �� -   5        
�������$��2q�5	   -   $ 5,5,5,5��6A   
�������$ Center=�5
   :   $��Stretch=�5      ._�5      �� ��. $��Center=�6B   6_   5      5      5      5      .��5      �� $	��250��5      
����2��
���   �$��2q�5      $ 10,80,10,80��5      $��Stretch=�5      $��Stretch=�5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��38* �6#   �� 5      	 � RowDefinitions	 5      �� 5       �� 5!      �� 5"      .+�5%      �� -   5        $��
HeaderText��5%   n   $��5,5,4.6,4.8q�6   $��0��6-   
�������$
��test��6M   $��Cambria)�6Y   $��20(�6�   $��2��6�   �� 5&      -   5        $��InputTextBox��5&   A   $	��150��6   $��Stretch=�6%   
 �� TextWrapping$

 Wrap=�5'      " �� VerticalScrollBarVisibility$ Visible=�6*    �� AcceptsReturn . $��10,35,10.956,88.4q�5(   .   $��3��6I   �� 5)      $��2��6    �� Orientation$ 
Horizontal=�6&   $��Center=�6?   $��Bottom=�6\   $��42��6w   $��70,0,19.956,0.4q�6�   .+�5*      �� -   5        $��OKButton��5*   A   #��[  6   $��5q�6Q   $��OK��6\   $	��129��6�    �� 	IsDefault . ��6i   Z� 6�   �� 5+      -   5        $��CancelButton��5+   A   #��[  6   $��5q�6U   $��Cancel��6`   $	��140��6�    �� IsCancel . ��6q   Z� 6�   5,      5-      5.      5/      
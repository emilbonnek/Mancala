   M S B A M L   `   `   `  ����B  >Mancala, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MancalaMancala  5   	       Mancala.LauncherWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Mancala     5   	     � Title$  LauncherWindow��5   	   $	��400��6    $	��600��6-   .��5	      � $��0,0,0,0q�6   .+�5
   
   �� -   5        $��Start new game��5
      $
��Left=�6*   $��250,220,0,0q�6Z   $	��Top=�6o   $	��100��6�   �� 5   
   -   5         ��Name$ _Name1��5      $
��Left=�6z   $��23��6�   $��
80,164,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $��Player 1 Name��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ _Name2��5      $��Right=�6z   $��23��6�   $��
0,164,80,0q�6�   $
 Wrap=�6�   $��Player 2 Name��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$
 Box1��5      $��Player1��6!   $
��Left=�62   $	��100��6M   $��
100,35,0,0q�6Z   $	��Top=�6n   $��85��6�   .��5      �� .+�5      	� -   5         	�Name$ _RadioAILeft��5      $��AI��64   $��RightToLeft=�6`   $
��Left=�6}   $��	20,20,0,0q�6�   	� 5      -   5        $ _RadioPlayerLeft��5      $��Player��68   $��RightToLeft=�6h   $
��Left=�6�   $��0,10,0,0q�6�    d� 	IsChecked$
 TrueY�6�   5      5      �� 5   
   -   5        $
 Box2��5      $��Player2��6!   $��Right=�62   $	��100��6N   $��
0,35,100,0q�6[   $	��Top=�6o   $��85��6�   �� 5   
   .+�5      	� -   5        $ _RadioAIRight��5      $��AI��61   $��Right=�6^   $��0,63,141.6,0q�6z   $	��Top=�6�   	� 5      -	   5        $ _RadioPlayerRight��5      $��Player��65   $��Right=�6e   $��
0,20,124,0q�6�   $	��Top=�6�   $
 TrueY�6�   5      �� 5   
   $
��Left=�6   $��262,295,0,0q�6/   $
��Wrap=�6D   $��MANCALA��6X   $	��Top=�6g   $��16(�6   5      5      
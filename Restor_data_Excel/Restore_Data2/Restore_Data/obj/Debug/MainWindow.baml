   M S B A M L   `   `   `  ����E  AUCGeneraFi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:UCGeneraFi;assembly=UCGeneraFi
UCGeneraFi  5   
    Restore_Data   Restore_Data.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   C
UCGeneraFi,clr-namespace:UCGeneraFi;assembly=UCGeneraFi     5   
   $	��200��5   	   $	��500��6   ���#D4D7DE.��5      � .+�5	   
   �   � RowDefinitions  5
      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $	��150 �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ 
btn_Import��5   "   $��4��6      	styleVert#��[  69   $��Import��6\   6m   �� 5      -   5        $ 
btn_Export��5   2   $��4��6   $��1��6"   $��Export��6F   #��[  6W   $
��Left=�6�   �� 5      -   5         ��Name$ label��5   "   $��0��6   $��Server name :��61     	BoldLabel#��[ 6I   $
��Left=�6l   $��Center=�6�   �� 5      -   5        $ label1��5   !   $��1��6   $��Database name :��61   #��[ 6K   $
��Left=�6n   $��Center=�6�   �� 5      -   5         ��Name$ txt_servername��5   3   $��0��6   $��1��6#   $��MONPC-PC\SQLSERVER2014��6K    �� TextAlignment$ Center=�6i   $
��Left=�6�   $��23��6�   $	��200��6�   $��Center=�6�   �� 5       -   5        $ 
txt_namedb��5    4   $��1��6   $��1��6$   $��GPBUG��6H   $ Center=�6U   $
��Left=�6l   $��26��6�   $	��200��6�   $��Center=�6�   �� 5"      -   5        $ label22��5"   "   $��2��6   $��Nom de la table :��63   #��[ 6O   $
��Left=�6r   $��Center=�6�   �� 5#      -   5        $ label222��5#   "   $��3��6   $��Nom du colonne :��64   #��[ 6O   $
��Left=�6r   $��Center=�6�   �� 5$      -	   5        $ txt_nameTable��5$   4   $��2��6   $��1��6$   $��	VersionBD��6K   $ Center=�6\   $
��Left=�6s   $��26��6�   $	��200��6�   $��Center=�6�   �� 5%      -
   5        $ txt_nameColonne��5%   4   $��3��6   $��1��6$   $��	VersionBD��6M   $ Center=�6^   $
��Left=�6u   $��26��6�   $	��200��6�   $��Center=�6�   5&      5)      5,      
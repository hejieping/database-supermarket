   M S B A M L   `   `   `  ����F  BSuperMarket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:SuperMarket.UIWpf.ManagerSuperMarket.UIWpf.Manager  5      -    'SuperMarket.UIWpf.Manager.WareHouseInfo   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:SuperMarket.UIWpf.Manager     5      .��5	      � ���	#FFD4CDCD  � RowDefinitions  5
   
   �� 5      $��0* �6   �� 5      .+�5   
   �� $��0q�6   $��0,40,0,0q�6%   ���	#FFFFF8F8 �� CornerRadius$ 10��6N   $	��230��6`   $	��Top=�6m   $��2��6�   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5   '      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6   $��10,9,9,5q�69   6~   5      �� 5   
   $��1q�6   ���	#FFFFF8F8$ 10��6<   $��35��6N   $	��Top=�6Z   $��2��6r   �� 5   
   $��1q�6   $��	0,315,0,0q�6%   ���	#FFFFF8F8$ 10��6O   $	��135��6a   $	��Top=�6n   $��2��6�   �� 5   
   $��1q�6   ���	#FFFFF8F8$ 10��6<   $��35��6N   $	��Top=�6Z   $��	0,275,0,0q�6r   $��2��6�   �� 5   
   -   5         ��Name$ label��5      $��仓库信息��6   $��9,2,505,415q�6.   $��18.667(�6C   $��.HeitiUI SC)�6U   $��Center=�6n   ���	#FF535050$��2��6�   �� 5   
   -   5        $ 
label_Copy��5      $��仓库信息详情��6$   $��4,280,470,137q�65   $��18.667(�6L   $��.HeitiUI SC)�6^   ���	#FF535050$��2��6�     5   
   -   5        $ dataGridDetail��5   #   $ * 6   $��
10,325,9,9q�6;   $��2��6O   5       � 	Resources 5       �� 
TargetType   textBoxTemplate ��Name �� EndPoint	 �� 
StartPoint
 � Color � Offset��%  &	        �� 5   
   " ��6   .R�5      �� $ ButtonTemplate��6l   #��z��6   $��0q�6A   $ 8,8,8,8��6U   ��5      �� 5      $	 0,1&�6*   $		 0,0&�69   .��5      � $
 	#FFCBCBCB��6'   $	 0.0Q�69   5      5       5!      5"      5#      
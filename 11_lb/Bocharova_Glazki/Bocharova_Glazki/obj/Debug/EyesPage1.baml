   M S B A M L   `   `   `  ����K  GBocharova_Glazki, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Bocharova_GlazkiBocharova_Glazki  5           Bocharova_Glazki.EyesPage1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Bocharova_Glazki     5      -   5          N� Title$  	EyesPage1��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   �� 5      $
��auto �6   6,   .+�5   
   �� $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��Center=�6W   $��0 10 0 0q�6r   .+�5      �� -   5        $��TBCount��5      $��20(�6'   $��0 5 0 0q�65   �� 5      -   5        $��TBAllRecords��5      $��20(�6,   $��0 5 0 0q�6:   �� 5      -   5        $��LeftDirButton��5      $��<��6*   $��20��69   $��20��6D   6n   �� 5      -   5        $��PageListBox��5   $   $��20(�6   $��Center=�6U   ��5      �� 5      .R�5      �� $ 
Horizontal=�6%   5      5      �� 5      -   5        $��RightDirButton��5      $��>��6+   $��20��6:   $��20��6E   6p   5       �� 5"   
   $ 
Horizontal=�6   $��Center=�6.   $��0��6K   $
��0 20q�6X   .+�5$      �� -   5        $��
TBoxSearch��5$      $	��300��6N   $��15(�6Z   $��10q�6h   6w   �� 5&      -   5        $��sortComboBox��5&   Y   $��15(�6�   $	��200��6�   $��10q�6�   $��10 0 0 0q�6�   .@�5'      �� -	   5        $��noSort��5'       �� 
IsSelected . $��Сортировка��6E   �� 5(      по возр скидки6,   �� 5)      по убв скидки6+   �� 5*      $"по возр приоритета60   �� 5+      " по убв приоритета6/   �� 5,      (&по возр наименования62   �� 5-      &$по убв наименования61   5.      �� 50      -
   5        $��filterComboBox��50   1   $	��100��6   $��15(�6#   $��10 0 0 0q�6G   $��10q�6Y   .@�51      �� Все6!   �� 52      ЗАО6!   �� 53      МКК6!   �� 54      МФО6!   �� 55      ОАО6!   �� 56      ООО6!   �� 57      ПАО6!   58      59      �� 5<   
   -   5        $��EyesListView��5<      $��1��6'   ��5=      �� 5>      .R�5?      � $
��0 10q�6    � ColumnDefinitions 5@      �� 5A      61   �� 5B      61    5D      �� 5E      $��50 �6,   �� 5F      $��* �6,   �� 5G      $��* �6,   �� 5H      $��* �6,   .+�5J      �� $	��150��6    $��0��6,   $��UniformToFill=�6<   $��4��6T   $��Center=�6e   $��	10 0 50 0q�6�   ��5K      �� 5L   "    �� Path$
 Logo�6*    �� TargetNullValue 5M   &   ��5N   *   Resources/picture.png��5P   +   5R   #   5T      �� 5V      $��1��6%   $��0��65   $ 
Horizontal=�6B   .+�5W      �� $
��Left=�6b   $��20(�6}   ��6(   �� *AgentTypeString+ �� StringFormat$ {0:0} |��6}   �� 5X      $
��Left=�6A   $��20(�6\   $
��Wrap=�6j   $��5 0 0 0q�6~   ��6)   �� *Title+6~   5Y      �� 5Z      $��2��6%   $��1��65   .+�5\      �� $
��Left=�6Z   $��15(�6u   ��6(   �� *Phone+$ Номер: {0}��6u   5]      �� 5^      $��1��6a   $��2��6q   $
��Left=�6~   $��15(�6�   ��6$   �� *
Priority+$' !Приоритетность: {0}��6�   5_      5`      5b      5g      5h      
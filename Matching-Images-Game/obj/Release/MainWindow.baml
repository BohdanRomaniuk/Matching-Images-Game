   M S B A M L   `   `   `  ���� O  KMatching-Images-Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Matching_Images_GameMatching_Images_Game  %    Matching_Images_Game.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:Matching_Images_Game       � Title$!  Парні картинки��$	��375��$	��470�� � Icon$ matching-icon.ico��.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� -    ��Name$ groupBox��$��Меню��$	��5,0q�$��0��.���  � ColumnDefinitions �� $
��0.5* ��� $
��0.5* �.+�� $��0�� �� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� -    ��Name$ labeSize��$&�� Розмірність поля:����  �� DisplayMemberPath$ Value�� �� SelectedValuePath$	 Key��$��1��$��3q����� *
FieldSizes+ �� SelectedItem �� *	FieldSize+�� -   $ 
labelDelay��$(��"Затримка картинок:��$
��Left=�$	��Top=�$��1��� $��1��$��1�� �� $
��0.7* ��� $
��0.3* �.+��� -   	 ��Name$	 	delayTime��
 �� TextWrapping$

 Wrap=�$��3q����� *	DelayTime+�� -   $ delayTimeSec��$��сек.��$��1��� $��1�� �� $
��Auto ��� $
��Auto �.+�� $��0�� �� $
��0.5* ��� $
��0.5* �.+��� -   $ labelUserName��$��Ім'я гравця:���� -   $	 userName��$

 Wrap=�$��1��$��3q����� *	GamerName+�� -    ��Name$ 	startGame��$��Почати гру��$��1��$��3q����� *	StartGame+�� -	   $ 	groupBox1��$2��,10 найкращих результатів��$	��5,0q�$��1��.���� -
    ��Name$ listView�� �� AlternationCount$ 10������ *BestResults+ � 	Resources (   "Matching_Images_Game.DateConverter   IndexConverter��%   &	           �� View �� .P��� $	��№�� �� Width$ 25�� �� DisplayMemberBinding ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� 	Converter# [  �� $��Ім'я гравця��$	 145�� �� *	GamerName+�� $%��Розмірність поля��$	 100�� �� *	FieldSize+�� $��Найкращий час��$	 100�� �� *
BestTime+�� $��Очки��$ 54�� �� *Points+
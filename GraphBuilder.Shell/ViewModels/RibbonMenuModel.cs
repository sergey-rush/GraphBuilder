using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using GraphBuilder.Core;
using GraphBuilder.Shell.Models;
using GraphBuilder.Shell.Views;

namespace GraphBuilder.Shell.ViewModels
{
    public static class RibbonMenuModel
    {
        #region Application Menu

        public static ControlData New
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Новый";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str, 
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/NewDocument_32x32.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/NewDocument_32x32.png", UriKind.Relative),
                            Command = new DelegateCommand(OnNewDocument),
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Open
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Открыть";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Открыть (Ctrl+O)";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/OpenDocument_32x32.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/OpenDocument_32x32.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Save
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Сохранить";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Сохранить (Ctrl+S)";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Save_32x32.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Save_32x32.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(OnSaveDocument),
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SaveAs
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Сохранить как";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Сохранить как (F12)";

                        ApplicationSplitMenuItemData splitMenuItemData = new ApplicationSplitMenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Save_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(OnSaveDocument),
                        };
                        _dataCollection[Str] = splitMenuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SaveAsXmlDocument
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "XML документ";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Сохранить как XML документ";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Document_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(OnSaveDocument),
                            KeyTip = "W",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SaveAsTextFile
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Текстовый файл";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Сохранить как текстовый файл *.csv";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Document_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(OnSaveDocument),
                            KeyTip = "T",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SaveAsPdfDocument
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "PDF документ";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Сохранить как PDF документ";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Document_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(OnSaveDocument),
                            KeyTip = "P",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }
        
        public static ControlData Print
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Печать";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Печать (Ctrl+P)";

                        ApplicationSplitMenuItemData splitMenuItemData = new ApplicationSplitMenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Print_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = splitMenuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData PrintOptions
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Настройки печати";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Выбрать принтер, количество копий, настройте другие опции перед печатью.";

                        ApplicationSplitMenuItemData splitMenuItemData = new ApplicationSplitMenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Print_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "P",
                        };
                        _dataCollection[Str] = splitMenuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData QuickPrint
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Быстрая печать";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Отправить документ на печать по умолчанию.";

                        ApplicationMenuItemData menuItemData = new ApplicationMenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Print_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "Q",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData PrintPreview
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Предварительный просмотр";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Предварительный просмотр документа перед печатью";

                        ApplicationMenuItemData menuItemData = new ApplicationMenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/PrintPreview_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "V",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }
        public static ControlData ImportMaster
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Импорт";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Запустить мастер импорта и выбрать наиболее важные данные";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/ImportMaster.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/ImportMaster.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(OnImportWindowOpen),
                            KeyTip = "P",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData FileImport
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Импорт из файла";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Запустить мастер импорта и выбрать наиболее важные данные";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/FileData.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/FileData.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(OnImportWindowOpen),
                            KeyTip = "P",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData About
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "О программе GraphBuilder";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        ApplicationMenuItemData menuItemData = new ApplicationMenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/NewDocument_32x32.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Close
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Выйти";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        ApplicationMenuItemData menuItemData = new ApplicationMenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Exit.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData RecentDocuments
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Recent Documents";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryCategoryData<RecentDocumentData> galleryCategoryData = new GalleryCategoryData<RecentDocumentData>();

                        for (int i = 0; i < 6; i++)
                        {
                            RecentDocumentData recentDocumentData = new RecentDocumentData()
                            {
                                Index = i + 1,
                                Label = "Recent Doc " + i,
                            };
                            galleryCategoryData.GalleryItemDataCollection.Add(recentDocumentData);
                        }

                        _dataCollection[Str] = galleryCategoryData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData WordOptions
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Настройки";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        ButtonData buttonData = new ButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Options_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "I",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ExitWord
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Выход";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        ButtonData buttonData = new ButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Exit.png", UriKind.Relative),
                            Command = ApplicationCommands.Close,
                            KeyTip = "X",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #endregion Application Menu
        
        #region Help Button

        public static ControlData Help
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Help";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        ButtonData Data = new ButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            ToolTipTitle = "Help (F1)",
                            ToolTipDescription = "Microsoft Ribbon for WPF",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #endregion

        public static ControlData Clipboard
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Буфер обмена";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GroupData Data = new GroupData(Str)
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Paste_16x16.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Paste_32x32.png", UriKind.Relative),
                            KeyTip = "ZC",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #region Font Group Model

        public static ControlData Font
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Font";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GroupData Data = new GroupData(Str)
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Font_16x16.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Font_32x32.png", UriKind.Relative),
                            KeyTip = "ZF",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Paste
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Вставить";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Вставить (Ctrl+V)";
                        string ToolTipDescription = "Вставить содержимое буфера обмена.";
                        string DropDownToolTipDescription = "Click here for more options such as pasting only the values or formatting.";

                        SplitButtonData splitButtonData = new SplitButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Paste_16x16.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Paste_32x32.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = ApplicationCommands.Paste,
                            KeyTip = "V",
                        };
                        splitButtonData.DropDownButtonData.ToolTipTitle = TooTipTitle;
                        splitButtonData.DropDownButtonData.ToolTipDescription = DropDownToolTipDescription;
                        splitButtonData.DropDownButtonData.Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute);
                        splitButtonData.DropDownButtonData.KeyTip = "V";
                        _dataCollection[Str] = splitButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData PasteSpecial
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Paste _Special";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Paste Special (Alt+Ctrl+V)";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Paste_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData PasteAsHyperlink
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Paste As _Hyperlink";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Paste_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Cut
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Вырезать";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string CutToolTipTitle = "Вырезать (Ctrl+X)";
                        string CutToolTipDescription = "Вырезать выбранное и в ставить в буфер обмена.";

                        ButtonData buttonData = new ButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Cut_16x16.png", UriKind.Relative),
                            ToolTipTitle = CutToolTipTitle,
                            ToolTipDescription = CutToolTipDescription,
                            Command = ApplicationCommands.Cut,
                            KeyTip = "X",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Copy
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Копировать";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Копировать (Ctrl+C)";
                        string ToolTipDescription = "Копировать выбранное в буфер обмена.";

                        ButtonData buttonData = new ButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Copy_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = ApplicationCommands.Copy,
                            KeyTip = "C",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData FormatPainter
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Format Painter";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Format Painter (Ctrl+Shift+C)";
                        string ToolTipDescription = "Copy the formatting from one place and apply it to another. \n\n Double click this button to apply the same formatting to multiple places in the document.";

                        ButtonData buttonData = new ButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/FormatPainter_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = HelpFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "FP",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData FontFace
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Font Face";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Font (Ctrl+Shift+F)";
                        string ToolTipDescription = "Change the font face.";

                        ComboBoxData comboBoxData = new ComboBoxData()
                        {
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            KeyTip = "FF",
                        };

                        _dataCollection[Str] = comboBoxData;
                    }
                    return _dataCollection[Str];
                }
            }
        }

       

      

      
        public static ControlData GrowFont
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Grow Font";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Grow Font (Ctrl+>)";
                        string ToolTipDescription = "Increase the font size.";

                        ButtonData buttonData = new ButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Font_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.IncreaseFontSize,
                            KeyTip = "FG",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ShrinkFont
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Shrink Font";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Shrink Font (Ctrl+<)";
                        string ToolTipDescription = "Decrease the font size.";

                        ButtonData buttonData = new ButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Font_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.DecreaseFontSize,
                            KeyTip = "FK",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ClearFormatting
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Clear Formatting";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Clear Formatting";
                        string ToolTipDescription = "Clear all the formatting from the selection, leaving only the plain text.";

                        ButtonData buttonData = new ButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/ClearFormatting_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = HelpFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "E",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Bold
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Bold";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Bold (Ctrl+B)";
                        string ToolTipDescription = "Make the selected text bold.";

                        ToggleButtonData toggleButtonData = new ToggleButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Bold_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.ToggleBold,
                            KeyTip = "1",
                        };
                        _dataCollection[Str] = toggleButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Italic
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Italic";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Italic (Ctrl+I)";
                        string ToolTipDescription = "Italicize the selected text.";

                        ToggleButtonData toggleButtonData = new ToggleButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Italic_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.ToggleItalic,
                            KeyTip = "2",
                        };
                        _dataCollection[Str] = toggleButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Underline
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Underline";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Underline (Ctrl+U)";
                        string ToolTipDescription = "Underline the selected text.";

                        SplitButtonData splitButtonData = new SplitButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/LineColor_16x16.png", UriKind.Relative),
                            Command = EditingCommands.ToggleUnderline,
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            KeyTip = "3",
                        };

                        _dataCollection[Str] = splitButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData UnderlineGallery
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Underline Gallery";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryData<string> galleryData = new GalleryData<string>()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/LineColor_16x16.png", UriKind.Relative),
                            Command = EditingCommands.ToggleUnderline,
                        };

                        GalleryCategoryData<string> galleryCategoryData = new GalleryCategoryData<string>();
                        galleryCategoryData.GalleryItemDataCollection.Add("Underline");
                        galleryCategoryData.GalleryItemDataCollection.Add("Double underline");
                        galleryCategoryData.GalleryItemDataCollection.Add("Thick underline");
                        galleryCategoryData.GalleryItemDataCollection.Add("Dotted underline");
                        galleryCategoryData.GalleryItemDataCollection.Add("Dashed underline");
                        galleryCategoryData.GalleryItemDataCollection.Add("Dot-dash underline");
                        galleryCategoryData.GalleryItemDataCollection.Add("Dot-dot-dash underline");
                        galleryCategoryData.GalleryItemDataCollection.Add("Wave underline");
                        galleryData.CategoryDataCollection.Add(galleryCategoryData);

                        _dataCollection[Str] = galleryData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData MoreUnderlines
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_More Underlines";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Color_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Strikethrough
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Strikethrough";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Strikethrough";
                        string ToolTipDescription = "Draw a line through the middle of the selected text.";

                        ToggleButtonData toggleButtonData = new ToggleButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Erase_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "4",
                        };
                        _dataCollection[Str] = toggleButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Subscript
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Subscript";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Subscript (Ctrl+=)";
                        string ToolTipDescription = "Create small letters below the test baseline.";

                        ToggleButtonData toggleButtonData = new ToggleButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/FontScript_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.ToggleSubscript,
                            KeyTip = "5",
                        };
                        _dataCollection[Str] = toggleButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Superscript
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Superscript";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Superscript (Ctrl+Shift++)";
                        string ToolTipDescription = "Create small letters above the line of text. \n\n To create a footnote, click Insert Footnote on References tab.";

                        ToggleButtonData toggleButtonData = new ToggleButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/FontScript_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.ToggleSuperscript,
                            KeyTip = "6",
                        };
                        _dataCollection[Str] = toggleButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ChangeCase
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Change Case";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Change Case";
                        string ToolTipDescription = "Change all the selected text to UPPERCASE, lowercase or other common capitalizations.";

                        _dataCollection[Str] = new MenuButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/FontScript_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = HelpFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "7",
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SentenceCase
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Sentence case.";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Uppercase
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_UPPERCASE";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Lowercase
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_lowercase";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData CapitalizeEachWord
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Capitalize Each Word";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ToggleCase
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_tOGGLE cASE";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

       
        #endregion Font Group Model

        #region Paragraph Group Model

        public static ControlData Paragraph
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Paragraph";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GroupData Data = new GroupData(Str)
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Paragraph_16x16.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Paragraph_32x32.png", UriKind.Relative),
                            KeyTip = "ZP",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Bullets
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Bullets";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Bullets";
                        string ToolTipDescription = "Start a bulleted list.\n\nClick the arrow to choose different bullet styles.";

                        SplitButtonData splitButtonData = new SplitButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Bullets_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            IsCheckable = true,
                            Command = EditingCommands.ToggleBullets,
                            IsVerticallyResizable = true,
                            IsHorizontallyResizable = true,
                            KeyTip = "U",
                        };
                        _dataCollection[Str] = splitButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData BulletsGallery
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Bullets Gallery";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryData<Uri> galleryData = new GalleryData<Uri>()
                        {
                        };

                        GalleryCategoryData<Uri> recentlyUsedCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Recently Used Bullets"
                        };

                        galleryData.CategoryDataCollection.Add(recentlyUsedCategoryData);

                        GalleryCategoryData<Uri> bulletLibraryCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Bullet Library"
                        };
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/DownArrow_32x32.png", UriKind.Relative));
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/LeftArrow_32x32.png", UriKind.Relative));
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Minus_32x32.png", UriKind.Relative));
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Plus_32x32.png", UriKind.Relative));
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/RefreshArrow_32x32.png", UriKind.Relative));
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/RightArrow_32x32.png", UriKind.Relative));
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Tick_32x32.png", UriKind.Relative));
                        bulletLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/UpArrow_32x32.png", UriKind.Relative));

                        galleryData.CategoryDataCollection.Add(bulletLibraryCategoryData);

                        GalleryCategoryData<Uri> documentBulletsCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Document Bullets"
                        };

                        documentBulletsCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Tick_32x32.png", UriKind.Relative));

                        galleryData.CategoryDataCollection.Add(documentBulletsCategoryData);

                        Action<Uri> galleryCommandExecuted = delegate(Uri parameter)
                        {
                            if (!recentlyUsedCategoryData.GalleryItemDataCollection.Contains(parameter))
                            {
                                recentlyUsedCategoryData.GalleryItemDataCollection.Add(parameter);
                            }
                        };

                        Func<Uri, bool> galleryCommandCanExecute = delegate(Uri parameter)
                        {
                            return true;
                        };

                        galleryData.Command = new DelegateCommand<Uri>(galleryCommandExecuted, galleryCommandCanExecute);
                        _dataCollection[Str] = galleryData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ChangeListLevel
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Change List Level";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/MultiLevelList_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ChangeListLevelGallery
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "ChangeListLevelGallery";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryData<string> galleryData = new GalleryData<string>()
                        {
                        };

                        GalleryCategoryData<string> categoryData = new GalleryCategoryData<string>()
                        {
                        };

                        categoryData.GalleryItemDataCollection.Add("  > First");
                        categoryData.GalleryItemDataCollection.Add("    > Second");
                        categoryData.GalleryItemDataCollection.Add("      > Third");
                        categoryData.GalleryItemDataCollection.Add("        > Fourth");
                        categoryData.GalleryItemDataCollection.Add("          > Fifth");
                        categoryData.GalleryItemDataCollection.Add("            > Sixth");
                        categoryData.GalleryItemDataCollection.Add("              > Seventh");
                        categoryData.GalleryItemDataCollection.Add("                > Eighth");
                        categoryData.GalleryItemDataCollection.Add("                  > Ninth");

                        galleryData.CategoryDataCollection.Add(categoryData);

                        galleryData.Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute);
                        _dataCollection[Str] = galleryData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DefaultNewBullet
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Default New Bullet...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DefaultNewNumberFormat
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Default New Number Format...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DefaultNewMultilevelList
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Default New Multilevel List...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DefaultNewListStyle
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Default New _List Style...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Default_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Numbering
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Numbering";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Numbering";
                        string ToolTipDescription = "Start a numbered list.\n\nClick the arrow to choose different numbering formats.";

                        SplitButtonData splitButtonData = new SplitButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Numbering_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            IsCheckable = true,
                            Command = EditingCommands.ToggleNumbering,
                            IsVerticallyResizable = true,
                            IsHorizontallyResizable = true,
                            KeyTip = "N",
                        };
                        _dataCollection[Str] = splitButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData NumberingGallery
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Numbering Gallery";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryData<Uri> galleryData = new GalleryData<Uri>()
                        {
                        };

                        GalleryCategoryData<Uri> recentlyUsedCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Recently Used Number Formats"
                        };

                        galleryData.CategoryDataCollection.Add(recentlyUsedCategoryData);

                        GalleryCategoryData<Uri> numberingLibraryCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Numbering Library"
                        };
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/DownArrow_32x32.png", UriKind.Relative));
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/LeftArrow_32x32.png", UriKind.Relative));
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Minus_32x32.png", UriKind.Relative));
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Plus_32x32.png", UriKind.Relative));
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/RefreshArrow_32x32.png", UriKind.Relative));
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/RightArrow_32x32.png", UriKind.Relative));
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Tick_32x32.png", UriKind.Relative));
                        numberingLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/UpArrow_32x32.png", UriKind.Relative));

                        galleryData.CategoryDataCollection.Add(numberingLibraryCategoryData);

                        GalleryCategoryData<Uri> documentNumberFormatsCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Document Number Formats"
                        };

                        documentNumberFormatsCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Tick_32x32.png", UriKind.Relative));

                        galleryData.CategoryDataCollection.Add(documentNumberFormatsCategoryData);

                        Action<Uri> galleryCommandExecuted = delegate(Uri parameter)
                        {
                            if (!recentlyUsedCategoryData.GalleryItemDataCollection.Contains(parameter))
                            {
                                recentlyUsedCategoryData.GalleryItemDataCollection.Add(parameter);
                            }
                        };

                        Func<Uri, bool> galleryCommandCanExecute = delegate(Uri parameter)
                        {
                            return true;
                        };

                        galleryData.Command = new DelegateCommand<Uri>(galleryCommandExecuted, galleryCommandCanExecute);
                        _dataCollection[Str] = galleryData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData MultilevelList
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "MultilevelList";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Multilevel List";
                        string ToolTipDescription = "Start a multilevel list.\n\nClick the arrow to choose different multilevel list styles.";

                        MenuButtonData menuButtonData = new MenuButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/MultiLevelList_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            IsVerticallyResizable = true,
                            IsHorizontallyResizable = true,
                            KeyTip = "M",
                        };
                        _dataCollection[Str] = menuButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData MultilevelListGallery
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "MultilevelList Gallery";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryData<Uri> galleryData = new GalleryData<Uri>()
                        {
                            CanUserFilter = true,
                        };

                        GalleryCategoryData<Uri> currentListCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Current List"
                        };

                        galleryData.CategoryDataCollection.Add(currentListCategoryData);

                        GalleryCategoryData<Uri> listLibraryCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "List Library"
                        };
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/DownArrow_32x32.png", UriKind.Relative));
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/LeftArrow_32x32.png", UriKind.Relative));
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Minus_32x32.png", UriKind.Relative));
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Plus_32x32.png", UriKind.Relative));
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/RefreshArrow_32x32.png", UriKind.Relative));
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/RightArrow_32x32.png", UriKind.Relative));
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Tick_32x32.png", UriKind.Relative));
                        listLibraryCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/UpArrow_32x32.png", UriKind.Relative));

                        galleryData.CategoryDataCollection.Add(listLibraryCategoryData);

                        GalleryCategoryData<Uri> documentListsCategoryData = new GalleryCategoryData<Uri>()
                        {
                            Label = "Lists in Current Documents"
                        };

                        documentListsCategoryData.GalleryItemDataCollection.Add(new Uri("/GraphBuilder.Shell;component/Images/Tick_32x32.png", UriKind.Relative));

                        galleryData.CategoryDataCollection.Add(documentListsCategoryData);

                        Action<Uri> galleryCommandExecuted = delegate(Uri parameter)
                        {
                            if (!currentListCategoryData.GalleryItemDataCollection.Contains(parameter))
                            {
                                currentListCategoryData.GalleryItemDataCollection.Clear();
                                currentListCategoryData.GalleryItemDataCollection.Add(parameter);
                            }
                        };

                        Func<Uri, bool> galleryCommandCanExecute = delegate(Uri parameter)
                        {
                            return true;
                        };

                        galleryData.Command = new DelegateCommand<Uri>(galleryCommandExecuted, galleryCommandCanExecute);
                        _dataCollection[Str] = galleryData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DecreaseIndent
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "DecreaseIndent";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Decrease Indent";
                        string ToolTipDescription = "Decreases the indent level of the paragraph.";

                        ButtonData buttonData = new ButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/DecreaseIndent_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.DecreaseIndentation,
                            KeyTip = "AO",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData IncreaseIndent
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "IncreaseIndent";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Increase Indent";
                        string ToolTipDescription = "Increases the indent level of the paragraph.";

                        ButtonData buttonData = new ButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/IncreaseIndent_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.IncreaseIndentation,
                            KeyTip = "AI",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Sort
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Sort";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Sort";
                        string ToolTipDescription = "Alphabetize the selected text or sort numerical data.";
                        string ToolTipFooterTitle = "Press F1 for more help.";

                        ButtonData buttonData = new ButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Sort_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = ToolTipFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "SO",
                        };
                        _dataCollection[Str] = buttonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ShowHide
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "ShowHide";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Show/Hide (Ctrl + *)";
                        string ToolTipDescription = "Show paragraph marks and other hidden formatting symbols.";
                        string ToolTipFooterTitle = "Press F1 for more help.";

                        ToggleButtonData toggleButtonData = new ToggleButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/ShowHide_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = ToolTipFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "8",
                        };
                        _dataCollection[Str] = toggleButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData AlignTextLeft
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "AlignTextLeft";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Align Text Left (Ctrl + L)";
                        string ToolTipDescription = "Align text to the left.";

                        RadioButtonData radioButtonData = new RadioButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/AlignLeft_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.AlignLeft,
                            KeyTip = "AL",
                        };
                        _dataCollection[Str] = radioButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData AlignTextCenter
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "AlignTextCenter";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Center (Ctrl + E)";
                        string ToolTipDescription = "Center text.";

                        RadioButtonData radioButtonData = new RadioButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/AlignCenter_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.AlignCenter,
                            KeyTip = "AC",
                        };
                        _dataCollection[Str] = radioButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData AlignTextRight
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "AlignTextRight";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Align Text Left (Ctrl + L)";
                        string ToolTipDescription = "Align text to the right.";

                        RadioButtonData radioButtonData = new RadioButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/AlignRight_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.AlignRight,
                            KeyTip = "AR",
                        };
                        _dataCollection[Str] = radioButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Justify
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Justify";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Justify (Ctrl + J)";
                        string ToolTipDescription = "Aligns text to both left and right margins, adding extra space between words as necessary.\n\n";
                        ToolTipDescription += "This creates a clean along left and right side of the page.";

                        RadioButtonData radioButtonData = new RadioButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Justify_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = EditingCommands.AlignJustify,
                            KeyTip = "AJ",
                        };
                        _dataCollection[Str] = radioButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LineSpacing
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "LineSpacing";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Line Spacing";
                        string ToolTipDescription = "Change the spacing between line of text.\n\n";
                        ToolTipDescription += "You can also customize the amount of space added before and after the paragraphs.";

                        MenuButtonData menuButtonData = new MenuButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/LineSpacing_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "K",
                        };
                        _dataCollection[Str] = menuButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #region LineSpacing Model

        private static void SetIsCheckedOfLineSpacingMenuItem(MenuItemData menuItemData, object parameter)
        {
            menuItemData.IsChecked = (menuItemData == parameter);
        }

        private static void LineSpacingMenuItemDefaultExecute(MenuItemData menuItemData)
        {
            SetIsCheckedOfLineSpacingMenuItem((MenuItemData)LineSpacingFirstValue, menuItemData);
            SetIsCheckedOfLineSpacingMenuItem((MenuItemData)LineSpacingSecondValue, menuItemData);
            SetIsCheckedOfLineSpacingMenuItem((MenuItemData)LineSpacingThirdValue, menuItemData);
            SetIsCheckedOfLineSpacingMenuItem((MenuItemData)LineSpacingFourthValue, menuItemData);
            SetIsCheckedOfLineSpacingMenuItem((MenuItemData)LineSpacingFifthValue, menuItemData);
            SetIsCheckedOfLineSpacingMenuItem((MenuItemData)LineSpacingSixthValue, menuItemData);
        }

        private static bool LineSpacingMenuItemDefaultCanExecute(MenuItemData menuItemData)
        {
            return true;
        }

        public static ControlData LineSpacingFirstValue
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "LineSpacingFirstValue";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = "1.0",
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(LineSpacingMenuItemDefaultExecute, LineSpacingMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LineSpacingSecondValue
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "LineSpacingSecondValue";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = "1.15",
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(LineSpacingMenuItemDefaultExecute, LineSpacingMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LineSpacingThirdValue
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "LineSpacingThirdValue";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = "1.5",
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(LineSpacingMenuItemDefaultExecute, LineSpacingMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LineSpacingFourthValue
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "LineSpacingFourthValue";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = "2.0",
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(LineSpacingMenuItemDefaultExecute, LineSpacingMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LineSpacingFifthValue
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "LineSpacingFifthValue";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = "2.5",
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(LineSpacingMenuItemDefaultExecute, LineSpacingMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LineSpacingSixthValue
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "LineSpacingSixthValue";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = "3.0",
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(LineSpacingMenuItemDefaultExecute, LineSpacingMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LineSpacingOptions
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Line Spacing Options...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData AddSpaceBeforeParagraph
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Add Space _Before Paragraph";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/UpArrow_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData RemoveSpaceAfterParagraph
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Remove Space _After Paragraph";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/DownArrow_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #endregion

        public static ControlData Shading
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Shading";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Shading";
                        string ToolTipDescription = "Color the background behind selected text or paragraph";
                        string ToolTipFooterTitle = "Press F1 for more help.";

                        SplitButtonData splitButtonData = new SplitButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Shading_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = ToolTipFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "H",
                        };
                        _dataCollection[Str] = splitButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Borders
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Borders";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Borders";

                        SplitButtonData splitButtonData = new SplitButtonData()
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Borders_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            IsCheckable = true,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "B",
                        };
                        _dataCollection[Str] = splitButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #region Borders Model

        private static void BorderMenuItemDefaultExecute(MenuItemData menuItemData)
        {
            MenuItemData bottomBorder = (MenuItemData)BottomBorder;
            MenuItemData topBorder = (MenuItemData)TopBorder;
            MenuItemData leftBorder = (MenuItemData)LeftBorder;
            MenuItemData rightBorder = (MenuItemData)RightBorder;
            MenuItemData noBorder = (MenuItemData)NoBorder;
            MenuItemData allBorders = (MenuItemData)AllBorders;
            MenuItemData outsideBorders = (MenuItemData)OutsideBorders;
            MenuItemData insideBorders = (MenuItemData)InsideBorders;
            MenuItemData insideHorizontalBorder = (MenuItemData)InsideHorizontalBorder;
            MenuItemData insideVerticalBorder = (MenuItemData)InsideVerticalBorder;

            if (menuItemData == bottomBorder ||
                menuItemData == topBorder ||
                menuItemData == leftBorder ||
                menuItemData == rightBorder)
            {
                outsideBorders.IsChecked = (bottomBorder.IsChecked &&
                    topBorder.IsChecked &&
                    leftBorder.IsChecked &&
                    rightBorder.IsChecked);
            }

            if (menuItemData == outsideBorders)
            {
                bottomBorder.IsChecked = topBorder.IsChecked = leftBorder.IsChecked = rightBorder.IsChecked = outsideBorders.IsChecked;
            }

            if (menuItemData == insideHorizontalBorder ||
                menuItemData == insideVerticalBorder)
            {
                insideBorders.IsChecked = (insideHorizontalBorder.IsChecked &&
                    insideVerticalBorder.IsChecked);
            }

            if (menuItemData == insideBorders)
            {
                insideHorizontalBorder.IsChecked = insideVerticalBorder.IsChecked = insideBorders.IsChecked;
            }

            if (menuItemData == noBorder)
            {
                bottomBorder.IsChecked = false;
                topBorder.IsChecked = false;
                leftBorder.IsChecked = false;
                rightBorder.IsChecked = false;
                outsideBorders.IsChecked = false;
                insideBorders.IsChecked = false;
                insideHorizontalBorder.IsChecked = false;
                insideVerticalBorder.IsChecked = false;
            }

            if (menuItemData == allBorders)
            {
                bottomBorder.IsChecked = true;
                topBorder.IsChecked = true;
                leftBorder.IsChecked = true;
                rightBorder.IsChecked = true;
                outsideBorders.IsChecked = true;
                insideBorders.IsChecked = true;
                insideHorizontalBorder.IsChecked = true;
                insideVerticalBorder.IsChecked = true;
            }
        }

        private static bool BordersMenuItemDefaultCanExecute(MenuItemData menuItemData)
        {
            return true;
        }

        public static ControlData BottomBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Bottom Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/BottomBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData TopBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "To_p Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/TopBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LeftBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Left Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/LeftBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData RightBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Right Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/RightBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData NoBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_No Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/NoBorder_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData AllBorders
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_All Borders";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Borders_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData InsideBorders
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Inside Borders";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/InsideBorders_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData OutsideBorders
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Out_side Borders";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/OuterBorders_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData InsideHorizontalBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Inside _Horizontal Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/InsideHorizontalBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData InsideVerticalBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Inside _Vertical Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/InsideVerticalBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DiagonalDownBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Diagonal Do_wn Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/DiagonalDownBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DiagonalUpBorder
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Diagonal _Up Border";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/DiagonalUpBorder_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand<MenuItemData>(BorderMenuItemDefaultExecute, BordersMenuItemDefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData HorizontalLine
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Hori_zontal Line";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/InsideHorizontalBorder_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData DrawTable
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Draw Table";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/DrawTable_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ViewGridLines
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "View _Gridlines";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/ShowGridlines_16x16.png", UriKind.Relative),
                            IsCheckable = true,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData BordersAndShading
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "B_orders And Shading";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        _dataCollection[Str] = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/BordersAndShading_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #endregion

        #endregion Paragraph Group Model

        #region Styles Group Model

        public static ControlData Styles
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Styles";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GroupData Data = new GroupData(Str)
                        {
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/StylesGroup.png", UriKind.Relative),
                            KeyTip = "ZS",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }


        public static ControlData ChangeStyles
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Change Styles";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Change Styles";
                        string ToolTipDescription = "Change the set of styles, colors, fonts, and paragraph spacing used in this document.";

                        MenuButtonData menuButtonData = new MenuButtonData()
                        {
                            Label = Str,
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Styles_32x32.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            KeyTip = "G",
                        };
                        _dataCollection[Str] = menuButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData StylesSet
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Style Set";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Forecolor_16x16.png", UriKind.Relative),
                            IsVerticallyResizable = true,
                            KeyTip = "Y",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Colors
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Colors";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/ChooseColor_16x16.png", UriKind.Relative),
                            IsVerticallyResizable = true,
                            KeyTip = "C",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Fonts
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Fonts";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Font_16x16.png", UriKind.Relative),
                            IsVerticallyResizable = true,
                            KeyTip = "F",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData ParagraphSpacing
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Paragraph Spacing";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/ParagraphSpacing_16x16.png", UriKind.Relative),
                            KeyTip = "P",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SetAsDefault
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Set as Default";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = Str;
                        string ToolTipDescription = "Set the cuurent style set and theme as the default when you create a new document.";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "S",
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static GalleryData<StylesSet> StylesSetGalleryData
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "StylesSetGalleryData";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        // TODO: replace string with an object (IsChecked, StyleName). Define DataTemplate
                        GalleryData<StylesSet> stylesData = new GalleryData<StylesSet>();
                        GalleryCategoryData<StylesSet> singleCategory = new GalleryCategoryData<StylesSet>();

                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Default (Black and White)", IsSelected = true });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Distinctive" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Elegant" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Fancy" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Formal" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Manuscript" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Modern" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Newsprint" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Perspective" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Simple" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Thatch" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Traditional" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Word 2003" });
                        singleCategory.GalleryItemDataCollection.Add(new StylesSet() { Label = "Word 2010" });

                        stylesData.CategoryDataCollection.Clear();
                        stylesData.CategoryDataCollection.Add(singleCategory);
                        _dataCollection[Str] = stylesData;
                    }

                    return _dataCollection[Str] as GalleryData<StylesSet>;
                }
            }
        }

        public static GalleryData<string> StylesColorsGalleryData
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "StylesColorsGalleryData";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        // TODO: replace string with an object (Color Palette, StyleName). Define DataTemplate
                        GalleryData<string> stylesData = new GalleryData<string>();
                        GalleryCategoryData<string> singleCategory = new GalleryCategoryData<string>();

                        singleCategory.Label = "Built-In";
                        singleCategory.GalleryItemDataCollection.Add("Office");
                        singleCategory.GalleryItemDataCollection.Add("Grayscale");
                        singleCategory.GalleryItemDataCollection.Add("Adjacency");
                        singleCategory.GalleryItemDataCollection.Add("Angles");
                        singleCategory.GalleryItemDataCollection.Add("Apex");
                        singleCategory.GalleryItemDataCollection.Add("Apothecary");
                        singleCategory.GalleryItemDataCollection.Add("Aspect");
                        singleCategory.GalleryItemDataCollection.Add("Austin");
                        singleCategory.GalleryItemDataCollection.Add("Black Tie");
                        singleCategory.GalleryItemDataCollection.Add("Civic");
                        singleCategory.GalleryItemDataCollection.Add("Clarity");
                        singleCategory.GalleryItemDataCollection.Add("Composite");
                        singleCategory.GalleryItemDataCollection.Add("Concourse");
                        singleCategory.GalleryItemDataCollection.Add("Couture");
                        singleCategory.GalleryItemDataCollection.Add("Elemental");
                        singleCategory.GalleryItemDataCollection.Add("Equity");
                        singleCategory.GalleryItemDataCollection.Add("Essential");
                        singleCategory.GalleryItemDataCollection.Add("Executive");
                        singleCategory.GalleryItemDataCollection.Add("Flow");
                        singleCategory.GalleryItemDataCollection.Add("Foundry");
                        singleCategory.GalleryItemDataCollection.Add("Grid");
                        singleCategory.GalleryItemDataCollection.Add("Horizon");
                        singleCategory.GalleryItemDataCollection.Add("Median");
                        singleCategory.GalleryItemDataCollection.Add("Newsprint");
                        singleCategory.GalleryItemDataCollection.Add("Perspective");
                        singleCategory.GalleryItemDataCollection.Add("Solstice");
                        singleCategory.GalleryItemDataCollection.Add("Technic");
                        singleCategory.GalleryItemDataCollection.Add("Urban");
                        singleCategory.GalleryItemDataCollection.Add("Verve");
                        singleCategory.GalleryItemDataCollection.Add("Waveform");

                        stylesData.CategoryDataCollection.Add(singleCategory);
                        _dataCollection[Str] = stylesData;
                    }

                    return _dataCollection[Str] as GalleryData<string>;
                }
            }
        }

        public static GalleryData<ThemeFonts> StylesFontsGalleryData
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "StylesFontsGalleryData";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryData<ThemeFonts> stylesData = new GalleryData<ThemeFonts>();
                        GalleryCategoryData<ThemeFonts> singleCategory = new GalleryCategoryData<ThemeFonts>();

                        singleCategory.Label = "Built-In";
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office 2", Field2 = "Calibri", Field3 = "Cambria", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office Classic", Field2 = "Arial", Field3 = "Times New Roman", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office Classic 2", Field2 = "Arial", Field3 = "Arial", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Adjacency", Field2 = "Franklin Gothic", Field3 = "Franklin Gothic Book", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Angles", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Apex", Field2 = "Lucida Sans", Field3 = "Book Antiqua", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Apothecary", Field2 = "Book Antiqua", Field3 = "Century Gothic", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Aspect", Field2 = "Verdana", Field3 = "Verdana", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Austin", Field2 = "Century Gothic", Field3 = "Century Gothic", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Black Tie", Field2 = "Garamond", Field3 = "Garamond", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Civic", Field2 = "Georgia", Field3 = "Georgia", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });
                        singleCategory.GalleryItemDataCollection.Add(new ThemeFonts() { Label = "Office", Field2 = "Cambria", Field3 = "Calibri", Field1 = "/GraphBuilder.Shell;component/Images/ThemeFonts.png" });

                        stylesData.CategoryDataCollection.Add(singleCategory);
                        _dataCollection[Str] = stylesData;
                    }

                    return _dataCollection[Str] as GalleryData<ThemeFonts>;
                }
            }
        }

        public static GalleryData<string> StylesParagraphGalleryData
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "StylesParagraphGalleryData";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GalleryData<string> stylesData = new GalleryData<string>();
                        GalleryCategoryData<string> firstCategory = new GalleryCategoryData<string>();
                        firstCategory.Label = "Style Set";
                        firstCategory.GalleryItemDataCollection.Add("Traditional");

                        GalleryCategoryData<string> secondCategory = new GalleryCategoryData<string>();
                        secondCategory.Label = "Built-In";
                        secondCategory.GalleryItemDataCollection.Add("No Paragraph Space");
                        secondCategory.GalleryItemDataCollection.Add("Compact");
                        secondCategory.GalleryItemDataCollection.Add("Tight");
                        secondCategory.GalleryItemDataCollection.Add("Open");
                        secondCategory.GalleryItemDataCollection.Add("Relaxed");
                        secondCategory.GalleryItemDataCollection.Add("Double");

                        stylesData.CategoryDataCollection.Add(firstCategory);
                        stylesData.CategoryDataCollection.Add(secondCategory);
                        _dataCollection[Str] = stylesData;
                    }

                    return _dataCollection[Str] as GalleryData<string>;
                }
            }
        }

        #endregion

        #region Editing model

        public static ControlData Editing
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Editing";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        GroupData Data = new GroupData(Str)
                        {
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Find_16x16.png", UriKind.Relative),
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Find_32x32.png", UriKind.Relative),
                            KeyTip = "ZN",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Find
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Find";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Find (Ctrl+F)";
                        string ToolTipDescription = "Find text or other content in the document.";
                        string DropDownToolTipDescription = "Find and select specific text, formatting or other type of information within the document.";
                        string DropDownToolTipFooter = "You can also replace the information with new text or formatting.";

                        SplitButtonData FindSplitButtonData = new SplitButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Find_16x16.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "FD",
                        };
                        FindSplitButtonData.DropDownButtonData.ToolTipTitle = TooTipTitle;
                        FindSplitButtonData.DropDownButtonData.ToolTipDescription = DropDownToolTipDescription;
                        FindSplitButtonData.DropDownButtonData.ToolTipFooterDescription = DropDownToolTipFooter;
                        FindSplitButtonData.DropDownButtonData.Command = new DelegateCommand(delegate { });
                        _dataCollection[Str] = FindSplitButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData FindMenuItem
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Find";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Find (Ctrl+F)";
                        string ToolTipDescription = "Find text or other content in the document.";

                        MenuItemData FindMenuItemData = new MenuItemData()
                        {
                            Label = Str,
                            ToolTipTitle = TooTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Find_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "F",
                        };
                        _dataCollection[Str] = FindMenuItemData;
                    }

                    return _dataCollection[Str];
                }
            }

        }

        public static ControlData AdvancedFind
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Advanced Find...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Advanced Find";
                        string ToolTipDescription = "Find text in the document.";

                        MenuItemData FindMenuItemData = new MenuItemData()
                        {
                            Label = Str,
                            ToolTipTitle = TooTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Find_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "A",
                        };
                        _dataCollection[Str] = FindMenuItemData;
                    }

                    return _dataCollection[Str];
                }
            }

        }

        public static ControlData GoTo
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Go To...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Go To (Ctrl+G)";
                        string ToolTipDescription = "Navigate to a specific place in the document.\n\r\n\r" +
                        "Depending on the type of the document, you can navigate to a specific page number, line number, footnote, table, comment, or other object.";
                        string ToolTipFooterTitle = "Press F1 for more help.";
                        MenuItemData MenuItemData = new MenuItemData()
                        {
                            Label = Str,
                            ToolTipTitle = TooTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = ToolTipFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/GoTo_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "G",
                        };
                        _dataCollection[Str] = MenuItemData;
                    }

                    return _dataCollection[Str];
                }
            }

        }

        public static ControlData Replace
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Replace";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ReplaceToolTipTitle = "Replace (Ctrl+H)";
                        string ReplaceToolTipDescription = "Replace text in the document.";

                        ButtonData ReplaceButtonData = new ButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Replace_16x16.png", UriKind.Relative),
                            ToolTipTitle = ReplaceToolTipTitle,
                            ToolTipDescription = ReplaceToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "R",
                        };
                        _dataCollection[Str] = ReplaceButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData Select
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Select";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Select";
                        string ToolTipDescription = "Select text or objects in the document.\n\r\n\r" +
                        "Use Select Object to allow you to select objects that had been positioned behind the text.";

                        MenuButtonData SelectMenuButtonData = new MenuButtonData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Select_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "SL",
                        };
                        _dataCollection[Str] = SelectMenuButtonData;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SelectAll
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Select _All";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Select All (Ctrl+A)";
                        string ToolTipDescription = "Select all items";

                        MenuItemData Data = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Select_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "A",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SelectObjects
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Select _Objects";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Select Objects";
                        string ToolTipDescription = "Select rectangular regions of ink strokes, shapes and text";

                        MenuItemData Data = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/Select_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "O",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SelectAllText
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "_Select All Text With Similar Formatting (No Data)";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        MenuItemData Data = new MenuItemData()
                        {
                            Label = Str,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "S",
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData SelectionPane
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Selection _Pane...";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = "Selection Pane";
                        string ToolTipDescription = "Show the Selection Pane to help select individual objects and to change their order and visibility.";

                        MenuItemData Data = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/GraphBuilder.Shell;component/Images/SelectionPane_16x16.png", UriKind.Relative),
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            KeyTip = "P",
                            IsCheckable = true,
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        #endregion

        #region Insert Table Group Model

        public static ControlData Table
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Таблица";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string ToolTipTitle = Str;
                        string ToolTipDescription = "Показать таблицу документа";

                        _dataCollection[Str] = new MenuButtonData()
                        {
                            LargeImage = new Uri("/GraphBuilder.Shell;component/Images/Table_32x32.png", UriKind.Relative),
                            Label = Str,
                            ToolTipTitle = ToolTipTitle,
                            ToolTipDescription = ToolTipDescription,
                            ToolTipFooterTitle = HelpFooterTitle,
                            ToolTipFooterImage = new Uri("/GraphBuilder.Shell;component/Images/Help_16x16.png", UriKind.Relative),
                            Command = new DelegateCommand(OnShowTable),
                            KeyTip = "T",
                        };
                    }

                    return _dataCollection[Str];
                }
            }
        }
       

        public static TabData DesignTab
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Стиль";

                    if (!_miscData.ContainsKey(Str))
                    {
                        TabData designTabData = new TabData() { Header = Str, ContextualTabGroupHeader = "Table Tools" };
                        _miscData[Str] = designTabData;
                    }

                    return _miscData[Str] as TabData;
                }
            }
        }

        public static TabData LayoutTab
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Анализ";

                    if (!_miscData.ContainsKey(Str))
                    {
                        TabData layoutTabData = new TabData() { Header = Str, ContextualTabGroupHeader = "Table Tools" };
                        _miscData[Str] = layoutTabData;
                    }

                    return _miscData[Str] as TabData;
                }
            }
        }

        public static ContextualTabGroupData TableTabGroup
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Table Tools";

                    if (!_miscData.ContainsKey(Str))
                    {
                        ContextualTabGroupData tableData = new ContextualTabGroupData() { Header = Str };
                        tableData.TabDataCollection.Add(DesignTab);
                        tableData.TabDataCollection.Add(LayoutTab);

                        _miscData[Str] = tableData;
                    }

                    return _miscData[Str] as ContextualTabGroupData;
                }
            }
        }
        
        public static ControlData HeaderRow
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Header Row";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        CheckBoxData Data = new CheckBoxData()
                        {
                            Label = Str,
                            ToolTipTitle = Str,
                            ToolTipDescription = "Display special formatting for the first row of the table.",
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            IsChecked = true,
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData FirstColumn
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "First Column";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        CheckBoxData Data = new CheckBoxData()
                        {
                            Label = Str,
                            ToolTipTitle = Str,
                            ToolTipDescription = "Display special formatting for the first column of the table.",
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            IsChecked = true,
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData TotalRow
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Total Row";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        CheckBoxData Data = new CheckBoxData()
                        {
                            Label = Str,
                            ToolTipTitle = Str,
                            ToolTipDescription = "Display special formatting for the last row of the table.",
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData LastColumn
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Last Column";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        CheckBoxData Data = new CheckBoxData()
                        {
                            Label = Str,
                            ToolTipTitle = Str,
                            ToolTipDescription = "Display special formatting for the last column of the table.",
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData BandedRows
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Banded Rows";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        CheckBoxData Data = new CheckBoxData()
                        {
                            Label = Str,
                            ToolTipTitle = Str,
                            ToolTipDescription = "Display banded rows.",
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                            IsChecked = true,
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }

        public static ControlData BandedColumns
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Banded Columns";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        CheckBoxData Data = new CheckBoxData()
                        {
                            Label = Str,
                            ToolTipTitle = Str,
                            ToolTipDescription = "Display banded columns.",
                            Command = new DelegateCommand(DefaultExecuted, DefaultCanExecute),
                        };
                        _dataCollection[Str] = Data;
                    }

                    return _dataCollection[Str];
                }
            }
        }
        #endregion

        static RibbonMenuModel()
        {
            OnOpenStartPage();
            //OnNewDocument();
        }

        #region Commands

        public static void OnOpenStartPage()
        {
            //StartPageViewModel document = new StartPageViewModel();
            //Bootstrap.Current.MainViewModel.Documents.Add(document);
        }

        public static void OnNewDocument()
        {
            Document document = new Document();
            document.DocumentMode = DocumentMode.Auto;
            Bootstrap.Current.CreateDocument(document);
        }
        
        public static void OnImportWindowOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.*";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "Все файлы (*.*)|*.*|XML файлы (*.xml)|*.xml|CSV файлы (*.csv)|*.csv|Текстовые файлы (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Encoding encoding = Encoding.GetEncoding("windows-1251");
                List<string> lines = AppFile.ReadTextFile(ofd.FileName, 100, encoding);

                if (lines.Count > 0)
                {
                    WizardViewModel wizardViewModel = new WizardViewModel();
                    new ImportViewModel(wizardViewModel, lines);
                    wizardViewModel.WizardWindow.ShowDialog();
                }
            }
        }

        public static void OnSaveDocument()
        {
            //DocumentViewModel documentModel = (DocumentViewModel)Bootstrap.Current.MainViewModel.ActiveContent;
            //Document document = ((CanvasViewModel)documentModel.ContentControl.DataContext).Document;
            //string filename = documentModel.Title;
            //using (SaveFileDialog dlg = new SaveFileDialog())
            //{
            //    dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //    dlg.RestoreDirectory = true;
            //    dlg.FileName = filename;
            //    dlg.DefaultExt = ".xml";
            //    dlg.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            //    DialogResult result = dlg.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
                    
            //        //document.Nodes = documentModel.VirtualCanvas.Nodes;
            //        //document.Links = documentModel.VirtualCanvas.Links;
            //        //Document document = new Document();
            //        filename = dlg.FileName;
            //        string text = document.SerializeToXml();

            //        XmlDocument xdoc = new XmlDocument();
            //        xdoc.LoadXml(text);
            //        xdoc.Save(filename);
            //    }
            //}
        }

        public static void OnShowTable()
        {
            //Bootstrap.Current.MainViewModel.ActiveContent.
        }

        #endregion

        private static void DefaultExecuted()
        {
        }

        private static bool DefaultCanExecute()
        {
            return true;
        }

        private static RibbonUserControl WordControl
        {
            get
            {
                if (System.Windows.Application.Current.Properties.Contains("RibbonUserControlRef"))
                {
                    WeakReference wordControlRef = System.Windows.Application.Current.Properties["RibbonUserControlRef"] as WeakReference;
                    if (wordControlRef != null)
                    {
                        return wordControlRef.Target as RibbonUserControl;
                    }
                }
                return null;
            }

        }

        #region Data

        private const string HelpFooterTitle = "Нажмите F1 для вызова справки.";
        private static object _lockObject = new object();
        private static Dictionary<string, ControlData> _dataCollection = new Dictionary<string, ControlData>();

        // Store any data that doesnt inherit from ControlData
        private static Dictionary<string, object> _miscData = new Dictionary<string, object>();

        #endregion Data

    }
}
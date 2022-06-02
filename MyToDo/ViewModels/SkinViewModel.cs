using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Service;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyToDo.ViewModels
{
    public class SkinViewModel : BindableBase
    {
        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value;RaisePropertyChanged(); }
        }

        private Color? _selectedColor;
        public Color? SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    RaisePropertyChanged();

                    if (value is Color color)
                    {
                        ChangeHue(color);
                    }
                }
            }
        }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public DelegateCommand<object> ChangeHueCommand { get; private set; }

        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private readonly IAccountService service;
        private Color? _primaryColor;
        public SkinViewModel(IAccountService service)
        {
            this.service = service;
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
            ITheme theme = paletteHelper.GetTheme();

            _primaryColor = theme.PrimaryMid.Color;

            SelectedColor = _primaryColor;
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme(); 
            modificationAction?.Invoke(theme); 
            paletteHelper.SetTheme(theme);
        }

        private async void ChangeHue(object obj)
        {
            var hue = (Color)obj;
            var themeColor = hue.ToString();
            ITheme theme = paletteHelper.GetTheme();
            theme.PrimaryLight = new ColorPair(hue.Lighten());
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue.Darken());
            paletteHelper.SetTheme(theme);
            var currentUser = AppSession.CurrentUser;
            currentUser.ThemeColor = themeColor;
            await service.UpdateThemeColor(currentUser.Id,themeColor);
        }

    }
}

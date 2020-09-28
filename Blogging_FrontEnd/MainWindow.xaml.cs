using Blogging_Interactions;
using CodeFirst_APP.Blogging.ModelExtensions;
using CodeFirst_APP.Blogging.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blogging_FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        BloggingHelper _bHelper;
        BlogExtended _currentBlog;
        ICollection<BlogExtended> _blogs;
        public BlogExtended CurrentBlog 
        { 
            get => _currentBlog;
            private set
            {
                _currentBlog = value;
                OnPropertyChanged();
            }
        }
        public ICollection<BlogExtended> Blogs 
        {
            get => _blogs; 
            private set
            {
                _blogs = value;
                OnPropertyChanged();
            } 
        }
        public MainWindow()
        {
            _bHelper = new BloggingHelper();
            Blogs = _bHelper.GetAllExtendedBlogsAsList();
            DataContext = this;
            InitializeComponent();
        }
        private void lstBlogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentBlog = (BlogExtended)((ListView)sender).SelectedItem ?? CurrentBlog;
            NavPanel.Navigate(new Blog_Posts(CurrentBlog));
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentBlog != null)
            {
                ToggleEditMode();
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentBlog != null)
            {
                ToggleEditMode();
                _bHelper.SaveBlog(CurrentBlog);
                Blogs = _bHelper.GetAllExtendedBlogsAsList();
            }
        }
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentBlog != null)
            {
                ToggleEditMode();
                _bHelper.RevertBlog(CurrentBlog);
                Blogs = _bHelper.GetAllExtendedBlogsAsList();
                CurrentBlog = Blogs.Where(x => x.BlogId == CurrentBlog.BlogId).FirstOrDefault();
                NavPanel.Navigate(new Blog_Posts(CurrentBlog));
            }
        }
        private void lstBlogs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((BlogExtended)((ListView)sender).SelectedItem).Name != "Add new")
            {
                if (MessageBox.Show("Are you sure you want to delete this blog?" +
                    "\nAll attached posts will also be deleted", "Delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _bHelper = new BloggingHelper();
                    _bHelper.DeleteBlog((BlogExtended)((ListView)sender).SelectedItem);
                    CurrentBlog = null;
                    Blogs = _bHelper.GetAllExtendedBlogsAsList();
                }
            }
        }

        void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void ToggleEditMode()
        {
            CurrentBlog.IsInEditMode = !CurrentBlog.IsInEditMode;
            tbName.IsEnabled = !tbName.IsEnabled;
            btnEdit.IsEnabled = !btnEdit.IsEnabled;
            btnSave.IsEnabled = !btnSave.IsEnabled;
            btnUndo.IsEnabled = !btnUndo.IsEnabled;
            lstBlogs.IsEnabled = !lstBlogs.IsEnabled;
            ((Blog_Posts)NavPanel.Content).tbContent.IsEnabled = !((Blog_Posts)NavPanel.Content).tbContent.IsEnabled;
            ((Blog_Posts)NavPanel.Content).tbTitle.IsEnabled = !((Blog_Posts)NavPanel.Content).tbTitle.IsEnabled;
        }

    }
}

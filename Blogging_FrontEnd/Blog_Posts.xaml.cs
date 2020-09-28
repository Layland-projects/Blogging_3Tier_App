using CodeFirst_APP.Blogging.Models;
using CodeFirst_APP.Blogging.ModelExtensions;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Blogging_Interactions;

namespace Blogging_FrontEnd
{
    /// <summary>
    /// Interaction logic for Blog_Pages.xaml
    /// </summary>
    public partial class Blog_Posts : Page, INotifyPropertyChanged
    {
        Post _currentPost;
        BlogExtended _currentBlog;
        PostHelper _pHelper;
        ICollection<Post> _posts;

        public ICollection<Post> Posts
        {
            get => _posts;
            private set
            {
                _posts = value;
                OnPropertyChanged();
            }
        }
        public BlogExtended CurrentBlog 
        {
            get => _currentBlog;
            private set
            {
                _currentBlog = value;
                OnPropertyChanged();
            }
        }
        public Post CurrentPost 
        { 
            get
            {
                return _currentPost;
            }
            private set 
            {
                _currentPost = value;
                OnPropertyChanged();
            }
        }

        public Blog_Posts()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Blog_Posts(BlogExtended blog)
        {
            CurrentBlog = blog;
            if (CurrentBlog != null)
            {
                Posts = CurrentBlog.Posts;
                if (CurrentBlog.IsInEditMode)
                {
                    tbTitle.IsEnabled = true;
                    tbContent.IsEnabled = true;
                }
            }
            InitializeComponent();
            DataContext = this;
        }

        private void lstPosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentPost = (Post)((ListView)sender).SelectedItem ?? CurrentPost;
        }

        void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void lstPosts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((Post)((ListView)sender).SelectedItem).Title != "Add new")
            {
                if (MessageBox.Show("Delete this post?", "Delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _pHelper = new PostHelper();
                    CurrentBlog = _pHelper.DeletePost(CurrentBlog, (Post)((ListView)sender).SelectedItem);
                    CurrentPost = null;
                    Posts = _pHelper.GetAllPostsForBlogAsList(CurrentBlog);
                }
            }
        }
    }
}

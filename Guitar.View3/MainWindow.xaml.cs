using System;
using System.Collections.Generic;
using System.Linq;
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
using Guitar.Model3;

namespace Guitar.View3 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Guitar.Model3.GuitarDBEntities guitarEnts = new GuitarDBEntities ();
        bool isLoading = true;

        public MainWindow () {
        }

        private void Window_Loaded_1 (object sender, RoutedEventArgs e) {
            isLoading = false;            
            SetAuthorsListDataContext ();
            this.Topmost = false;
        }

        #region Author

        private void authorsList_SelectionChanged (object sender, SelectionChangedEventArgs e) {
            SetSongsLinksDataContext ();
        }
        void SetAuthorsListDataContext () {
            if (isLoading) {
                return;
            }
            try {
                guitarEnts = new GuitarDBEntities ();
                authorsList.DataContext = null;
                if (foreignCheck.IsChecked == true) {
                    if (domesticCheck.IsChecked == true)
                        authorsList.DataContext = GuitarMain.GetAuthors ();
                    else
                        authorsList.DataContext = GuitarMain.GetAuthors (genreEnum.Strano ());
                }
                else {
                    if (domesticCheck.IsChecked == true)
                        authorsList.DataContext = GuitarMain.GetAuthors (genreEnum.Domace ());
                    else
                        authorsList.DataContext = null;
                }
            }
            catch {
                MessageBox.Show ("Database error");
            }
        }

        private void addAuthor_Click (object sender, RoutedEventArgs e) {
            if ((foreignCheck.IsChecked == true && domesticCheck.IsChecked == true) ||
                (foreignCheck.IsChecked == false && domesticCheck.IsChecked == false)) {
                MessageBox.Show ("Odaberi samo jedan zanr");
                return;
            }

            InputWindow newAuthorWin = new InputWindow ("Unesi ime autora");
            newAuthorWin.Owner = this;
            newAuthorWin.ShowDialog ();
            if (newAuthorWin.accepted == true) {
                Author newAuthor = new Author ();
                newAuthor.authorName = newAuthorWin.inputTextbox.Text;
                if (foreignCheck.IsChecked == true)
                    newAuthor.authorGenre = genreEnum.Strano ();
                else
                    newAuthor.authorGenre = genreEnum.Domace ();
                GuitarMain.SaveAuthor (newAuthor);
            }
            SetAuthorsListDataContext ();
        }
        private void removeAuthor_Click (object sender, RoutedEventArgs e) {
            if (authorsList.SelectedItem == null) {
                MessageBox.Show ("Odaberi jednog autora");
                return;
            }
            Author selectedAuthor = authorsList.SelectedItem as Author;
            GuitarMain.DeleteAuthor (selectedAuthor);
            SetAuthorsListDataContext ();
        }

        #region genreCheck

        private void foreignCheck_Checked (object sender, RoutedEventArgs e) {
            SetAuthorsListDataContext ();
        }
        private void domesticCheck_Checked (object sender, RoutedEventArgs e) {
            SetAuthorsListDataContext ();
        }
        private void foreignCheck_Unchecked (object sender, RoutedEventArgs e) {
            SetAuthorsListDataContext ();
        }
        private void domesticCheck_Unchecked (object sender, RoutedEventArgs e) {
            SetAuthorsListDataContext ();
        }


        #endregion

        #endregion

        #region Song

        private void songsList_SelectionChanged (object sender, SelectionChangedEventArgs e) {
            SetSongsLinksDataContext ();
        }
        private void openYoutube_Click (object sender, RoutedEventArgs e) {
            if (songsList.SelectedItem == null) {
                MessageBox.Show ("Odaberi pjesmu");
                return;
            }
            Song selectedSong = songsList.SelectedItem as Song;
            try {
                System.Diagnostics.Process.Start (selectedSong.youtubeAdress);
            }
            catch {
                MessageBox.Show ("Adresa nije valjana");
            }
        }

        private void addSong_Click (object sender, RoutedEventArgs e) {
            if (authorsList.SelectedItem == null) {
                MessageBox.Show ("Odaberi jednog autora");
                return;
            }
            InputWindow newSongWin = new InputWindow ("Unesi ime pjesme");
            Song newSong = new Song ();
            newSongWin.Owner = this;
            newSongWin.ShowDialog ();
            if (newSongWin.accepted == true) {
                Author selectedAuthor = authorsList.SelectedItem as Author;
                newSong.songName = newSongWin.inputTextbox.Text;
                newSong.Author = selectedAuthor;

                InputWindow newSongWinYoutubeLink = new InputWindow ("Unesi link pjesme");
                newSongWinYoutubeLink.Owner = this;
                newSongWinYoutubeLink.ShowDialog ();
                if (newSongWinYoutubeLink.accepted == true) {
                    newSong.youtubeAdress = newSongWinYoutubeLink.inputTextbox.Text;
                    GuitarMain.SaveSong (newSong);
                }
            }
            SetSongsLinksDataContext ();
        }
        private void removeSong_Click (object sender, RoutedEventArgs e) {
            if (songsList.SelectedItem == null) {
                MessageBox.Show ("Odaberi pjesmu");
                return;
            }
            Song selectedSong = songsList.SelectedItem as Song;
            GuitarMain.DeleteSong (selectedSong);
            SetSongsLinksDataContext ();
        }

        #region tabs

        private void addTab_Click (object sender, RoutedEventArgs e) {
            addInternetLink (hyperlinkEnums.Tab ());
        }
        private void removeTab_Click (object sender, RoutedEventArgs e) {
            deleteInternetLink (tabsList);
        }

        #endregion

        #region tutorials

        private void addTutorial_Click (object sender, RoutedEventArgs e) {
            addInternetLink (hyperlinkEnums.Tutorial ());
        }
        private void removeTutorial_Click (object sender, RoutedEventArgs e) {
            deleteInternetLink (tutorialsList);
        }

        #endregion

        #region covers

        private void addCover_Click (object sender, RoutedEventArgs e) {
            addInternetLink (hyperlinkEnums.Cover());
        }
        private void removeCover_Click (object sender, RoutedEventArgs e) {
            deleteInternetLink (coversList);
        }
        
        #endregion

        private void addInternetLink (string type) {
            if (songsList.SelectedItem == null) {
                MessageBox.Show ("Odaberi pjesmu");
                return;
            }
            Song selectedSong = songsList.SelectedItem as Song;
            Guitar.Model3.InternetLinks newLink = new Guitar.Model3.InternetLinks ();
            InputWindow newTabWinName = new InputWindow ("Unesi naziv linka");
            InputWindow newTabWinLink = new InputWindow ("Unesi adresu linka");
            newTabWinLink.Owner = this;
            newTabWinName.Owner = this;

            newTabWinName.ShowDialog ();
            if (newTabWinName.accepted)
                newLink.linkName = newTabWinName.inputTextbox.Text;
            else
                return;
            newTabWinLink.ShowDialog ();
            if (newTabWinLink.accepted)
                newLink.linkAdress = newTabWinLink.inputTextbox.Text;
            else
                return;
            newLink.Song = selectedSong;
            newLink.linkType = type;
            GuitarMain.SaveInternetLink (newLink);
            SetSongsLinksDataContext ();
        }
        private void deleteInternetLink (ListBox listbox) {
            if (listbox.SelectedItem == null) {
                MessageBox.Show ("Odaberi link");
                return;
            }
            InternetLinks link = listbox.SelectedItem as InternetLinks;
            GuitarMain.DeleteLink (link);
            SetSongsLinksDataContext ();
        }
        private void openInternetLink (ListBox listbox) {
            InternetLinks link = listbox.SelectedItem as InternetLinks;
            try {
                System.Diagnostics.Process.Start (link.linkAdress);
            }
            catch {
                MessageBox.Show ("Adresa nije valjana.");
            }
        }

        private void SetSongsLinksDataContext () {
            Author selectedAuthor = authorsList.SelectedItem as Author;
            if (selectedAuthor != null) {
                songsList.DataContext = GuitarMain.GetSongs (selectedAuthor);
            }
            tabsList.DataContext = null;
            tutorialsList.DataContext = null;
            coversList.DataContext = null;

            Song selectedSong = songsList.SelectedItem as Song;
            if (selectedSong != null) {

                tabsList.DataContext = GuitarMain.GetLinks (selectedSong, hyperlinkEnums.Tab ());
                tutorialsList.DataContext = GuitarMain.GetLinks (selectedSong, hyperlinkEnums.Tutorial ());
                coversList.DataContext = GuitarMain.GetLinks (selectedSong, hyperlinkEnums.Cover ());
            }
        }
        
        #endregion
    }

}

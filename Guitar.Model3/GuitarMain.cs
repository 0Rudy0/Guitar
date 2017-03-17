using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.ComponentModel;

namespace Guitar.Model3 {
    public static class GuitarMain {
        private static Guitar.Model3.GuitarDBEntities guitarEnts = new GuitarDBEntities ();

        public static List<Model3.InternetLinks> GetLinks (Song song, string type) {
            List<Model3.InternetLinks> newSet = new List<Model3.InternetLinks>();
            foreach (InternetLinks tempLink in guitarEnts.InternetLinks) {
                if (tempLink.songID == song.songID && tempLink.linkType == type) {
                    newSet.Add (tempLink);
                }
            }
            newSet.Sort((a, b) => a.linkName.CompareTo(b.linkName));
            return newSet;
        }

        public static List<Model3.Author> GetAuthors () {
            List<Model3.Author> newSet = new List<Model3.Author>();
            foreach (Model3.Author tempAuth in guitarEnts.Author) {
                    newSet.Add (tempAuth);
            }
            newSet.Sort((a, b) => a.authorName.CompareTo(b.authorName));
            return newSet;
        }

        public static List<Model3.Author> GetAuthors(string type)
        {
            List<Model3.Author> newSet = new List<Model3.Author>();
            foreach (Model3.Author tempAuth in guitarEnts.Author)
            {
                if (tempAuth.authorGenre == type) {
                    newSet.Add (tempAuth);
                }
            }
            newSet.Sort((a, b) => a.authorName.CompareTo(b.authorName));
            return newSet;
        }

        public static List<Song> GetSongs(Model3.Author author)
        {
            List<Song> newSet = new List<Song> ();
            foreach (Song tempSong in guitarEnts.Song) {
                if (tempSong.authorID == author.authorID) {
                    newSet.Add (tempSong);
                }
            }
            newSet.Sort((a, b) => a.songName.CompareTo(b.songName));
            return newSet;
        }

        public static void SaveAuthor(Model3.Author newAuthor)
        {
            guitarEnts.Author.AddObject (newAuthor);
            guitarEnts.SaveChanges ();            
        }

        public static void SaveSong (Song newSong) {
            guitarEnts.Song.AddObject (newSong);
            guitarEnts.SaveChanges ();
        }

        public static void SaveInternetLink (InternetLinks newLink) {
            guitarEnts.InternetLinks.AddObject (newLink);
            guitarEnts.SaveChanges ();
        }

        public static void DeleteAuthor(Model3.Author author)
        {
            guitarEnts.Author.DeleteObject (author);
            guitarEnts.SaveChanges ();
        }

        public static void DeleteSong (Song song) {
            guitarEnts.Song.DeleteObject (song);
            guitarEnts.SaveChanges ();
        }

        public static void DeleteLink (InternetLinks link) {
            guitarEnts.InternetLinks.DeleteObject (link);
            guitarEnts.SaveChanges ();
        }

        
    }

}

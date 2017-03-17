using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;

namespace Guitar.WebApp.Controllers
{
	public class DataController : Controller
	{
		// GET: Data
		public String GetAuthorSongs(int authorId)
		{
			GuitarDatabaseEntities entities = new GuitarDatabaseEntities();
			List<Song> songs = entities.Songs.Where(s => s.authorID.Equals(authorId)).ToList();
			for (var i = 0; i < songs.Count; i++)
			{
				songs[i].authorID = 0;
				songs[i].Author = null;
				foreach (InternetLink il in songs[i].InternetLinks)
				{
					il.songID = 0;
					il.Song = null;
				}
			}
			return new JavaScriptSerializer().Serialize(songs);
		}

		public String GetOldLinks()
		{
			GuitarDatabaseEntities entities = new GuitarDatabaseEntities();
			List<String> songNames = new List<string>();
			foreach (Song s in entities.Songs)
			{
				using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
				{
					try
					{
						string htmlCode = client.DownloadString(s.youtubeLink);
						if (htmlCode.Contains("Ĺ˝ao nam je zbog toga."))
						{
							songNames.Add(s.songName);
							s.youtubeLink = Guid.NewGuid().ToString();
						}
					}
					catch
					{
						songNames.Add(s.songName);
						s.youtubeLink = Guid.NewGuid().ToString();
					}

				}
			}

			foreach (InternetLink i in entities.InternetLinks)
			{
				using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
				{
					try
					{
						string htmlCode = client.DownloadString(i.linkAdress);
						if (htmlCode.Contains("Ĺ˝ao nam je zbog toga."))
						{
							songNames.Add(i.Song.songName + " - " + i.linkName);
							entities.InternetLinks.Remove(i);
						}
					}
					catch
					{
						songNames.Add(i.Song.songName + " - " + i.linkName);
						entities.InternetLinks.Remove(i);
					}
					
				}
			}

			entities.SaveChanges();
			return (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(songNames);
		}

		public string GetEmptySongs()
		{
			GuitarDatabaseEntities entities = new GuitarDatabaseEntities();
			List<String> songNames = new List<string>();
			string noTab = "Bez akordova<ul>";
			string noTutorial = "Bez tutoriala<ul>";
			string noCover = "Bez covera<ul>";
			foreach (Song s in entities.Songs.OrderBy(x => x.songName))
			{
				if (s.InternetLinks.Where(x => x.isTab).Count() == 0)
				{
					songNames.Add(s.songName);
					noTab += "<li>" + s.songName + "</li>";
				}
				if (s.InternetLinks.Where(x => x.isTutorial).Count() == 0)
				{
					noTutorial += "<li>" + s.songName + "</li>";
				}
				if (s.InternetLinks.Where(x => x.isCover).Count() == 0)
				{
					noCover += "<li>" + s.songName + "</li>";
				}
			}
			//return (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(songNames);
			noTab += "</ul><br/><br/>";
			noTutorial += "</ul><br/><br/>";
			noCover += "</ul><br/><br/>";
			return noTab + noTutorial + noCover;
		}
	}
}
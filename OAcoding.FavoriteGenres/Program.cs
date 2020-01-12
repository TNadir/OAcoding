using System;
using System.Collections.Generic;

namespace OAcoding.FavoriteGenres
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Dictionary<string, List<string>> userSongs = new Dictionary<string, List<string>>();

            userSongs.Add("David", new List<string>() { "song1", "song2", "song3", "song4", "song8" });
            userSongs.Add("Emma", new List<string>() { "song5", "song6", "song7" });

            Dictionary<string, List<string>> songGenres = new Dictionary<string, List<string>>();

            songGenres.Add("Rock", new List<string>() { "song1", "song3" });
            songGenres.Add("Dubstep", new List<string>() { "song7" });
            songGenres.Add("Techno", new List<string>() { "song2", "song4" });
            songGenres.Add("Pop", new List<string>() { "song5", "song6" });
            songGenres.Add("Jazz", new List<string>() { "song8", "song9" });

            var res = FavouriteGenre(userSongs, songGenres);
            foreach (var item in res)
            {
                Console.WriteLine("{0} : [ {1} ]", item.Key, string.Join(", ", item.Value));
            }
            Console.ReadKey();
        }


        public static Dictionary<string, List<string>>
               FavouriteGenre(Dictionary<string, List<string>> userSongs,
                              Dictionary<string, List<string>> songGenres)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            // Map Songs to genre 
            Dictionary<string, string> songstogenre = new Dictionary<string, string>();
            foreach (var item in songGenres)
            {
                string genre = item.Key;
                List<string> songs = item.Value;
                foreach (string song in songs)
                {
                    if (!songstogenre.ContainsKey(song))
                    {
                        songstogenre.Add(song, genre);
                    }
                }
            }

            foreach (var item in userSongs)
            {
                // count of each genre for every user 
                Dictionary<string, int> genrecount = new Dictionary<string, int>();
                int max = 0;
                string user = item.Key;
                List<string> usersongs = item.Value;
                foreach (string song in usersongs)
                {
                    if (genrecount.ContainsKey(songstogenre[song]))
                    {
                        genrecount[songstogenre[song]]++;
                        max = Math.Max(max, genrecount[songstogenre[song]]);
                    }
                    else
                        genrecount.Add(songstogenre[song], 1);
                }

                foreach (var genre in genrecount)
                {
                    if (!result.ContainsKey(user))
                        result.Add(user, new List<string>());
                    if (genre.Value == max)
                        result[user].Add(genre.Key);
                }

            }


            return result;
        }

    }
}

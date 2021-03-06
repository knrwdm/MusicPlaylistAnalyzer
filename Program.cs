﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MusicPlaylistAnalyzer
{
    class Song
    {
        public string Name;
        public string Artist;
        public string Album;
        public string Genre;
        public int Size;
        public int Time;
        public int Year;
        public int Plays;

        public Song(string Name, string Artist, string Album, string Genre, int Size, int Time, int Year, int Plays)
        {
            this.Name = Name;
            this.Artist = Artist;
            this.Album = Album;
            this.Genre = Genre;
            this.Size = Size;
            this.Time = Time;
            this.Year = Year;
            this.Plays = Plays;
        }

        override public string ToString()
        {
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string report = null;
            int i;

            List<Song> RowCol = new List<Song>();

            try
            {
                if (File.Exists("Playlist.txt"))
                {
                    StreamReader sr = new StreamReader("Playlist.txt");
                    i = 0;
                    string line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        i = i + 1;
                        try
                        {
                            string[] col = line.Split('\t');

                            if (col.Length < 8)
                            {
                                Console.WriteLine("Improper amount of Columns.");
                                break;
                            }
                            else
                            {
                                Song data = new Song((col[0]), (col[1]), (col[2]), (col[3]), Int32.Parse(col[4]), Int32.Parse(col[5]), Int32.Parse(col[6]), Int32.Parse(col[7]));
                                RowCol.Add(data);
                            }
                        }
                        catch
                        {
                            Console.Write("Error.");
                            break;
                        }
                    }
                    sr.Close();
                }
                else
                {
                    Console.WriteLine("Sorry, the file was not found.");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Sorry, the file cannot be opened.");
            }

            try
            {
                Song[] songs = RowCol.ToArray();
                using (StreamWriter write = new StreamWriter("Report.txt"))
                {
                    write.WriteLine("Music Playlist Report");
                    write.WriteLine("");



                    var plays = from song in songs where song.Plays >= 200 select song;
                    report += "Songs that received 200 or more plays:\n";
                    foreach (Song song in plays)
                    {
                        report += song + "\n";
                    }


                    var alternative = from song in songs where song.Genre == "Alternative" select song;
                    i = 0;
                    foreach (Song song in alternative)
                    {
                        i++;
                    }
                    report += "Number of Alternative songs: {i} \n";


                    var rap = from song in songs where song.Genre == "Hip-Hop/Rap" select song;
                    i = 0;
                    foreach (Song song in rap)
                    {
                        i++;
                    }
                    report += "Number of Hip-Hop/Rap songs: {i}\n";


                    var fishbowl = from song in songs where song.Album == "Welcome to the Fishbowl" select song;
                    report += "Songs from the album Welcome to the Fishbowl:\n";
                    foreach (Song song in fishbowl)
                    {
                        report += song + "\n";
                    }


                    var songs1970 = from song in songs where song.Year < 1970 select song;
                    report += "Songs from before 1970:\n";
                    foreach (Song song in songs1970)
                    {
                        report += song + "\n";
                    }


                    var names = from song in songs where song.Name.Length > 85 select song.Name;
                    report += "Song names longer than 85 characters:\n";
                    foreach (string name in names)
                    {
                        report += name + "\n";
                    }


                    var longest = from song in songs orderby song.Time descending select song;
                    report += "Longest song:\n";
                    report += longest.First();

                    write.Write(report);

                    write.Close();
                }
                Console.WriteLine("Yay! Your Report.txt Has Been Created!");
            }

            catch (Exception)
            {
                Console.WriteLine("Your report cannot be opened or written to.");
            }
            Console.ReadLine();
        }
    }
}
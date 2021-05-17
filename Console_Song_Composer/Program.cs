using System;

namespace Console_Song_Composer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! Enjoy the Console Song!");

            //Change this value to adjust how long a quarter note is in milliseconds
            int standardNoteInMillisecs = 1600;
            string[] song = SongComposer();
            PlaySong(standardNoteInMillisecs, song);
        }

        //This is where you can compose the song to be played. To do so, alternate music note names
        //and note lengths in the array. Example: "a2", "whole", "c3", "half"
        static string[] SongComposer()
        {
            string[] songComposer = new string[] { "f6", "eighth", "e6", "eighth", "f6", "eighth", "e6", "eighth", "f6", "eighth", "c6", "eighth",
                                                          "d6", "eighth", "aSharp5", "eighth", "a5", "quarter", "g5", "quarter", "f5", "quarter", "f6", "half" };
            return songComposer;
        }

        static void PlaySong(int standardNoteInMillisecs, string[] song)
        {
            string[] songNoteToneNames = ExtractSongNoteTones(song);
            string[] songNoteDurations = ExtractSongNoteDurations(song);

            if (songNoteToneNames.Length != songNoteDurations.Length)
            {
                Console.WriteLine("The welcome song's number of note tones and number of note durations are not the same.");
                return;
            }

            //This converts the song to the needed format of notes in hertz and duration in milliseconds
            int[] songMelodyInHertz = ConvertMusicNoteNamesToHz(songNoteToneNames);
            int[] songNoteDurationsInMillisecs = ConvertMusicNoteDurationsToMillisecs(songNoteDurations, standardNoteInMillisecs);

            SongPlayer(songMelodyInHertz, songNoteDurationsInMillisecs);
        }

        static void SongPlayer(int[] songMelody, int[] noteDurations)
        {
            for (int i = 0; i < songMelody.Length; i++)
            {
                Console.Beep(songMelody[i], noteDurations[i]);
            }
        }

        //This function takes a song that has been composed as an array and returns an array with the note tones
        static string[] ExtractSongNoteTones(string[] song)
        {
            int halfOfSongArrayLength = song.Length / 2;
            string[] songNoteTonesArray = new string[halfOfSongArrayLength];

            for (int i = 0; i < song.Length; i += 2)
            {
                songNoteTonesArray[i / 2] = song[i];
            }

            return songNoteTonesArray;
        }

        //This function takes a song that has been composed as an array and returns an array with the note durations
        static string[] ExtractSongNoteDurations(string[] song)
        {
            int halfOfSongArrayLength = song.Length / 2;
            string[] songNoteDurationsArray = new string[halfOfSongArrayLength];

            for (int i = 1; i < song.Length; i += 2)
            {
                songNoteDurationsArray[i / 2] = song[i];
            }

            return songNoteDurationsArray;
        }

        //This fuction takes an array of the melody notes of a desired song and returns an array of the music note tones in hertz
        static int[] ConvertMusicNoteNamesToHz(string[] melody)
        {
            string[] musicNoteNamesArray = new string[] { "d4", "dSharp4", "e4", "f4", "fSharp4", "g4", "gSharp4", "a4", "aSharp4", "b4",
                                                          "c5", "cSharp5", "d5", "dSharp5", "e5", "f5", "fSharp5", "g5", "gSharp5", "a5", "aSharp5", "b5",
                                                          "c6", "cSharp6", "d6", "dSharp6", "e6", "f6", "fSharp6", "g6", "gSharp6", "a6", "aSharp6", "b6",
                                                          "c7", "cSharp7", "d7", "dSharp7", "e7", "f7" };
            int[] musicNoteHzArray = new int[] { 294, 311, 330, 349, 370, 392, 415, 440, 466, 494, 523, 554, 587, 622, 659, 698, 740, 784, 831, 880, 932,
                                                 988, 1047, 1109, 1175, 1245, 1319, 1397, 1480, 1568, 1661, 1760, 1865, 1976, 2093, 2217, 2349, 2489,
                                                 2637, 2794 };

            //Creates a new int array that is the same size as the melody string array
            int[] melodyInHertz = new int[melody.Length];

            //This for loop finds the index of the melody note in the musicNoteNamesArray, and then sets the value of the melodyInHz array to the value at that index
            //in the musciNoteHzArray
            for (int i = 0; i < melody.Length; i++)
            {
                melodyInHertz[i] = musicNoteHzArray[Array.IndexOf(musicNoteNamesArray, melody[i])];
            }

            return melodyInHertz;
        }

        //This function coverts the array of note durations written as strings into an array of note durations in milliseconds
        static int[] ConvertMusicNoteDurationsToMillisecs(string[] noteDuration, int standardDuration)
        {
            //This sets the value of the length of each not in milliseconds based on the standardDuration of a whole note
            string[] noteDurationNames = new string[] { "whole", "half", "quarter", "eighth", "sixteenth" };
            int[] noteDurationMillisecs = new int[] { standardDuration, Convert.ToInt32(.5 * standardDuration), Convert.ToInt32(.25 * standardDuration),
                                                         Convert.ToInt32(.125 * standardDuration), Convert.ToInt32(.0625 * standardDuration) };

            //Creates a new int array that is the same size as the noteDuration string array
            int[] noteDurationsInMillisecs = new int[noteDuration.Length];

            //This loop converts the note string names in the noteDuration array into the note durations in milliseconds
            for (int i = 0; i < noteDuration.Length; i++)
            {
                noteDurationsInMillisecs[i] = noteDurationMillisecs[Array.IndexOf(noteDurationNames, noteDuration[i])];
            }

            return noteDurationsInMillisecs;
        }
    }
}

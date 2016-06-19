namespace RPG_ConsoleGame.Sound
{
    using System;
    using System.IO;
    using System.Media;
    using System.Threading;

    public class Sound
    {
        static int[,] musicSheet;

        public static void SFX(SoundEffects sfx)
        {
            switch (sfx)
            {
                case SoundEffects.DefaultTheme:
                    PlaySoundFromFile(@"..\..\Music\Music.wav");
                    break;
            }
        }

        private static void PlaySoundFromFile(string filePath)
        {
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.Play();
            }
        }

        public static void Music()
        {
            if (File.Exists(@"..\..\Music.mus"))
            {
                StreamReader musicFile = new StreamReader(@"..\..\Music.mus");
                LoadMusicFromFile(musicFile);
            }
            else if (File.Exists(@"Music.mus"))
            {
                StreamReader musicFile = new StreamReader(@"Music.mus");
                LoadMusicFromFile(musicFile);
            }
            else
            {
                throw new FileNotFoundException();
            }
            new Thread(() => SomeMusic()).Start();
        }

        static void LoadMusicFromFile(StreamReader loadMusic)
        {
            int lines = int.Parse(loadMusic.ReadLine());
            musicSheet = new int[lines, 2];
            for (int i = 0; i < lines; i++)
            {
                string[] musicLine = loadMusic.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                musicSheet[i, 0] = int.Parse(musicLine[0]);
                musicSheet[i, 1] = int.Parse(musicLine[1]);
            }
        }

        public static void SomeMusic()
        {
            while (true)
            {
                for (int line = 0; line < musicSheet.GetLength(0); line++)
                {
                    if (musicSheet[line, 1] != 0)
                    {
                        Console.Beep(musicSheet[line, 0], musicSheet[line, 1]);
                    }
                    else
                    {
                        Thread.Sleep(musicSheet[line, 0]);
                    }
                }
            }
        }
    }
}


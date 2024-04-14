/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 12-APR-2024
/// Last updated: 13-APR-2024
///
/// File Contents: 
///     encrypt
///     decrypt
///     set frequency
///     set speed (wpm)
///     play a morse message
///     play a tone
///     try to get key from dictionary, using a value
///     
/// Notes:
///     Audio quality is awful and needs to be improved.
///     Speed isn't very accurate to the words per minute.
///     Need to implement Farnsworth Timing.
/// 
/// </summary>
/// 

using System.Media;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Encrypt
{
    
    /// <summary>
    ///     This class provides Morse Code encryption and
    ///     decryption for strings of text. Permitted characters
    ///     are a-z, A-Z, and 0-9.
    /// </summary>
    public class Morse
    {

        private float _frequency;    // frequency of sine tone in MHz
        private int _dit;            // dot length in milliseconds
        private int _dah;            // dash length in milliseconds
        private int _intraCharSpace; // length of pause within characters in milliseconds
        private int _interCharSpace; // length of pause between characters in milliseconds
        private int _interWordSpace; // length of pause between words in milliseconds

        private Dictionary<char, string> _morseLookup = new Dictionary<char, string>()
        {
            {'A', ".-"},
            {'B', "-..."},
            {'C', "-.-."},
            {'D', "-.."},
            {'E', "."},
            {'F', "..-."},
            {'G', "--."},
            {'H', "...."},
            {'I', ".."},
            {'J', ".---"},
            {'K', "-.-"},
            {'L', ".-.."},
            {'M', "--"},
            {'N', "-."},
            {'O', "---"},
            {'P', ".--."},
            {'Q', "--.-"},
            {'R', ".-."},
            {'S', "..."},
            {'T', "-"},
            {'U', "..-"},
            {'V', "...-"},
            {'W', ".--"},
            {'X', "-..-"},
            {'Y', "-.--"},
            {'Z', "--.."},
            {'0', "-----"},
            {'1', ".----"},
            {'2', "..---"},
            {'3', "...--"},
            {'4', "....-"},
            {'5', "....."},
            {'6', "-...."},
            {'7', "--..."},
            {'8', "---.."},
            {'9', "----."},
            {' ', "/"}
        };

        //short[] _wave;
        //byte[] _binaryWave;
        //private const int _sampleRate = 44100;
        //private const short _bitsPerSample = 16;

        public Morse()
        {
            _frequency = 600f;

            int speed = 20;                            // words per minute
            int timeUnit = (60 * 1000) / (50 * speed); // milliseconds per dit

            _dit = timeUnit;
            _dah = timeUnit * 3;
            _intraCharSpace = timeUnit;
            _interCharSpace = timeUnit * 3;
            _interWordSpace = timeUnit * 7;
        }
        
        
        /// <summary>
        ///     Farnsworth Timing not currently implemented.
        /// </summary>
        /// <param name="frequency">frequency of tone</param>
        /// <param name="speed">message speed in words per minute</param>
        /// <param name="farnsworthTiming">increase pauses for novices</param>
        public Morse(float frequency, int speed, bool farnsworthTiming)
        {        
            _frequency = frequency;

            int timeUnit = (60 * 1000) / (50 * speed); // milliseconds per dit

            _dit = timeUnit;
            _dah = timeUnit * 3;
            _intraCharSpace = timeUnit;
            _interCharSpace = timeUnit * 3;
            _interWordSpace = timeUnit * 7;
        }


        /// <summary>
        ///     Encrypts the plain text messages.
        ///     Permitted characters are a-z, A-Z, and 0-9.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Encrypt(string plainText)
        {
            StringBuilder cipherText = new StringBuilder();
            string morseChar;

            foreach (char character in plainText.ToUpper())
            {
                if (_morseLookup.TryGetValue(character, out morseChar))
                {
                    cipherText.Append(morseChar + " ");
                }
                else
                {
                    throw new Exception("Valid characters are A-Z and 0-9");
                }
            }

            return cipherText.ToString();
        }


        /// <summary>
        ///     Decrypts a Morse Code message.
        ///     A valid message will be formed only of periods (short/dit/dot), 
        ///     dashes (long/dah/dash), forward slashes, and spaces, with a space between 
        ///     each plain text character and a space, forward slash, space (" / ")
        ///     between each word.
        ///     
        ///     e.g. HELLO WORLD => .... . .-.. .-.. --- / .-- --- .-. .-.. -.. 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Decrypt(string cipherText)
        {
            StringBuilder plainText = new StringBuilder();

            string[] split = cipherText.Split(" ");

            char plainChar;

            foreach (string s in split)
            {
                if (TryGetKey(_morseLookup, s, out plainChar))
                {
                    plainText.Append(plainChar);
                }
                else if (s.Equals("")) { } // ignore
                else if (s.Equals("/"))
                {
                    plainText.Append(" ");
                }
                else
                {
                    throw new Exception("Invalid morse character on input.");
                }
            }

            return plainText.ToString();
        }


        /// <summary>
        ///     Sets the frequency of tones for a played
        ///     Morse Code message.
        ///     Reasonable values are 100 to 10000.
        /// </summary>
        /// <param name="frequency"></param>
        public void SetFrequency(float frequency)
        {
            _frequency = frequency;
        }


        /// <summary>
        ///     Sets the speed (in words per minute) of the 
        ///     played Morse Code message.
        ///     Reasonable values are 5 to 25.
        /// </summary>
        /// <param name="speed"></param>
        public void SetSpeed(int speed)
        {
            int timeUnit = (60 * 1000) / (50 * speed); // milliseconds per dit

            _dit = timeUnit;
            _dah = timeUnit * 3;
            _intraCharSpace = timeUnit;
            _interCharSpace = timeUnit * 3;
            _interWordSpace = timeUnit * 7;
        }

        public void SetVolume()
        {
            // gain?
        }


        /// <summary>
        ///     Plays the Morse tones of each character of
        ///     a Morse Code message according to the speed and
        ///     frequency set.
        /// </summary>
        /// <param name="morseMessage"></param>
        public void PlayMessageTones(string morseMessage)
        {
            foreach (char c in morseMessage)
            {
                switch (c)
                {
                    case '.':
                        PlayTone(_dit);
                        Thread.Sleep(_intraCharSpace);
                        break;
                    case '-':
                        PlayTone(_dah);
                        Thread.Sleep(_intraCharSpace);
                        break;
                    case ' ':
                        Thread.Sleep(_interCharSpace);
                        break;
                    case '/':
                        Thread.Sleep(_interWordSpace);
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        ///     Plays an individual tone (frequency previously
        ///     set) for to provided length of time.
        /// </summary>
        /// <param name="toneLength">Length of time in milliseconds</param>
        private void PlayTone(int toneLength)
        {
            var sampleProvider = new SignalGenerator()
            {
                Gain = .2,
                Frequency = _frequency,
                Type = SignalGeneratorType.Sin
            }.Take(TimeSpan.FromMilliseconds(toneLength));

            using (var wo = new WaveOutEvent())
            {
                wo.Init(sampleProvider);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(10);
                }
            }
        }
        

        /// <summary>
        ///     A helper method to determine if a corresponding
        ///     key exists for the provided value of a key-value pair
        ///     in the provided dictionary.
        ///     Provides the key if it exists.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TryGetKey(Dictionary<char, string> dictionary, string value, out char key)
        {
            foreach (KeyValuePair<char, string> pair in dictionary)
            {
                if (pair.Value == value)
                {
                    key = pair.Key;
                    return true;
                }
            }

            key = default(char);
            return false;
        }


        //public void PlayToneTest()
        //{
            //_wave = new short[_sampleRate];
            //_binaryWave = new byte[_sampleRate * sizeof(short)];

            //for(int i = 0; i < _sampleRate; i++)
            //{
            //    _wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * _frequency) / _sampleRate) * i));
            //}

            //Buffer.BlockCopy(_wave, 0, _binaryWave, 0, _wave.Length * sizeof(short));

            //using (MemoryStream memoryStream = new MemoryStream())
            //using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            //{
            //    short blockAlign = _bitsPerSample / 8;
            //    int subChunkTwoSize = _sampleRate * blockAlign;
                
            //    binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
            //    binaryWriter.Write(36 + subChunkTwoSize);
            //    binaryWriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
            //    binaryWriter.Write(16);
            //    binaryWriter.Write((short)1);
            //    binaryWriter.Write((short)1);
            //    binaryWriter.Write(_sampleRate);
            //    binaryWriter.Write(_sampleRate * blockAlign);
            //    binaryWriter.Write(blockAlign);
            //    binaryWriter.Write(_bitsPerSample);
            //    binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });
            //    binaryWriter.Write(subChunkTwoSize);
            //    binaryWriter.Write(_binaryWave);
            //    memoryStream.Position = 0;
            //    //new SoundPlayer(memoryStream).Play();
            //    SoundPlayer player = new SoundPlayer(memoryStream);
            //}
        //}



    }
}

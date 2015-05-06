using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TARLABS.VBScriptFormatter
{
    internal class PeekableTextReader : TextReader
    {
        TextReader _reader;
        List<int> buffer;
        private int _lastRead;

        public PeekableTextReader(TextReader Reader)
        {
            _reader = Reader;
            buffer = new List<int>();
        }

        public string PeekTill(Func<int, int, bool> predicate)
        {
            int n = 0;
            StringBuilder sb = new StringBuilder();

            while (predicate(n, Peek(n)))
            {
                sb.Append((char) Peek(n++));
            }

            return sb.ToString();
        }

        public string PeekAfterWhiteSpace(ref int n)
        {
            int ch;
            //n = 0;

            do
            {
                ch = Peek(n++);
            } while (IsWhiteSpaceChar(ch) || ch == '_' || ch == '\r' || ch == '\n');

            n--;
            if (n == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            while (IsAlphaNumeric(ch = Peek(n++)))
                sb.Append((char) ch);
            n--;
            return sb.ToString();
        }

        public string PeekTill(Func<int, bool> predicate)
        {
            int n = 0;
            StringBuilder sb = new StringBuilder();

            while (!isEOLorEOF(n) && predicate(Peek(n)))
            {
                sb.Append((char) Peek(n++));
            }

            return sb.ToString();
        }
        public string ReadTill(Func<int, bool> predicate)
        {
            StringBuilder sb = new StringBuilder();
            while (Peek() != -1 && predicate((char) Peek()))
            {
                sb.Append((char) Read());
            }

            return sb.ToString();
        }

        public string PeekTillEndOfLine()
        {
            return PeekTill((n, ch) => !isEOLorEOF(n));
        }
        public string ReadTillEndOfLine()
        {
            return ReadTill(ch => !isEOLorEOF());
        }

        public string PeekTillEndOfStatement()
        {
            return PeekTill(ch => ch != ':');
        }
        public string ReadTillEndOfStatement()
        {
            return ReadTill(ch => ch != ':');
        }

        public string PeakAlphaNumeric()
        {
            return PeekTill(ch => IsAlphaNumeric(ch));
        }

        public string ReadAlphaNumeric()
        {
            return ReadTill(ch => IsAlphaNumeric(ch));
        }

        public string PeekTillWhiteSpace()
        {
            return PeekTill(ch => IsWhiteSpaceChar(ch));
        }

        public string ReadTillWhiteSpace()
        {
            return ReadTill(ch => IsWhiteSpaceChar(ch));
        }

        public override int Peek()
        {
            if (buffer.Count != 0)
                return buffer[0];
            return Peek(0);
        }
        public override int Read()
        {
            _lastRead = Peek(0);
            buffer.RemoveAt(0);
            return _lastRead;
        }

        public string PeekString(int length)
        {
            Peek(length);

            if (length < 0)
                return "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append((char) buffer[i]);
            }
            return sb.ToString();
        }
        public string ReadString()
        {
            StringBuilder sb = new StringBuilder();
            int n = 0;

            if (Peek(n) == '"')
                sb.Append((char) Read());

            while (!isEOLorEOF())
            {
                if (Peek(0) == '"')
                {
                    if (Peek(1) == '"')
                    {
                        sb.Append((char) Read());
                        sb.Append((char) Read());
                    }
                    else
                    {
                        //end of string has been found
                        sb.Append((char) Read());
                        return sb.ToString();
                    }
                }
                else
                {
                    sb.Append((char) Read());
                }
            }

            return sb.ToString();
        }

        public string Read(int count)
        {
            StringBuilder sb = new StringBuilder();
            while (Peek(0) != -1 && count-- > 0)
            {
                sb.Append((char) Read());
            }
            return sb.ToString();
        }

        public int Peek(int length)
        {
            while (length >= buffer.Count && _reader.Peek() != -1)
            {
                buffer.Add(_reader.Read());
            }

            if (length == -1)
                return _lastRead;

            if (length >= buffer.Count)
                return -1;
            return buffer[length];
        }

        public virtual bool IsWhiteSpaceChar(char ch)
        {
            if (ch == ' ' || ch == '\t' || ch == '\f' || ch == '\v')
                return true;
            return false;
        }
        public virtual bool IsWhiteSpaceChar(int ch)
        {
            return IsWhiteSpaceChar((char) ch);
        }

        public bool IsAlphaNumeric(int ch)
        {
            return IsAlphaNumeric((char) ch);
        }
        public bool IsAlphaNumeric(char ch)
        {
            return char.IsDigit(ch) || char.IsLetterOrDigit(ch) || ch == '_';
        }

        //public string ReadComment()
        //{
        //    return ReadTill(ch => true);
        //}
        //public string PeekComment()
        //{
        //    return PeekTill(ch => true);
        //}


        public bool isEOLorEOF()
        {
            return isEOLorEOF(0);
        }
        public bool isEOLorEOF(int n)
        {
            if (Peek(n) == -1)
                return true;
            if (Peek(n) == '\r' && Peek(n + 1) == '\n')
            {
                //Read();
                return true;
            }
            if (Peek(n) == '\n')
                return true;

            return false;
        }

        internal string ReadNumber()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ReadTill(ch => char.IsDigit((char) ch)));
            if (Peek() == '.')
                sb.Append((char) Read());
            if (sb.Length == 0)
                return "";
            sb.Append(ReadTill(ch => char.IsDigit((char) ch)));
            if (Peek() == 'e' || Peek() == 'E')
            {
                sb.Append((char) Read());
                if (Peek() == '+' || Peek() == '-')
                    sb.Append((char) Read());
                sb.Append(ReadTill(ch => char.IsDigit((char) ch)));
            }
            return sb.ToString();
        }
    }

}

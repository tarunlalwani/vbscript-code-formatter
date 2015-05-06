using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TARLABS.VBScriptFormatter
{
    internal sealed class TokenReader
    {
        PeekableTextReader pkReader;

        internal class TokenTag
        {
            public ParseTokenType tokenType;
            public object value;

            public TokenTag(ParseTokenType type, object value)
            {
                tokenType = type;
                this.value = value;
            }
        }

        internal enum ParseTokenType
        {
            WHITESPACE,
            EOF,
            IF,
            ELSE_IF,
            END_IF,
            IF_ONE_LINE,
            IF_ELSE_ONE_LINE,
            PROPERTY,
            END_PROPERTY,
            FUNCTION,
            END_FUNCTION,
            SELECT_CASE,
            CASE_STATEMENT,
            CASE_ELSE,
            FOR_LOOP,
            DO_LOOP,
            WHILE_LOOP,
            WHILE_LOOP_WEND,
            DO_LOOP_WHILE,
            DO_LOOP_UNTIL,
            FOR_LOOP_NEXT,
            FOR_EACHLOOP,
            DIM,
            IDENTIFIER,
            STRING,
            COMMENT,
            NUMBER,
            HEXNUMBER,
            CLASS,
            END_CLASS,
            DOT_OPERATOR,
            BINARY_OPERATOR,
            ARTHMETIC_OPERATOR,
            COMPARISON_OPERATOR,
            REDIM,
            EXIT_FOR,
            EXIT_FUNCTION,
            EXIT_SUB,
            EXIT_PROPERTY,
            EXIT_DO,
            UNKNOWN,
            CLOSE_BRACKET,
            OPEN_BRACKET,
            NEWLINE,
            DATE,
            STATEMENT_CONTINUATION,
            PUBLIC,
            PRIVATE,
            DEFAULT,
            COMMA,
            OBJECT_NOTHING,
            VALUE_NULL,
            VALUE_TRUE,
            VALUE_FALSE,
            VALUE_EMPTY,
            REDIM_PRESERVE,
            ELSE,
            END_SUB,
            DO_LOOP_END,
            PRESERVE,
            FOR_LOOP_IN,
            END,
            ACTIVE_SCREEN,
            CONST,
            SET_OPERATOR,
            EXIT,
            THEN,
            FOR_LOOP_TO,
            NEW_OPERATOR,
            BYREF,
            BYVAL,
            CASE,
            SELECT,
            VBSCRIPT_FUNCTION,
            EXPLICIT,
            OPTION,
            RESUME,
            ERROR,
            ERR,
            ON,
            REGEXP,
            ERASE,
            CALL,
            WITH,
            STOP,
            VBSCRIPT_CONSTANT_COLOR,
            VBSCRIPT_CONSTANT_COMPARE,
            VBSCRIPT_CONSTANT_DATEFORMAT,
            VBSCRIPT_CONSTANT_ERROR,
            VBSCRIPT_CONSTANT_MSGBOX_RETVAL,
            VBSCRIPT_CONSTANT_MSGBOX,
            VBSCRIPT_CONSTANT_TRISTATE,
            VBSCRIPT_CONSTANT_VARTYPE,
            VBSCRIPT_CONSTANT_STRING,
            CLASS_NAME,
            STATEMENT_SEPARATOR,
            VARIABLE_NAME,
            CONST_VARIABLE_NAME,
            SUB,
            STEP,
            GOTO,
            END_SELECT,
            END_WITH,
            ON_ERROR_GOTO_0,
            ON_ERROR_RESUME_NEXT,
            DO_LOOP_END_WHILE,
            DO_LOOP_END_UNTIL,
            DO_LOOP_START_UNTIL,
            DO_LOOP_START_WHILE,
            INVALID
        }

        private ParseTokenType lastParsedToken;
        private ParseTokenType lastNonWSParsedToken;


        private void RecordToken(ParseTokenType TokenType, object value)
        {
            if (!(value is string))
            {
                if (value is int || value is char)
                    value = ((char) (int) value).ToString();
            }
            tokens.Add(new TokenTag(TokenType, value));
            //if (TokenType == ParseTokenType.UNKNOWN)
            //    Debug.WriteLine(string.Format("{0}, {1}", TokenType, value));

            lastParsedToken = TokenType;
            if (TokenType == ParseTokenType.WHITESPACE || TokenType == ParseTokenType.NEWLINE)
                lastNonWSParsedToken = TokenType;
        }

        public TokenReader()
        {
            LoadKeywords();
            ResetVariables();
            staticIndent = "";
            dynamicIndent = "";
        }


        private void ResetVariables()
        {
            lastLine = "";
            curLine = "";
            bUseIndent = false;
            bNextLineInContinuation = false;
            indentationLevel = 0;
        }

        private List<TokenTag> tokens;
        private Dictionary<string, ParseTokenType> oKeywordsDict = new Dictionary<string, ParseTokenType>(StringComparer.InvariantCultureIgnoreCase);
        private Dictionary<string, string> oKeywordsCaseDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        private void LoadKeywords()
        {
            oKeywordsDict.Clear();
            oKeywordsDict.Add("Step", ParseTokenType.STEP);
            oKeywordsDict.Add("Class", ParseTokenType.CLASS);
            oKeywordsDict.Add("Const", ParseTokenType.CONST);
            oKeywordsDict.Add("Function", ParseTokenType.FUNCTION);
            oKeywordsDict.Add("Property", ParseTokenType.PROPERTY);
            oKeywordsDict.Add("Sub", ParseTokenType.SUB);
            oKeywordsDict.Add("Goto", ParseTokenType.GOTO);

            oKeywordsDict.Add("Xor", ParseTokenType.BINARY_OPERATOR);
            oKeywordsDict.Add("Or", ParseTokenType.BINARY_OPERATOR);
            oKeywordsDict.Add("And", ParseTokenType.BINARY_OPERATOR);
            oKeywordsDict.Add("Not", ParseTokenType.BINARY_OPERATOR);
            oKeywordsDict.Add("Eqv", ParseTokenType.BINARY_OPERATOR);
            oKeywordsDict.Add("Imp", ParseTokenType.BINARY_OPERATOR);

            oKeywordsDict.Add("=", ParseTokenType.COMPARISON_OPERATOR);
            oKeywordsDict.Add("<=", ParseTokenType.COMPARISON_OPERATOR);
            oKeywordsDict.Add(">=", ParseTokenType.COMPARISON_OPERATOR);
            oKeywordsDict.Add("<>", ParseTokenType.COMPARISON_OPERATOR);
            oKeywordsDict.Add("Is", ParseTokenType.COMPARISON_OPERATOR);
            oKeywordsDict.Add("<", ParseTokenType.COMPARISON_OPERATOR);
            oKeywordsDict.Add(">", ParseTokenType.COMPARISON_OPERATOR);

            oKeywordsDict.Add("Mod", ParseTokenType.ARTHMETIC_OPERATOR);

            oKeywordsDict.Add("Dim", ParseTokenType.DIM);
            oKeywordsDict.Add("ReDim", ParseTokenType.REDIM);
            oKeywordsDict.Add("Preserve", ParseTokenType.PRESERVE);

            oKeywordsDict.Add("Public", ParseTokenType.PUBLIC);
            oKeywordsDict.Add("Private", ParseTokenType.PRIVATE);

            oKeywordsDict.Add("Default", ParseTokenType.DEFAULT);
            oKeywordsDict.Add("Next", ParseTokenType.FOR_LOOP_NEXT);

            oKeywordsDict.Add("Nothing", ParseTokenType.OBJECT_NOTHING);
            oKeywordsDict.Add("Null", ParseTokenType.VALUE_NULL);
            oKeywordsDict.Add("True", ParseTokenType.VALUE_TRUE);
            oKeywordsDict.Add("False", ParseTokenType.VALUE_FALSE);
            oKeywordsDict.Add("Empty", ParseTokenType.VALUE_EMPTY);

            oKeywordsDict.Add("ByVal", ParseTokenType.BYVAL);
            oKeywordsDict.Add("ByRef", ParseTokenType.BYREF);

            oKeywordsDict.Add("Select", ParseTokenType.SELECT);
            oKeywordsDict.Add("Case", ParseTokenType.CASE);

            oKeywordsDict.Add("If", ParseTokenType.IF);
            oKeywordsDict.Add("Else", ParseTokenType.ELSE);
            oKeywordsDict.Add("ElseIf", ParseTokenType.ELSE_IF);

            oKeywordsDict.Add("Exit", ParseTokenType.EXIT);
            oKeywordsDict.Add("End", ParseTokenType.END);
            oKeywordsDict.Add("Then", ParseTokenType.THEN);

            oKeywordsDict.Add("Err", ParseTokenType.ERR);
            oKeywordsDict.Add("RegExp", ParseTokenType.REGEXP);

            oKeywordsDict.Add("Call", ParseTokenType.CALL);

            oKeywordsDict.Add("Erase", ParseTokenType.ERASE);

            oKeywordsDict.Add("With", ParseTokenType.WITH);
            oKeywordsDict.Add("Stop", ParseTokenType.STOP);

            oKeywordsDict.Add("On", ParseTokenType.ON);
            oKeywordsDict.Add("Error", ParseTokenType.ERROR);
            oKeywordsDict.Add("Resume", ParseTokenType.RESUME);
            oKeywordsDict.Add("Option", ParseTokenType.OPTION);
            oKeywordsDict.Add("Explicit", ParseTokenType.EXPLICIT);

            oKeywordsDict.Add("Do", ParseTokenType.DO_LOOP);
            oKeywordsDict.Add("While", ParseTokenType.WHILE_LOOP);
            oKeywordsDict.Add("Wend", ParseTokenType.WHILE_LOOP_WEND);
            oKeywordsDict.Add("Until", ParseTokenType.DO_LOOP_UNTIL);
            oKeywordsDict.Add("Loop", ParseTokenType.DO_LOOP_END);
            oKeywordsDict.Add("For", ParseTokenType.FOR_LOOP);
            //oKeywordsDict.Add("Each", ParseTokenType.eac);
            oKeywordsDict.Add("To", ParseTokenType.FOR_LOOP_TO);
            oKeywordsDict.Add("In", ParseTokenType.FOR_LOOP_IN);
            oKeywordsDict.Add("Set", ParseTokenType.SET_OPERATOR);
            oKeywordsDict.Add("New", ParseTokenType.NEW_OPERATOR);

            oKeywordsDict.Add("Abs", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Array", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Asc", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Atn", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CBool", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CByte", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CCur", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CDate", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CDbl", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Chr", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CInt", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CLng", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Conversions", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Cos", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CreateObject", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CSng", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("CStr", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Date", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("DateAdd", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("DateDiff", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("DatePart", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("DateSerial", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("DateValue", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Day", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Derived Math", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Escape", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Eval", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Exp", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Filter", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("FormatCurrency", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("FormatDateTime", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("FormatNumber", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("FormatPercent", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("GetLocale", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("GetObject", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("GetRef", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Hex", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Hour", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("InputBox", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("InStr", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("InStrRev", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Int, Fix", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("IsArray", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("IsDate", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("IsEmpty", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("IsNull", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("IsNumeric", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("IsObject", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Join", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("LBound", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("LCase", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Left", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Len", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("LoadPicture", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Log", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("LTrim", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Maths", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Mid", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Minute", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Month", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("MonthName", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("MsgBox", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Now", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Oct", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Replace", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("RGB", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Right", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Rnd", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Round", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("RTrim", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("ScriptEngine", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("ScriptEngineBuildVersion", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("ScriptEngineMajorVersion", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("ScriptEngineMinorVersion", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Second", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("SetLocale", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Sgn", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Sin", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Space", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Split", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Sqr", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("StrComp", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("String", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("StrReverse", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Tan", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Time", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Timer", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("TimeSerial", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("TimeValue", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Trim", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("TypeName", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("UBound", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("UCase", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Unescape", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("VarType", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Weekday", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("WeekdayName", ParseTokenType.VBSCRIPT_FUNCTION);
            oKeywordsDict.Add("Year", ParseTokenType.VBSCRIPT_FUNCTION);

            oKeywordsDict.Add("vbCr", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("VbCrLf", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("vbFormFeed", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("vbLf", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("vbNewLine", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("vbNullChar", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("vbNullString", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("vbTab", ParseTokenType.VBSCRIPT_CONSTANT_STRING);
            oKeywordsDict.Add("vbVerticalTab", ParseTokenType.VBSCRIPT_CONSTANT_STRING);

            oKeywordsDict.Add("vbEmpty", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbNull", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbInteger", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbLong", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbSingle", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbDouble", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbCurrency", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbDate", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbString", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbObject", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbError", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbBoolean", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbVariant", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbDataObject", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbDecimal", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbByte", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);
            oKeywordsDict.Add("vbArray", ParseTokenType.VBSCRIPT_CONSTANT_VARTYPE);

            oKeywordsDict.Add("vbUseDefault", ParseTokenType.VBSCRIPT_CONSTANT_TRISTATE);
            oKeywordsDict.Add("vbTrue", ParseTokenType.VBSCRIPT_CONSTANT_TRISTATE);
            oKeywordsDict.Add("vbFalse", ParseTokenType.VBSCRIPT_CONSTANT_TRISTATE);

            oKeywordsDict.Add("vbOKOnly", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbOKCancel", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbAbortRetryIgnore", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbYesNoCancel", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbYesNo", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbRetryCancel", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbCritical", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbQuestion", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbExclamation", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbInformation", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbDefaultButton1", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbDefaultButton2", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbDefaultButton3", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbDefaultButton4", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbApplicationModal", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);
            oKeywordsDict.Add("vbSystemModal", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX);

            oKeywordsDict.Add("vbOK", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX_RETVAL);
            oKeywordsDict.Add("vbCancel", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX_RETVAL);
            oKeywordsDict.Add("vbAbort", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX_RETVAL);
            oKeywordsDict.Add("vbRetry", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX_RETVAL);
            oKeywordsDict.Add("vbIgnore", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX_RETVAL);
            oKeywordsDict.Add("vbYes", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX_RETVAL);
            oKeywordsDict.Add("vbNo", ParseTokenType.VBSCRIPT_CONSTANT_MSGBOX_RETVAL);

            oKeywordsDict.Add("vbObjectError", ParseTokenType.VBSCRIPT_CONSTANT_ERROR);

            oKeywordsDict.Add("vbGeneralDate", ParseTokenType.VBSCRIPT_CONSTANT_DATEFORMAT);
            oKeywordsDict.Add("vbLongDate", ParseTokenType.VBSCRIPT_CONSTANT_DATEFORMAT);
            oKeywordsDict.Add("vbShortDate", ParseTokenType.VBSCRIPT_CONSTANT_DATEFORMAT);
            oKeywordsDict.Add("vbLongTime", ParseTokenType.VBSCRIPT_CONSTANT_DATEFORMAT);
            oKeywordsDict.Add("vbShortTime", ParseTokenType.VBSCRIPT_CONSTANT_DATEFORMAT);

            oKeywordsDict.Add("vbBinaryCompare", ParseTokenType.VBSCRIPT_CONSTANT_COMPARE);
            oKeywordsDict.Add("vbTextCompare", ParseTokenType.VBSCRIPT_CONSTANT_COMPARE);

            oKeywordsDict.Add("vbBlack", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);
            oKeywordsDict.Add("vbRed", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);
            oKeywordsDict.Add("vbGreen", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);
            oKeywordsDict.Add("vbYellow", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);
            oKeywordsDict.Add("vbBlue", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);
            oKeywordsDict.Add("vbMagenta", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);
            oKeywordsDict.Add("vbCyan", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);
            oKeywordsDict.Add("vbWhite", ParseTokenType.VBSCRIPT_CONSTANT_COLOR);

            foreach (string key in oKeywordsDict.Keys)
            {
                oKeywordsCaseDict.Add(key, key);
            }
        }

        //Regex oRegWhiteSpace = new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //Regex oRegIf = new Regex(@"\bIf\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //Regex oRegThen = new Regex(@"\bThen\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //Regex oRegEach = new Regex(@"\bFor\s+Each\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //Regex oRegPreserve = new Regex(@"^\ReDim\s+Preserve\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //Regex oRegComment = new Regex(@"'.*|REM(\W.*|)$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private void ParseStage1(PeekableTextReader oReader)
        {
            tokens = new List<TokenTag>();
            pkReader = oReader;

            int curChar, nextChar;
            char ch;
            string word, nextWord;

            while ((curChar = pkReader.Peek()) != -1)
            {
                word = "";

                ch = (char) curChar;
                switch (ch)
                {
/*                    case '~':
                    case ';':
                    case '?':
                    case '|':
                    case '`':
                    case '!':
                    case '{':
                    case '}':
                        RecordToken(ParseTokenType.UNKNOWN, pkReader.Read());
                        break;*/
                    case '\t':
                    case '\v':
                    case ' ':
                    case '\f':
                        RecordToken(ParseTokenType.WHITESPACE, pkReader.ReadTillWhiteSpace());
                        break;
                    case '(':
                        RecordToken(ParseTokenType.OPEN_BRACKET, pkReader.Read());
                        break;
                    case ')':
                        RecordToken(ParseTokenType.CLOSE_BRACKET, pkReader.Read());
                        break;
                    case '"':
                        RecordToken(ParseTokenType.STRING, pkReader.ReadString());
                        break;
                    case '\'':
                        RecordToken(ParseTokenType.COMMENT, pkReader.ReadTillEndOfLine());
                        break;
                    case '#':
                        pkReader.Read();
                        word = "#" + pkReader.ReadTill(charRead => charRead != '#') + "#";
                        pkReader.Read();
                        RecordToken(ParseTokenType.DATE, word);
                        break;
                    case '[':
                        word = pkReader.ReadTill(charRead => charRead != ']') + "]";
                        pkReader.Read();
                        RecordToken(ParseTokenType.IDENTIFIER, word);
                        break;
                    case '_':
                        RecordToken(ParseTokenType.STATEMENT_CONTINUATION, pkReader.Read());
                        break;
                    case ':':
                        RecordToken(ParseTokenType.STATEMENT_SEPARATOR, pkReader.Read());
                        break;
                    case '@':
                        if (pkReader.Peek(1) == '@')
                            RecordToken(ParseTokenType.COMMENT, pkReader.ReadTillEndOfLine());
                        else
                            RecordToken(ParseTokenType.UNKNOWN, pkReader.Read());
                        break;
                    case '+':
                    case '^':
                    case '%':
                    case '-':
                    case '*':
                    case '/':
                    case '\\':
                    case '=':
                        RecordToken(ParseTokenType.ARTHMETIC_OPERATOR, pkReader.Read());
                        break;
                    case '<':
                        nextChar = pkReader.Peek(1);
                        if (nextChar == '>')
                            RecordToken(ParseTokenType.COMPARISON_OPERATOR, pkReader.Read(2));
                        else if (nextChar == '=')
                            RecordToken(ParseTokenType.COMPARISON_OPERATOR, pkReader.Read(2));
                        else
                            RecordToken(ParseTokenType.COMPARISON_OPERATOR, pkReader.Read());
                        break;
                    case '>':
                        nextChar = pkReader.Peek(1);
                        if (nextChar == '=')
                            RecordToken(ParseTokenType.COMPARISON_OPERATOR, pkReader.Read(2));
                        else
                            RecordToken(ParseTokenType.COMPARISON_OPERATOR, pkReader.Read());
                        break;
                    case '&':
                        nextChar = pkReader.Peek(1);
                        if (nextChar == 'H' || nextChar == 'h')
                            //this is a hexa decimal value
                            RecordToken(ParseTokenType.HEXNUMBER, (char) pkReader.Read() + pkReader.ReadAlphaNumeric());
                        else
                            RecordToken(ParseTokenType.ARTHMETIC_OPERATOR, pkReader.Read());
                        break;
                    case '.':
                        if (char.IsDigit((char) pkReader.Peek()))
                            RecordToken(ParseTokenType.NUMBER, pkReader.ReadNumber());
                        else
                            RecordToken(ParseTokenType.DOT_OPERATOR, pkReader.Read());
                        //record dot operator
                        break;
                    case '\r':
                        if (pkReader.Peek(1) == '\n')
                            RecordToken(ParseTokenType.NEWLINE, pkReader.Read(2));
                        else
                            RecordToken(ParseTokenType.NEWLINE, pkReader.Read());
                        break;
                    case '\n':
                        RecordToken(ParseTokenType.NEWLINE, pkReader.Read());
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        RecordToken(ParseTokenType.NUMBER, pkReader.ReadNumber());
                        break;
                    case ',':
                        RecordToken(ParseTokenType.COMMA, pkReader.Read());
                        break;
                    default:
                        if (!pkReader.IsAlphaNumeric(pkReader.Peek()))
                        {
                            RecordToken(ParseTokenType.INVALID, pkReader.Read());
                            continue;
                        }

                        word = pkReader.ReadAlphaNumeric();
                        int n = 0;
                        if (lastNonWSParsedToken != ParseTokenType.DOT_OPERATOR && oKeywordsDict.ContainsKey(word))
                        {
                            switch (word.ToLower())
                            {
                                case "do":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n).ToLower();
                                    if (nextWord == "while")
                                    {
                                        pkReader.Read(n);
                                        RecordToken(ParseTokenType.DO_LOOP_START_WHILE, "Do While");
                                    }
                                    else if (nextWord == "until")
                                    {
                                        pkReader.Read(n);
                                        RecordToken(ParseTokenType.DO_LOOP_START_UNTIL, "Do Until");
                                    }
                                    else
                                        goto default;
                                    break;
                                case "loop":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n).ToLower();
                                    if (nextWord == "while")
                                    {
                                        pkReader.Read(n);
                                        RecordToken(ParseTokenType.DO_LOOP_END_WHILE, "Loop While");
                                    }
                                    else if (nextWord == "until")
                                    {
                                        pkReader.Read(n);
                                        RecordToken(ParseTokenType.DO_LOOP_END_UNTIL, "Loop Until");
                                    }
                                    break;
                                case "for":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n);
                                    if (nextWord.ToLower() == "each")
                                    {
                                        pkReader.Read(n);
                                        RecordToken(ParseTokenType.FOR_EACHLOOP, "For Each");
                                    }
                                    else
                                        goto default;
                                    break;
                                case "on":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n);
                                    if (nextWord.ToLower() == "error")
                                    {
                                        string nextWord1 = pkReader.PeekAfterWhiteSpace(ref n).ToLower();
                                        string nextWord2 = pkReader.PeekAfterWhiteSpace(ref n).ToLower();

                                        if (nextWord1 == "resume" && nextWord2 == "next")
                                        {
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.ON_ERROR_RESUME_NEXT, "On Error Resume Next");
                                        }
                                        else if (nextWord1 == "goto" && nextWord2 == "0")
                                        {
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.ON_ERROR_GOTO_0, "On Error GoTo 0");
                                        }
                                        else
                                            goto default;
                                    }
                                    break;
                                case "case":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n);

                                    if (nextWord.ToLower() == "else")
                                    {
                                        pkReader.Read(n);
                                        RecordToken(ParseTokenType.CASE_ELSE, "Case Else");
                                    }
                                    else
                                        goto default;
                                    break;
                                case "select":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n);

                                    if (nextWord.ToLower() == "case")
                                    {
                                        pkReader.Read(n);
                                        RecordToken(ParseTokenType.SELECT_CASE, "Select Case");
                                    }
                                    else
                                        goto default;
                                    break;
                                case "end":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n);

                                    switch (nextWord.ToLower())
                                    {
                                        case "function":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.END_FUNCTION, "End Function");
                                            break;
                                        case "class":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.END_CLASS, "End Class");
                                            break;
                                        case "sub":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.END_SUB, "End Sub");
                                            break;
                                        case "property":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.END_PROPERTY, "End Property");
                                            break;
                                        case "if":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.END_IF, "End If");
                                            break;
                                        case "with":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.END_WITH, "End With");
                                            break;
                                        case "select":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.END_SELECT, "End Select");
                                            break;
                                        default:
                                            RecordToken(oKeywordsDict[word], oKeywordsCaseDict[word]);
                                            break;
                                    }
                                    break;
                                case "exit":
                                    nextWord = pkReader.PeekAfterWhiteSpace(ref n);

                                    switch (nextWord.ToLower())
                                    {
                                        case "function":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.EXIT_FUNCTION, "Exit Function");
                                            break;
                                        case "for":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.EXIT_FOR, "Exit For");
                                            break;
                                        case "do":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.EXIT_DO, "Exit Do");
                                            break;
                                        case "property":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.EXIT_PROPERTY, "Exit Property");
                                            break;
                                        case "sub":
                                            pkReader.Read(n);
                                            RecordToken(ParseTokenType.EXIT_SUB, "Exit Sub");
                                            break;
                                        default:
                                            RecordToken(oKeywordsDict[word], word);
                                            break;
                                    }
                                    break;
                                default:
                                    RecordToken(oKeywordsDict[word], oKeywordsCaseDict[word]);
                                    break;
                            }
                        }
                        else
                        {
                            switch (word.ToUpper())
                            {
                                case "REM":
                                    RecordToken(ParseTokenType.COMMENT, word + pkReader.ReadTillEndOfLine());
                                    break;
                                default:
                                    RecordToken(ParseTokenType.UNKNOWN, word);
                                    break;
                            }
                        }
                        break;
                }
            }

            RecordToken(ParseTokenType.EOF, null);
        }

        Action<TokenTag> DoNothing = t => { };

        private int SkipToTokens(int n, ParseTokenType[] tokenType, Action<TokenTag> action = null)
        {
            int i = n;
            action = action ?? DoNothing;
            while (!tokenType.Contains(tokens[++i].tokenType) && i < tokens.Count)
                action(tokens[i]);
            return i;
        }

        private int SkipToToken(int n, ParseTokenType tokenType)
        {
            return SkipToTokens(n, new[] { tokenType });
        }

        private int SkipToken(int n, ParseTokenType tokenType)
        {
            return SkipTokens(n, new[] { tokenType });
        }

        private int SkipTokens(int n, ParseTokenType[] tokenType, Action<TokenTag> action = null)
        {
            int i = n;
            action = action ?? DoNothing;
            while (tokenType.Contains(tokens[++i].tokenType) && i < tokens.Count)
                action(tokens[i]);
            return i;
        }

        private int SkipWhiteSpaces(int n, Action<TokenTag> action = null, bool bLineContinuation = true)
        {
            int i = n;
            action = action ?? DoNothing;

            bool bIgnoreNewLine = false;
            while (i < tokens.Count)
            {
                switch (tokens[++i].tokenType)
                {
                    case ParseTokenType.WHITESPACE:
                        break;
                    default:
                        if (bLineContinuation && tokens[i].tokenType == ParseTokenType.STATEMENT_CONTINUATION)
                        {
                            bIgnoreNewLine = true;
                            break;
                        }
                        if (bLineContinuation && bIgnoreNewLine && tokens[i].tokenType == ParseTokenType.NEWLINE)
                        {
                            bIgnoreNewLine = false;
                            break;
                        }
                        return i;
                }
                action(tokens[i]);
            }
            return i;
        }

        private int SkipToEndOfLine(int n, Action<TokenTag> action = null, bool breakOnSeperator = false)
        {
            int i = n;
            action = action ?? DoNothing;

            bool bIgnoreNewLine = false;
            while (i < tokens.Count)
            {
                switch (tokens[++i].tokenType)
                {
                    case ParseTokenType.STATEMENT_SEPARATOR:
                        if (breakOnSeperator)
                            return i;
                        break;
                    case ParseTokenType.NEWLINE:
                        if (bIgnoreNewLine)
                        {
                            bIgnoreNewLine = false;
                            break;
                        }
                        return i;
                    case ParseTokenType.STATEMENT_CONTINUATION:
                        bIgnoreNewLine = true;
                        break;
                    default:
                        break;
                }
                action(tokens[i]);
            }
            return i;
        }


        private int SkipToEndOfStatement(int n, Action<TokenTag> action = null)
        {
            return SkipToEndOfLine(n, action, true);
        }

        int indentationLevel;

        int SetIndent(int value)
        {
            indentationLevel = value;
            return value;
        }

        int ChangeIndent(int n = 0)
        {
            indentationLevel += n;
            if (indentationLevel < 0)
                indentationLevel = 0;
            return indentationLevel;
        }

        string[] indentTabs = {
                                "",
                                "\t",
                                "\t\t",
                                "\t\t\t",
                                "\t\t\t\t",
                                "\t\t\t\t\t",
                                "\t\t\t\t\t\t",
                                "\t\t\t\t\t\t\t",
                                "\t\t\t\t\t\t\t\t",
                                "\t\t\t\t\t\t\t\t\t",
                                "\t\t\t\t\t\t\t\t\t\t",
                                "\t\t\t\t\t\t\t\t\t\t\t",
                                "\t\t\t\t\t\t\t\t\t\t\t\t",
                                "\t\t\t\t\t\t\t\t\t\t\t\t\t"
        };
        StringBuilder fCode = new StringBuilder();
        String lastLine;
        String curLine;

        public void LoadStats()
        {
            bool bLastNewLine = false;

            Classes = 0;
            Functions = 0;
            Comments = 0;
            Variables = 0;
            BlankLines = 0;
            ConstVariables = 0;
            BlankLines = 0;

            foreach (var tokenTag in tokens)
            {
                switch (tokenTag.tokenType)
                {
                    case ParseTokenType.CLASS:
                        Classes++;
                        break;
                    case ParseTokenType.FUNCTION:
                    case ParseTokenType.SUB:
                        Functions++;
                        break;
                    case ParseTokenType.COMMENT:
                        Comments++;
                        break;
                    case ParseTokenType.VARIABLE_NAME:
                        Variables++;
                        break;
                    case ParseTokenType.CONST_VARIABLE_NAME:
                        ConstVariables++;
                        break;
                }

                if (bLastNewLine)
                {
                    if (tokenTag.tokenType == ParseTokenType.WHITESPACE)
                    {
                        //ignore white space
                    }
                    else if (tokenTag.tokenType == ParseTokenType.NEWLINE)
                    {
                        //now we have got a new line. 
                        BlankLines++;
                    }
                    else
                    {
                        bLastNewLine = false;
                    }
                }
                else
                {
                    if (tokenTag.tokenType == ParseTokenType.NEWLINE)
                        bLastNewLine = true;
                }
            }
        }

        public int BlankLines { get; private set; }

        public int ConstVariables { get; private set; }

        public int Variables { get; private set; }

        public int Comments { get; private set; }

        public int Functions { get; private set; }

        public int Classes { get; private set; }

        public string FormatCode(string code)
        {
            ResetVariables();
            var codeReader = new PeekableTextReader(new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(code))));
            string copyrightMessage = @"'This source code has been formatted with Free VBScript Source Code Formatter 
'Tool by Tarun Automation Research & Labs Pvt. Ltd (TARLABS™) - http://www.tarlabs.com

";

            ParseStage1(codeReader);
            ParseStage2();
            string formattedCode = fCode.ToString().Trim();
            if (formattedCode.Contains(copyrightMessage.Trim()))
                return fCode.ToString();
            else
                return copyrightMessage + formattedCode;
        }

        public string StartIndentString { get { return staticIndent; } set { staticIndent = value; } }
        public string FormattedCode { get { return fCode.ToString(); } }
        public string IndentationString
        {
            set
            {
                indentTabs[0] = "";
                indentTabs[1] = value;
                for (int i = 2; i < indentTabs.Length; i++)
                {
                    indentTabs[i] = indentTabs[i - 1] + value;
                }
            }
        }

        public bool RemoveComments { get; set; }

        private TokenTag lastToken;
        private bool bUseIndent;
        private bool bNextLineInContinuation;

        private string staticIndent;
        private string dynamicIndent;
        private ParseTokenType lastNonWSLNParsedToken;

        private string GetIndent(int delta = 0)
        {
            if (!bUseIndent)
                return "";
            bUseIndent = false;

            if (indentationLevel + delta < 0)
                return staticIndent + dynamicIndent + indentTabs[0];

            if (indentationLevel + delta >= indentTabs.Length)
                return staticIndent + dynamicIndent + indentTabs[indentTabs.Length - 1];
            return staticIndent + dynamicIndent + indentTabs[indentationLevel + delta];
        }

        private void GetTokenImpact(TokenTag token, out int indent, out int lineIndent, out string WSBefore, out string WSAfter)
        {
            indent = 0;
            WSBefore = "";
            WSAfter = "";
            lineIndent = 0;

            switch (token.tokenType)
            {

                case ParseTokenType.ARTHMETIC_OPERATOR:
                case ParseTokenType.BINARY_OPERATOR:
                case ParseTokenType.COMPARISON_OPERATOR:
                case ParseTokenType.STATEMENT_SEPARATOR:
                case ParseTokenType.STEP:
                case ParseTokenType.ON:
                case ParseTokenType.FOR_LOOP_TO:
                case ParseTokenType.FOR_LOOP_IN:
                    WSBefore = " ";
                    WSAfter = " ";
                    break;
                case ParseTokenType.BYREF:
                case ParseTokenType.BYVAL:
                case ParseTokenType.CALL:
                case ParseTokenType.CONST:
                case ParseTokenType.DEFAULT:
                case ParseTokenType.COMMA:
                case ParseTokenType.DIM:
                case ParseTokenType.ERASE:
                case ParseTokenType.IF_ELSE_ONE_LINE:
                case ParseTokenType.IF_ONE_LINE:
                case ParseTokenType.NEW_OPERATOR:
                case ParseTokenType.OPTION:
                case ParseTokenType.PRESERVE:
                case ParseTokenType.PRIVATE:
                case ParseTokenType.PUBLIC:
                case ParseTokenType.REDIM:
                case ParseTokenType.RESUME:
                case ParseTokenType.ERROR:
                case ParseTokenType.SET_OPERATOR:
                    WSAfter = " ";
                    break;
                case ParseTokenType.THEN:
                case ParseTokenType.STATEMENT_CONTINUATION:
                    WSBefore = " ";
                    WSAfter = " ";
                    break;
                case ParseTokenType.WITH:
                case ParseTokenType.FUNCTION:
                case ParseTokenType.PROPERTY:
                case ParseTokenType.SUB:
                case ParseTokenType.CLASS:
                case ParseTokenType.IF:
                    indent = 1;
                    WSAfter = " ";
                    lineIndent = -1;
                    break;
                case ParseTokenType.END_SUB:
                case ParseTokenType.END_WITH:
                case ParseTokenType.END_FUNCTION:
                case ParseTokenType.END_CLASS:
                case ParseTokenType.END_IF:
                case ParseTokenType.END_PROPERTY:
                    indent = -1;
                    break;
                case ParseTokenType.SELECT_CASE:
                    WSAfter = " ";
                    //we increment twice so that when each case statement coments we
                    //just decrease the indent by 1 using 
                    indent = 2;
                    lineIndent = -2;
                    break;
                case ParseTokenType.CASE:
                case ParseTokenType.CASE_ELSE:
                    lineIndent = -1;
                    WSAfter = " ";
                    break;
                case ParseTokenType.END_SELECT:
                    indent = -2;
                    break;
                case ParseTokenType.ELSE_IF:
                    WSAfter = " ";
                    lineIndent = -1;
                    break;
                case ParseTokenType.ELSE:
                    lineIndent = -1;
                    break;
                case ParseTokenType.FOR_LOOP:
                case ParseTokenType.FOR_EACHLOOP:
                case ParseTokenType.WHILE_LOOP:
                case ParseTokenType.DO_LOOP:
                case ParseTokenType.DO_LOOP_START_UNTIL:
                case ParseTokenType.DO_LOOP_START_WHILE:
                    WSAfter = " ";
                    indent = 1;
                    lineIndent = -1;
                    break;
                case ParseTokenType.FOR_LOOP_NEXT:
                case ParseTokenType.DO_LOOP_END:
                case ParseTokenType.DO_LOOP_END_UNTIL:
                case ParseTokenType.DO_LOOP_END_WHILE:
                case ParseTokenType.WHILE_LOOP_WEND:
                    indent = -1;
                    WSAfter = " ";
                    break;
            }
        }

        ParseTokenType[] ignoreWSToken = new[] { ParseTokenType.DOT_OPERATOR, ParseTokenType.EOF, ParseTokenType.OPEN_BRACKET , ParseTokenType.INVALID};
        ParseTokenType[] operatorTokens = new[] { ParseTokenType.ARTHMETIC_OPERATOR, ParseTokenType.BINARY_OPERATOR, ParseTokenType.COMPARISON_OPERATOR, ParseTokenType.STEP };

        private void PrintCode(TokenTag token, TokenTag nextToken)
        {
            string curTokenWSAfter, curTokenWSBefore, nextTokenWSBefore, nextTokenWSAfter;
            int curIndentDelta, curLineIndent, nextIndentDelta, nextLineIndent;

            GetTokenImpact(token, out curIndentDelta, out curLineIndent, out curTokenWSBefore, out curTokenWSAfter);

            //if next token is new line then lets not add a space after the current token
            curTokenWSAfter = nextToken.tokenType == ParseTokenType.NEWLINE ? "" : curTokenWSAfter;

            if (token.tokenType != ParseTokenType.NEWLINE && !ignoreWSToken.Contains(token.tokenType))
            {
                GetTokenImpact(nextToken, out nextIndentDelta, out nextLineIndent, out nextTokenWSBefore, out nextTokenWSAfter);

                //Let's make sure we don't merge two different tokens because of spacing
                if (curTokenWSAfter == "" && nextTokenWSBefore == "")
                {
                    switch (nextToken.tokenType)
                    {
                        case ParseTokenType.NEWLINE:
                        case ParseTokenType.DOT_OPERATOR:
                        case ParseTokenType.OPEN_BRACKET:
                        case ParseTokenType.CLOSE_BRACKET:
                        case ParseTokenType.COMMA:
                        case ParseTokenType.EOF:
                            break;
                        default:
                            curTokenWSAfter = " ";
                            break;
                    }

                }
            }


            switch (token.tokenType)
            {
                 case ParseTokenType.STATEMENT_CONTINUATION:
                    bNextLineInContinuation = true;
                    break;
                case ParseTokenType.NEWLINE:
                    if (lastToken != null && lastNonWSLNParsedToken == ParseTokenType.NEWLINE)
                    {
                        bUseIndent = true;
                        AppendCode(GetIndent(curLineIndent) + "\r\n");
                    }
                    else
                        AppendCode("\r\n");
                    bUseIndent = true;
                    if (bNextLineInContinuation)
                    {
                        //we need to add some smart identation to make sure the continued line is extra indented
                        //int length = lastLine.Trim().Length;
                        //get tabe size
                        //length = (int) (length / 4 * 0.4);
                        dynamicIndent = "\t\t";
                        //indentTabs[length];
                        bNextLineInContinuation = false;
                    }
                    else
                        dynamicIndent = "";
                    break;
            }

            ChangeIndent(curIndentDelta);

            if (token.tokenType == ParseTokenType.ELSE && nextToken.tokenType == ParseTokenType.IF)
                curTokenWSAfter = "\r\n";

            if (operatorTokens.Contains(token.tokenType) &&
                operatorTokens.Contains(lastNonWSParsedToken))
            {
                curTokenWSAfter = "";
                curTokenWSBefore = "";

                if (token.tokenType == ParseTokenType.BINARY_OPERATOR && (string)token.value == "Not")
                    curTokenWSAfter = " ";
            }

            if (RemoveComments && token.tokenType == ParseTokenType.COMMENT)
            {
                //lets not do anything and remove this comment
                if (nextToken.tokenType == ParseTokenType.NEWLINE)
                {
                    nextToken.tokenType = ParseTokenType.UNKNOWN;
                    nextToken.value = "";
                }
            }
            else if ((token.tokenType != ParseTokenType.WHITESPACE && token.tokenType != ParseTokenType.NEWLINE))
            {
                AppendCode(GetIndent(curLineIndent) + curTokenWSBefore + token.value + curTokenWSAfter);
            }

            if (token.tokenType == ParseTokenType.ELSE && nextToken.tokenType == ParseTokenType.IF)
            {
                //if have a mistaken else if and not ElseIf. Need to make sure 
                //we add a new line to it
                bUseIndent = true;
            }
        }

        private void AppendCode(string code)
        {
            fCode.Append(code);
            curLine += code;
            if (code.EndsWith("\r\n"))
            {
                lastLine = curLine;
                curLine = "";
            }
        }

        private void ParseStage2()
        {
            fCode.Clear();
            bUseIndent = true;
            //staticIndent = "\t  ";
            for (int i = 0; i < tokens.Count - 1; i++)
            {
                TokenTag curToken = tokens[i];
                TokenTag nextToken = null;
                int n = i + 1;

                while (n < tokens.Count && tokens[n].tokenType == ParseTokenType.WHITESPACE)
                    n++;

                if (n < tokens.Count)
                    nextToken = tokens[n];

                n = i;
                switch (curToken.tokenType)
                {
                    case ParseTokenType.IF:
                        n = SkipToToken(n, ParseTokenType.THEN);

                        while (tokens[++n].tokenType != ParseTokenType.NEWLINE)
                        {
                            if (!(tokens[n].tokenType == ParseTokenType.WHITESPACE ||
                                tokens[n].tokenType == ParseTokenType.COMMENT))
                            {
                                curToken.tokenType = ParseTokenType.IF_ELSE_ONE_LINE;
                                break;
                            }
                        }

                        break;
                    case ParseTokenType.DIM:
                        n = SkipToEndOfStatement(n, t =>
                        {
                            if (t.tokenType == ParseTokenType.UNKNOWN)
                                t.tokenType = ParseTokenType.VARIABLE_NAME;
                        });
                        break;
                    case ParseTokenType.CLASS:
                        n = SkipToToken(n, ParseTokenType.UNKNOWN);
                        tokens[n].tokenType = ParseTokenType.CLASS_NAME;
                        break;
                    case ParseTokenType.REDIM:
                        n = SkipToEndOfStatement(n, t =>
                        {
                            if (t.tokenType == ParseTokenType.UNKNOWN)
                                t.tokenType = ParseTokenType.VARIABLE_NAME;
                        });
                        break;
                    case ParseTokenType.CONST:
                        n = SkipToEndOfStatement(n, t =>
                        {
                            if (t.tokenType == ParseTokenType.UNKNOWN)
                                t.tokenType = ParseTokenType.CONST_VARIABLE_NAME;
                        });
                        break;
                    default:
                        break;
                }

                if (curToken.tokenType != ParseTokenType.WHITESPACE)
                    PrintCode(curToken, nextToken);

                lastToken = tokens[i];
                if (lastToken.tokenType != ParseTokenType.WHITESPACE && lastToken.tokenType != ParseTokenType.NEWLINE)
                {
                    lastNonWSParsedToken = lastToken.tokenType;
                }
                if (lastNonWSParsedToken != ParseTokenType.WHITESPACE)
                    lastNonWSLNParsedToken = lastToken.tokenType;
            }
        }

        //public int Read()
        //{
        //    char ch = char.MinValue;
        //    return Read(ref ch);
        //}

        //public int Read(ref char read)
        //{
        //    int readint = _oReader.Peek();
        //    if (readint != -1)
        //        read = (char) _oReader.Read();
        //    return readint;
        //}

        //public int Peek()
        //{
        //    char ch = char.MinValue;
        //    return Peek(ref ch);
        //}

        //public int Peek(ref char read)
        //{
        //    int readint = _oReader.Peek();
        //    if (readint != -1)
        //        read = (char) readint;
        //    return readint;
        //}

    }

}
